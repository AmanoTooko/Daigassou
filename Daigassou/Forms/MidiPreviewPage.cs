using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Daigassou.Controller;
using Daigassou.Utils;
using DaigassouDX.Controller;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using Sunny.UI;

namespace Daigassou.Forms
{
    public partial class MidiPreviewPage : UIPage
    {
        private string midiFilePath="";
        private int midiTrackIndex = 0;
        private bool isPreviewAll=false;
        private bool isRunning=false;
        private MidiPlayController pbController;
        private MidiFileParser midiFileParser;
        private Timer playProcessTimer;
        
        public MidiPreviewPage()
        {
            InitializeComponent();
            GetOutputDevice();
            pbController = new MidiPlayController();
            midiFileParser = new MidiFileParser();
            playProcessTimer = new Timer();
            playProcessTimer.Interval=1000;
            playProcessTimer.Tick += PlayProcessTimer_Tick;
        }

        private void PlayProcessTimer_Tick(object sender, EventArgs e)
        {
            var origText = pbController.GetProcess();
            int currentTime = Convert.ToInt32(origText.Split("/").First());
            int durationTime= Convert.ToInt32(origText.Split("/").Last());
            tbMidiProcess.Value = currentTime*100 / durationTime;
            lblProcess.Text =
                $"正在播放：{new TimeSpan(0, 0, 0, 0, currentTime).ToString("mm\\:ss")}/{new TimeSpan(0, 0, 0, 0, durationTime).ToString("mm\\:ss")}";
        }

        private void GetOutputDevice()
        {
            var Namelist = new List<string>();
            foreach (var device in OutputDevice.GetAll())
            {
                Namelist.Add(device.Name);
            }

            cbMidiDevice.DataSource = Namelist;
        }
        private void MidiPreviewPage_ReceiveParams(object sender, UIPageParamsArgs e)
        {
            var recvEvent = e.Value as CommObject;
            switch (recvEvent.eventId)
            {
                case eventCata.MIDI_FILE_NAME:
                    midiFilePath = recvEvent.payload.ToString();
                    lblScoreName.Text = $"乐谱名：{recvEvent.payload.ToString().Split('\\').Last()}";
                    break;
                case eventCata.TRACK_FILE_NAME:
                    midiTrackIndex= Convert.ToInt32(recvEvent.payload.ToString().Split('|').First());
                    lblTrackName.Text = $"轨道名：{recvEvent.payload.ToString().TrimStart($"{midiTrackIndex}|".ToCharArray())}";
                    break;
            }
        }

        private void swAll_ValueChanged(object sender, bool value)
        {
            isPreviewAll = value;
        }

        private void cbMidiDevice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void uiSymbolButton6_Click(object sender, EventArgs e)
        {
            if (btnStart.Symbol==61515)// start
            {
                if (!isRunning)
                {
                    midiFileParser.OpenFile(midiFilePath);
                    midiFileParser.GetTrackNames();
                    if (isPreviewAll)
                    {
                        pbController.SetPlaybackForPreview(midiFileParser.GetPlaybackForAll(), OutputDevice.GetByIndex(cbMidiDevice.SelectedIndex));
                    }
                    else
                    {
                        midiFileParser.Index = midiTrackIndex;
                        pbController.SetPlaybackForPreview(midiFileParser.GetPlayback(), OutputDevice.GetByIndex(cbMidiDevice.SelectedIndex));
                    }
                   
                }

                pbController.StartPlay(0);
                btnStart.Symbol = 61516;
                btnStart.SymbolOffset =new Point(0,0);
                isRunning = true;
                pbController.Playback_Finished_Notification += () =>
                {
                    ResetPlay();

                };
                playProcessTimer.Start();
            }
            else
            {
                pbController.PausePlay();
                btnStart.Symbol = 61515;
                btnStart.SymbolOffset = new Point(2, 1);
            }



        }

        private void uiLinkLabel1_Click(object sender, EventArgs e)
        {
            Process.Start("https://midiex.ffxiv.cat");
        }

        private void ResetPlay()
        {
            isRunning = false;
            pbController.StopPlay();
            playProcessTimer.Stop();
            btnStart.Symbol = 61515;
            btnStart.SymbolOffset = new Point(2, 1);
            tbMidiProcess.Value = 0;
            Action actionDelegate = () => { lblProcess.Text = "停止播放："; };
            BeginInvoke(actionDelegate);


        }
        private void uiSymbolButton5_Click(object sender, EventArgs e)
        {
            ResetPlay();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            pbController.SetPreviewOffset(500);
        }

        private void btnBackward_Click(object sender, EventArgs e)
        {
            pbController.SetPreviewOffset(-500);
        }
    }
}
