using Daigassou.Input_Midi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Daigassou.Properties;

namespace Daigassou
{
    public partial class ConfigForm : Form
    {
        private KeyController kc;
        public ConfigForm(KeyController _kc)
        {
            kc = _kc;
            InitializeComponent();
            InitValue();
        }

        private void InitValue()
        {
            minEventNum.Value = Settings.Default.MinEventMs;
            chordEventNum.Value = Settings.Default.MinChordMs;
            cbAutoChord.Checked = Settings.Default.AutoChord;
            tbNtpServer.Text = Settings.Default.NtpServer;
           
        }

        private void MinEventNum_NumChanged(object sender, EventArgs e)
        {
            Settings.Default.MinEventMs = (uint)minEventNum.Value;
            Settings.Default.Save();
        }

        private void ChordEventNum_NumChanged(object sender, EventArgs e)
        {
            Settings.Default.MinChordMs = (uint)chordEventNum.Value;
            Settings.Default.Save();
        }

        private void CbAutoChord_CheckedChangeEvent(object sender, EventArgs e)
        {
            Settings.Default.AutoChord = cbAutoChord.Checked;
            Settings.Default.Save();
        }

        private void CbBackgroundKey_CheckedChangeEvent(object sender, EventArgs e)
        {
            kc.isBackGroundKey = true;
            
        }

        private void TbNtpServer_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.NtpServer = tbNtpServer.Text;
            Settings.Default.Save();
        }
    }
}
