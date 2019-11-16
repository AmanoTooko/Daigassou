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
        private int ClickCount;
        private readonly HotKeyManager hkm;
        private readonly KeyController kc;
        private readonly HotKeyControl[] keyBindings;
        private readonly ArrayList keyList;

        public ConfigForm(ArrayList _keyList, KeyController _kc, HotKeyManager _hkm)
        {
            keyList = _keyList;
            InitializeComponent();
            keyBindings = new[] {hotKeyControl1, hotKeyControl2, hotKeyControl3, hotKeyControl4};
            kc = _kc;
            hkm = _hkm;
            InitValue();
        }

        private void InitValue()
        {
            hkm.Enabled = false;
            minEventNum.Value = Settings.Default.MinEventMs;
            chordEventNum.Value = Settings.Default.MinChordMs;
            cbAutoChord.Checked = Settings.Default.AutoChord;
            tbNtpServer.Text = Settings.Default.NtpServer;

            foreach (GlobalHotKey hkmEnumerateGlobalHotKey in hkm.EnumerateGlobalHotKeys)
            {
                var index = keyList.IndexOf(hkmEnumerateGlobalHotKey);

                keyBindings[index].Text = ((GlobalHotKey) keyList[index]).ToString().Split(';')[1].Trim();
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
            var index = Array.IndexOf(keyBindings, s);
            ((GlobalHotKey) keyList[index]).Enabled = false;
            ((GlobalHotKey) keyList[index]).Key = s.UserKey;
            ((GlobalHotKey) keyList[index]).Modifier = s.UserModifier;
            ((GlobalHotKey) keyList[index]).Enabled = true;
            KeyBinding.SaveConfig();
        }

        private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var hotKeyControl in keyBindings)
            {
                ;
                ((GlobalHotKey) keyList[Array.IndexOf(keyBindings, hotKeyControl)]).Enabled =
                    (hotKeyControl.Text != string.Empty);
            }


            KeyBinding.SaveConfig();
            hkm.RemoveHotKey("Start");
            hkm.RemoveHotKey("Stop");
            hkm.RemoveHotKey("PitchUp");
            hkm.RemoveHotKey("PitchDown");
            foreach (GlobalHotKey k in keyList)
                if (k.Enabled)
                    hkm.AddGlobalHotKey(k);
            hkm.Enabled = true;
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

        private void HotKeyControl1_HotKeyIsReset(object sender, EventArgs e)
        {
            var s = sender as HotKeyControl;
            var index = Array.IndexOf(keyBindings, s);
            ((GlobalHotKey) keyList[index]).Enabled = false;
            KeyBinding.SaveConfig();
        }
    }
}