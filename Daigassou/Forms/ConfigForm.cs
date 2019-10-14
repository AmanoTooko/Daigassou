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
using BondTech.HotkeyManagement.Win;
using Daigassou.Properties;

namespace Daigassou
{
    public partial class ConfigForm : Form
    {
        private KeyController kc;
        private BondTech.HotkeyManagement.Win.GlobalHotKey a;
        private HotKeyManager hkm;
        public ConfigForm(KeyController _kc)
        {
            kc = _kc;
            InitializeComponent();
            InitValue();

            hkm = new HotKeyManager(this);
            a = new BondTech.HotkeyManagement.Win.GlobalHotKey("test", Modifiers.Alt | Modifiers.Control, Keys.F5);
            //a.HotKeyPressed += A_HotKeyPressed;
            a.Enabled = true;
            hkm.AddGlobalHotKey(a);
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
            kc.InitBackGroundKey();
        }

        private void TbNtpServer_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.NtpServer = tbNtpServer.Text;
            Settings.Default.Save();
        }

        private void HotKeyControl1_HotKeyIsSet(object sender, BondTech.HotkeyManagement.Win.HotKeyIsSetEventArgs e)
        {
            
           
        }

        private void A_HotKeyPressed(object sender, GlobalHotKeyEventArgs e)
        {
            test();
        }

        private void test()
        {
            Console.WriteLine("mc");
        }
    }      
        
}
