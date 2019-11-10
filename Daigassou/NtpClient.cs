using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Daigassou
{
    internal class NtpClient
    {
        private readonly string _server;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NtpConnection" /> class.
        /// </summary>
        /// <param name="server">The server to connect to.</param>
        public NtpClient(string server)
        {
            if (string.IsNullOrEmpty(server)) throw new ArgumentException("Must be non-empty", nameof(server));

            _server = server;
        }

        public static TimeSpan Offset(string server = "pool.ntp.org")
        {
            double err;
            return new NtpClient(server).GetOffset(out err);
        }

        /// <inheritdoc />
        public TimeSpan GetOffset(out double errorMilliseconds)
        {

            TimeSpan offset=new TimeSpan(0);
            var addresses = Dns.GetHostEntry(_server).AddressList;
            var ipEndPoint = new IPEndPoint(addresses[0], 123);
            var socket =
                new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp) {ReceiveTimeout = 3000};
            errorMilliseconds = 0;
            try
            {
                var ntpData = new byte[48];
                ntpData[0] = 0x1B;
                socket.Connect(ipEndPoint);
                var localTransmitTime = DateTime.UtcNow; //T1
                socket.Send(ntpData);
                socket.Receive(ntpData);
                var localReceiveTime = DateTime.UtcNow; //T4
                socket.Close();
                var timeData = new byte[8];
                Array.Copy(ntpData, 32, timeData, 0, 8);
                var receiveTime = byteToTime(timeData); //T2
                Array.Copy(ntpData, 40, timeData, 0, 8);
                var transmitTime = byteToTime(timeData); //T3

                var fractPart = ((ulong) ntpData[10] << 8) |
                                ntpData[11];

                errorMilliseconds = fractPart * 1000 / 0x10000L;
                offset = localReceiveTime - transmitTime - (receiveTime - localTransmitTime); //((T4-T3)-(T2-T1))/2
                CommonUtilities.WriteLog($"localTransmitTime={localTransmitTime.ToString("O")}\r\n " +
                                         $"localReceiveTime = {localReceiveTime.ToString("O")}\r\n " +
                                         $"serverReceiveTime={receiveTime.ToString("O")}\r\n" +
                                         $"serverTransmitTime={transmitTime.ToString("O")}\r\n" +
                                         $"offset={offset.TotalMilliseconds}ms\r\n" +
                                         $"error={errorMilliseconds}ms");
            }
            catch (Exception e)
            {
                CommonUtilities.WriteLog(e.Message);
                MessageBox.Show("同步失败\r\n" + e.Message);
                throw e;

            }

            return offset;



        }

        private DateTime byteToTime(byte[] timeBytes)
        {
            var intPart = ((ulong) timeBytes[0] << 24) | ((ulong) timeBytes[1] << 16) | ((ulong) timeBytes[2] << 8) |
                          timeBytes[3];
            var fractPart = ((ulong) timeBytes[4] << 24) | ((ulong) timeBytes[5] << 16) | ((ulong) timeBytes[6] << 8) |
                            timeBytes[7];

            var milliseconds = intPart * 1000 + fractPart * 1000 / 0x100000000L;
            var networkDateTime = new DateTime(1900, 1, 1).AddMilliseconds((long) milliseconds);
            return networkDateTime;
        }
    }
}