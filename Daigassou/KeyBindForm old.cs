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
    public partial class KeyBindFormOld : Form
    {
        private const int NUMBER_OF_KEY=37;
        private Label[] noteLabels=new Label[NUMBER_OF_KEY];
        private TextBox[] keyBoxs=new TextBox[NUMBER_OF_KEY];
        public KeyBindFormOld()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
          
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


        }



        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tmpBox = (TextBox) sender;
            if (tmpBox == null) throw new ArgumentNullException(nameof(tmpBox));
            tmpBox.Text = e.KeyCode.ToString();
            KeyBinding.SetKeyToNote_8(Array.IndexOf(keyBoxs,tmpBox)+48,e.KeyCode);
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



        private void btnApply_Click(object sender, EventArgs e)
        {

        }
    }
}
