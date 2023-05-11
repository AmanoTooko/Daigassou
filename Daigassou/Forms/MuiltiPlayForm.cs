using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Daigassou.Controller;
using Daigassou.Properties;
using Daigassou.Utils;
using DaigassouDX.Controller;
using Sunny.UI;

namespace Daigassou.Forms
{
    public partial class MuiltiPlayForm : UIPage
    {
        private readonly NetworkParser networkParser;

        public MuiltiPlayForm()
        {
            InitializeComponent();
            
            networkParser = new NetworkParser();
        }

        public override void Init()
        {
            base.Init();
            UIStyles.InitColorful(Color.FromArgb(255, 141, 155), Color.White);
            uiLine1.ForeColor = Color.FromArgb(255, 113, 128);
            uiLine2.ForeColor = Color.FromArgb(255, 113, 128);
            uiLine3.ForeColor = Color.FromArgb(255, 113, 128);
        }

        public override void Final()
        {
            base.Final();
        }

        private void uiSwitch1_ValueChanged(object sender, bool value)
        {
            void startNetworkParser(Process process)
            {
                networkParser.process = process;
                networkParser.isUsingEnsembleAssist = radioBtnGA.Checked;
                networkParser.StartNetworkMonitor();
                networkParser.Play += NetworkParser_Play;
            }

            uiPanel1.Enabled = !value;
            radioBtnCtd.Enabled = !value;
            radioBtnGA.Enabled = !value;
            if (value)
            {
                
                if (ProcessKeyController.GetInstance().Process != null)
                {
                    startNetworkParser(ProcessKeyController.GetInstance().Process);

                    UIMessageTip.ShowOk($"检测到后台演奏，自动绑定至Pid={ProcessKeyController.GetInstance().Process.Id}", 2000, true,
                        PointToScreen(new Point(Location.X + 200, Location.Y + 200)));
                    return;
                }

                var processList = Utils.Utils.GetProcesses();
                if (processList.Count == 0)
                {
                    UIMessageTip.ShowError("未检测到游戏进程，无法使用网络合奏", 2000, true,
                        PointToScreen(new Point(Location.X + 200, Location.Y + 200)));
                    swNetworkAnalyze.Active = false;
                }
                else if (processList.Count == 1)
                {
                    startNetworkParser(processList[0]);
                    UIMessageTip.ShowOk($"已检测到游戏进程，自动绑定至Pid={processList[0].Id}", 2000,
                        true, PointToScreen(new Point(Location.X + 200, Location.Y + 200)));
                }
                else
                {
                    var processIndex = new List<int>();
                    var index = 0;

                    foreach (var process in processList)
                    {
                        processIndex.Add(process.Id);
                        Utils.Utils.SetGameTitle(process.MainWindowHandle, true, process.Id.ToString());
                    }

                    if (this.ShowSelectDialog(ref index, processIndex, "绑定后台进程", "请观察游戏窗口标题栏，数字即为PID，选定后标题会恢复。"))
                    {
                        startNetworkParser(processList[index]);

                        UIMessageTip.ShowOk($"已绑定至Pid={processList[index].Id}", 2000, true,
                            PointToScreen(new Point(Location.X + 200, Location.Y + 200)));
                        foreach (var process in processList) Utils.Utils.SetGameTitle(process.MainWindowHandle, false);
                    }
                    else
                    {
                        swNetworkAnalyze.Active = false;
                        foreach (var process in processList) Utils.Utils.SetGameTitle(process.MainWindowHandle, false);
                    }
                }
            }
            else
            {
                networkParser.StopNetworkMonitor();
            }
        }

        private void NetworkParser_Play(object sender, PlayEvent e)
        {
            switch (e.Mode)
            {
                case PlayEvent.playmode.COUNDOWN_TIMER_START:
                    SendParamToPage((int) PageID.SoloPlayPage,
                        new CommObject {eventId = eventCata.MIDI_CONTROL_START_COUNTDOWN, payload = e.Param});
                    break;
                case PlayEvent.playmode.ENSEMBLE_TIMER_START:
                    SendParamToPage((int) PageID.SoloPlayPage,
                        new CommObject {eventId = eventCata.MIDI_CONTROL_START_ENSEMBLE, payload = e.Param});
                    break;

                case PlayEvent.playmode.STOP:
                    SendParamToPage((int) PageID.SoloPlayPage,
                        new CommObject {eventId = eventCata.MIDI_CONTROL_STOP, payload = e.Param});
                    break;
                case PlayEvent.playmode.INSTRUAMENT_CHANGE:
                    SendParamToPage((int) PageID.SoloPlayPage,
                        new CommObject {eventId = eventCata.MIDI_CONTROL_INSTRUCODE, payload = e.Param});
                    break;
            }
        }

        private void MuiltiPlayForm_ReceiveParams(object sender, UIPageParamsArgs e)
        {
            var recvEvent = e.Value as CommObject;
            switch (recvEvent.eventId)
            {
                case eventCata.MIDI_FILE_NAME:

                    lblScoreName.Text = $"乐谱名：{recvEvent.payload.ToString().Split('\\').Last()}";
                    break;
                case eventCata.TRACK_FILE_NAME:
                    var midiTrackIndex = Convert.ToInt32(recvEvent.payload.ToString().Split('|').First());
                    lblTrackName.Text =
                        $"轨道名：{recvEvent.payload.ToString().TrimStart($"{midiTrackIndex}|".ToCharArray())}";
                    break;
            }
        }

        private void uiSymbolButton1_Click(object sender, EventArgs e)
        {
            var time = uiDatetimePicker1.Value - DateTime.Now;
            if (time.TotalMilliseconds > 0)
                SendParamToPage((int) PageID.SoloPlayPage,
                    new CommObject
                        {eventId = eventCata.MIDI_CONTROL_START_TIMER, payload = (int) time.TotalMilliseconds});
            else
                UIMessageTip.ShowError("合奏错误：定时的演奏时间已过");
        }

        private void MuiltiPlayForm_Load(object sender, EventArgs e)
        {
            uiDatetimePicker1.Value = DateTime.Now;
        }

        private void MuiltiPlayForm_Enter(object sender, EventArgs e)
        {
            uiDatetimePicker1.Value = DateTime.Now;
        }

        private void uiLinkLabel1_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.bilibili.com/video/BV11K411X7xC/");
        }
    }
}