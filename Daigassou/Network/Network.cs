using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Daigassou.Properties;
using Daigassou.Utils;
using NetFwTypeLib;
using System.Management;

namespace Daigassou.Network
{
    /*Thanks to the author of DFAssist https://github.com/devunt/DFAssist
     for code to capture and analyze packet*/
    internal partial class Network
    {
        Stopwatch sw = new Stopwatch();
        private struct IPPacket
        {
            public ProtocolFamily Version;
            public byte HeaderLength;
            public ProtocolType Protocol;

            public IPAddress SourceIPAddress;
            public IPAddress DestinationIPAddress;

            public byte[] Data;

            public bool IsValid;

            public IPPacket(byte[] buffer)
            {
                try
                {
                    var versionAndHeaderLength = buffer[0];
                    Version = versionAndHeaderLength >> 4 == 4 ? ProtocolFamily.InterNetwork : ProtocolFamily.InterNetworkV6;
                    HeaderLength = (byte)((versionAndHeaderLength & 15) * 4); // 0b1111 = 15

                    Protocol = (ProtocolType)buffer[9];

                    SourceIPAddress = new IPAddress(BitConverter.ToUInt32(buffer, 12));
                    DestinationIPAddress = new IPAddress(BitConverter.ToUInt32(buffer, 16));

                    Data = buffer.Skip(HeaderLength).ToArray();

                    IsValid = true;
                }
                catch (Exception ex)
                {
                    Version = ProtocolFamily.Unknown;
                    HeaderLength = 0;

                    Protocol = ProtocolType.Unknown;

                    SourceIPAddress = null;
                    DestinationIPAddress = null;

                    Data = null;

                    IsValid = false;
                    Log.Ex(ex, "l-packet-error-ip");
                }
            }
        }

        private struct TCPPacket
        {
            public ushort SourcePort;
            public ushort DestinationPort;
            public byte DataOffset;
            public TCPFlags Flags;

            public byte[] Payload;

            public bool IsValid;

            public TCPPacket(byte[] buffer)
            {
                try
                {
                    SourcePort = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 0));
                    DestinationPort = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 2));

                    var offsetAndFlags = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 12));
                    DataOffset = (byte)((offsetAndFlags >> 12) * 4);
                    Flags = (TCPFlags)(offsetAndFlags & 511); // 0b111111111 = 511

                    Payload = buffer.Skip(DataOffset).ToArray();

                    IsValid = true;
                }
                catch (Exception ex)
                {
                    SourcePort = 0;
                    DestinationPort = 0;
                    DataOffset = 0;
                    Flags = TCPFlags.NONE;

                    Payload = null;

                    IsValid = false;

                    Log.Ex(ex, "l-packet-error-tcp");
                }
            }
        }

        [Flags]
        public enum TCPFlags
        {
            NONE = 0,
            FIN = 1,
            SYN = 2,
            RST = 4,
            PSH = 8,
            ACK = 16,
            URG = 32,
            ECE = 64,
            CWR = 128,
            NS = 256,
        }
        private static class NativeMethods
        {
            [DllImport("Iphlpapi.dll", SetLastError = true)]
            public static extern uint GetExtendedTcpTable(IntPtr tcpTable, ref int tcpTableLength, bool sort, AddressFamily ipVersion, int tcpTableType, int reserved);
        }

        public const int TCP_TABLE_OWNER_PID_CONNECTIONS = 4;
        public readonly byte[] RCVALL_IPLEVEL = new byte[4] { 3, 0, 0, 0 };

        [StructLayout(LayoutKind.Sequential)]
        public struct TcpTable
        {
            public uint length;
            public TcpRow row;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TcpRow
        {
            public TcpState state;
            public uint localAddr;
            public uint localPort;
            public uint remoteAddr;
            public uint remotePort;
            public uint owningPid;
        }

        private List<Connection> connections = new List<Connection>();
        private string exePath;
        private MainForm mainForm;
        private Socket socket;
        private byte[] recvBuffer = new byte[0x20000];
        internal bool IsRunning { get; private set; } = false;
        private object lockAnalyse = new object();

        internal Network(MainForm mainForm)
        {
            exePath = Process.GetCurrentProcess().MainModule.FileName;
            this.mainForm = mainForm;
        }

        internal void StartCapture(Process process)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    sw.Start();
                    Log.I("l-network-starting");

                    if (IsRunning)
                    {
                        Log.E("l-network-error-already-started");
                        return;
                    }

                    UpdateGameConnections(process);

                    if (connections.Count < 2)
                    {
                        Log.E("l-network-error-no-connection");
                        return;
                    }

                    var localAddress = connections[0].localEndPoint.Address;

                    RegisterToFirewall();

                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
                    socket.Bind(new IPEndPoint(localAddress, 0));
                    socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);
                    socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AcceptConnection, true);
                    socket.IOControl(IOControlCode.ReceiveAll, RCVALL_IPLEVEL, null);
                    socket.ReceiveBufferSize = recvBuffer.Length * 4;
                    
                    //socket.BeginReceive(recvBuffer, 0, recvBuffer.Length, 0, new AsyncCallback(OnReceive), null);
                    IsRunning = true;
                    
                    Log.S("l-network-started");
                    sw.Stop();
                    Console.WriteLine($"Initialize time = {sw.ElapsedMilliseconds} ms");
                    sw.Reset();
                    int recvcount;
                    while (IsRunning)
                    {
                        recvcount=socket.Receive(recvBuffer);
                        var buffer = recvBuffer.Take(recvcount).ToArray();
                        Task.Run(() => { FilterAndProcessPacket(buffer); });
                    }
                }
                catch (Exception ex)
                {
                    Log.Ex(ex, "l-network-error-starting");
                }
            });
        }


        internal void StopCapture()
        {
            try
            {
                if (!IsRunning)
                {
                    Log.E("l-network-error-already-stopped");
                    return;
                }
                IsRunning = false;
                socket.Close();
                connections.Clear();
                socket.Dispose();
                
                //mainForm.overlayForm.SetStatus(false);

                Log.I("l-network-stopping");
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "l-network-error-stopping");
            }
        }

        internal void UpdateGameConnections(Process process)
        {
            var update = connections.Count < 2;
            var currentConnections = GetConnections(process);

            foreach (var connection in connections)
            {
                if (!currentConnections.Contains(connection))
                {
                    // 기존에 있던 연결이 끊겨 있음. 새롭게 갱신 필요
                    update = true;
                    Log.E("l-network-detected-connection-closing");
                    break;
                }
            }

            if (update)
            {
                //mainForm.overlayForm.SetStatus(false);

                var lobbyEndPoint = GetLobbyEndPoint(process);

                connections = currentConnections.Where(x => !x.remoteEndPoint.Equals(lobbyEndPoint)).ToList();

                foreach (var connection in connections)
                {
                    Log.I("l-network-detected-connection"+ connection.ToString());
                }
            }
        }

        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                lock (ar)
                {
                    
                    var length = socket.EndReceive(ar);
                    var buffer = recvBuffer.Take(length).ToArray();
                    socket.BeginReceive(recvBuffer, 0, recvBuffer.Length, 0, new AsyncCallback(OnReceive), null);
                    //TODO:multi thread cause packet recv time is not accuralte
                    Log.B(buffer,false);
                    Task.Run(()=>{ FilterAndProcessPacket(buffer); });

                }
                
            }
            catch (Exception ex) when (ex is ObjectDisposedException || ex is NullReferenceException)
            {
                IsRunning = false;
                socket = null;
                Log.S("l-network-stopped");
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "l-network-error-receiving-packet");
            }
        }

        private void FilterAndProcessPacket(byte[] buffer)
        {
            try
            {

                //Console.WriteLine($"analyze start at {DateTime.Now.ToString("hh:mm:ss.fff")} ");


                var ipPacket = new IPPacket(buffer);

                if (ipPacket.IsValid && ipPacket.Protocol == ProtocolType.Tcp)
                {
                    var tcpPacket = new TCPPacket(ipPacket.Data);

                    if (!tcpPacket.IsValid)
                    {
                        // invalid TCP packet
                        return;
                    }

                    if (!tcpPacket.Flags.HasFlag(TCPFlags.ACK | TCPFlags.PSH))
                    {
                        return;
                    }

                    var sourceEndPoint = new IPEndPoint(ipPacket.SourceIPAddress, tcpPacket.SourcePort);
                    var destinationEndPoint = new IPEndPoint(ipPacket.DestinationIPAddress, tcpPacket.DestinationPort);
                    var connection = new Connection() { localEndPoint = sourceEndPoint, remoteEndPoint = destinationEndPoint };
                    var reverseConnection = new Connection() { localEndPoint = destinationEndPoint, remoteEndPoint = sourceEndPoint };

                    if (!(connections.Contains(connection) || connections.Contains(reverseConnection)))
                    {
                        // Not a packet from server
                        return;
                    }

                    if (connections.Contains(reverseConnection))
                    {
                        // not an incoming packet
                        return;
                    }


                    // Analyze the packet from server
                    
                        AnalyseFFXIVPacket(tcpPacket.Payload);
                    
                
                    //Console.WriteLine($"analyze end at {DateTime.Now.ToString("hh:mm:ss.fff")} ");
                }
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "l-network-error-filtering-packet");
            }
        }

        private T GetInstance<T>(string typeName)
        {
            return (T)Activator.CreateInstance(Type.GetTypeFromProgID(typeName));
        }

        private void RegisterToFirewall()
        {
            try
            {
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

                    Log.S("l-firewall-registered");
                }
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "l-firewall-error");
            }
        }

        private IPEndPoint GetLobbyEndPoint(Process process)
        {
            IPEndPoint ipep = null;
            string lobbyHost = null;
            var lobbyPort = 0;

            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
                {
                    foreach (var @object in searcher.Get())
                    {
                        var commandline = @object["CommandLine"].ToString();
                        var argv = commandline.Split(' ');

                        foreach (var arg in argv)
                        {
                            var splitted = arg.Split('=');
                            if (splitted.Length == 2)
                            {
                                if (splitted[0] == "Dev.LobbyHost01")
                                {
                                    lobbyHost = splitted[1];
                                }
                                else if (splitted[0] == "Dev.LobbyPort01")
                                {
                                    lobbyPort = int.Parse(splitted[1]);
                                }
                            }
                        }
                    }
                }

                if (lobbyHost != null && lobbyPort > 0)
                {
                    var address = Dns.GetHostAddresses(lobbyHost)[0];
                    ipep = new IPEndPoint(address, lobbyPort);
                }
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "l-network-error-finding-lobby");
            }

            return ipep;
        }

        private List<Connection> GetConnections(Process process)
        {
            var connections = new List<Connection>();

            var tcpTable = IntPtr.Zero;
            var tcpTableLength = 0;

            if (NativeMethods.GetExtendedTcpTable(tcpTable, ref tcpTableLength, false, AddressFamily.InterNetwork, TCP_TABLE_OWNER_PID_CONNECTIONS, 0) != 0)
            {
                try
                {
                    tcpTable = Marshal.AllocHGlobal(tcpTableLength);
                    if (NativeMethods.GetExtendedTcpTable(tcpTable, ref tcpTableLength, false, AddressFamily.InterNetwork, TCP_TABLE_OWNER_PID_CONNECTIONS, 0) == 0)
                    {
                        var table = (TcpTable)Marshal.PtrToStructure(tcpTable, typeof(TcpTable));

                        var rowPtr = new IntPtr(tcpTable.ToInt64() + Marshal.SizeOf(typeof(uint)));
                        for (var i = 0; i < table.length; i++)
                        {
                            var row = (TcpRow)Marshal.PtrToStructure(rowPtr, typeof(TcpRow));

                            if (row.owningPid == process.Id)
                            {
                                var local = new IPEndPoint(row.localAddr, (ushort)IPAddress.NetworkToHostOrder((short)row.localPort));
                                var remote = new IPEndPoint(row.remoteAddr, (ushort)IPAddress.NetworkToHostOrder((short)row.remotePort));

                                connections.Add(new Connection() { localEndPoint = local, remoteEndPoint = remote });
                            }

                            rowPtr = new IntPtr(rowPtr.ToInt64() + Marshal.SizeOf(typeof(TcpRow)));
                        }
                    }
                }
                finally
                {
                    if (tcpTable != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(tcpTable);
                    }
                }
            }

            return connections;
        }

        private void AnalyseFFXIVPacket(byte[] payload)
        {
            try
            {


                while (true)
                {
                    if (payload.Length < 4)
                    {
                        break;
                    }

                    var type = BitConverter.ToUInt16(payload, 0);

                    if (type == 0x0000 || type == 0x5252)
                    {
                        if (payload.Length < 100)
                        {
                            break;
                        }

                        var length = BitConverter.ToInt32(payload, 24);
                        var tp = payload[58];
                        if (length <= 0 || payload.Length < length||tp!=0x8B)
                        {
                            break;
                        }

                        using (var messages = new MemoryStream(payload.Length))
                        {
                            using (var stream = new MemoryStream(payload, 0, length))
                            {
                                stream.Seek(73, SeekOrigin.Begin);

                                var l = payload[72];
                                byte[] msg = new byte[l];
                                Array.Copy(payload, 73, msg, 0, l);
                                Log.B(msg,true);//TODO: Time analyze 
                                ParameterController.GetInstance().AnalyzeNotes(msg);

                                
                                

                            }
                        }
                    }
                    else
                    {
                        // sticky tcp package process

                        for (var offset = 0; offset < payload.Length - 2; offset++)
                        {
                            var possibleType = BitConverter.ToUInt16(payload, offset);
                            if (possibleType == 0x5252)
                            {
                                payload = payload.Skip(offset).ToArray();
                                AnalyseFFXIVPacket(payload);
                                break;
                            }
                        }
                    }

                    break;
                    
                }
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "l-analyze-error");
            }
        }

        


        private enum State
        {
            IDLE,
            QUEUED,
            MATCHED,
        }
        private class Connection
        {
            public IPEndPoint localEndPoint { get; set; }
            public IPEndPoint remoteEndPoint { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }

                var connection = obj as Connection;

                return localEndPoint.Equals(connection.localEndPoint) && remoteEndPoint.Equals(connection.remoteEndPoint);
            }

            public override int GetHashCode()
            {
                return (localEndPoint.GetHashCode() + 0x0609) ^ remoteEndPoint.GetHashCode();
            }

            public override string ToString()
            {
                return $"{localEndPoint.ToString()} -> {remoteEndPoint.ToString()}";
            }
        }

    }
}
