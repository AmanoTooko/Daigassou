using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BondTech.HotkeyManagement.Win;

namespace Daigassou.Utils
{

    class HotKey
    {
        private static object locker;
        private static HotKey _hotKey;
        private HotKeyManager hkm;
        private Dictionary<string, BondTech.HotkeyManagement.Win.GlobalHotKey> hotKeysDictionary;

        public HotKey(object form)
        {
            //hotKeysDictionary=new Dictionary<string, BondTech.HotkeyManagement.Win.GlobalHotKey>();
            //hotKeysDictionary.Add("");

            //hkm = new BondTech.HotkeyManagement.Win.HotKeyManager((IWin32Window)form);
            //gbk = new BondTech.HotkeyManagement.Win.GlobalHotKey("test", Modifiers.Alt, Keys.F8);
            //gbk.HotKeyPressed += Gbk_HotKeyPressed;

            //hkm.AddGlobalHotKey(gbk);
        }
        public static HotKey GetInstance(object form)
        {
            lock (locker)
            {

                if (_hotKey == null)
                {
                    _hotKey = new HotKey(form);
                }
            }

            return _hotKey;
        }
    }
}
