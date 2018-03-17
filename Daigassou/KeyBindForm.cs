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
        private const int NUMBER_OF_KEY=37;
        private Label[] noteLabels=new Label[NUMBER_OF_KEY];
        private TextBox[] keyBoxs=new TextBox[NUMBER_OF_KEY];
        public KeyBindForm()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            string[] namePrefix = new[] { "低音", "普通", "高音" };
            for (int i = 0; i < NUMBER_OF_KEY; i++)
            {
                Label tmpLabel = new Label
                {
                    AutoSize = true,
                    Margin = new Padding(5, 15, 5, 5),
                    Size = new System.Drawing.Size(53, 20)
                };

                TextBox tmpTextBox = new TextBox
                {
                    Margin = new System.Windows.Forms.Padding(3, 15, 3, 3),
                    Size = new System.Drawing.Size(100, 26),
                    ReadOnly = true
                };
                tmpTextBox.KeyDown += textBox_KeyDown;
                noteLabels[i] = tmpLabel;
                keyBoxs[i] = tmpTextBox;
            }


            for (int i = 0; i < 12; i++)
            {
                for (int j =0; j <3; j++)
                {
                    noteLabels[i+j*12].Text = namePrefix[j] + (i+1).ToString("D2");
                    this.flowLayoutPanel1.Controls.Add(noteLabels[i+j*12]);//0 3 6 9..
                    this.flowLayoutPanel1.Controls.Add(keyBoxs[i +  j*12]);
                }
            }
            noteLabels[NUMBER_OF_KEY-1].Text ="最高01";
            flowLayoutPanel1.Controls.Add(noteLabels[NUMBER_OF_KEY-1]);
            flowLayoutPanel1.Controls.Add(keyBoxs[NUMBER_OF_KEY - 1]);
        }



        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tmpBox = (TextBox) sender;
            if (tmpBox == null) throw new ArgumentNullException(nameof(tmpBox));
            tmpBox.Text = e.KeyCode.ToString();
            KeyBinding.SetKeyToNote(Array.IndexOf(keyBoxs,tmpBox)+48,e.KeyCode);
        }

        private void KeyBindForm_Load(object sender, EventArgs e)
        {
            KeyBinding.LoadConfig();
            for (int i = 0; i < NUMBER_OF_KEY; i++)
            {

                keyBoxs[i].Text = KeyBinding.GetNoteToKey(i+48).ToString();
            }
        }

        private void KeyBindForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
