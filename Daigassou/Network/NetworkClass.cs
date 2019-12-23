using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Daigassou.Utils;
using Machina.FFXIV;
using Machina;
namespace Daigassou
{
    public class PacketEntry
    {
        public string Direction { get; set; }
        public string Message { get; set; }
        public string Timestamp { get; set; }
        public string RouteID { get; set; }
        [XmlElement(ElementName = "Data")]
        public string DataString { get; set; }
        [XmlIgnore]
        public byte[] Data { get; set; }
        public string Note { get; set; }
        public int Set { get; set; }
        public uint PacketUnixTime { get; set; }
        public long SystemMsTime { get; set; }
        public bool IsDecrypted { get; set; }
        public FFXIVNetworkMonitor.ConnectionType Connection { get; set; }

        // ignored values
        [XmlIgnore]
        public string Name { get; set; }
        [XmlIgnore]
        public string Comment { get; set; }
        [XmlIgnore]
        public string Size { get; set; }
        [XmlIgnore]
        public string Category { get; set; }
        [XmlIgnore]
        public int ActorControl { get; set; }
        [XmlIgnore]
        public bool IsVisible { get; set; }
        [XmlIgnore]
        public bool IsForSelf { get; set; }

        public PacketEntry()
        {
            IsVisible = true;
            RouteID = string.Empty;
        }
    }
    public class NetworkClass
    {
        public event EventHandler<Network.PlayEvent> Play;
        private void MessageReceived(long epoch, byte[] message, int set, FFXIVNetworkMonitor.ConnectionType connectionType)
        {
            var res = Parse(message);

            
            if (res.header.MessageType == 0x011E && Log.isBeta)//CountDown
            {
                var countDownTime = res.data[36];
                var nameBytes = new byte[18];
                Array.Copy(res.data, 41, nameBytes, 0, 18);
                var name = Encoding.UTF8.GetString(nameBytes) ?? "";
                Play?.Invoke(this, new Network.PlayEvent(0, Convert.ToInt32(countDownTime), name));
            }


            if (res.header.MessageType == 0x011C && Log.isBeta) //party check
            {
                Console.WriteLine("get!");
                var nameBytes = new byte[18];
                Array.Copy(res.data, 52, nameBytes, 0, 18);
                var name = Encoding.UTF8.GetString(nameBytes) ?? "";
                Play?.Invoke(this, new Network.PlayEvent(1, 0, name));

            }
            if (res.header.MessageType == 0x0272 && Log.isBeta) //party check
            {
                Console.WriteLine("272");

                Play?.Invoke(this, new Network.PlayEvent(1, 0, "紧急"));

            }
            

        }
        internal class ParseResult
        {
            public FFXIVMessageHeader header;
            public byte[] data;
        }
        internal class TimedNote
        {
            public DateTime StartTime;
            public int Note;
            public override string ToString()
            {
                return string.Format($"StartTime: {StartTime.ToString("HH:mm:ss.fff")}, Note: {Note.ToString("X2")}");
            }
        }

        private DateTime lastSentTime;
        private void MessageSent(long epoch, byte[] message, int set, FFXIVNetworkMonitor.ConnectionType connectionType)
        {
            var res = Parse(message);

            var item = new PacketEntry
            {
                IsVisible = true,
                ActorControl = -1,
                Data = message,
                Message = res.header.MessageType.ToString("X4"),
                Direction = "C",
                Category = set.ToString(),
               // Timestamp = Util.UnixTimeStampToDateTime(res.header.Seconds).ToString(@"MM\/dd\/yyyy HH:mm:ss"),
                Size = res.header.MessageLength.ToString(),
                Set = set,
                RouteID = res.header.RouteID.ToString(),
                PacketUnixTime = res.header.Seconds,
               // SystemMsTime = Millis(),
                Connection = connectionType
            };
            if (res.header.MessageType == 0x018B) //Bard Performance
            {
                var length = res.data[32];
                var notes = new byte[length];
                Array.Copy(res.data, 33, notes, 0, length);
                Log.B(notes, true);//TODO: Time analyze 
                ParameterController.GetInstance().AnalyzeNotes(notes);
            }
        }

        private static ParseResult Parse(byte[] data)
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            FFXIVMessageHeader head = (FFXIVMessageHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(FFXIVMessageHeader));
            handle.Free();

            ParseResult result = new ParseResult();
            result.header = head;
            result.data = data;

            return result;
        }

        public bool _shouldStop = false;
        public void Run(uint processID)
        {
            FFXIVNetworkMonitor monitor = new FFXIVNetworkMonitor();
            monitor.MonitorType = TCPNetworkMonitor.NetworkMonitorType.RawSocket;
            monitor.MessageReceived = MessageReceived;
            monitor.MessageSent = MessageSent;
            monitor.ProcessID = processID;
            monitor.Start();

            while (!_shouldStop)
            {
                // So don't burn the cpu while doing nothing
                Thread.Sleep(1);
            }

            Console.WriteLine("MachinaCaptureWorker: Terminating");
            monitor.Stop();
        }
    }
}
