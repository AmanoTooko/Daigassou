using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Daigassou
{
    
    public static class KeyBinding
    {
        private static Dictionary<int, Keys> keymap = new Dictionary<int, Keys>()
        {
            {48,Keys.D1},
            {49,Keys.D2},
            {50,Keys.D3},
            {51,Keys.D4},
            {52,Keys.D5},
            {53,Keys.D6},
            {54,Keys.D7},
            {55,Keys.D8},
            {56,Keys.D9},
            {57,Keys.D0},
            {58,Keys.OemMinus},
            {59,Keys.Oemplus},
            {60,Keys.NumPad1},
            {61,Keys.NumPad2},
            {62,Keys.NumPad3},
            {63,Keys.NumPad4},
            {64,Keys.NumPad5},
            {65,Keys.NumPad6},
            {66,Keys.NumPad7},
            {67,Keys.NumPad8},
            {68,Keys.NumPad9},
            {69,Keys.NumPad0},
            {70,Keys.Add},
            {71,Keys.Subtract},
            {72,Keys.F1},
            {73,Keys.F2},
            {74,Keys.F3},
            {75,Keys.F4},
            {76,Keys.F5},
            {77,Keys.F6},
            {78,Keys.F7},
            {79,Keys.F8},
            {80,Keys.F9},
            {81,Keys.F10},
            {82,Keys.F11},
            {83,Keys.F12},
            {84,Keys.Delete}
        };

        public static Keys GetNoteToKey(int note)
        {
            return keymap[note];
        }
        public static void SetKeyToNote(int note,Keys key)
        {
             keymap[note]=key;
            SaveConfig();
        }
        public static void SaveConfig()
        {
            ArrayList keyArrayList=new ArrayList();
            foreach (var key in keymap)
            {
                keyArrayList.Add(key.Value);
            }

            Properties.Settings.Default.KeyBinding = keyArrayList;
            Properties.Settings.Default.Save();
        }
        public static void LoadConfig()
        {
            var settingArrayList=  Properties.Settings.Default.KeyBinding;
            if (settingArrayList!=null)
            {
                for (int i = 0; i < settingArrayList.Count; i++)
                {
                    keymap[i + 48] = (Keys)settingArrayList[i];
                }
            }
        }
    }
}