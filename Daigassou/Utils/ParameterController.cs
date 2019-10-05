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
        public Queue<TimedNote> NetSyncQueue { get; }
        public Queue<TimedNote> LocalPlayQueue { get; }
        public volatile int InternalOffset;
        public volatile int Offset;
        public int Pitch { get; set; }
        public int Speed { get; set; }
        public bool NeedSync { get; set; } = true;
        private DateTime lastSentTime;
        private Timer offsetTimer;

        private ParameterController()
        {
            NetSyncQueue = new Queue<TimedNote>();
            LocalPlayQueue = new Queue<TimedNote>();
            offsetTimer=new Timer(1300);
            offsetTimer.AutoReset = false;
            
            offsetTimer.Elapsed += OffsetTimer_Elapsed;
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

                offsetTimer.Enabled = false;
                var nowTime = DateTime.Now;
                lastSentTime = nowTime;

                var packetTime = 0;
                var ret = new Queue<TimedNote>();
                for (int i = 0; i < msg.Length; i++)
                {
                    if (msg[i] == 0xFF)
                    {
                        packetTime += Convert.ToInt32(msg[i + 1]);
                        if (i < msg.Length - 2 && msg[i + 2] != 0xFF && msg[i + 2] != 0xFE)
                        {
                            ret.Enqueue(new TimedNote() { Note = msg[i + 2], StartTime = nowTime + new TimeSpan(0, 0, 0, 0, packetTime) });
                        }
                    }
                }

                OffsetSync(packetTime);

                offsetTimer.Enabled = true;
#if true
                foreach (var timedNote in ret)
                {
                    timedNote.StartTime -= new TimeSpan(0, 0, 0, 0, packetTime);
                    Console.WriteLine(timedNote.ToString());
                }
#endif
                
            }

        }
        private void OffsetSync(int packetTime)
        {

            if (NeedSync)
            {
                Offset = InternalOffset+(500-packetTime);
                Console.WriteLine($"InternalOffset is sync to {Offset}");
                NeedSync = false;
            }

        }
    }
}
