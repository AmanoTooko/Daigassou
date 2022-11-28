using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Daigassou.Properties;

namespace Daigassou.Utils
{
    public class CommObject
    {
        public eventCata eventId { get; set; }

        /// <summary>
        /// EventID = MIDI_FILE_NAME: string, midiFilepath
        /// EventID = TRACK_FILE_NAME: string, trackIndex | trackName
        /// EventID = MIDI_CONTROL_START_COUNTDOWN: int, startOffset
        /// EventID = MIDI_CONTROL_START_ENSEMBLE: int, startOffset
        /// EventID = MIDI_CONTROL_STOP: No Payload
        /// EventID = MIDI_CONTROL_INSTRUCODE: int , instrument code
        /// </summary>
        public object payload { get; set; }

    }

    public enum eventCata:int
    {
        MIDI_FILE_NAME=1000,     
        TRACK_FILE_NAME=1001,
        MIDI_FILE_NAME_CROSS=1010,
        MIDI_CONTROL_START_COUNTDOWN=2000,
        MIDI_CONTROL_START_ENSEMBLE=2001,
        MIDI_CONTROL_STOP =2010,
        MIDI_CONTROL_INSTRUCODE = 2020,

    }
    public class Utils
    {
        private const string OriginalTitle = @"FINAL FANTASY XIV";
        private const uint WM_SETTEXT = 0x000C;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint wmMsg, IntPtr wParam, string lParam);

        ///
        public static void SetGameTitle(IntPtr intPtr, bool customTitle = true, string name = "最终幻想XIV")
        {
            var title = customTitle ? name : "最终幻想XIV";
            SendMessage(intPtr, WM_SETTEXT, IntPtr.Zero, title);
        }

        public static List<Process> GetProcesses()
        {
            var processes_dx11 = Process.GetProcessesByName("ffxiv_dx11");
            return processes_dx11.ToList();
        }

        public static void TimeSync()
        {
            double error;
            try
            {
                
                var offset = new NtpClient(Settings.Default.NtpServer).GetOffset(out error);
                
                // TODO: error handler
                if (CommonUtilities.SetSystemDateTime.SetLocalTimeByStr(
                        DateTime.Now.AddMilliseconds(offset.TotalMilliseconds * -0.5))) ;

            }
            catch (Exception e)
            {
                

            }



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
            bool flag = false;
            SystemTime sysTime = new SystemTime();
            sysTime.wYear = Convert.ToUInt16(dt.Year);
            sysTime.wMonth = Convert.ToUInt16(dt.Month);
            sysTime.wDay = Convert.ToUInt16(dt.Day);
            sysTime.wHour = Convert.ToUInt16(dt.Hour);
            sysTime.wMinute = Convert.ToUInt16(dt.Minute);
            sysTime.wSecond = Convert.ToUInt16(dt.Second);
            sysTime.wMiliseconds = Convert.ToUInt16(dt.Millisecond);
            try
            {
                flag = SetSystemDateTime.SetLocalTime(ref sysTime);
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