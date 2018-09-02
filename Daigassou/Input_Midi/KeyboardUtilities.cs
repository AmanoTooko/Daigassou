using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Midi.Devices;
using Midi.Messages;

namespace Daigassou.Input_Midi
{
    public class KeyboardUtilities
    {
        private static IInputDevice midiKeyboard;
        private static object NoteOnlock=new object();
        private static object NoteOfflock = new object();
        KeyboardUtilities()
        {

        }

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
                    midiKeyboard.NoteOn += NoteOn;
                    midiKeyboard.NoteOff += NoteOff;
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
            try
            {
                midiKeyboard.StopReceiving();
                midiKeyboard.Close();
                midiKeyboard.RemoveAllEventHandlers();
            }
            catch (Exception e)
            {
                MessageBox.Show($"断开错误 \r\n {e.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
        ~KeyboardUtilities()
        {
            if (midiKeyboard == null) return;
            midiKeyboard.StopReceiving();
            midiKeyboard.Close();
            midiKeyboard.RemoveAllEventHandlers();
        }

        public static void NoteOn(NoteOnMessage msg)
        {
            lock (NoteOnlock)
            {
                if (Convert.ToInt32(msg.Pitch) <= 84 && Convert.ToInt32(msg.Pitch) >= 48)
                    KeyController.KeyboardPress(KeyBinding.GetNoteToCtrlKey(Convert.ToInt32(msg.Pitch)),
                        KeyBinding.GetNoteToKey(Convert.ToInt32(msg.Pitch)));
            }
        }

        public static void NoteOff(NoteOffMessage msg)
        {
            lock (NoteOfflock)
            {
                if (Convert.ToInt32(msg.Pitch) <= 84 && Convert.ToInt32(msg.Pitch) >= 48)
                    KeyController.KeyboardRelease(KeyBinding.GetNoteToCtrlKey(Convert.ToInt32(msg.Pitch)),
                        KeyBinding.GetNoteToKey(Convert.ToInt32(msg.Pitch)));
            }
        }

    }
}
