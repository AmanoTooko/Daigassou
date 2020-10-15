using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daigassou.Utils
{

    public class KeyplayClass
    {
        public string Filename="妹有名字";
        public int BPM = 80;
        public KeyplayTrackClass[] Tracks;
    }
    public class KeyplayTrackClass
    {
        public string name = "妹取名";
        public int instrumentId = -1;
        public Queue<KeyPlayList> notes;
    }
    public class constStringClass
    {
        public static string[] instrumentlist = { "妹有乐器", "竖琴", "钢琴", "鲁特琴", "提琴拨弦", "长笛", "双簧管", "单簧管", "横笛", "排箫", "定音鼓", "邦戈鼓", "低音鼓", "小军鼓", "镲", "小号", "长号", "大号", "圆号", "萨克斯管" };
        public static string IDtoString(int id)
        {
            if (id <= 0 || id > instrumentlist.Length)
            {
                return "妹有乐器";
            }
            else
            {
                return instrumentlist[id];
            }    
        }
       
    }
}
