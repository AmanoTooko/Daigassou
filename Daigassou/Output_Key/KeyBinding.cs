using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BondTech.HotkeyManagement.Win;
using Daigassou.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Daigassou
{
    public static class KeyBinding
    {
        public static ArrayList hotkeyArrayList;

        private static Dictionary<int, int> _keymap = new Dictionary<int, int>
        {
            {48, 73},
            {49, 56},
            {50, 79},
            {51, 57},
            {52, 80},
            {53, 219},
            {54, 48},
            {55, 221},
            {56, 189},
            {57, 220},
            {58, 187},
            {59, 222},
            {60, 81},
            {61, 50},
            {62, 87},
            {63, 51},
            {64, 69},
            {65, 82},
            {66, 53},
            {67, 84},
            {68, 54},
            {69, 89},
            {70, 55},
            {71, 85},
            {72, 90},
            {73, 83},
            {74, 88},
            {75, 68},
            {76, 67},
            {77, 86},
            {78, 71},
            {79, 66},
            {80, 72},
            {81, 78},
            {82, 74},
            {83, 77},
            {84, 191}
        };

        private static readonly Dictionary<string, Keys> _ctrKeyMap = new Dictionary<string, Keys>
        {
            {"OctaveLower", Keys.ShiftKey},
            {"OctaveHigher", Keys.ControlKey}
        };

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);

        public static char GetKeyChar(Keys k)
        {
            var nonVirtualKey = MapVirtualKey((uint) k, 2);
            var mappedChar = Convert.ToChar(nonVirtualKey);
            return mappedChar;
        }

        public static Keys GetNoteToKey(int note)
        {
            if (!Settings.Default.IsEightKeyLayout) return (Keys) _keymap[note];
            if (note == 84) return (Keys)_keymap[84];
            else return (Keys) _keymap[note % 12 + 60];

        }

        public static Keys GetNoteToCtrlKey(int note)
        {
            if (note < 60)
                return _ctrKeyMap["OctaveLower"];
            if (note > 71)
                return _ctrKeyMap["OctaveHigher"];
            return Keys.None;
        }

        public static void SetKeyToNote_22(int note, int keyValue)
        {
            _keymap[note] = keyValue;
            SaveConfig();
        }

        public static void SetKeyToNote_13(int note, int key)
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
                Settings.Default.KeyBinding13 = keyArrayList;
                Settings.Default.CtrlKeyBinding = ctrlKeyArrayList;
            }
            else
            {
                Settings.Default.KeyBinding37 = keyArrayList;
            }

            Settings.Default.HotKeyBinding = JsonConvert.SerializeObject(hotkeyArrayList);
            
            Settings.Default.Save();
        }

        public static void LoadConfig()
        {
            var settingArrayList = Settings.Default.KeyBinding37;

            if (Settings.Default.IsEightKeyLayout) settingArrayList = Settings.Default.KeyBinding13;
            var settingKeyArrayList = Settings.Default.CtrlKeyBinding;
            if (settingArrayList != null)
                for (var i = 0; i < settingArrayList.Count; i++)
                    _keymap[i + 48] = (int) settingArrayList[i];

            if (settingKeyArrayList != null)
            {
                _ctrKeyMap["OctaveLower"] = (Keys)settingKeyArrayList[0];
                _ctrKeyMap["OctaveHigher"] = (Keys)settingKeyArrayList[1];
            }


            
            var tmpArraylist = JsonConvert.DeserializeObject<ArrayList>(Settings.Default.HotKeyBinding);
            hotkeyArrayList = new ArrayList();
            foreach (JObject j in tmpArraylist)
                hotkeyArrayList.Add(new GlobalHotKey(j["Name"].ToString(), (Modifiers) j["Modifiers"].Value<int>(),
                    (Keys) j["Key"].Value<int>(), j["Enabled"].Value<bool>()));
            
        }

        public static string SaveConfigToFile()
        {
            var json = JsonConvert.SerializeObject(_keymap);
            Debug.WriteLine(json);
            return json;
        }

        public static void LoadConfigFromFile(string config)
        {
            try
            {
                var _tmp = JsonConvert.DeserializeObject<Dictionary<int, int>>(config);
                _keymap = _tmp;
            }
            catch
            {
                // ignored
            }
            finally
            {
                SaveConfig();
            }
        }
    }
}