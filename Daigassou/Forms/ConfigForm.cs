using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using BondTech.HotkeyManagement.Win;
using Daigassou.Input_Midi;
using Daigassou.Properties;

namespace Daigassou
{
    public partial class ConfigForm : Form
    {
        private HotKeyManager hkm;
        private KeyController kc;
        private HotKeyControl[] keyBindings;
        private ArrayList keyList;
        private int ClickCount = 0;
        public ConfigForm(ArrayList _keyList)
        {
            keyList = _keyList;
            InitializeComponent();
            keyBindings = new[] {hotKeyControl1, hotKeyControl2, hotKeyControl3, hotKeyControl4};
            InitValue();
        }

        private void InitValue()
        {
            minEventNum.Value = Settings.Default.MinEventMs;
            chordEventNum.Value = Settings.Default.MinChordMs;
            cbAutoChord.Checked = Settings.Default.AutoChord;
            tbNtpServer.Text = Settings.Default.NtpServer;
            for (int i = 0; i < keyBindings.Length; i++)
            {
                keyBindings[i].Text = ((GlobalHotKey) keyList[i]).ToString().Split(';')[1].Trim();
                
            }
        }

        private void MinEventNum_NumChanged(object sender, EventArgs e)
        {
            Settings.Default.MinEventMs = (uint) minEventNum.Value;
            Settings.Default.Save();
        }

        private void ChordEventNum_NumChanged(object sender, EventArgs e)
        {
            Settings.Default.MinChordMs = (uint) chordEventNum.Value;
            Settings.Default.Save();
        }

        private void CbAutoChord_CheckedChangeEvent(object sender, EventArgs e)
        {
            Settings.Default.AutoChord = cbAutoChord.Checked;
            Settings.Default.Save();
        }


        private void TbNtpServer_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.NtpServer = tbNtpServer.Text;
            Settings.Default.Save();
        }

        private void HotKeyControl1_HotKeyIsSet(object sender, HotKeyIsSetEventArgs e)
        {
            var s = sender as HotKeyControl;
            var index = Array.IndexOf(keyBindings,s);
            ((GlobalHotKey) keyList[index]).Key = s.UserKey;
            ((GlobalHotKey)keyList[index]).Modifier = s.UserModifier;
            KeyBinding.SaveConfig();
        }

        private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (GlobalHotKey key in keyList)
            {
                key.Enabled = true;
            }
            KeyBinding.SaveConfig();

        }



        private void Panel2_Click(object sender, EventArgs e)
        {
            if (ClickCount++ > 5)
            {
                kc.isBackGroundKey = true;
                kc.InitBackGroundKey(BackgroundKey.GetPids().FirstOrDefault());
                MessageBox.Show("后台演奏已开启");
            }
        }
    }
}