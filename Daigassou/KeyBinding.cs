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
            {48,Keys.A },
            {49,Keys.A },
            {50,Keys.A },
            {51,Keys.A },
            {52,Keys.A },
            {53,Keys.A },
            {54,Keys.A },
            {55,Keys.A },
            {56,Keys.A },
            {57,Keys.A },
            {58,Keys.A },
            {59,Keys.A },//lower
            {60,Keys.D1 },
            {61,Keys.A },
            {62,Keys.D3 },
            {63,Keys.A },
            {64,Keys.D5 },
            {65,Keys.D6 },
            {66,Keys.A },
            {67,Keys.D8},
            {68,Keys.A },
            {69,Keys.D0 },
            {70,Keys.A },
            {71,Keys.A},//normal
            {72,Keys.A },
            {73,Keys.A },
            {74,Keys.A },
            {75,Keys.A },
            {76,Keys.A },
            {77,Keys.A },
            {78,Keys.A },
            {79,Keys.A },
            {80,Keys.A },
            {81,Keys.A },
            {82,Keys.A },
            {83,Keys.A },//higher
            {84,Keys.A }
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
            if (settingArrayList.Count!=0)
            {
                for (int i = 0; i < settingArrayList.Count; i++)
                {
                    keymap[i + 48] = (Keys)settingArrayList[i];
                }
            }
        }
    }
}