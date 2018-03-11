using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Daigassou
{
    public class KeyController
    {

        [DllImport("User32.dll")]
        public static extern void keybd_event(Keys bVk, Byte bScan, Int32 dwFlags, Int32 dwExtraInfo);
        [DllImport("user32.dll")]
        static extern uint MapVirtualKey(uint uCode, uint uMapType);

        Stopwatch sw = new Stopwatch();
        
        public KeyController()
        {
            
        }

        public static void KeyboardPress(Keys viKeys)
        {
                Console.WriteLine($"{viKeys.ToString()} has been pressed at {Environment.TickCount}");
                keybd_event(viKeys, (byte)MapVirtualKey((uint)viKeys, 0), 0, 0);
                Thread.Sleep(3);
                keybd_event(viKeys, (byte)MapVirtualKey((uint)viKeys, 0), 2, 0);
        }

        public static void KeyPlayBack(Queue<KeyPlayList> keyQueue,int tick)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            int startTime = Environment.TickCount;
            long targetTime = startTime;
            int duration = 0;
                while (keyQueue.Any() == true && !cts.Token.IsCancellationRequested)
                {
                    KeyPlayList nextKey = keyQueue.Dequeue();
                    duration=(int)(tick * nextKey.Tick);
                    targetTime = startTime + duration;
                    while (true)
                    {
                        if (targetTime<=Environment.TickCount)
                        {
                            break;
                        }
                    }

                    startTime = Environment.TickCount;
                    Console.WriteLine($" i called function at {startTime} with target time is {targetTime}");
                    KeyboardPress(nextKey.Key);
                }
  
            
        }


    }
    public class KeyPlayList
    {
        public KeyPlayList(Keys key,uint tick)
        {
            Tick = tick;
            Key =key;
        }

        public uint Tick;
        public Keys Key;
    }
}