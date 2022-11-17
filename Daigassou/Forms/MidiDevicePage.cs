using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace Daigassou.Forms
{
    public partial class MidiDevicePage : UIPage
    {
        public MidiDevicePage()
        {
            InitializeComponent();
        }
        public override void Init()
        {
            base.Init();
            uiLine3.ForeColor = Color.FromArgb(255, 113, 128);
            uiLine2.ForeColor = Color.FromArgb(255, 113, 128);
        }

        public override void Final()
        {
            base.Final();

        }

        private void tbMidiKey_ValueChanged(object sender, EventArgs e)
        {
            lblMidiKey.Text = $"键盘起始Key +{tbMidiKey.Value * 12}";
        }
    }
}
