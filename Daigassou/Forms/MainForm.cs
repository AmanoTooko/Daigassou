using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using BondTech.HotkeyManagement.Win;
using Daigassou.Input_Midi;
using Daigassou.Properties;
using GlobalHotKey;
using Daigassou.Utils;
using HotKey = GlobalHotKey.HotKey;
using HotKeyManager = GlobalHotKey.HotKeyManager;

namespace Daigassou
{
    public partial class MainForm : Form
    {
        internal BondTech.HotkeyManagement.Win.HotKeyManager hkm;
        internal BondTech.HotkeyManagement.Win.GlobalHotKey gbk;
        private readonly HotKeyManager hotKeyManager = new HotKeyManager();
        private readonly KeyBindFormOld keyForm22 = new KeyBindFormOld();
        private readonly KeyBindForm8Key keyForm8 = new KeyBindForm8Key();
        private readonly MidiToKey mtk = new MidiToKey();
        private readonly KeyController kc=new KeyController();
        private HotKey _hotKeyF10;
        private HotKey _hotKeyF12;
        private HotKey _hotKeyF9;
        private HotKey _hotKeyF8;
        private bool _runningFlag;
        private List<string> _tmpScore;
        private Queue<KeyPlayList> keyPlayLists;
        private CancellationTokenSource cts = new CancellationTokenSource();
        public MainForm()
        {
            InitializeComponent();
            formUpdate();
            KeyBinding.LoadConfig();
            ThreadPool.SetMaxThreads(25, 50);
            Task.Run(() => { CommonUtilities.GetLatestVersion(); });
           
            Text += $" Ver{Assembly.GetExecutingAssembly().GetName().Version}";
            cbMidiKeyboard.DataSource = KeyboardUtilities.GetKeyboardList();
        }

        private void Gbk_HotKeyPressed(object sender, GlobalHotKeyEventArgs e)
        {
            Console.WriteLine("test");
        }

        private void formUpdate()
        {
            
            if (Settings.Default.IsEightKeyLayout)
            {
                btn8key.BackgroundImage = Resources.ka1;
                btn22key.BackgroundImage = Resources.kb0;
                btnSwitch.BackgroundImage = Resources.a0;
                btn8key.Enabled = true;
                btn22key.Enabled = false;
            }
            else
            {
                btn8key.BackgroundImage = Resources.ka0;
                btn22key.BackgroundImage = Resources.kb1;
                btnSwitch.BackgroundImage = Resources.a1;
                btn8key.Enabled = false;
                btn22key.Enabled = true;
            }
        }


        private void HotKeyManagerPressed(object sender, KeyPressedEventArgs e)
        {
            switch (e.HotKey.Key)
            {
                case Key.F10 when _runningFlag == false:
                    if ((Path.GetExtension(midFileDiag.FileName)!=".mid")&& (Path.GetExtension(midFileDiag.FileName) != ".midi"))
                    {
                        MessageBox.Show("没有midi你演奏个锤锤？", "喵喵喵？", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        break;
                    }
                    _runningFlag = true;
                    timer1.Interval = 1000;
                    timer1.Start();
                    mtk.OpenFile(midFileDiag.FileName);
                    mtk.GetTrackManagers();
                    keyPlayLists = mtk.ArrangeKeyPlaysNew((double)(mtk.GetBpm() / nudBpm.Value));
                    
                    break;
                case Key.F11 when _runningFlag:
                    _runningFlag = false;
                    cts.Cancel();
                    break;
                case Key.F8 when _runningFlag:
                    ParameterController.GetInstance().Pitch -= 1;
                    
                    break;
                case Key.F9 when _runningFlag:
                    ParameterController.GetInstance().Pitch += 1;
                    
                    break;
            }
        }

        private Task NewCancellableTask(CancellationToken token)
        {
            return Task.Run(() =>
            {
                //var keyPlayLists = mtk.ArrangeKeyPlays(mtk.Index);
                ParameterController.GetInstance().InternalOffset = (int)numericUpDown2.Value;
                ParameterController.GetInstance().Offset = 0;
                kc.KeyPlayBack(keyPlayLists, 1, cts.Token);
                _runningFlag = false;
            }, token);
        }

        private void trackComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mtk.Index = trackComboBox.SelectedIndex;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _hotKeyF10 = hotKeyManager.Register(Key.F10, System.Windows.Input.ModifierKeys.Control);
                _hotKeyF12 = hotKeyManager.Register(Key.F11, System.Windows.Input.ModifierKeys.Control);
                _hotKeyF8 = hotKeyManager.Register(Key.F8, System.Windows.Input.ModifierKeys.Control);
                _hotKeyF9 = hotKeyManager.Register(Key.F9, System.Windows.Input.ModifierKeys.Control);
                //hkm = new BondTech.HotkeyManagement.Win.HotKeyManager(this);
                //gbk = new BondTech.HotkeyManagement.Win.GlobalHotKey("test", Modifiers.Alt, Keys.F8);
                //gbk.HotKeyPressed += Gbk_HotKeyPressed;

                //hkm.AddGlobalHotKey(gbk);
            }
            catch (Win32Exception)
            {
                MessageBox.Show("无法注册快捷键，请检查是否被其他程序占用。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }


            hotKeyManager.KeyPressed += HotKeyManagerPressed;
        }

        private void selectFileButton_Click(object sender, EventArgs e)
        {
            if (midFileDiag.ShowDialog() == DialogResult.OK)
                mtk.OpenFile(midFileDiag.FileName);
            else
                return;

            pathTextBox.Text = midFileDiag.FileName;
            _tmpScore = mtk.GetTrackManagers(); //note tracks
            var bpm = mtk.GetBpm();
            var tmp = new List<string>();

            if (_tmpScore != null)
                for (var i = 0; i < _tmpScore.Count; i++)
                    tmp.Add($"track_{i}:{_tmpScore[i]}");


            trackComboBox.DataSource = tmp;
            trackComboBox.SelectedIndex = 0;
            if (bpm >= nudBpm.Maximum)
                nudBpm.Value = nudBpm.Maximum;
            else if (bpm <= nudBpm.Minimum)
                nudBpm.Value = nudBpm.Minimum;
            else
                nudBpm.Value = bpm;
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            mtk.Offset = EnumPitchOffset.OctaveLower;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            mtk.Offset = EnumPitchOffset.None;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            mtk.Offset = EnumPitchOffset.OctaveHigher;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            mtk.Bpm = (int) nudBpm.Value;
        }

        private void SyncButton_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            mtk.OpenFile(midFileDiag.FileName);
            keyPlayLists = mtk.ArrangeKeyPlaysNew((double)(mtk.GetBpm() / nudBpm.Value));

            var interval = dateTimePicker1.Value - DateTime.Now;

            timer1.Interval = (int) interval.TotalMilliseconds + (int) numericUpDown2.Value <= 0
                ? 1000
                : (int) interval.TotalMilliseconds + (int) numericUpDown2.Value;
            timer1.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            hotKeyManager.Unregister(_hotKeyF10);
            hotKeyManager.Unregister(_hotKeyF12);
            hotKeyManager.Unregister(_hotKeyF8);
            hotKeyManager.Unregister(_hotKeyF9);
            // Dispose the hotkey manager.
            hotKeyManager.Dispose();
            KeyboardUtilities.Disconnect();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            keyForm8.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            _runningFlag = true;
            cts = new CancellationTokenSource();
            NewCancellableTask(cts.Token);
        }


        private void btnKeyboardConnect_Click(object sender, EventArgs e)
        {
            if (cbMidiKeyboard.SelectedItem != null)
                if (cbMidiKeyboard.Enabled)
                {
                    if (KeyboardUtilities.Connect(cbMidiKeyboard.SelectedItem.ToString(),kc) == 0)
                    {
                        cbMidiKeyboard.Enabled = false;
                        btnKeyboardConnect.BackgroundImage = Resources.btn2;
                    }
                }
                else
                {
                    KeyboardUtilities.Disconnect();
                    cbMidiKeyboard.Enabled = true;
                    cbMidiKeyboard.DataSource = KeyboardUtilities.GetKeyboardList();
                    btnKeyboardConnect.BackgroundImage = Resources.btn1;
                }
        }
        

        protected override void WndProc(ref Message m)
        {
            try
            {
                const int WM_DEVICECHANGE = 0x0219;
                switch (m.Msg)
                {
                    case WM_DEVICECHANGE:
                        cbMidiKeyboard.DataSource = KeyboardUtilities.GetKeyboardList();
                        break;
                    default:
                        break;
                }
            }

            catch (Exception ex)
            {
               
            }

            base.WndProc(ref m);
        }
        private void cbMidiKeyboard_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cbMidiKeyboard.DataSource = KeyboardUtilities.GetKeyboardList();
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            if (Settings.Default.IsEightKeyLayout)
                Settings.Default.IsEightKeyLayout = false;
            else
                Settings.Default.IsEightKeyLayout = true;

            Settings.Default.Save();
            formUpdate();
        }

        private void btn22key_Click(object sender, EventArgs e)
        {
            keyForm22.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //BackgroundKey.Keytest();
            new AboutForm(kc).ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void TimeSync()
        {
            double error;
            var offset = new NtpClient(Properties.Settings.Default.NtpServer).GetOffset(out error);
            //TODO:error handler
            if (CommonUtilities.SetSystemDateTime.SetLocalTimeByStr(
                DateTime.Now.AddMilliseconds(offset.TotalMilliseconds * -0.5)))
                tlblTime.Text = $"已同步 误差{offset.TotalMilliseconds}ms";
            else
                tlblTime.Text = "设置时间出错";
        }


        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (mtk.PlaybackStart((int) nudBpm.Value) == 0)
            {
                btnPlay.BackgroundImage = Resources.c_play_1;
                btnPause.BackgroundImage = Resources.c_pause;
                btnStop.BackgroundImage = Resources.c_stop;
                
                lblPlay.Text = "正在试听";
                lblMidiName.Text = Path.GetFileNameWithoutExtension(midFileDiag.FileName);
                playTimer.Start();
            }

            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (mtk.PlaybackRestart() == 0)
            {
                btnPlay.BackgroundImage = Resources.c_play;
                btnPause.BackgroundImage = Resources.c_pause;
                btnStop.BackgroundImage = Resources.c_stop_1;
                lblPlay.Text = "试听已停止";
                timeLabel.Text = "";
                playTimer.Stop();
            }
            
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (mtk.PlaybackPause() == 0)
            {
                btnPlay.BackgroundImage = Resources.c_play;
                btnPause.BackgroundImage = Resources.c_pause_1;
                btnStop.BackgroundImage = Resources.c_stop;
                lblPlay.Text = "试听暂停";
            }
            
        }

        private bool isCaptureFlag = false;
        private Network.Network net;
        private void BtnTimeSync_Click(object sender, EventArgs e)
        {
            TimeSync();
            if (isCaptureFlag)
            {
                net.StopCapture();
                isCaptureFlag = false;
                (sender as Button).Text = "开始抓包";
            }
            else
            {
                if (net==null)net = new Network.Network(this);
                try
                {
                    net.StartCapture(Daigassou.Utils.FFProcess.FindFFXIVProcess().First());
                    isCaptureFlag = true;
                    (sender as Button).Text = "停止抓包";
                }
                catch (Exception exception)
                {
                    MessageBox.Show("你开游戏了吗？", "喵喵喵？", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                
            }
        }

        private void NumericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = mtk.PlaybackInfo();
        }

        private void ToolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            mtk.SaveToFile();
        }
    }
}