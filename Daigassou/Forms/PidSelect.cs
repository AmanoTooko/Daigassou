using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Daigassou.Input_Midi;

namespace Daigassou.Forms
{
    public partial class PidSelect : Form
    {
        private KeyController _kc;
        public PidSelect(KeyController kc)
        {
            _kc = kc;
            InitializeComponent();

            comboBox1.DataSource = BackgroundKey.GetPids().ToList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _kc.isBackGroundKey = true;
            _kc.InitBackGroundKey((IntPtr)Convert.ToInt32(comboBox1.SelectedItem.ToString()));
            MessageBox.Show("后台演奏已开启");
            Close();
        }
    }
}
