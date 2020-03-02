using Daigassou.Input_Midi;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Daigassou.Forms
{
    public partial class PidSelect : Form
    {
        private BackgroundKey kc = new BackgroundKey();
        public delegate void PidSelector(int id);
        public PidSelect.PidSelector GetPid;
        public PidSelect()
        {
            
            InitializeComponent();

            comboBox1.DataSource = BackgroundKey.GetPids().ToList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.GetPid(Convert.ToInt32(this.comboBox1.SelectedItem?.ToString()));
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.kc.Init(Process.GetProcessById(Convert.ToInt32(this.comboBox1.SelectedItem?.ToString())).MainWindowHandle);
            System.Threading.Timer timer1 = new System.Threading.Timer((TimerCallback)(x => this.kc.BackgroundKeyPress(Keys.Space)), new object(), 100, 0);
            System.Threading.Timer timer2 = new System.Threading.Timer((TimerCallback)(x => this.kc.BackgroundKeyRelease(Keys.Space)), new object(), 200, 0);
        }
    }
}
