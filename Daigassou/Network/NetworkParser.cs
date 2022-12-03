using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Machina.FFXIV;
using Machina.FFXIV.Headers;
using Machina.FFXIV.Oodle;
using Machina.Infrastructure;
using NetFwTypeLib;

namespace Daigassou.Controller
{
    public class PlayEvent : EventArgs
    {
        public enum playmode
        {
            COUNDOWN_TIMER_START = 1, //text= player name;param=start time
            ENSEMBLE_TIMER_START = 2, //text= player name;param=start time
            STOP = 4, // text=player name;
            PAUSE = 8,
            OFFSET_CHANGE = 16, //param = offset(ms);
            CONFIRM_START = 32, //no param
            INSTRUAMENT_CHANGE = 64 //param=instrument code no text
        }

        private readonly playmode mode;
        private readonly int param;
        private readonly String text;

        public PlayEvent(playmode mode, int param, String text)
        {
            this.mode = mode;
            this.param = param;
            this.text = text;
        }

        public String Text
        {
            get { return text; }
        }

        public int Param
        {
            get { return param; }
        }

        public playmode Mode
        {
            get { return mode; }
        }
    }

    public class NetworkParser 
    {
        //public static uint countDownPacket =  size=80 POS=0X26
        //public static uint ensembleStopPacket = size=48;
        //public static uint partyStopPacket = size=48; 0x22 = MARK
        //public static uint ensembleStartPacket = size=88 0x32=bpm
        //public static uint ensemblePacket = size=1064
        //public static uint ensembleConfirmPacket = size=56,0x32=bpm
        //public static uint instruSendingPacket = size=64,instruPOS=0x24

        public static Dictionary<string, ushort> opcodeDict = new Dictionary<string, ushort>()
        {
            {"countDownPacket", 0x0240},
            {"ensembleStopPacket", 0x0102},
            {"partyStopPacket", 0x0079},
            {"ensembleStartPacket", 0x023C},
            {"ensemblePacket", 0x25c},
            {"ensembleConfirmPacket", 0x03a6},
            {"InstruSendingPacket", 0x00b1}
        };

        public bool ensembleProcessFlag = true;
        public bool isUsingEnsembleAssist = false;
        private FFXIVNetworkMonitor monitor = new FFXIVNetworkMonitor();
        public Process process;
        public event EventHandler<PlayEvent> Play;

        public bool StartNetworkMonitor()
        {
            try
            {
                monitor.MessageReceivedEventHandler = (
                    TCPConnection connection,
                    long epoch,
                    byte[] message
                ) => MessageReceived(connection, epoch, message);
                
                monitor.MessageSentEventHandler = (
                    TCPConnection connection,
                    long epoch,
                    byte[] message
                ) => MessageSent(connection, epoch, message);
                monitor.MonitorType = Properties.Settings.Default.isUsingWinPCap? NetworkMonitorType.WinPCap: NetworkMonitorType.RawSocket; 
                monitor.ProcessIDList.Add((uint) process.Id);
                monitor.OodleImplementation = OodleImplementation.Ffxiv;
                monitor.OodlePath = process.MainModule.FileName;
                RegisterToFirewall();//todo:performance flame point?. only need once 
                monitor.Start();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private static ParseResult Parse(byte[] data)
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            Server_MessageHeader head =
                (Server_MessageHeader) Marshal.PtrToStructure(handle.AddrOfPinnedObject(),
                    typeof(Server_MessageHeader));
            handle.Free();

            ParseResult result = new ParseResult();
            result.header = head;
            result.data = data;

            return result;
        }

        private void MessageSent(TCPConnection connection, long epoch, byte[] message)
        {
            var res = Parse(message);


            ushort opCode = res.header.MessageType;
            if (opCode == opcodeDict["InstruSendingPacket"] && res.data[32]==0x1c) //当前乐器
            {
                var instruCode = res.data[36];
                if (instruCode <= 28)
                {
                    Play?.Invoke(this, new PlayEvent(PlayEvent.playmode.INSTRUAMENT_CHANGE, instruCode, ""));
                }
            }
        }

        private void MessageReceived(TCPConnection connection, long epoch, byte[] message)
        {
            var res = Parse(message);


            ushort opCode = res.header.MessageType;

            if (isUsingEnsembleAssist)
            {
                if (opCode == opcodeDict["ensembleStartPacket"] ) //ensemble start
                {
                    var unixTime = BitConverter.ToUInt32(res.data, 24);
                    //ParameterController.GetInstance().isEnsembleSync = true;
                    Play?.Invoke(
                        this,
                        new PlayEvent(
                            PlayEvent.playmode.ENSEMBLE_TIMER_START,
                            3000,
                            " "
                        )
                    );
                }


                if (opCode == opcodeDict["ensembleConfirmPacket"] )
                {
                    if (BitConverter.ToUInt32(res.data, 40) != res.header.ActorID)//confirm packet also send to sender itself
                        Play?.Invoke(this, new PlayEvent(PlayEvent.playmode.CONFIRM_START, 0, " "));
                }
                if (opCode == opcodeDict["ensembleStopPacket"])
                     //Stop
                {
                    Play?.Invoke(this, new PlayEvent(PlayEvent.playmode.STOP, 0, " "));
                }
            }
            else
            {
                if (opCode == opcodeDict["countDownPacket"]) //小队倒计时
                {
                    var countDownTime = res.data[38];
                    var unixTime = BitConverter.ToUInt32(res.data, 24);
                    var nameBytes = new byte[18];
                    Array.Copy(res.data, 43, nameBytes, 0, 18);
                    var name = Encoding.UTF8.GetString(nameBytes) ?? "";

                    Play?.Invoke(
                        this,
                        new PlayEvent(PlayEvent.playmode.COUNDOWN_TIMER_START, countDownTime*1000, name)
                    );
                }

                if (
                    opCode == opcodeDict["partyStopPacket"]
                     )
                 //Stop
                {
                    Play?.Invoke(this, new PlayEvent(PlayEvent.playmode.STOP, 0, " "));
                }

            }



        }

        public void StopNetworkMonitor()
        {
            if (monitor.ProcessID==0)
            {
                return;
            }
            monitor?.Stop();
        }

        /// <summary>
        ///     put current process to firewall
        /// </summary>
        public static void RegisterToFirewall()
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
                p.StandardInput.WriteLine(
                    "netsh advfirewall firewall add rule name=\"WinClient\" dir=in program=\""
                    + exePath
                    + "\" action=allow localip=any remoteip=any security=notrequired description=DFAssist"
                ); //cmd执行的语句
                //p.StandardOutput.ReadToEnd(); //读取命令执行信息
                p.StandardInput.WriteLine("exit"); //退出

                var netFwMgr = GetInstance<INetFwMgr>("HNetCfg.FwMgr");
                var netAuthApps = netFwMgr.LocalPolicy.CurrentProfile.AuthorizedApplications;

                var isExists = false;
                foreach (var netAuthAppObject in netAuthApps)
                {
                    var netAuthApp = netAuthAppObject as INetFwAuthorizedApplication;
                    if (
                        netAuthApp != null
                        && netAuthApp.ProcessImageFileName == exePath
                        && netAuthApp.Enabled
                    )
                    {
                        isExists = true;
                    }
                }

                if (!isExists)
                {
                    var netAuthApp = GetInstance<INetFwAuthorizedApplication>(
                        "HNetCfg.FwAuthorizedApplication"
                    );

                    netAuthApp.Enabled = true;
                    netAuthApp.Name = "Daigassou";
                    netAuthApp.ProcessImageFileName = exePath;
                    netAuthApp.Scope = NET_FW_SCOPE_.NET_FW_SCOPE_ALL;

                    netAuthApps.Add(netAuthApp);
                }
                //Log.S("l-firewall-registered");
            }
            catch (Exception ex)
            {
                // Log.Ex(ex, "l-firewall-error");
            }
        }

        private static T GetInstance<T>(string typeName)
        {
            return (T) Activator.CreateInstance(Type.GetTypeFromProgID(typeName));
        }

        internal class ParseResult
        {
            public byte[] data;
            public Server_MessageHeader header;
        }
    }
}