using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Melanchall.DryWetMidi.Smf;

namespace Daigassou
{
    public class KeyController
    {
        [DllImport("User32.dll")]
        public static extern void keybd_event(Keys bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);

        private static Keys _lastCtrlKey;

        public static void KeyboardPress(int pitch)
        {
            if (Properties.Settings.Default.IsEightKeyLayout==true)
            {
                KeyboardPress(KeyBinding.GetNoteToCtrlKey(pitch),KeyBinding.GetNoteToKey(pitch));
                return;
            }
            else
            {
                KeyboardPress(KeyBinding.GetNoteToKey(pitch));
            }

        }

        public static void KeyboardPress(Keys ctrKeys, Keys viKeys)
        {
#if DEBUG
            Console.WriteLine($@"{ctrKeys.ToString() + viKeys.ToString()} has been pressed at {Environment.TickCount}");
#endif
            keybd_event(_lastCtrlKey, (byte)MapVirtualKey((uint)_lastCtrlKey, 0), 2, 0);
            Thread.Sleep(1);
            keybd_event(ctrKeys, (byte) MapVirtualKey((uint) ctrKeys, 0), 0, 0);
            keybd_event(viKeys, (byte) MapVirtualKey((uint) viKeys, 0), 0, 0);
            _lastCtrlKey = ctrKeys;
            Thread.Sleep(10);

        }

        private static void KeyboardPress(Keys viKeys)
        {

            keybd_event(viKeys, (byte) MapVirtualKey((uint) viKeys, 0), 0, 0);
            Thread.Sleep(10);

        }


        public static void KeyboardRelease(int pitch)
        {
            if (Properties.Settings.Default.IsEightKeyLayout == true)
            {
                KeyboardRelease(KeyBinding.GetNoteToCtrlKey(pitch), KeyBinding.GetNoteToKey(pitch));
                return;
            }
            else
            {
                KeyboardRelease(KeyBinding.GetNoteToKey(pitch));
            }
        }
        public static void KeyboardRelease(Keys ctrKeys, Keys viKeys)
        {

            keybd_event(ctrKeys, (byte)MapVirtualKey((uint)ctrKeys, 0), 2, 0);
            keybd_event(viKeys, (byte)MapVirtualKey((uint)viKeys, 0), 2, 0);

        }


        public static void KeyboardRelease(Keys viKeys)
        {
            keybd_event(viKeys, (byte)MapVirtualKey((uint)viKeys, 0), 2, 0);

        }

        public static void KeyPlayBack(Queue<KeyPlayList> keyQueue, int tick, CancellationToken token)
        {
            var startTime = Environment.TickCount;
            while (keyQueue.Any() && !token.IsCancellationRequested)
            {
                var nextKey = keyQueue.Dequeue();
                var duration = tick * nextKey.Tick;
                long targetTime = startTime + duration;
                while (true)
                    if (targetTime <= Environment.TickCount)
                        break;

                startTime = Environment.TickCount;
                
                if (nextKey.Ev==KeyPlayList.NoteEvent.NoteOn)
                {
                    KeyboardPress(nextKey.Pitch);
                }
                else
                {
                    KeyboardRelease(nextKey.Pitch);
                }

#if _log
                Console.WriteLine($@" i called function at {startTime} with target time is {targetTime}");
#endif
            }
        }
    }
     
    public class KeyPlayList
    {
        public KeyPlayList(NoteEvent ev, int pitch, long tick)
        {
            Tick = tick;
            Ev = ev;
            Pitch = pitch;
        }


        public enum  NoteEvent:int
        {
            NoteOff,
            NoteOn
        }

        public NoteEvent Ev;
        public long Tick;
        public int Pitch;
    }
}