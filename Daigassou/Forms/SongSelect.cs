using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Daigassou.Forms
{
    public partial class SongSelect : Form
    {
        public SongSelect()
        {
            InitializeComponent();
        }
        public delegate void IdSelector(string id);
        public IdSelector Getid;
        private void button1_Click(object sender, EventArgs e)
        {
            
            Getid(textBox1.Text);
            this.Close();
        }
    }
}
