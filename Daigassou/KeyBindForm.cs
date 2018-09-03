using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Daigassou
{
    public partial class KeyBindForm : Form
    {
        private TextBox[] keyBoxs = new TextBox[13];

        public KeyBindForm()
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


            for (var i = 0; i < 13; i++)
            {
                keyBoxs[i].KeyDown += textBox_KeyDown;
            }
        }


        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tmpBox = (TextBox) sender;
            if (tmpBox == null) throw new ArgumentNullException(nameof(tmpBox));
            tmpBox.Text = e.KeyCode.ToString();
            KeyBinding.SetKeyToNote_8(Array.IndexOf(keyBoxs, tmpBox) + 60, e.KeyCode);
        }

        private void KeyBindForm_Load(object sender, EventArgs e)
        {
            KeyBinding.LoadConfig();
            for (int i = 0; i < 12; i++)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
            {
                keyBoxs[i].Text = KeyBinding.GetNoteToKey(i + 60).ToString();
            }
            keyBoxs[12].Text = KeyBinding.GetNoteToKey(84).ToString();
            
            
        }

        private void KeyBindForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tmpSelection = comboBoxLow.SelectedItem.ToString();


            switch (comboBox1.SelectedItem.ToString())
            {
                case "Ctrl":
                    KeyBinding.SetCtrlKeyToNote(72, Keys.ControlKey);
                    break;
                case "Alt":
                    KeyBinding.SetCtrlKeyToNote(72, Keys.Alt);
                    break;
                case "Shift":
                    KeyBinding.SetCtrlKeyToNote(72, Keys.ShiftKey);
                    break;
                default:
                    break;
            }
            List<String> tmpDataSource = new List<string> { "Ctrl", "Alt", "Shift" };

            tmpDataSource.Remove(comboBox1.SelectedItem.ToString());
            comboBoxLow.SelectedIndexChanged -= comboBoxLow_SelectedIndexChanged;
            comboBoxLow.DataSource = tmpDataSource;
            comboBoxLow.SelectedItem = tmpSelection;
            comboBoxLow.SelectedIndexChanged += comboBoxLow_SelectedIndexChanged;




        }

        private void comboBoxLow_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tmpSelection = comboBox1.SelectedItem.ToString();
            switch (comboBoxLow.SelectedItem.ToString())
            {
                case "Ctrl":
                    KeyBinding.SetCtrlKeyToNote(59, Keys.ControlKey);
                    break;
                case "Alt":
                    KeyBinding.SetCtrlKeyToNote(59, Keys.Alt);
                    break;
                case "Shift":
                    KeyBinding.SetCtrlKeyToNote(59, Keys.ShiftKey);
                    break;
                default:
                    break;
            }
            List<String> tmpDataSource = new List<string> { "Ctrl", "Alt", "Shift" };

            tmpDataSource.Remove(comboBoxLow.SelectedItem.ToString());
            comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
            comboBox1.DataSource = tmpDataSource;
            comboBox1.SelectedItem = tmpSelection;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }
    }
}