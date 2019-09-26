using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Daigassou.Utils;
using Melanchall.DryWetMidi.Smf.Interaction;
using Midi.Devices;
using Midi.Messages;

namespace Daigassou.Input_Midi
{
    public static class KeyboardUtilities
    {
        private static IInputDevice midiKeyboard;
        private static readonly object NoteOnlock=new object();
        private static readonly object NoteOfflock = new object();
        private static readonly object noteLock=new object();
        private static Queue<NoteMessage> noteQueue = new Queue<NoteMessage>();
        private static readonly CancellationTokenSource cts=new CancellationTokenSource();
        public static int Connect(int index)
        {
            midiKeyboard = DeviceManager.InputDevices[index];
            if (midiKeyboard.IsOpen==true)
            {
                return -1;
            }
            else
            {
                try
                {
                    midiKeyboard.Open();
                    midiKeyboard.StartReceiving(null);
                    
                    midiKeyboard.NoteOn +=(msg)=>
                    {
                        lock (noteLock)
                        {
                            noteQueue.Enqueue(msg);
                        }
                        
                    }; 
                    midiKeyboard.NoteOff += (msg) =>
                    {
                        lock (noteLock)
                        {
                            noteQueue.Enqueue(msg);
                        }

                    };

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

        public static void Disconnect()
        {
            if (midiKeyboard == null) return;
            if (midiKeyboard.IsOpen == true)
            {
                try
                {
                    midiKeyboard.StopReceiving();
                    midiKeyboard.Close();
                    midiKeyboard.RemoveAllEventHandlers();
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
            List<string> ret=new List<string>();
            DeviceManager.UpdateInputDevices();
            foreach (var device in DeviceManager.InputDevices)
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
                        case NoteOnMessage keyon:
                            NoteOn(keyon);
                            Thread.Sleep(minimumInterval);
                            break;
                        case NoteOffMessage keyoff:
                            NoteOff(keyoff);
                            break;

                    }
                    Thread.Sleep(10);
                }
                
            }



        }
        public static void NoteOn(NoteOnMessage msg)
        {
            lock (NoteOnlock)
            {
                Log.Debug($"msg  {msg.Pitch} on at time {DateTime.Now:O}");
                if (Convert.ToInt32(msg.Pitch) <= 84 && Convert.ToInt32(msg.Pitch) >= 48)
                {
                    if (msg.Velocity==0)//note off
                    {
                        KeyController.KeyboardRelease(Convert.ToInt32(msg.Pitch));
                    }
                    else
                    {
                        KeyController.KeyboardPress(Convert.ToInt32(msg.Pitch));
                    }
                }
                    
            }
        }

        public static void NoteOff(NoteOffMessage msg)
        {
            lock (NoteOfflock)
            {
                Log.Debug($"msg  {msg.Pitch} off at time {DateTime.Now:O}");
                if (Convert.ToInt32(msg.Pitch) <= 84 && Convert.ToInt32(msg.Pitch) >= 48)
                    KeyController.KeyboardRelease(Convert.ToInt32(msg.Pitch));
            }
        }

    }
}
