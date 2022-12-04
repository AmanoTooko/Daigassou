using System;
using System.Drawing;
using System.Windows.Forms;
using Daigassou.Controller;
using Daigassou.Utils;
using DaigassouDX.Controller;
using Sunny.UI;

namespace Daigassou.Forms
{
    public partial class PlayPage : UIPage
    {
        private readonly MidiFileParser midiFileParser;
        private readonly MidiPlayController midiPlayController;
        private readonly Timer playProcessTimer;
        private readonly Instrument instru;
        private bool isRunning;


        public PlayPage()
        {
            InitializeComponent();
            midiFileParser = new MidiFileParser();
            instru = new Instrument();
            midiPlayController = new MidiPlayController();
            playProcessTimer = new Timer();
            playProcessTimer.Interval = 1000;
            playProcessTimer.Tick += PlayProcessTimer_Tick;
            isRunning = false;
        }

        public override void Init()
        {
            base.Init();
            uiLine1.ForeColor = Color.FromArgb(255, 113, 128);
            uiLine2.ForeColor = Color.FromArgb(255, 113, 128);
        }

        public override void Final()
        {
            base.Final();
        }

        private void PlayProcessTimer_Tick(object sender, EventArgs e)
        {
            var origText = midiPlayController.GetProcess();
            var currentTime = Convert.ToInt32(origText.Split("/").First());
            var durationTime = Convert.ToInt32(origText.Split("/").Last());
            tbMidiProcess.Value = currentTime * 100 / durationTime;
            lblProcess.Text =
                $"正在播放：{new TimeSpan(0, 0, 0, 0, currentTime).ToString("mm\\:ss")}/{new TimeSpan(0, 0, 0, 0, durationTime).ToString("mm\\:ss")}";
        }

        private void PlayForm_ReceiveParams(object sender, UIPageParamsArgs e)
        {
            Action<UIPageParamsArgs> actionDelegate = ex =>
            {
                var comm = ex.Value as CommObject;
                switch (comm.eventId)
                {
                    case eventCata.MIDI_CONTROL_START_COUNTDOWN:
                        UIMessageTip.ShowOk($"网络合奏：收到小队倒计时信号，将于{Convert.ToInt32(comm.payload) / 1000}秒后开始演奏。", 1000,
                            true, PointToScreen(new Point(Location.X + 200, Location.Y + 200)));
                        btnStart_Click(sender, e);
                        break;
                    case eventCata.MIDI_CONTROL_START_TIMER:
                        UIMessageTip.ShowOk($"合奏：收到时间设定信号，将于{Convert.ToInt32(comm.payload) / 1000}秒后开始演奏。", 1000, true,
                            PointToScreen(new Point(Location.X + 200, Location.Y + 200)));
                        btnStart_Click(sender, e);
                        break;
                    case eventCata.MIDI_CONTROL_START_ENSEMBLE:
                        UIMessageTip.ShowOk("网络合奏：收到合奏助手信号，将于3秒后开始演奏。", 1000, true,
                            PointToScreen(new Point(Location.X + 200, Location.Y + 200)));
                        btnStart_Click(sender, e);
                        break;
                    case eventCata.MIDI_CONTROL_START_KEY:
                        UIMessageTip.ShowOk("收到快捷键信号，将于1秒后开始演奏。", 1000, true,
                            PointToScreen(new Point(Location.X + 200, Location.Y + 200)));
                        comm.payload = 1000;
                        btnStart_Click(sender, e);
                        break;
                    case eventCata.MIDI_CONTROL_PITCHUP:
                        tbKey.Value++;
                        btnConfirmKey_Click(sender, e);
                        break;
                    case eventCata.MIDI_CONTROL_PITCHDOWN:
                        tbKey.Value--;
                        btnConfirmKey_Click(sender, e);
                        break;
                    case eventCata.MIDI_CONTROL_STOP:
                        ResetPlay();
                        UIMessageTip.ShowWarning("收到停止信号，已停止演奏。", 1000, true,
                            PointToScreen(new Point(Location.X + 200, Location.Y + 200)));

                        break;
                    case eventCata.MIDI_CONTROL_INSTRUCODE:
                        instru.IntruID = Convert.ToInt32(comm.payload);
                        SendParamToFrame(e.Value);
                        break;
                    case eventCata.MIDI_FILE_NAME_CROSS:
                        openFileDialog1.FileName = comm.payload.ToString();
                        LoadMidiFile();
                        break;
                }

                Console.Write(e.Value);
            };
            BeginInvoke(actionDelegate, e);
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            SendParamToPage((int) PageID.SettingPage, "测试+1");
        }

        private void uiTrackBar1_ValueChanged(object sender, EventArgs e)

        {
            var speed = tbSpeed.Value > 50 ? 1 + (tbSpeed.Value - 50) / 50.0 : 0.5 + tbSpeed.Value / 100.0;
            lblSpeed.Text = $"当前速度 {speed.ToString("F2")} 倍";
            btnConfirmSpeed.Enabled = true;
        }

        private void tbKey_ValueChanged(object sender, EventArgs e)
        {
            lblKey.Text = $"当前音高  {tbKey.Value}";
            btnConfirmKey.Enabled = true;
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //发送midi信息
                LoadMidiFile();
                Utils.Utils.SendMessageToAll(eventCata.MIDI_FILE_NAME_CROSS, openFileDialog1.FileName);
            }
        }

        private void LoadMidiFile()
        {
            tbFilename.Text = openFileDialog1.FileName.Split("\\").Last();

            SendParamToPage((int) PageID.MultiPlayPage,
                new CommObject {eventId = eventCata.MIDI_FILE_NAME, payload = openFileDialog1.FileName});
            SendParamToPage((int) PageID.PreviewPlayPage,
                new CommObject {eventId = eventCata.MIDI_FILE_NAME, payload = openFileDialog1.FileName});
            //开始读取track信息

            midiFileParser.OpenFile(openFileDialog1.FileName);
            var trackNameList = midiFileParser.GetTrackNames();
            var tempInstrument = new Instrument();
            var tempIndex = 0;
            for (var i = 0; i < trackNameList.Count; i++)
            {
                var name = trackNameList[i];
                if (tempInstrument.AliasToCode(name))
                {
                    trackNameList[i] = $"[{tempInstrument}]{name}";
                    if (tempInstrument.IntruID == instru.IntruID) tempIndex = i;
                }
            }

            cbTrackname.Clear();
            cbTrackname.DataSource = trackNameList;
            cbTrackname.SelectedIndex = tempIndex;
        }

        private void cbTrackname_SelectedIndexChanged(object sender, EventArgs e)
        {
            SendParamToPage((int) PageID.MultiPlayPage,
                new CommObject
                {
                    eventId = eventCata.TRACK_FILE_NAME,
                    payload = $"{cbTrackname.SelectedIndex}|{cbTrackname.SelectedItem}"
                });
            SendParamToPage((int) PageID.PreviewPlayPage,
                new CommObject
                {
                    eventId = eventCata.TRACK_FILE_NAME,
                    payload = $"{cbTrackname.SelectedIndex}|{cbTrackname.SelectedItem}"
                });
            midiFileParser.Index = cbTrackname.SelectedIndex;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Symbol == 61515) // start
            {
                if (!isRunning)
                {
                    if (e is UIPageParamsArgs)
                    {
                        var comm = (e as UIPageParamsArgs).Value as CommObject;
                        ///midiPlayController.StartPlay(Convert.ToInt32(comm.payload));
                        StartPlay(Convert.ToInt32(comm.payload));
                    }
                    else
                    {
                        StartPlay(1000);
                    }
                }


                else
                {
                    midiPlayController.StartPlay(0);
                    btnStart.Symbol = 61516;
                    btnStart.SymbolOffset = new Point(0, 1);
                }

                
            }
            else
            {
                
                midiPlayController.PausePlay();
                btnStart.Symbol = 61515;
                btnStart.SymbolOffset = new Point(2, 1);
            }
        }

        private void ResetPlay()
        {
            isRunning = false;
            midiPlayController.StopPlay();
            playProcessTimer.Stop();
            btnStart.Symbol = 61515;
            btnStart.SymbolOffset = new Point(2, 1);
            tbMidiProcess.Value = 0;
            Action actionDelegate = () => { lblProcess.Text = "停止播放：00:00/00:00"; };
            BeginInvoke(actionDelegate);
        }

        private void StartPlay(int offset)
        {
            if (tbFilename.Text == "")
            {
                UIMessageTip.ShowError("妹有选择Midi文件", 3000, true,
                    PointToScreen(new Point(Location.X + 200, Location.Y + 200)));
                return;
            }

            midiPlayController.SetPlayback(midiFileParser.GetPlayback());
            midiPlayController.SetSpeed(tbSpeed.Value > 50
                ? 1 + (tbSpeed.Value - 50) / 50.0
                : 0.5 + tbSpeed.Value / 100.0);
            midiPlayController.SetPitch(tbKey.Value);
            midiPlayController.StartPlay(offset);
            lyricPoster.LrcStart(openFileDialog1.FileName, offset);


            midiPlayController.Playback_Finished_Notification += () => { ResetPlay(); };
            playProcessTimer.Start();
            isRunning = true;
            btnStart.Symbol = 61516;
            btnStart.SymbolOffset = new Point(0, 1);
        }

        private void btnConfirmSpeed_Click(object sender, EventArgs e)
        {
            btnConfirmSpeed.Enabled = false;

            midiPlayController.SetSpeed(tbSpeed.Value > 50
                ? 1 + (tbSpeed.Value - 50) / 50.0
                : 0.5 + tbSpeed.Value / 100.0);
        }

        private void btnConfirmKey_Click(object sender, EventArgs e)
        {
            btnConfirmKey.Enabled = false;

            midiPlayController.SetPitch(tbKey.Value);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ResetPlay();
        }

        private void btnBackward_Click(object sender, EventArgs e)
        {
            midiPlayController.SetOffset(-50);
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            midiPlayController.SetOffset(50);
        }
    }
}