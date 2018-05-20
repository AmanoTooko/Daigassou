using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using GlobalHotKey;
using Midi.Devices;
using Midi.Enums;
using Midi.Instruments;
using Midi.Messages;

namespace Daigassou
{
    public partial class MainForm : Form
    {
        private MidiToKey mtk= new MidiToKey();
        private List<string> _tmpScore;
        private readonly HotKeyManager hotKeyManager = new HotKeyManager();
        private HotKey _hotKeyF10;
        private HotKey _hotKeyF12;

        private CancellationTokenSource cts = new CancellationTokenSource();
        private KeyBindForm keyForm=new KeyBindForm();
        private bool runningFlag = false;
        private IInputDevice midiKeyboard;
        public MainForm()
        {
            InitializeComponent();
            KeyBinding.LoadConfig();
            DeviceManager.UpdateInputDevices();
            if (DeviceManager.InputDevices.Count == 1)
            {
                
                midiKeyboard= DeviceManager.InputDevices.First();
                midiKeyboard.Open();
                midiKeyboard.StartReceiving(null);
                midiKeyboard.NoteOn += NoteOn;
            }
            
        }
        public void NoteOn(NoteOnMessage msg)
        {
            lock (this)
            {
                if (Convert.ToInt32(msg.Pitch)<=82&&Convert.ToInt32(msg.Pitch)>=48)
                {
                    KeyController.KeyboardPress(KeyBinding.GetNoteToCtrlKey(Convert.ToInt32(msg.Pitch)), KeyBinding.GetNoteToKey(Convert.ToInt32(msg.Pitch)));

                }
            }
        }



        private void HotKeyManagerPressed(object sender, KeyPressedEventArgs e)
        {
            
            if (e.HotKey.Key == Key.F10&&runningFlag==false)
            {
                runningFlag = true;
                cts=new CancellationTokenSource();
                NewCancellableTask(cts.Token);
            }
            if (e.HotKey.Key == Key.F11&&runningFlag==true)
            {
                runningFlag = false;
                cts.Cancel();
            }
 
        }
        private Task NewCancellableTask(CancellationToken token)
        {
            return Task.Run(() =>
            {
                Queue<KeyPlayList> keyPlayLists = mtk.ArrangeKeyPlays(mtk.Index);
                KeyController.KeyPlayBack(keyPlayLists, 1, cts.Token);
            });
        }

        private void trackComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            noteTextBox.Text = _tmpScore[trackComboBox.SelectedIndex];
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
            
            if (midFileDiag.ShowDialog()==DialogResult.OK)
            {
                mtk.OpenFile(midFileDiag.FileName);
            }

            pathTextBox.Text = midFileDiag.FileName;
            _tmpScore = mtk.getTrackManagers();//note tracks
            List<string> tmp = new List<string>();
            for (int i = 0; i < _tmpScore.Count; i++)
            {
                tmp.Add("track_" + i.ToString());
            }
            trackComboBox.DataSource = tmp;
            trackComboBox.SelectedIndex = 0;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("设好了瞎点什么，给我关了");
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
            mtk.Bpm = (int)numericUpDown1.Value;
        }

        private void SyncButton_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            TimeSpan interval = dateTimePicker1.Value - DateTime.Now;
            timer1.Interval = ((int)interval.TotalMilliseconds + (int)numericUpDown2.Value)<=0?1000: ((int)interval.TotalMilliseconds + (int)numericUpDown2.Value);
            timer1.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            hotKeyManager.Unregister(_hotKeyF10);
            hotKeyManager.Unregister(_hotKeyF12);
            // Dispose the hotkey manager.
            hotKeyManager.Dispose();
            midiKeyboard.StopReceiving();
            midiKeyboard.Close();
            midiKeyboard.RemoveAllEventHandlers();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            keyForm.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            runningFlag = true;
            cts = new CancellationTokenSource();
            NewCancellableTask(cts.Token);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new AboutForm().ShowDialog();
        }
    }
}
