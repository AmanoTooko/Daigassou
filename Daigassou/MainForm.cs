using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Daigassou.Input_Midi;
using Daigassou.Properties;
using GlobalHotKey;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Smf;
using Melanchall.DryWetMidi.Smf.Interaction;

namespace Daigassou
{
    public partial class MainForm : Form
    {
        private readonly HotKeyManager hotKeyManager = new HotKeyManager();
        private readonly KeyBindFormOld keyForm22 = new KeyBindFormOld();
        private readonly KeyBindForm keyForm8 = new KeyBindForm();
        private readonly MidiToKey mtk = new MidiToKey();
        private HotKey _hotKeyF10;
        private HotKey _hotKeyF12;
        private bool _runningFlag;
        private List<string> _tmpScore;

        private CancellationTokenSource cts = new CancellationTokenSource();

        public MainForm()
        {
            InitializeComponent();
            formUpdate();
            KeyBinding.LoadConfig();
            CommonUtilities.GetLatestVersion();

            cbMidiKeyboard.DataSource = KeyboardUtilities.GetKeyboardList();
        }

        private void formUpdate()
        {
            Text += $" Ver{Assembly.GetExecutingAssembly().GetName().Version}";
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
                    _runningFlag = true;
                    
                    cts = new CancellationTokenSource();
                    NewCancellableTask(cts.Token);
                    break;
                case Key.F11 when _runningFlag:
                    _runningFlag = false;
                    cts.Cancel();
                    break;
            }
        }

        private Task NewCancellableTask(CancellationToken token)
        {
            return Task.Run(() =>
            {
                mtk.ArrangeKeyPlaysNew(mtk.Index);
                //var keyPlayLists = mtk.ArrangeKeyPlays(mtk.Index);
                //KeyController.KeyPlayBack(keyPlayLists, 1, cts.Token);
                //_runningFlag = false;
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
                    tmp.Add("track_" + i);


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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new AboutForm().ShowDialog();
        }


        private void btnKeyboardConnect_Click(object sender, EventArgs e)
        {
            if (cbMidiKeyboard.SelectedItem != null)
                if (cbMidiKeyboard.Enabled)
                {
                    if (KeyboardUtilities.Connect(cbMidiKeyboard.SelectedIndex) == 0)
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
            new AboutForm().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void btnTimeSync_Click(object sender, EventArgs e)
        {
            double error=0;
            var offset=new NtpClient("ntp3.aliyun.com").GetOffset(out error);
            if (CommonUtilities.SetSystemDateTime.SetLocalTimeByStr(DateTime.Now.AddMilliseconds(offset.TotalMilliseconds*-0.5)))
            {
                tlblTime.Text = $"已同步 误差{error}ms";
            }
            else
            {
                tlblTime.Text = $"设置时间出错";
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            mtk.PlaybackStart();
            tlblPlay.Text = $"正在试听: {Path.GetFileNameWithoutExtension(midFileDiag.FileName)}";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mtk.PlaybackPause();
            tlblPlay.Text = $"暂停试听: {Path.GetFileNameWithoutExtension(midFileDiag.FileName)}";
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            mtk.PlaybackRestart();
            tlblPlay.Text = $"试听已停止";
        }
    }
}