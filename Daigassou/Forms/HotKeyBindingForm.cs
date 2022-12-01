using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using BondTech.HotkeyManagement.Win;
using Daigassou.Utils;
using DaigassouDX.Controller;
using NHotkey.WindowsForms;
using Sunny.UI;

namespace Daigassou.Forms
{
    public partial class HotKeyBindingForm : UIForm
    {
        

        private Dictionary<string,HotKeyControl> keyBindings;

        public HotKeyBindingForm()
        {
            InitializeComponent();
            UIStyles.InitColorful(Color.FromArgb(255, 141, 155), Color.White);

            keyBindings = new Dictionary<string, HotKeyControl>()
            {
                {"Start",hotKeyControl1},
                {"Stop",hotKeyControl2},
                {"PitchUp",hotKeyControl3},
                {"PitchDown",hotKeyControl4},


            };
            loadKeyconfig();
            
        }



        private void loadKeyconfig()
        {
            foreach (HotkeyWrapper hkw in HotkeyUtils.GetInstance().hotkeysArrayList)
            {
                if (keyBindings.ContainsKey(hkw.name))
                {
                    keyBindings[hkw.name].Text = 
                                                 (hkw.hk.HasFlag(Keys.Control) ? "Ctrl+ " : string.Empty) +
                                                 (hkw.hk.HasFlag(Keys.Shift) ? "Shift+ " : string.Empty) +
                                                 (hkw.hk.HasFlag(Keys.Alt) ? "Alt+ " : string.Empty) + hkw.hk.ToString().Split(',').First();

                        
                    if (hkw.isOccupied) keyBindings[hkw.name].Text += "  !已占用！";
                }
            }

            HotkeyManager.Current.IsEnabled = false;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {

            Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            HotkeyUtils.GetInstance(null).ResetConfig();
            loadKeyconfig();
        }


        private void KeyBindingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            HotkeyManager.Current.IsEnabled = true;
        }

        private void hotKeyControl4_HotKeyIsSet(object sender, HotKeyIsSetEventArgs e)
        {
            var s = sender as HotKeyControl;
            var name = keyBindings.Where(x => x.Value == s).First().Key;
            if (name!=null)
            {
                var finalKey = s.UserKey;
                if (s.UserModifier.HasFlag(Modifiers.Control))
                {
                    finalKey |= Keys.Control;
                }
                if (s.UserModifier.HasFlag(Modifiers.Alt))
                {
                    finalKey |= Keys.Alt;
                }
                if (s.UserModifier.HasFlag(Modifiers.Shift))
                {
                    finalKey |= Keys.Shift;
                }
                if (s.UserModifier.HasFlag(Modifiers.Win))
                {
                    finalKey |= Keys.LWin;
                }

                HotkeyUtils.GetInstance().UpdateHotkey(new HotkeyWrapper(name, finalKey));
            }
       
            HotkeyUtils.GetInstance(null).SaveConfig();
            loadKeyconfig();

        }

        private void hotKeyControl4_HotKeyIsReset(object sender, EventArgs e)
        {
            var s = sender as HotKeyControl;
            var name = keyBindings.Where(x => x.Value == s).First().Key;
            HotkeyUtils.GetInstance().RemoveHotKey(name);
            HotkeyUtils.GetInstance().SaveConfig();


        }

        private void HotKeyBindingForm_ReceiveParams(object sender, UIPageParamsArgs e)
        {

        }
    }
}