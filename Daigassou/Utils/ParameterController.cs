using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Daigassou.Utils
{
    internal class TimedNote
    {
        public DateTime StartTime;
        public int Note;
        public override string ToString()
        {
            return string.Format($"StartTime: {StartTime.ToString("HH:mm:ss.fff")}, Note: {Note.ToString("X2")}");
        }
    }
    internal class ParameterController
    {

        private static ParameterController Parameter;
        private static readonly object locker = new object();
        public Queue<TimedNote> NetSyncQueue { get; set; }
        public Queue<TimedNote> LocalPlayQueue { get; set; }
        public volatile int InternalOffset;
        public volatile int Offset;
        public int Pitch { get; set; }
        public int Speed { get; set; }
        public bool NeedSync { get; set; } = true;
        private DateTime lastSentTime;
        private Timer offsetTimer;
        public bool isEnsembleSync { get; set; } = false;
        public static uint countDownPacket = 110;//0x06e;
        public static uint ensembleStopPacket = 378;//0x017a;
        public static uint partyStopPacket = 233;// 0x0e9;
        public static uint ensembleStartPacket = 741;// 0x2e5;
        public static uint ensemblePacket = 571;// 0x23b;
        //public static uint ensembleConfirmPacket = 0x2e5;//369
        public static uint instruSendingPacket = 0x01FA;


        private ParameterController()
        {
            NetSyncQueue = new Queue<TimedNote>();
            LocalPlayQueue = new Queue<TimedNote>();

        }

        private void OffsetTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Offset = InternalOffset;
            Console.WriteLine($"Clear offset.now is {Offset}");
            lock (locker)
            {
                if ((DateTime.Now - lastSentTime).TotalMilliseconds > 1200)
                {
                    NeedSync = true;
                }
            }
        }

        public static ParameterController GetInstance()
        {
            lock (locker)
            {

                if (Parameter == null)
                {
                    Parameter = new ParameterController();
                }
            }

            return Parameter;
        }

        public void CheckSyncStatus()
        {
            lock (locker)
            {
                if ((DateTime.Now - lastSentTime).TotalMilliseconds > 1200)
                {
                    NeedSync = true;
                }
            }
        }

        internal void AnalyzeEnsembleNotes(byte[] msg)
        {
             //not yet implemented
        }
        internal void AnalyzeNotes(byte[] msg)
        {
            /*
             *音符包解析理论
             * 主要类型：间隔符0xFF 延迟符D Note符N
             * 规则：
             * 1 两个间隔符中间必定会有一个延迟符D,D最大为0XFA
             * 2 可能会存在Note符N，N一定在D后面
             * 3 数据包的第1byte大多数情况为间隔符，偶尔会有其他符号出现，此时应当直接忽略
             * 4 Note符分为普通Note和Note OFF,Note OFF为0xFE
             * 5 目前最小间隔为64=100ms 实测在105ms以上时如果系统两帧采集到key up 和Key down，才会有noteoff信号，否则只有note信号本身
             * 9 由于发送时可能会发生丢包现象，因此系统会在200ms左右再次发送同样的数据包
             */
            lock (locker)
            {
                Console.Write(DateTime.Now.ToString("hh:mm:ss.fff :"));
                foreach (var b in msg)
                {
                    Console.Write($"{b.ToString("X2")} ");
                }
                Console.WriteLine();
               

                lastSentTime = DateTime.Now;
                var startTime = lastSentTime + new TimeSpan(0, 0, 0, 0, -500);
                var packetTime = 0;

                for (int i = 0; i < msg.Length; i++)
                {
                    if (msg[i] != 0xFF && msg[i] != 0xFE)
                    {
                        packetTime = (i * 50);

                        LocalPlayQueue.Enqueue(new TimedNote() { Note = msg[i], StartTime = startTime + new TimeSpan(0, 0, 0, 0, packetTime) });

                    }
                }

                //OffsetSync(packetTime);

               
#if true
                while (LocalPlayQueue.Count > 0)
                {
                    var note = LocalPlayQueue.Dequeue();
                    while (NetSyncQueue.Any())
                    {
                        var netNote = NetSyncQueue.Dequeue();
                        if (note.Note == netNote.Note)
                        {
                            var offset = note.StartTime - netNote.StartTime;
                            if (offset.TotalMilliseconds > 75)
                            {

                                Log.overlayLog(note.ToString() + $"Offset={offset.TotalMilliseconds}");
                            }
                            break;
                        }

                    }

                }
#endif

            }

        }
        private void OffsetSync(int packetTime)
        {

            if (NeedSync)
            {
                Offset = InternalOffset + (500 - packetTime);
                Console.WriteLine($"InternalOffset is sync to {Offset}");
                Log.overlayLog($"网络同步:内部延迟同步至{Offset}毫秒");
                NeedSync = false;
            }

        }
    }
}
