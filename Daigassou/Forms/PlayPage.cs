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
    public partial class PlayPage : UIPage
    {
        public PlayPage()
        {
            InitializeComponent();
            
        }
        public override void Init()
        {
            base.Init();
            uiLine1.ForeColor = Color.FromArgb(255, 113, 128);
            uiLine2.ForeColor = Color.FromArgb(255, 113, 128);
        }

        public override void Final()
        {
            base.Final();

        }

        private void PlayForm_ReceiveParams(object sender, UIPageParamsArgs e)
        {
            Console.Write(e.Value);
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            
            this.SendParamToPage((int) PageID.SettingPage,"测试+1");
        }

        private void uiTrackBar1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
