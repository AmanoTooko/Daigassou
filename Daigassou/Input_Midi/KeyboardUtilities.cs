using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Daigassou.Properties;
using Daigassou.Utils;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Core;

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
        public static KeyController kc;

        public static int Connect(string name, KeyController _keyController)
        {
            wetMidiKeyboard = InputDevice.GetById(Convert.ToInt32(name.Split('|')[1]));
            {
                try
                {
                    wetMidiKeyboard.EventReceived += MidiKeyboard_EventReceived;
                    wetMidiKeyboard.StartEventsListening();
                    cts = new CancellationTokenSource();
                    kc = _keyController;
                    kc.UpdateKeyMap();
                    Task.Run(() =>
                    {
                        //var keyPlayLists = mtk.ArrangeKeyPlays(mtk.Index);
                        NoteProcess(cts.Token);
                    }, cts.Token);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"连接错误 \r\n {e.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return 0;
        }

        private static void MidiKeyboard_EventReceived(object sender, MidiEventReceivedEventArgs e)
        {
            Log.overlayLog($"收到Note@{DateTime.Now.ToString("HH: mm:ss.fff")} ");
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
                    wetMidiKeyboard.Reset();
                    wetMidiKeyboard.EventReceived -= MidiKeyboard_EventReceived;
                    wetMidiKeyboard.Dispose();
                    cts.Cancel();
                }
                catch (Exception e)
                {
                    MessageBox.Show($"断开错误 \r\n {e.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        public static List<string> GetKeyboardList()
        {
            var ret = new List<string>();

            foreach (var device in InputDevice.GetAll()) ret.Add(device.Name+"|"+device.Id);

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
                Log.Debug($"msg  {msg.NoteNumber} on at time {DateTime.Now:O}");
                if (Convert.ToInt32(msg.NoteNumber) <= 84 && Convert.ToInt32(msg.NoteNumber) >= 48)
                {
                    if (msg.Velocity == 0) //note off
                        kc.KeyboardRelease(Convert.ToInt32(msg.NoteNumber));
                    else
                        kc.KeyboardPress(Convert.ToInt32(msg.NoteNumber));
                }
            }
        }

        public static void NoteOff(NoteOffEvent msg)
        {
            lock (NoteOfflock)
            {
                Log.Debug($"msg  {msg.NoteNumber} off at time {DateTime.Now:O}");
                if (Convert.ToInt32(msg.NoteNumber) <= 84 && Convert.ToInt32(msg.NoteNumber) >= 48)
                    kc.KeyboardRelease(Convert.ToInt32(msg.NoteNumber));
            }
        }
    }
}