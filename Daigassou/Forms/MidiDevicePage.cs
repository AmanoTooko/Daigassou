using System;
using System.Drawing;
using Daigassou.Input_Midi;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using Sunny.UI;

namespace Daigassou.Forms
{
    public partial class MidiDevicePage : UIPage
    {
        public MidiDevicePage()
        {
            InitializeComponent();
            GetInputDevice();
        }

        public override void Init()
        {
            base.Init();
            uiLine3.ForeColor = Color.FromArgb(255, 113, 128);
            uiLine2.ForeColor = Color.FromArgb(255, 113, 128);
        }

        public override void Final()
        {
            base.Final();
        }

        private void tbMidiKey_ValueChanged(object sender, EventArgs e)
        {
            lblMidiKey.Text = $"键盘起始Key +{tbMidiKey.Value * 12}";
            KeyboardUtilities.offset = 48 - tbMidiKey.Value * 12;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (uiLight1.State == UILightState.On)
            {
                KeyboardUtilities.Disconnect();
                KeyboardUtilities.eventHandler -= KeyboardUtilities_eventHandler;
                uiLight1.State = UILightState.Off;
                btnConnect.Text = "连接";
            }
            else
            {
                if (KeyboardUtilities.Connect(cbInputDevice.SelectedIndex))
                {
                    KeyboardUtilities.eventHandler += KeyboardUtilities_eventHandler;
                    uiLight1.State = UILightState.On;
                }

                btnConnect.Text = "断开";
            }
        }

        private void KeyboardUtilities_eventHandler(object sender, MidiEventReceivedEventArgs e)
        {
            if (e.Event.EventType == MidiEventType.NoteOn || e.Event.EventType == MidiEventType.NoteOff)
            {
                var ev = e.Event as NoteEvent;
                Action actionDelegate = () =>
                {
                    tbKeyTest.Text =
                        $"当前按键 原始Key={Convert.ToInt32(ev.NoteNumber)} 程序输入Key={Convert.ToInt32(ev.NoteNumber) + 48 - tbMidiKey.Value * 12}";
                };
                BeginInvoke(actionDelegate);
            }
        }

        private void GetInputDevice()
        {
            cbInputDevice.DataSource = KeyboardUtilities.GetKeyboardList();
        }
    }
}