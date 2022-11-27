using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Daigassou.Input_Midi;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using Sunny.UI;

namespace Daigassou.Forms
{
    public partial class MidiDevicePage : UIPage
    {
        private DevicesConnector Connector;
        public MidiDevicePage()
        {
            InitializeComponent();
            GetDevices();
            
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

        protected override void WndProc(ref Message m)
        {
            try
            {
                const int WM_DEVICECHANGE = 0x0219;
                switch (m.Msg)
                {
                    case WM_DEVICECHANGE:
                        GetDevices();
                        break;
                }
            }

            catch (Exception ex)
            {
            }

            base.WndProc(ref m);
        }
        private void tbMidiKey_ValueChanged(object sender, EventArgs e)
        {
            lblMidiKey.Text = $"键盘起始Key +{tbMidiKey.Value * 12}";
            KeyboardUtilities.offset = 48 - tbMidiKey.Value * 12;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (uiLight1.State == UILightState.On )
            {
                KeyboardUtilities.Disconnect();
                KeyboardUtilities.eventHandler -= KeyboardUtilities_eventHandler;
                uiLight1.State = UILightState.Off;
                btnConnect.Text = "连接";
                
                if (Connector!=null)
                {
                    Connector?.Disconnect();
                    foreach (var output in Connector.OutputDevices)
                    {
                        output.Dispose();
                    }
                }

                GetDevices();



            }
            else 
            {
                if (KeyboardUtilities.Connect(cbInputDevice.SelectedIndex))
                {
                    KeyboardUtilities.eventHandler += KeyboardUtilities_eventHandler;
                    uiLight1.State = UILightState.On;
                    btnConnect.Text = "断开";
                    if (uiSplit.Active)
                    {
                        var selectedDevices = new List<int>();
                        for (int i = 0; i < cbTVkeyboard.Nodes.Count; i++)
                        {
                            
                            if (cbTVkeyboard.Nodes[i].Checked)
                                selectedDevices.Add(cbTVkeyboard.Nodes[i].Index);
                        }
                        Connector = KeyboardUtilities.virtualConnector(selectedDevices.ToArray());
                        Connector?.Connect();
                    }
                }
                
                
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

        private void GetDevices()
        {
            cbInputDevice.DataSource = KeyboardUtilities.GetKeyboardList();
            
            foreach (var device in KeyboardUtilities.GetOutputDeviceList())
            {
                cbTVkeyboard.Nodes.Add(device);
            }
            
        }

        private void uiSplit_ValueChanged(object sender, bool value)
        {

            if (!value && Connector!=null)
            {
                foreach (var output in Connector.OutputDevices)
                {
                    output.Dispose();
                }
                
            }
        }
    }
}