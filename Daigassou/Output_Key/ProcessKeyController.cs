using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Daigassou.Properties;
using Newtonsoft.Json;

namespace DaigassouDX.Controller
{
    public class ProcessKeyController
    {
        private const int WmKeydown = 0x0100;
        private const int WmKeyup = 0x0101;

        public static readonly Dictionary<int, int> _initkeymap = new Dictionary<int, int>
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
            {84, 191},
            {108,49},
            {109,52},
            {110,188},
            {111,190},
            {112,65},
            
            

        };
        public static  Dictionary<int, int> _keymap = new Dictionary<int, int>
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
        public static char GetKeyChar(Keys k)
        {
            var nonVirtualKey = MapVirtualKey((uint)k, 2);
            var mappedChar = Convert.ToChar(nonVirtualKey);
            return mappedChar;
        }
        public static void SaveKeyConfig(Dictionary<int, int> keyBinding)
        {
            _keymap = keyBinding;
            var jsonText= JsonConvert.SerializeObject(keyBinding);
            Settings.Default.KeyBinding = jsonText;
            Settings.Default.Save();
        }
        public static void LoadKeyConfig()
        {
            if (Settings.Default.KeyBinding!="")
            {
                try
                {
                    var jsonObject = (Dictionary<int, int>)JsonConvert.DeserializeObject(Settings.Default.KeyBinding, typeof(Dictionary<int, int>));
                    _keymap = jsonObject;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    _keymap = _initkeymap;
                }
                
            }
            else
            {
                _keymap = _initkeymap;
            }
        }

        public static void ResetKeyConfig()
        {
            _keymap = _initkeymap;
            SaveKeyConfig(_keymap);
        }
        private readonly object keyLock = new object();

        public Process Process;

        [DllImport("user32.dll", EntryPoint = "PostMessage", CallingConvention = CallingConvention.Winapi)]
        public static extern bool PostMessage(IntPtr hwnd, uint msg, uint wParam, uint lParam);

        [DllImport("User32.dll")]
        public static extern void keybd_event(Keys bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);

        private static ProcessKeyController controller;
        public static ProcessKeyController GetInstance()
        {
            if (controller==null)
            {
                controller = new ProcessKeyController();
            }

            return controller;
        }
        internal void ReleaseAllKey()
        {
            foreach (var item in _keymap) ReleaseKeyBoardByPitch(item.Key);
        }


        public void PressKeyBoardByPitch(int pitch)
        {
            if ((pitch >= 48 && pitch <= 84) || (pitch >= 108 && pitch <= 113 && Settings.Default.isUsingGuitarKey))
                KeyDownBoardByKey((Keys) _keymap[pitch]);

        }

        public void ReleaseKeyBoardByPitch(int pitch)
        {
            if ((pitch >= 48 && pitch <= 84) || (pitch >= 108 && pitch <= 113 && Settings.Default.isUsingGuitarKey))
                KeyUpBoardByKey((Keys) _keymap[pitch]);
        }

        public void KeyDownBoardByKey(Keys viKeys)
        {
            if (Settings.Default.isBackgroundKey)
            {
                if (Process != null && Process.MainWindowHandle != IntPtr.Zero)
                    PostMessage(Process.MainWindowHandle, WmKeydown, (uint) viKeys, 0);
            }
            else
            {
                keybd_event(viKeys, (byte) MapVirtualKey((uint) viKeys, 0U), 0, 0);
            }
        }

        public void KeyUpBoardByKey(Keys viKeys)
        {
            if (Settings.Default.isBackgroundKey)
            {
                if (Process != null && Process.MainWindowHandle != IntPtr.Zero)
                    PostMessage(Process.MainWindowHandle, WmKeyup, (uint) viKeys, 0);
            }
            else
            {
                keybd_event(viKeys, (byte) MapVirtualKey((uint) viKeys, 0U), 2, 0);
            }
        }

        /// <summary>
        ///     Press a key after [startOffset]ms and keep [duration]ms to release.
        /// </summary>
        /// <param name="viKeys">Key need to be press</param>
        /// <param name="startOffset">Start delay, ms</param>
        /// <param name="duration">duration </param>
        public void KeyPressBoardByKey(Keys viKeys, uint startOffset, uint duration)
        {
            if (Process != null && Process.MainWindowHandle != IntPtr.Zero)
            {
                var t = Task.Run(delegate
                {
                    Debug.WriteLine("start waiting"+DateTime.Now.ToFileTimeUtc());
                    Thread.Sleep((int)startOffset);
                    PostMessage(Process.MainWindowHandle, WmKeydown, (uint) viKeys, 0);
                    Debug.WriteLine("Keypress" + DateTime.Now.ToFileTimeUtc());
                    Thread.Sleep((int)duration);
                    PostMessage(Process.MainWindowHandle, WmKeyup, (uint) viKeys, 0);
                    Debug.WriteLine("Keyup" + DateTime.Now.ToFileTimeUtc());
                });
            }
        }
    }
}