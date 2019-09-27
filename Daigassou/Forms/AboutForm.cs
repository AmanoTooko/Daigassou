using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Daigassou.Input_Midi;

namespace Daigassou
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private int ClickCount = 0;
        private void AboutForm_Load(object sender, EventArgs e)
        {
            lblVersion.Text = "Ver " + Assembly.GetExecutingAssembly().GetName().Version;
        }

        private void LblVersion_Click(object sender, EventArgs e)
        {
            if (ClickCount++>5)
            {
                KeyController.isBackGroundKey = true;
                BackgroundKey.Init();
                MessageBox.Show("启用后台播放");
            }
        }
    }
}
