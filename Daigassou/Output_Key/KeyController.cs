// Decompiled with JetBrains decompiler
// Type: Daigassou.KeyController
// Assembly: Daigassou, Version=2.5.2.5, Culture=neutral, PublicKeyToken=null
// MVID: 24E3D967-932D-40D8-89F0-CFA3C67A7EA7
// Assembly location: D:\dev\Daigassou\Daigassou\bin\x64\Debug\Daigassou.exe

using Daigassou.Input_Midi;
using Daigassou.Properties;
using Daigassou.Utils;
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
        private static Dictionary<int, Keys> _keymap = new Dictionary<int, Keys>();
        private readonly BackgroundKey bkKeyController = new BackgroundKey();
        private readonly object keyLock = new object();
        public volatile bool isBackGroundKey = false;
        public volatile bool isPlayingFlag = false;
        public volatile bool isRunningFlag = false;
        public delegate void stopped();
        public stopped stopHandler;
        public int pauseOffset = 0;
        private Keys _lastCtrlKey;
        private bool initFlag=true;
        [DllImport("User32.dll")]
        public static extern void keybd_event(Keys bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);

        public void KeyboardPress(int pitch)
        {
            if (pitch > 84 || pitch < 48)
                return;
            if (Settings.Default.IsEightKeyLayout)
                this.KeyboardPress(KeyBinding.GetNoteToCtrlKey(pitch), KeyBinding.GetNoteToKey(pitch));
            else if (this.isBackGroundKey)
                this.bkKeyController.BackgroundKeyPress(KeyBinding.GetNoteToKey(pitch));
            else
                this.KeyboardPress(KeyBinding.GetNoteToKey(pitch));
            ParameterController.GetInstance().NetSyncQueue.Enqueue(new Daigassou.Utils.TimedNote()
            {
                Note = pitch - 24,
                StartTime = DateTime.Now
            });
            if(ParameterController.GetInstance().isEnsembleSync)
            {
                Log.overlayLog($"{pitch} pressed");
            }
            if (initFlag)
            {
                Log.overlayLog($"{pitch} note on");
                initFlag = false;
            }

        }

        public void KeyboardPress(Keys ctrKeys, Keys viKeys)
        {
            
            if(_lastCtrlKey!=ctrKeys)
            {
                KeyController.keybd_event(this._lastCtrlKey, (byte)KeyController.MapVirtualKey((uint)this._lastCtrlKey, 0U), 2, 0);
                Thread.Sleep(8);
                if ((uint)ctrKeys > 0U)
                {                    
                    KeyController.keybd_event(ctrKeys, (byte)KeyController.MapVirtualKey((uint)ctrKeys, 0U), 0, 0);
                    Thread.Sleep(8);
                }
            }

            KeyController.keybd_event(viKeys, (byte)KeyController.MapVirtualKey((uint)viKeys, 0U), 0, 0);
            this._lastCtrlKey = ctrKeys;
        }

        private void KeyboardPress(Keys viKeys)
        {
            lock (this.keyLock)
                KeyController.keybd_event(viKeys, (byte)KeyController.MapVirtualKey((uint)viKeys, 0U), 0, 0);
        }

        public void KeyboardRelease(int pitch)
        {
            lock (this.keyLock)
            {
                if (pitch > 84 || pitch < 48)
                    return;
                if (Settings.Default.IsEightKeyLayout)
                    this.KeyboardRelease(KeyBinding.GetNoteToCtrlKey(pitch), KeyBinding.GetNoteToKey(pitch));
                else if (this.isBackGroundKey)
                    this.bkKeyController.BackgroundKeyRelease(KeyBinding.GetNoteToKey(pitch));
                else
                    this.KeyboardRelease(KeyBinding.GetNoteToKey(pitch));
            }
        }

        public void KeyboardRelease(Keys ctrKeys, Keys viKeys)
        {

            KeyController.keybd_event(viKeys, (byte)KeyController.MapVirtualKey((uint)viKeys, 0U), 2, 0);
            Thread.Sleep(5);
        }

        public void KeyboardRelease(Keys viKeys)
        {
            KeyController.keybd_event(viKeys, (byte)KeyController.MapVirtualKey((uint)viKeys, 0U), 2, 0);
        }

        public void InitBackGroundKey(IntPtr pid)
        {
            this.bkKeyController.Init(pid);
        }

        public void ResetKey()
        {
            this.isPlayingFlag = false;
            this.isRunningFlag = false;
            initFlag = true;
            this.pauseOffset = 0;
            this.KeyboardRelease(Keys.ControlKey);
            Thread.Sleep(1);
            this.KeyboardRelease(Keys.ShiftKey);
            Thread.Sleep(1);
            this.KeyboardRelease(Keys.Menu);
            Thread.Sleep(1);
            for (int note = 48; note < 84; ++note)
            {
                this.KeyboardRelease(KeyBinding.GetNoteToKey(note));
                Thread.Sleep(1);
            }
                
            ParameterController.GetInstance().Pitch = 0;
        }

        public void UpdateKeyMap()
        {
            for (int note = 48; note < 84; ++note)
                KeyController._keymap[note] = KeyBinding.GetNoteToKey(note);
        }

        public void KeyPlayBack(
          Queue<KeyPlayList> keyQueue,
          double speed,
          CancellationToken token,
          int startOffset)
        {
            this.isRunningFlag = true;
            initFlag = true;
            this.UpdateKeyMap();
            double? timeMs = keyQueue.LastOrDefault<KeyPlayList>()?.TimeMs;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (keyQueue.Any<KeyPlayList>())
            {
                KeyPlayList keyPlayList = keyQueue.Dequeue();
                double num1 = (double)startOffset + keyPlayList.TimeMs * speed;
                while (true)
                {
                    if (!this.isPlayingFlag || num1 + (double)ParameterController.GetInstance().Offset + (double)this.pauseOffset > (double)stopwatch.ElapsedMilliseconds)
                        Thread.Sleep(1);
                    else
                        break;
                }
                if (keyPlayList.Ev == KeyPlayList.NoteEvent.NoteOn)
                    this.KeyboardPress(keyPlayList.Pitch + ParameterController.GetInstance().Pitch);
                else
                    this.KeyboardRelease(keyPlayList.Pitch + ParameterController.GetInstance().Pitch);
                double num2 = keyPlayList.TimeMs * 100.0;
                double? nullable = timeMs;
                Daigassou.Utils.Log.overlayProcess(((int)(nullable.HasValue ? new double?(num2 / nullable.GetValueOrDefault()) : new double?()).Value).ToString());
            }
            Daigassou.Utils.Log.overlayLog("演奏：演奏结束");
            if(stopHandler!=null)
            {
                stopHandler.BeginInvoke(null, null);
            }
            this.ResetKey();
        }
    }
    public class KeyPlayList
    {
        public KeyPlayList.NoteEvent Ev;
        public int Pitch;
        public double TimeMs;

        public KeyPlayList(KeyPlayList.NoteEvent ev, int pitch, double timeMs)
        {
            this.TimeMs = timeMs;
            this.Ev = ev;
            this.Pitch = pitch;
        }

        public enum NoteEvent
        {
            NoteOff,
            NoteOn,
        }
    }
}
