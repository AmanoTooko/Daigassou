using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int Offset { get; }
        public int Pitch { get; }
        public int Speed { get; }

        private ParameterController()
        {
            NetSyncQueue=new Queue<TimedNote>();
            LocalPlayQueue=new Queue<TimedNote>();
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

        public void OffsetSync()
        {
            lock (locker)
            {
                
            }
        }
    }
}
