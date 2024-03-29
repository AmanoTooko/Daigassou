﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using AutoUpdaterDotNET;
using Daigassou.Controller;
using Daigassou.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NHotkey;
using NHotkey.WindowsForms;
using Sunny.UI;

namespace Daigassou.Utils
{
    public class CommObject
    {
        public eventCata eventId { get; set; }

        /// <summary>
        ///     EventID = MIDI_FILE_NAME: string, midiFilepath
        ///     EventID = TRACK_FILE_NAME: string, trackIndex | trackName
        ///     EventID = MIDI_CONTROL_START_COUNTDOWN: int, startOffset
        ///     EventID = MIDI_CONTROL_START_ENSEMBLE: int, startOffset
        ///     EventID = MIDI_CONTROL_STOP: No Payload
        ///     EventID = MIDI_CONTROL_INSTRUCODE: int , instrument code
        /// </summary>
        public object payload { get; set; }
    }

    public enum eventCata
    {
        MIDI_FILE_NAME = 1000,
        TRACK_FILE_NAME = 1001,
        MIDI_FILE_NAME_CROSS = 1010,
        MIDI_CONTROL_START_COUNTDOWN = 2000,
        MIDI_CONTROL_START_ENSEMBLE = 2001,
        MIDI_CONTROL_START_TIMER = 2002,
        MIDI_CONTROL_START_KEY = 2003,
        MIDI_CONTROL_STOP = 2010,
        MIDI_CONTROL_INSTRUCODE = 2020,
        MIDI_CONTROL_PITCHUP = 2031,
        MIDI_CONTROL_PITCHDOWN = 2032
    }

    public class Utils
    {
        private const string OriginalTitle = @"FINAL FANTASY XIV";
        private const uint WM_SETTEXT = 0x000C;
        private const int WM_COPYDATA = 0x004A;
        private const uint WM_MIDICONTROL = 0x3378;

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int msg, int wParam, ref COPYDATASTRUCT IParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint wmMsg, IntPtr wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint wmMsg, IntPtr wParam, int lParam);

        ///
        public static void SetGameTitle(IntPtr intPtr, bool customTitle = true, string name = "最终幻想XIV")
        {
            var title = customTitle ? name : "最终幻想XIV";
            SendMessage(intPtr, WM_SETTEXT, IntPtr.Zero, title);
        }

        public static List<Process> GetProcesses()
        {
            var processes_dx11 = Process.GetProcessesByName("ffxiv_dx11");
            var processes_dx9 = Process.GetProcessesByName("ffxiv");
            return processes_dx11.ToList().Union(processes_dx9.ToList()).ToList();
        }

        public static void TimeSync()
        {
            double error;
            try
            {
                var offset = new NtpClient(Settings.Default.NtpServer).GetOffset(out error);

                // TODO: error handler
                if (SetSystemDateTime.SetLocalTimeByStr(
                        DateTime.Now.AddMilliseconds(offset.TotalMilliseconds * -0.5))) ;
            }
            catch (Exception e)
            {
            }
        }

        public static void SendMessageToAll(eventCata ev, string message)
        {
            foreach (var process in Process.GetProcessesByName("Daigassou"))
                if (process.MainWindowHandle != Process.GetCurrentProcess().MainWindowHandle)
                {
                    if (ev == eventCata.MIDI_FILE_NAME_CROSS)
                    {
                        var arr = Encoding.Default.GetBytes(message);//Issue: In ja-jp system, encoding will lost info

                        var len = arr.Length;

                        COPYDATASTRUCT cdata;

                        cdata.dwData = (IntPtr) 100;

                        cdata.lpData = message;

                        cdata.cData = len + 1;

                        SendMessage(process.MainWindowHandle, WM_COPYDATA, 0, ref cdata);
                    }
                    else
                    {
                        SendMessage(process.MainWindowHandle, WM_MIDICONTROL, IntPtr.Zero, (int) ev);
                    }
                }
        }

        public static void CheckForUpdate(AutoUpdater.ParseUpdateInfoHandler AutoUpdaterOnParseUpdateInfoEvent)
        {
            AutoUpdater.ParseUpdateInfoEvent += AutoUpdaterOnParseUpdateInfoEvent;
            AutoUpdater.ShowSkipButton = false;
            AutoUpdater.LetUserSelectRemindLater = false;
            AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Hours;
            AutoUpdater.RemindLaterAt = 12;


#if DEBUG
            AutoUpdater.Start("https://up.xiv.pub/version_test.json");
#else
            AutoUpdater.Start("https://up.xiv.pub/version.json");
#endif
        }


       

        public struct COPYDATASTRUCT

        {
            public IntPtr dwData;

            public int cData;

            [MarshalAs(UnmanagedType.LPStr)] public string lpData;
        }
    }

    public class HotkeyWrapper
    {
        public Keys hk;
        public bool isOccupied;

        public string name = "";

        public HotkeyWrapper(string _name, Keys _mk)
        {
            name = _name;
            hk = _mk;
        }
    }

    public class HotkeyUtils
    {
        //public HotKeyManager hkm;
        private static HotkeyUtils hku;

        public EventHandler<HotkeyEventArgs> HotKeyHandler;

        public HotkeyUtils()
        {
           
            hotkeysArrayList = new ArrayList();
        }

        public ArrayList hotkeysArrayList { get; set; }

        public static HotkeyUtils GetInstance(Form f = null)
        {
            if (hku == null) hku = new HotkeyUtils();

            return hku;
        }

        public void RemoveHotKey(string name)
        {
            HotkeyManager.Current.Remove(name);
            hotkeysArrayList.Remove(hotkeysArrayList.ToList<HotkeyWrapper>().Find(x => x.name == name));
        }

        public bool UpdateHotkey(HotkeyWrapper hwWrapper)
        {
            HotkeyManager.Current.IsEnabled = false;
            var currentHotkey = hotkeysArrayList.ToList<HotkeyWrapper>().Find(x => x.name == hwWrapper.name);
            if (currentHotkey == null)
                hotkeysArrayList.Add(hwWrapper);
            else
                hotkeysArrayList[hotkeysArrayList.IndexOf(currentHotkey)] = hwWrapper;

            var ret = true;
            try
            {
                HotkeyManager.Current.AddOrReplace(hwWrapper.name, hwWrapper.hk, HotkeyUtils_HotKeyPressed);
            }
            catch (Exception e)
            {
                UIMessageTip.ShowError("热键已注册");
                ret = false;
                hwWrapper.isOccupied = true;
            }

            HotkeyManager.Current.IsEnabled = true;
            return ret;
        }

        public void LoadConfig()
        {
            hotkeysArrayList.Clear();
            if (Settings.Default.HotKeyBinding != "")
            {
                var tmpArraylist = JsonConvert.DeserializeObject<ArrayList>(Settings.Default.HotKeyBinding);

                foreach (JObject j in tmpArraylist)
                    hotkeysArrayList.Add(
                        new HotkeyWrapper(j["name"].Value<string>(), (Keys) j["hk"].Value<int>()));
            }
        }

        public void ResetConfig()
        {
            var registFlag = true;
            HotkeyManager.Current.IsEnabled = false;
            hotkeysArrayList.Clear();
            hotkeysArrayList.Add(new HotkeyWrapper("Start", Keys.Control | Keys.F10));
            hotkeysArrayList.Add(new HotkeyWrapper("Stop", Keys.Control | Keys.F11));
            hotkeysArrayList.Add(new HotkeyWrapper("PitchUp", Keys.Control | Keys.Alt | Keys.Up));
            hotkeysArrayList.Add(new HotkeyWrapper("PitchDown", Keys.Control | Keys.Alt | Keys.Down));

            foreach (HotkeyWrapper hk in hotkeysArrayList)
                try
                {
                    HotkeyManager.Current.AddOrReplace(hk.name, hk.hk, HotkeyUtils_HotKeyPressed);
                }
                catch (Exception e)
                {
                    hk.isOccupied = true;
                    registFlag = false;
                }

            if (!registFlag) UIMessageTip.ShowError("部分快捷键注册失败");

            HotkeyManager.Current.IsEnabled = true;
        }

        public void SaveConfig()
        {
            Settings.Default.HotKeyBinding = JsonConvert.SerializeObject(hotkeysArrayList);
            Settings.Default.Save();
        }

        public void InitHotKey()
        {
            HotkeyManager.Current.IsEnabled = false;


            if (Settings.Default.HotKeyBinding != "")
            {
                LoadConfig();
            }
            else
            {
                ResetConfig();
                SaveConfig();
            }


            var registFlag = true;
            foreach (HotkeyWrapper hk in hotkeysArrayList)
                try
                {
                    HotkeyManager.Current.AddOrReplace(hk.name, hk.hk, HotkeyUtils_HotKeyPressed);
                }
                catch (Exception e)
                {
                    hk.isOccupied = true;
                    registFlag = false;
                }

            if (!registFlag) UIMessageTip.ShowError("部分快捷键注册失败");


            HotkeyManager.Current.IsEnabled = true;
        }


        private void HotkeyUtils_HotKeyPressed(object sender, HotkeyEventArgs e)
        {
            HotKeyHandler.Invoke(sender, e);
        }
    }

    public struct SystemTime
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMiliseconds;
    }

    public class SetSystemDateTime
    {
        [DllImport("Kernel32.dll")]
        public static extern bool SetLocalTime(ref SystemTime sysTime);

        public static bool SetLocalTimeByStr(DateTime dt)
        {
            var flag = false;
            var sysTime = new SystemTime();
            sysTime.wYear = Convert.ToUInt16(dt.Year);
            sysTime.wMonth = Convert.ToUInt16(dt.Month);
            sysTime.wDay = Convert.ToUInt16(dt.Day);
            sysTime.wHour = Convert.ToUInt16(dt.Hour);
            sysTime.wMinute = Convert.ToUInt16(dt.Minute);
            sysTime.wSecond = Convert.ToUInt16(dt.Second);
            sysTime.wMiliseconds = Convert.ToUInt16(dt.Millisecond);
            try
            {
                flag = SetLocalTime(ref sysTime);
            }
            catch (Exception e)
            {
            }

            return flag;
        }
    }

    public class KeyPlayList
    {
        public enum NoteEvent
        {
            NoteOff,
            NoteOn
        }

        public NoteEvent Ev;
        public int Pitch;
        public double TimeMs;

        public KeyPlayList(NoteEvent ev, int pitch, double timeMs)
        {
            TimeMs = timeMs;
            Ev = ev;
            Pitch = pitch;
        }
    }
}