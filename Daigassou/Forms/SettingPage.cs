using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DaigassouDX.Controller;
using Newtonsoft.Json.Linq;
using Sunny.UI;

namespace Daigassou.Forms
{
    public partial class SettingPage : UIPage
    {
        public SettingPage()
        {
            InitializeComponent();
            Init();
        }

        public override void Init()
        {
            base.Init();
            uiLine1.ForeColor = Color.FromArgb(255, 113, 128);
            uiLine2.ForeColor = Color.FromArgb(255, 113, 128);
            uiLine3.ForeColor = Color.FromArgb(255, 113, 128);
            LoadSettings();
        }

        public override void Final()
        {
            base.Final();

        }

        private void LoadSettings()
        {
            updChordMinMs.Value = (int)Properties.Settings.Default.MinChordMs;
            updIntervalMinMs.Value = (int)Properties.Settings.Default.MinEventMs;
            swEnableAnalyze.Active = Properties.Settings.Default.isUsingAnalysis;
            swEnableGuitarKey.Active = Properties.Settings.Default.isUsingGuitarKey;
            swUsingPcap.Active = Properties.Settings.Default.isUsingWinPCap;
            swEnableAnalyze.Active = Properties.Settings.Default.isUsingAnalysis;
            swEnableBackgroundPlay.Active = Properties.Settings.Default.isBackgroundKey;
            tbNTPServerAddr.Text = Properties.Settings.Default.NtpServer;
        }
        private void uiSymbolButton2_Click(object sender, EventArgs e)
        {
            new KeyBindingForm().ShowDialog();
        }



        private void btnAbout_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void tbNTPServerAddr_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.NtpServer = tbNTPServerAddr.Text;
            Properties.Settings.Default.Save();
        }

        private void swUsingPcap_ValueChanged(object sender, bool value)
        {
            Properties.Settings.Default.isUsingWinPCap = value;
            Properties.Settings.Default.Save();
        }

        private void swEnableAnalyze_ValueChanged(object sender, bool value)
        {
            Properties.Settings.Default.isUsingAnalysis = value;
            Properties.Settings.Default.Save();
        }

        private void swEnableGuitarKey_ValueChanged(object sender, bool value)
        {
            Properties.Settings.Default.isUsingGuitarKey = value;
            Properties.Settings.Default.Save();
        }

        private void swEnableBackgroundPlay_ValueChanged(object sender, bool value)
        {
            Properties.Settings.Default.isBackgroundKey = value;
            Properties.Settings.Default.Save();
            if (value)
            {
                var processList = Utils.Utils.GetProcesses();
                if (processList.Count==0)
                {
                    UIMessageTip.ShowError("未检测到游戏进程，后台演奏关闭");
                    swEnableBackgroundPlay.Active = false;
                }
                else if (processList.Count == 1)
                {
                    ProcessKeyController.GetInstance().Process = processList[0];
                    UIMessageTip.ShowOk($"已检测到后台进程，绑定至Pid={ProcessKeyController.GetInstance().Process.Id}");
                }
                else
                {
                    var processIndex = new List<int>();
                    int index=0;

                    foreach (var process in processList)
                    {
                        processIndex.Add(process.Id);
                        Utils.Utils.SetGameTitle(process.MainWindowHandle,true,process.Id.ToString());
                    }

                    if (this.ShowSelectDialog(ref index, processIndex,"绑定后台进程","请观察游戏窗口标题栏，数字即为PID，选定后标题会恢复。"))
                    {
                        ProcessKeyController.GetInstance().Process = processList[index];
                        UIMessageTip.ShowOk($"已绑定至Pid={ProcessKeyController.GetInstance().Process.Id}");
                        foreach (var process in processList)
                        {
                            Utils.Utils.SetGameTitle(process.MainWindowHandle, false);
                        }
                    }
                    else
                    {
                        swEnableBackgroundPlay.Active = false;
                        foreach (var process in processList)
                        {
                            Utils.Utils.SetGameTitle(process.MainWindowHandle, false);
                        }
                    }

                }

            }
            else
            {
                ProcessKeyController.GetInstance().Process = null;

            }
        }

        private void updChordMinMs_ValueChanged(object sender, int value)
        {
            Properties.Settings.Default.MinChordMs = (uint)value;
            Properties.Settings.Default.Save();
        }

        private void updIntervalMinMs_ValueChanged(object sender, int value)
        {
            Properties.Settings.Default.MinEventMs = (uint)value;
            Properties.Settings.Default.Save();
        }
    }
}
