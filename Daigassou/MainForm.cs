using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Daigassou.Input_Midi;
using Daigassou.Properties;
using GlobalHotKey;
using Midi.Devices;
using Midi.Messages;

namespace Daigassou
{
    public partial class MainForm : Form
    {
        private readonly HotKeyManager hotKeyManager = new HotKeyManager();
        private HotKey _hotKeyF10;
        private HotKey _hotKeyF12;
        private bool _runningFlag;
        private List<string> _tmpScore;

        private CancellationTokenSource cts = new CancellationTokenSource();
        private readonly KeyBindForm keyForm = new KeyBindForm();

        private readonly MidiToKey mtk = new MidiToKey();

        public MainForm()
        {
            InitializeComponent();
            KeyBinding.LoadConfig();



            cbMidiKeyboard.DataSource = KeyboardUtilities.GetKeyboardList();
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
                var keyPlayLists = mtk.ArrangeKeyPlays(mtk.Index);
                KeyController.KeyPlayBack(keyPlayLists, 1, cts.Token);
            }, token);
        }

        private void trackComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mtk.Index = trackComboBox.SelectedIndex;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _hotKeyF10 = hotKeyManager.Register(Key.F10, System.Windows.Input.ModifierKeys.Control);
            _hotKeyF12 = hotKeyManager.Register(Key.F11, System.Windows.Input.ModifierKeys.Control);
            hotKeyManager.KeyPressed += HotKeyManagerPressed;
        }

        private void selectFileButton_Click(object sender, EventArgs e)
        {
            if (midFileDiag.ShowDialog() == DialogResult.OK) mtk.OpenFile(midFileDiag.FileName);

            pathTextBox.Text = midFileDiag.FileName;
            _tmpScore = mtk.GetTrackManagers(); //note tracks
            var tmp = new List<string>();
            for (var i = 0; i < _tmpScore.Count; i++) tmp.Add("track_" + i);

            trackComboBox.DataSource = tmp;
            trackComboBox.SelectedIndex = 0;
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
            mtk.Bpm = (int) numericUpDown1.Value;
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
            keyForm.Show();
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

        private void RBEightKey_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.IsEightKeyLayout = true;
        }

        private void RB22Key_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.IsEightKeyLayout = false;
        }

        private void btnKeyboardConnect_Click(object sender, EventArgs e)
        {
            if (cbMidiKeyboard.Enabled==true)
            {
                if (KeyboardUtilities.Connect(cbMidiKeyboard.SelectedIndex) == 0)
                {
                    cbMidiKeyboard.Enabled = false;
                    btnKeyboardConnect.Text = "断开";
                }
            }
            else
            {
                KeyboardUtilities.Disconnect();
                cbMidiKeyboard.Enabled = true;
                cbMidiKeyboard.DataSource = KeyboardUtilities.GetKeyboardList();
                btnKeyboardConnect.Text = "连接";
            }
            
            
        }

        private void cbMidiKeyboard_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cbMidiKeyboard.DataSource = KeyboardUtilities.GetKeyboardList();
        }
    }
}