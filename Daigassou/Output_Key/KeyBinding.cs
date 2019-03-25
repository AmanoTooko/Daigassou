using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Daigassou.Properties;

namespace Daigassou
{
    public static class KeyBinding
    {
        private static readonly Dictionary<int, Keys> _keymap = new Dictionary<int, Keys>
        {
            {48, Keys.Q},
            {49, Keys.D2},
            {50, Keys.W},
            {51, Keys.D3},
            {52, Keys.E},
            {53, Keys.R},
            {54, Keys.D5},
            {55, Keys.T},
            {56, Keys.D6},
            {57, Keys.Y},
            {58, Keys.D7},
            {59, Keys.U},
            {60, Keys.Q},
            {61, Keys.D2},
            {62, Keys.W},
            {63, Keys.D3},
            {64, Keys.E},
            {65, Keys.R},
            {66, Keys.D5},
            {67, Keys.T},
            {68, Keys.D6},
            {69, Keys.Y},
            {70, Keys.D7},
            {71, Keys.U},
            {72, Keys.Q},
            {73, Keys.D2},
            {74, Keys.W},
            {75, Keys.D3},
            {76, Keys.E},
            {77, Keys.R},
            {78, Keys.D5},
            {79, Keys.T},
            {80, Keys.D6},
            {81, Keys.Y},
            {82, Keys.D7},
            {83, Keys.U},
            {84, Keys.I}
        };

        private static readonly Dictionary<string, Keys> _ctrKeyMap = new Dictionary<string, Keys>
        {
            {"OctaveLower", Keys.ShiftKey},
            {"OctaveHigher", Keys.ControlKey}
        };

        public static Keys GetNoteToKey(int note)
        {
            return _keymap[note];
        }

        public static Keys GetNoteToCtrlKey(int note)
        {
            if (note < 60)
                return _ctrKeyMap["OctaveLower"];
            if (note > 71)
                return _ctrKeyMap["OctaveHigher"];
            return Keys.None;
        }

        public static void SetKeyToNote_22(int note, Keys key)
        {
            _keymap[note] = key;
            SaveConfig();
        }

        public static void SetKeyToNote_8(int note, Keys key)
        {
            var offset = note % 12;
            if (note == 72)
            {
                _keymap[84] = key;
            }
            else
            {
                _keymap[48 + offset] = key;
                _keymap[60 + offset] = key;
                _keymap[72 + offset] = key;
            }

            SaveConfig();
        }

        public static void SetCtrlKeyToNote(int note, Keys key)
        {
            if (note < 60)
                _ctrKeyMap["OctaveLower"] = key;
            else if (note > 71) _ctrKeyMap["OctaveHigher"] = key;
            SaveConfig();
        }

        public static void SaveConfig()
        {
            var keyArrayList = new ArrayList();
            foreach (var key in _keymap) keyArrayList.Add(key.Value);

            var ctrlKeyArrayList = new ArrayList();
            foreach (var key in _ctrKeyMap) ctrlKeyArrayList.Add(key.Value);

            if (Settings.Default.IsEightKeyLayout)
            {
                Settings.Default.KeyBinding8 = keyArrayList;
                Settings.Default.CtrlKeyBinding = ctrlKeyArrayList;
            }
            else
            {
                Settings.Default.KeyBinding22 = keyArrayList;
            }

            Settings.Default.Save();
        }

        public static void LoadConfig()
        {
            var settingArrayList = Settings.Default.KeyBinding22;

            if (Settings.Default.IsEightKeyLayout) settingArrayList = Settings.Default.KeyBinding8;

            //ArrayList clear = new ArrayList();
            //Properties.Settings.Default.KeyBinding8 = clear;
            //Properties.Settings.Default.Save();
            var settingKeyArrayList = Settings.Default.CtrlKeyBinding;
            if (settingArrayList != null)
                for (var i = 0; i < settingArrayList.Count; i++)
                    _keymap[i + 48] = (Keys) settingArrayList[i];

            if (settingKeyArrayList == null) return;
            _ctrKeyMap["OctaveLower"] = (Keys) settingKeyArrayList[0];
            _ctrKeyMap["OctaveHigher"] = (Keys) settingKeyArrayList[1];
        }
    }
}