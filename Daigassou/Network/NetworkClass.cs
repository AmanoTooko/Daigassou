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
using NetFwTypeLib;


namespace Daigassou
{
    public class PlayEvent : EventArgs
    {
        private readonly String text;
        private readonly int time;
        private readonly int mode;

        public PlayEvent(int mode, int Time, String text)
        {
            this.mode = mode;
            this.time = Time;
            this.text = text;
        }
        public String Text { get { return text; } }
        public int Time { get { return time; } }
        public int Mode
        {
            get { return mode; }
        }
    }
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
        public event EventHandler<PlayEvent> Play;
        private void MessageReceived(long epoch, byte[] message, int set, FFXIVNetworkMonitor.ConnectionType connectionType)
        {
            var res = Parse(message);


            if (res.header.MessageType == ParameterController.countDownPacket)//CountDown
            {
                var countDownTime = res.data[36];
                var unixTime = BitConverter.ToUInt32(res.data, 24);
                var nameBytes = new byte[18];
                Array.Copy(res.data, 41, nameBytes, 0, 18);
                var name = Encoding.UTF8.GetString(nameBytes) ?? "";
                Play?.Invoke(this, new PlayEvent(0, Convert.ToInt32(unixTime + countDownTime), name));
            }


            if (res.header.MessageType == ParameterController.partyStopPacket|| res.header.MessageType == ParameterController.ensembleStopPacket) //Stop
            {

                Play?.Invoke(this, new PlayEvent(1, 0, " "));

            }
            if (res.header.MessageType == ParameterController.ensembleStartPacket)//ensemble start
            {

                var unixTime = BitConverter.ToUInt32(res.data, 24);
                ParameterController.GetInstance().isEnsembleSync = true;
                Play?.Invoke(this, new PlayEvent(0, Convert.ToInt32(unixTime + 6), " "));

            }
            if (res.header.MessageType == ParameterController.ensemblePacket)//ensemble mode
            {
                if(ParameterController.GetInstance().isEnsembleSync)
                ParameterController.GetInstance().AnalyzeEnsembleNotes(res.data);
                ParameterController.GetInstance().isEnsembleSync = false;
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
            try
            {
                var res = Parse(message);
                if (res.header.MessageType == ParameterController.instruSendingPacket) //Bard Performance 
                {
                   var type = BitConverter.ToUInt16(res.data, 32);
                    if (type == (0x071C))
                    {
                        byte data = res.data[36];
                        if (data <= 19)
                        {
                            Play?.Invoke(this, new PlayEvent(2, 0, constStringClass.IDtoString(data)));
                            Log.overlayLog($"乐器更换：{constStringClass.IDtoString(data)}");
                        }

                    }

                }
            }
            catch (Exception e)
            {
                Log.overlayLog(e.ToString());
                
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
            Log.overlayLog($"开始检测进程：{processID}");
            FFXIVNetworkMonitor monitor = new FFXIVNetworkMonitor();
            RegisterToFirewall();
            monitor.MonitorType = Daigassou.Properties.Settings.Default.isUsingWinPCap? TCPNetworkMonitor.NetworkMonitorType.WinPCap: TCPNetworkMonitor.NetworkMonitorType.RawSocket;
            monitor.MessageReceived = MessageReceived;
            monitor.MessageSent = MessageSent;
            monitor.ProcessID = processID;
            monitor.Start();

            while (!_shouldStop)
            {
                // So don't burn the cpu while doing nothing
                Thread.Sleep(5);
            }

            Console.WriteLine("MachinaCaptureWorker: Terminating");
            monitor.Stop();

        }
        private void RegisterToFirewall()
        {
            try
            {
                Process p = new Process();
                var exePath = Process.GetCurrentProcess().MainModule.FileName;
                p.StartInfo.FileName = "cmd.exe"; //命令
                p.StartInfo.UseShellExecute = false; //不启用shell启动进程
                p.StartInfo.RedirectStandardInput = true; // 重定向输入
                p.StartInfo.RedirectStandardOutput = true; // 重定向标准输出
                p.StartInfo.RedirectStandardError = true; // 重定向错误输出 
                p.StartInfo.CreateNoWindow = true; // 不创建新窗口
                p.Start();
                p.StandardInput.WriteLine("netsh advfirewall firewall add rule name=\"WinClient\" dir=in program=\"" + exePath + "\" action=allow localip=any remoteip=any security=notrequired description=DFAssist"); //cmd执行的语句
                                                                                                                                                                                                                        //p.StandardOutput.ReadToEnd(); //读取命令执行信息
                p.StandardInput.WriteLine("exit"); //退出

                var netFwMgr = GetInstance<INetFwMgr>("HNetCfg.FwMgr");
                var netAuthApps = netFwMgr.LocalPolicy.CurrentProfile.AuthorizedApplications;

                var isExists = false;
                foreach (var netAuthAppObject in netAuthApps)
                {
                    var netAuthApp = netAuthAppObject as INetFwAuthorizedApplication;
                    if (netAuthApp != null && netAuthApp.ProcessImageFileName == exePath && netAuthApp.Enabled)
                    {
                        isExists = true;
                    }
                }

                if (!isExists)
                {
                    var netAuthApp = GetInstance<INetFwAuthorizedApplication>("HNetCfg.FwAuthorizedApplication");

                    netAuthApp.Enabled = true;
                    netAuthApp.Name = "Daigassou";
                    netAuthApp.ProcessImageFileName = exePath;
                    netAuthApp.Scope = NET_FW_SCOPE_.NET_FW_SCOPE_ALL;

                    netAuthApps.Add(netAuthApp);

                }
                Log.S("l-firewall-registered");
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "l-firewall-error");
            }
        }

        private T GetInstance<T>(string typeName)
        {
            return (T)Activator.CreateInstance(Type.GetTypeFromProgID(typeName));
        }
    }
}
