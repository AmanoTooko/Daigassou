using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DaigassouDX.Controller;
using Sunny.UI;

namespace Daigassou.Forms
{
    public partial class KeyBindingForm : UIForm
    {
        private UITextBox[] keyBoxes;
        Dictionary<int, int> keyConfig ;
        public KeyBindingForm()
        {
            InitializeComponent();
            UIStyles.InitColorful(Color.FromArgb(255, 141, 155), Color.White);
            keyBoxes = new UITextBox[42]
            {
                uiTextBox1,
                uiTextBox2,
                uiTextBox3,
                uiTextBox4,
                uiTextBox5,
                uiTextBox6,
                uiTextBox7,
                uiTextBox8,
                uiTextBox9,
                uiTextBox10,
                uiTextBox11,
                uiTextBox12,
                uiTextBox13,
                uiTextBox14,
                uiTextBox15,
                uiTextBox16,
                uiTextBox17,
                uiTextBox18,
                uiTextBox19,
                uiTextBox20,
                uiTextBox21,
                uiTextBox22,
                uiTextBox23,
                uiTextBox24,
                uiTextBox25,
                uiTextBox26,
                uiTextBox27,
                uiTextBox28,
                uiTextBox29,
                uiTextBox30,
                uiTextBox31,
                uiTextBox32,
                uiTextBox33,
                uiTextBox34,
                uiTextBox35,
                uiTextBox36,
                uiTextBox37,
                uiTextBox38,
                uiTextBox39,
                uiTextBox40,
                uiTextBox41,
                uiTextBox42,
            };
            loadKeyconfig();
            
        }
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            var tmpBox = (UITextBox)sender;
            var index = Array.IndexOf(keyBoxes, tmpBox);
            if (tmpBox == null) throw new ArgumentNullException(nameof(tmpBox));
            tmpBox.Text = ProcessKeyController.GetKeyChar(e.KeyCode).ToString();

            if (index<37)
            {
                keyConfig[index+48] = (int)e.KeyCode;
            }
            else
            {
                keyConfig[index + 108] = (int)e.KeyCode;
            }
            


        }
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void loadKeyconfig()
        {
            ProcessKeyController.LoadKeyConfig();
            foreach (var keypair in ProcessKeyController._keymap)
            {
                if (keypair.Key<85)
                {
                    keyBoxes[keypair.Key - 48].Text = ProcessKeyController.GetKeyChar((Keys) keypair.Value).ToString();
                }
                else
                {
                    keyBoxes[keypair.Key - 108].Text = ProcessKeyController.GetKeyChar((Keys)keypair.Value).ToString();
                }
                
            }

            keyConfig = ProcessKeyController._keymap;

        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {

            ProcessKeyController.SaveKeyConfig(keyConfig);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ProcessKeyController.ResetKeyConfig();
            loadKeyconfig();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void KeyBindingForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
