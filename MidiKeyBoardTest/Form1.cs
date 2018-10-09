using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidiKeyBoardTest
{
    public partial class 一个测试midi键盘连接的小工具 : Form
    {
        public 一个测试midi键盘连接的小工具()
        {
            InitializeComponent();
            cbMidiKeyboard.DataSource = KeyboardUtilities.GetKeyboardList();
        }

        private void btnKeyboardConnect_Click(object sender, EventArgs e)
        {
            if (cbMidiKeyboard.SelectedItem != null)
            {
                if (cbMidiKeyboard.Enabled)
                {
                    if (KeyboardUtilities.Connect(cbMidiKeyboard.SelectedIndex) == 0)
                    {
                        cbMidiKeyboard.Enabled = false;
                        btnKeyboardConnect.Text="已连接";
                        KeyboardUtilities.logs = "";
                        timer1.Enabled = true;
                    }
                }
                else
                {
                    KeyboardUtilities.Disconnect();
                    cbMidiKeyboard.Enabled = true;
                    cbMidiKeyboard.DataSource = KeyboardUtilities.GetKeyboardList();
                    btnKeyboardConnect.Text = "未连接";
                    timer1.Enabled = false;
                }
            }
            cbMidiKeyboard.DataSource = KeyboardUtilities.GetKeyboardList();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            richTextBox1.Text = KeyboardUtilities.logs;
        }
    }
}
