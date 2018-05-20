using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Daigassou
{
    public class KeyController
    {
        [DllImport("User32.dll")]
        public static extern void keybd_event(Keys bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);

        private Stopwatch sw = new Stopwatch();

        public static void KeyboardPress(Keys ctrKeys, Keys viKeys)
        {
#if __DEBUG
            Console.WriteLine($"{viKeys.ToString()} has been pressed at {Environment.TickCount}");
#endif
            keybd_event(ctrKeys, (byte) MapVirtualKey((uint) ctrKeys, 0), 0, 0);
            keybd_event(viKeys, (byte) MapVirtualKey((uint) viKeys, 0), 0, 0);
            Thread.Sleep(3);
            keybd_event(ctrKeys, (byte) MapVirtualKey((uint) ctrKeys, 0), 2, 0);
            keybd_event(viKeys, (byte) MapVirtualKey((uint) viKeys, 0), 2, 0);
        }

        public static void KeyPlayBack(Queue<KeyPlayList> keyQueue, int tick, CancellationToken token)
        {
            var startTime = Environment.TickCount;
            long targetTime = startTime;
            var duration = 0;
            while (keyQueue.Any() && !token.IsCancellationRequested)
            {
                var nextKey = keyQueue.Dequeue();
                duration = tick * nextKey.Tick;
                targetTime = startTime + duration;
                while (true)
                    if (targetTime <= Environment.TickCount)
                        break;

                startTime = Environment.TickCount;
                Console.WriteLine($" i called function at {startTime} with target time is {targetTime}");
                KeyboardPress(nextKey.CtrKey,nextKey.Key);
            }
        }
    }

    public class KeyPlayList
    {
        public KeyPlayList(Keys ctrKey,Keys key, int tick)
        {
            Tick = tick;
            Key = key;
            CtrKey = ctrKey;
        }

        public int Tick;
        public Keys Key;
        public Keys CtrKey;
    }
}