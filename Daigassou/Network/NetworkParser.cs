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
            TIMER_START = 1, //text= player name;param=start time
            STOP = 2, // text=player name;
            PAUSE = 4,
            OFFSET_CHANGE = 8, //param = offset(ms);
            CONFIRM_START = 16, //no param
            INSTRUAMENT_CHANGE = 32 //param=instrument code no text
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
        //public static uint countDownPacket = 794;//0x01D6;//106; size=80 POS=0X26
        //public static uint ensembleStopPacket = 140;//0x0104;//1B8; size=48;
        //public static uint partyStopPacket = 245;//0x012E;// 2B3;size=48;
        //public static uint ensembleStartPacket = 176;//0x03CA;// 233; size=88 0x32=bpm
        //public static uint ensemblePacket = 807;//0x0240;// 213;
        //public static uint ensembleConfirmPacket = 696;//0x01E3;//2EB size=56,0x32=bpm
        //public static uint instruSendingPacket = 785;//0x033D;//354 size=64


        public static Dictionary<string, ushort> opcodeDict = new Dictionary<string, ushort>()
        {
            {"countDownPacket", 0x360},
            {"ensembleStopPacket",0x304 },
            {"partyStopPacket", 0x02d1},
            {"ensembleStartPacket", 0X2AE},
            {"ensemblePacket", 0x25c},
            {"ensembleConfirmPacket", 0x1A9},
            {"InstruSendingPacket", 0x1BA}
        };

        public bool ensembleProcessFlag = true;
        private FFXIVNetworkMonitor monitor = new FFXIVNetworkMonitor();
        public Process process;
        public event EventHandler<PlayEvent> Play;

        public bool StartNetworkMonitor()
        {
            Stopwatch sw= new Stopwatch();
            sw.Start();
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
                Debug.WriteLine($"sw time {sw.ElapsedMilliseconds}ms");
                monitor.MonitorType = NetworkMonitorType.WinPCap; //TODO: Fix here to setting
                monitor.ProcessIDList.Add((uint) process.Id);
                monitor.OodleImplementation = OodleImplementation.Ffxiv;
                monitor.OodlePath = process.MainModule.FileName;
                RegisterToFirewall();//todo:performance flame point. only need once 
                Debug.WriteLine($"sw time {sw.ElapsedMilliseconds}ms");
                monitor.Start();
                Debug.WriteLine($"sw time {sw.ElapsedMilliseconds}ms");
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
            if (opCode == opcodeDict["InstruSendingPacket"]) //小队倒计时
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

            if (opCode == opcodeDict["countDownPacket"]) //小队倒计时
            {
                var countDownTime = res.data[38];
                var unixTime = BitConverter.ToUInt32(res.data, 24);
                var nameBytes = new byte[18];
                Array.Copy(res.data, 43, nameBytes, 0, 18);
                var name = Encoding.UTF8.GetString(nameBytes) ?? "";

                Play?.Invoke(
                    this,
                    new PlayEvent(PlayEvent.playmode.TIMER_START, Convert.ToInt32(unixTime + countDownTime), name)
                );
            }

            if (
                opCode == opcodeDict["partyStopPacket"]
                || (opCode == opcodeDict["ensembleStopPacket"] && ensembleProcessFlag)
            ) //Stop
            {
                Play?.Invoke(this, new PlayEvent(PlayEvent.playmode.STOP, 0, " "));
            }

            if (opCode == opcodeDict["ensembleStartPacket"] && ensembleProcessFlag) //ensemble start
            {
                var unixTime = BitConverter.ToUInt32(res.data, 24);
                //ParameterController.GetInstance().isEnsembleSync = true;
                Play?.Invoke(
                    this,
                    new PlayEvent(
                        PlayEvent.playmode.TIMER_START,
                        Convert.ToInt32(unixTime + 5),
                        " "
                    )
                );
            }

            if (opCode == opcodeDict["ensemblePacket"] && ensembleProcessFlag) //ensemble mode
            {
                //if (ParameterController.GetInstance().isEnsembleSync)
                //    ParameterController.GetInstance().AnalyzeEnsembleNotes(message);
                //ParameterController.GetInstance().isEnsembleSync = false;
            }

            if (opCode == opcodeDict["ensembleConfirmPacket"] && ensembleProcessFlag)
            {
                if (BitConverter.ToUInt32(res.data, 40) != res.header.ActorID)//confirm packet also send to sender itself
                    Play?.Invoke(this, new PlayEvent(PlayEvent.playmode.CONFIRM_START, 0, " "));
            }
        }

        public void StopNetworkMonitor()
        {
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