using BondTech.HotkeyManagement.Win;
using Daigassou.Forms;
using Daigassou.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

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
            keyBindings = new[] {hotKeyControl1, hotKeyControl2, hotKeyControl3, hotKeyControl4,hotKeyControl5};
            kc = _kc;
            hkm = _hkm;
            InitValue();
            tabControl1.TabPages.Remove(tbLyric);
        }

        private void InitValue()
        {
            hkm.Enabled = false;
            minEventNum.Value = Settings.Default.MinEventMs;
            chordEventNum.Value = Settings.Default.MinChordMs;
            cbAutoChord.Checked = Settings.Default.AutoChord;
            tbNtpServer.Text = Settings.Default.NtpServer;
            cbUsingAnalysis.Checked = Settings.Default.isUsingAnalysis;
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
            hkm.RemoveHotKey("Pause");
            hkm.RemoveHotKey("PitchUp");
            hkm.RemoveHotKey("PitchDown");
            foreach (GlobalHotKey k in keyList)
                if (k.Enabled)
                    hkm.AddGlobalHotKey(k);
            hkm.Enabled = true;
        }


        private void Panel2_Click(object sender, EventArgs e)
        {
            PidSelect pidSelect = new PidSelect();
            pidSelect.GetPid += (PidSelect.PidSelector)(x =>
            {
                this.kc.isBackGroundKey = true;
                this.kc.InitBackGroundKey(Process.GetProcessById(x).MainWindowHandle);
            });
            pidSelect.ShowDialog();
        }

        private void HotKeyControl1_HotKeyIsReset(object sender, EventArgs e)
        {
            var s = sender as HotKeyControl;
            var index = Array.IndexOf(keyBindings, s);
            ((GlobalHotKey) keyList[index]).Enabled = false;
            KeyBinding.SaveConfig();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbLrcEnable_CheckedChanged(object sender, EventArgs e)
        {
            Daigassou.Utils.lyricPoster.IsLrcEnable = cbLrcEnable.Checked;
        }

        private void nudPort_ValueChanged(object sender, EventArgs e)
        {
            Daigassou.Utils.lyricPoster.port = (uint)nudPort.Value;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbSuffix_SelectedIndexChanged(object sender, EventArgs e)
        {
            Daigassou.Utils.lyricPoster.suffix = cbSuffix.SelectedItem.ToString();
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            
        }

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            if (!tabControl1.TabPages.Contains(tbLyric)&&e.Button==MouseButtons.Middle)
                tabControl1.TabPages.Add(tbLyric);
        }

        private void cbPcap_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.isUsingWinPCap = cbPcap.Checked;
            Settings.Default.Save();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.isUsingAnalysis = cbUsingAnalysis.Checked;
            Settings.Default.Save();
        }

        private void cbUsingAnalysis_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.isUsingAnalysis = cbUsingAnalysis.Checked;
            Settings.Default.Save();
        }
    }
}