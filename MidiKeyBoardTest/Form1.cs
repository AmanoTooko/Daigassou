using Melanchall.DryWetMidi.Multimedia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidiKeyBoardTest
{
    public partial class 一个测试midi键盘连接的小工具 : Form
    {
        public 一个测试midi键盘连接的小工具()
        {
            InitializeComponent();
            
        }

        private void btnKeyboardConnect_Click(object sender, EventArgs e)
        {
            var inputDevice = InputDevice.GetByName("Roland Digital Piano");
            inputDevice.Connect();
            inputDevice.StartEventsListening();
            inputDevice.EventReceived += InputDevice_EventReceived;
            //using (var inputDevice = InputDevice.GetByName("Roland Digital Piano"))
            var outputDevice1 = OutputDevice.GetByName("loopMIDI Port");
            var outputDevice2 = OutputDevice.GetByName("loopMIDI Port 1");
            
                //{
            var devicesConnector = new DevicesConnector(inputDevice, outputDevice1, outputDevice2);
            devicesConnector.Connect();
            //    inputDevice.Connect();
            //    inputDevice.StartEventsListening();
            //    inputDevice.EventReceived += InputDevice_EventReceived;
            //}
        }

        private void InputDevice_EventReceived(object sender, MidiEventReceivedEventArgs e)
        {
            Debug.WriteLine(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
    }
}
