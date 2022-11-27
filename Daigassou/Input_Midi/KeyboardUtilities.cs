using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Daigassou.Properties;
using Daigassou.Utils;
using DaigassouDX.Controller;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.Core;
using Sunny.UI;
using System.Diagnostics;

namespace Daigassou.Input_Midi
{
    public static class KeyboardUtilities
    {
        private static InputDevice wetMidiKeyboard;
        private static readonly object NoteOnlock = new object();
        private static readonly object NoteOfflock = new object();
        private static readonly object noteLock = new object();
        private static readonly Queue<NoteEvent> noteQueue = new Queue<NoteEvent>();
        private static CancellationTokenSource cts = new CancellationTokenSource();
        public static int offset;
        public static event EventHandler<MidiEventReceivedEventArgs> eventHandler;
        // public static KeyController kc;

        public static DevicesConnector virtualConnector(int[] indexs)
        {
            IOutputDevice[] outputDevices = new IOutputDevice[indexs.Length];
            
            for (int i = 0; i < indexs.Length; i++)
            {
                var tmpDevice = OutputDevice.GetByIndex(indexs[i]);
                outputDevices[i] = tmpDevice;

            }

            var dv = new DevicesConnector(wetMidiKeyboard, outputDevices);

            return dv;
        }
        public static bool Connect(int Index)
        {
            wetMidiKeyboard = InputDevice.GetByIndex(Index);
            {
                try
                {
                    wetMidiKeyboard.EventReceived += MidiKeyboard_EventReceived;
                    wetMidiKeyboard.SilentNoteOnPolicy = SilentNoteOnPolicy.NoteOff;
                    wetMidiKeyboard.StartEventsListening();
                    cts = new CancellationTokenSource();

                    Task.Run(() =>
                    {
                        
                        NoteProcess(cts.Token);
                    }, cts.Token);
                    return true;
                }
                catch (Exception e)
                {
                    UIMessageTip.ShowError($"连接错误 \r\n {e.Message}");
                    return false;
                }
            }

            
        }

        private static void MidiKeyboard_EventReceived(object sender, MidiEventReceivedEventArgs e)
        {
            Log.overlayLog($"收到Note@{DateTime.Now.ToString("HH: mm:ss.fff")} ");
            eventHandler?.Invoke(sender, e);

            switch (e.Event)
            {
                case NoteOnEvent @event:
                    noteQueue.Enqueue(@event);
                    break;
                case NoteOffEvent @event:
                    noteQueue.Enqueue(@event);
                    break;
            }
        }

        public static void Disconnect()
        {
            if (wetMidiKeyboard == null) return;
            if (wetMidiKeyboard.IsListeningForEvents)
                try
                {
                    wetMidiKeyboard.StopEventsListening();
                    wetMidiKeyboard.EventReceived -= MidiKeyboard_EventReceived;
                    wetMidiKeyboard.Dispose();
                    cts.Cancel();
                }
                catch (Exception e)
                {
                    UIMessageTip.ShowError($"断开错误 \r\n {e.Message}");
                    
                }

            eventHandler = null;
        }

        public static List<string> GetKeyboardList()
        {
            var ret = new List<string>();
            var index = 0;
            foreach (var device in InputDevice.GetAll())
            {
                ret.Add(device.Name+"|"+index++);
            }

            return ret;
        }
        public static List<string> GetOutputDeviceList()
        {
            var ret = new List<string>();
            var index = 0;
            foreach (var device in OutputDevice.GetAll())
            {
                ret.Add(device.Name + "|" + index++);
            }

            return ret;
        }

        public static void NoteProcess(CancellationToken token)
        {
           
            
            var minimumInterval = (int) Settings.Default.MinEventMs;
            while (!token.IsCancellationRequested)
            {
                NoteEvent nextKey;
                lock (noteLock)
                {
                    if (noteQueue.Count <= 0)
                    {
                        Thread.Sleep(1); continue;
                    }
                        
                    nextKey = noteQueue.Dequeue();
                }
                
                switch (nextKey)
                {
                    case NoteOnEvent keyon:
                        NoteOn(keyon);
                        Thread.Sleep(minimumInterval);
                        break;
                    case NoteOffEvent keyoff:
                        NoteOff(keyoff);
                        Thread.Sleep(minimumInterval);
                        break;
                }
                Thread.Sleep(1);
            }
        }

        public static void NoteOn(NoteOnEvent msg)
        {
            lock (NoteOnlock)
            {

                var pitch = Convert.ToInt32(msg.NoteNumber + offset);


                Log.Debug($"msg  {msg.NoteNumber} on at time {DateTime.Now:O}");
                if (pitch <= 84 && pitch >= 48)
                {
                    ProcessKeyController.GetInstance().PressKeyBoardByPitch(pitch);
                   
                }
            }
        }

        public static void NoteOff(NoteOffEvent msg)
        {
            lock (NoteOfflock)
            {
                var pitch = Convert.ToInt32(msg.NoteNumber + offset);
                Log.Debug($"msg  {msg.NoteNumber} off at time {DateTime.Now:O}");
                if (pitch <= 84 && pitch >= 48)
                    ProcessKeyController.GetInstance().ReleaseKeyBoardByPitch(pitch);
            }
        }
    }
}