using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Daigassou.Utils;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Smf;
using Melanchall.DryWetMidi.Smf.Interaction;
using Midi.Devices;
using Midi.Messages;
using InputDevice = Midi.Devices.InputDevice;

namespace Daigassou.Input_Midi
{
    public static class KeyboardUtilities
    {
        private static Melanchall.DryWetMidi.Devices.InputDevice wetMidiKeyboard;
        private static readonly object NoteOnlock=new object();
        private static readonly object NoteOfflock = new object();
        private static readonly object noteLock=new object();
        private static Queue<NoteEvent> noteQueue = new Queue<NoteEvent>();
        private static CancellationTokenSource cts=new CancellationTokenSource();

        public static int Connect(string name){

            wetMidiKeyboard = Melanchall.DryWetMidi.Devices.InputDevice.GetByName(name);
            {
                try
                {
                    wetMidiKeyboard.EventReceived += MidiKeyboard_EventReceived;
                    wetMidiKeyboard.StartEventsListening();
                    cts = new CancellationTokenSource();


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
            switch (e.Event)
            {
                case NoteOnEvent @event:
                    noteQueue.Enqueue(@event);
                    break;
                case NoteOffEvent @event:
                    noteQueue.Enqueue(@event);
                    break;
                default:
                    break;
            }

        }

        public static void Disconnect()
        {
            if (wetMidiKeyboard == null) return;
            if (wetMidiKeyboard.IsListeningForEvents == true)
            {
                try
                {
                    wetMidiKeyboard.StopEventsListening();
                    wetMidiKeyboard.Reset();
                    wetMidiKeyboard.EventReceived-= MidiKeyboard_EventReceived;
                    wetMidiKeyboard.Dispose();
                    cts.Cancel();
                }
                catch (Exception e)
                {
                    MessageBox.Show($"断开错误 \r\n {e.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }


        }

        public static List<string> GetKeyboardList()
        {
            List<string> ret = new List<string>();
            
            foreach (var device in Melanchall.DryWetMidi.Devices.InputDevice.GetAll())
            {
                ret.Add(device.Name);
            }

            return ret;
        }


        public static void NoteProcess(CancellationToken token)
        {
            var minimumInterval = (int) Properties.Settings.Default.MinEventMs;

            while (!token.IsCancellationRequested)
            {
                
                lock (noteLock)
                {
                    if (noteQueue.Count <= 0)
                    {
                        
                        continue;
                    }
                    var nextKey = noteQueue.Dequeue();
                    switch (nextKey)
                    {
                        case NoteOnEvent keyon:
                            NoteOn(keyon);
                            Thread.Sleep(minimumInterval);
                            break;
                        case NoteOffEvent keyoff:
                            NoteOff(keyoff);
                            break;

                    }
                    Thread.Sleep(5);
                }
                
            }



        }
        public static void NoteOn(NoteOnEvent msg)
        {
            lock (NoteOnlock)
            {
                Log.Debug($"msg  {msg.NoteNumber} on at time {DateTime.Now:O}");
                if (Convert.ToInt32(msg.NoteNumber) <= 84 && Convert.ToInt32(msg.NoteNumber) >= 48)
                {
                    if (msg.Velocity==0)//note off
                    {
                        KeyController.KeyboardRelease(Convert.ToInt32(msg.NoteNumber));
                    }
                    else
                    {
                        KeyController.KeyboardPress(Convert.ToInt32(msg.NoteNumber));
                    }
                }
                    
            }
        }

        public static void NoteOff(NoteOffEvent msg)
        {
            lock (NoteOfflock)
            {
                Log.Debug($"msg  {msg.NoteNumber} off at time {DateTime.Now:O}");
                if (Convert.ToInt32(msg.NoteNumber) <= 84 && Convert.ToInt32(msg.NoteNumber) >= 48)
                    KeyController.KeyboardRelease(Convert.ToInt32(msg.NoteNumber));
            }
        }

    }
}
