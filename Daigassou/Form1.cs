using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using GlobalHotKey;

namespace Daigassou
{
    public partial class Form1 : Form
    {
        private MidiToKey mtk= new MidiToKey();
        private List<string> _tmpScore;
        private readonly HotKeyManager hotKeyManager = new HotKeyManager();
        private HotKey _hotKeyF10;
        private HotKey _hotKeyF12;
        private Task _t;
        readonly CancellationTokenSource cts = new CancellationTokenSource();
        public Form1()
        {
            InitializeComponent();

        }




        private void HotKeyManagerPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.HotKey.Key == Key.F5&&(_t==null||_t.Status!=TaskStatus.Running))
            {
                _t = new Task(() => { KeyController.KeyPlayBack(mtk.ArrangeKeyPlays(mtk.Index), 1); },cts.Token);
                _t.Start();
            }
            if (e.HotKey.Key == Key.F11 &&_t!=null)
            {
                cts.Cancel();
            }
 
        }
        private void trackComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            noteTextBox.Text = _tmpScore[trackComboBox.SelectedIndex];
            mtk.Index = trackComboBox.SelectedIndex;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _hotKeyF10 = hotKeyManager.Register(Key.F10, System.Windows.Input.ModifierKeys.None);
            _hotKeyF12 = hotKeyManager.Register(Key.F11, System.Windows.Input.ModifierKeys.None);
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
            MessageBox.Show("我还没准备好啊-功能未实装");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            hotKeyManager.Unregister(_hotKeyF10);
            hotKeyManager.Unregister(_hotKeyF12);
            // Dispose the hotkey manager.
            hotKeyManager.Dispose();
        }
    }
}
