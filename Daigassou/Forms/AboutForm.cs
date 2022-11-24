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
using Sunny.UI;

namespace Daigassou
{
    public partial class AboutForm : Form
    {
        
        public AboutForm()
        {
            
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            lblVersion.Text = "Ver " + Assembly.GetExecutingAssembly().GetName().Version;
        }


    }
}
