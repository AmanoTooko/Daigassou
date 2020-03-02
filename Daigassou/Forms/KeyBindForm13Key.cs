using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Daigassou.Input_Midi;

namespace Daigassou
{
    public partial class KeyBindForm8Key : Form
    {
        private TextBox[] keyBoxs = new TextBox[13];
        private const int OCTAVE_KEY_LOW = 59;
        private const int OCTAVE_KEY_HIGH = 72;
        public KeyBindForm8Key()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            keyBoxs = new TextBox[13]
            {
                textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10,
                textBox11, textBox12, textBox13
            };


            for (var i = 0; i < 13; i++) keyBoxs[i].KeyDown += textBox_KeyDown;
        }


        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            var tmpBox = (TextBox) sender;
            if (tmpBox == null) throw new ArgumentNullException(nameof(tmpBox));
            tmpBox.Text = e.KeyCode.ToString();
            KeyBinding.SetKeyToNote_13(Array.IndexOf(keyBoxs, tmpBox) + 60, e.KeyValue);
        }

        private void KeyBindForm_Load(object sender, EventArgs e)
        {
            KeyBinding.LoadConfig();
            for (var i = 0; i < 13; i++) keyBoxs[i].Text = KeyBinding.GetKeyChar(KeyBinding.GetNoteToKey(i + 72)).ToString();
            //keyBoxs[12].Text = KeyBinding.GetKeyChar(KeyBinding.GetNoteToKey(84)).ToString();
            var settingLower = KeyBinding.GetNoteToCtrlKey(OCTAVE_KEY_LOW);
            var settingHigher = KeyBinding.GetNoteToCtrlKey(OCTAVE_KEY_HIGH);
            switch (settingLower)
            {
                case Keys.ControlKey: cbOctaveLower.Text = "Ctrl";
                    break;
                case Keys.Menu: cbOctaveLower.Text = "Alt";
                    break;
                case Keys.ShiftKey: cbOctaveLower.Text = "Shift";
                    break;
            }

            switch (settingHigher)
            {
                case Keys.ControlKey:
                    cbOctaveHigher.Text = "Ctrl";
                    break;
                case Keys.Menu:
                    cbOctaveHigher.Text = "Alt";
                    break;
                case Keys.ShiftKey:
                    cbOctaveHigher.Text = "Shift";
                    break;
            }
        }

        private void KeyBindForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbOctaveHigh_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tmpSelection = cbOctaveLower.Text;
            

            switch (cbOctaveHigher.Text)
            {
                case "Ctrl":
                    KeyBinding.SetCtrlKeyToNote(OCTAVE_KEY_HIGH, Keys.ControlKey);
                    break;
                case "Alt":
                    KeyBinding.SetCtrlKeyToNote(OCTAVE_KEY_HIGH, Keys.Menu);
                    break;
                case "Shift":
                    KeyBinding.SetCtrlKeyToNote(OCTAVE_KEY_HIGH, Keys.ShiftKey);
                    break;
                default:
                    break;
            }

            var tmpDataSource = new List<string> {"Ctrl", "Alt", "Shift"};

            tmpDataSource.Remove(cbOctaveHigher.Text);
            cbOctaveLower.SelectedIndexChanged -= cbOctaveLow_SelectedIndexChanged;
            cbOctaveLower.DataSource = tmpDataSource;
            cbOctaveLower.SelectedItem = tmpSelection;
            cbOctaveLower.SelectedIndexChanged += cbOctaveLow_SelectedIndexChanged;
        }

        private void cbOctaveLow_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tmpSelection = cbOctaveHigher.Text;
            switch (cbOctaveLower.Text)
            {
                case "Ctrl":
                    KeyBinding.SetCtrlKeyToNote(OCTAVE_KEY_LOW, Keys.ControlKey);
                    break;
                case "Alt":
                    KeyBinding.SetCtrlKeyToNote(OCTAVE_KEY_LOW, Keys.Menu);
                    break;
                case "Shift":
                    KeyBinding.SetCtrlKeyToNote(OCTAVE_KEY_LOW, Keys.ShiftKey);
                    break;
                default:
                    break;
            }

            var tmpDataSource = new List<string> {"Ctrl", "Alt", "Shift"};

            tmpDataSource.Remove(cbOctaveLower.Text);
            cbOctaveHigher.SelectedIndexChanged -= cbOctaveHigh_SelectedIndexChanged;
            cbOctaveHigher.DataSource = tmpDataSource;
            cbOctaveHigher.SelectedItem = tmpSelection;
            cbOctaveHigher.SelectedIndexChanged += cbOctaveHigh_SelectedIndexChanged;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}