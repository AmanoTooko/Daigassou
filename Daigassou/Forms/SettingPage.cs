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
    public partial class SettingPage : UIPage
    {
        public SettingPage()
        {
            InitializeComponent();
        }

        private void SettingPage_ReceiveParams(object sender, UIPageParamsArgs e)
        {
            uiTextBox1.Text = e.Value.ToString();
        }
    }
}
