using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Daigassou.Utils
{
    public class Instrument
    {
        public static readonly Dictionary<int, string> InstrumentName = new Dictionary<
            int,
            string
        >
        {
            {0, "妹有乐器"},
            {1, "竖琴"},
            {2, "钢琴"},
            {3, "鲁特琴"},
            {4, "提琴拨弦"},
            {5, "长笛"},
            {6, "双簧管"},
            {7, "单簧管"},
            {8, "横笛"},
            {9, "排箫"},
            {10, "定音鼓"},
            {11, "邦戈鼓"},
            {12, "低音鼓"},
            {13, "小军鼓"},
            {14, "镲"},
            {15, "小号"},
            {16, "长号"},
            {17, "大号"},
            {18, "圆号"},
            {19, "萨克斯管"},
            {20, "小提琴"},
            {21, "中提琴"},
            {22, "大提琴"},
            {23, "重音提琴"},
            {24, "过载吉他"},
            {25, "清音吉他"},
            {26, "闷音吉他"},
            {27, "强力和弦"},
            {28, "装逼吉他"}
        };

        public static readonly Dictionary<string, int> InstrumentAlias = new Dictionary<
            string,
            int
        >
        {
            {"竖琴", 1},
            {"shu qin", 1},
            {"shuqin", 1},
            {"harp", 1},
            {"hrp", 1},
            {"钢琴", 2},
            {"gang qin", 2},
            {"gangqin", 2},
            {"piano", 2},
            {"grand piano", 2},
            {"grand_piano", 2},
            {"grandpiano", 2},
            {"pno", 2},
            {"鲁特琴", 3},
            {"lu te qin", 3},
            {"luteqin", 3},
            {"steelguitar", 3},
            {"steel guitar", 3},
            {"steel_guitar", 3},
            {"guitar", 3},
            {"lute", 3},
            {"lt", 3},
            {"鲁特", 3},
            {"fiddle", 3},
            {"提琴拨弦", 4},
            {"ti qin bo xian", 4},
            {"tiqinboxian", 4},
            {"pizzicato", 4},
            {"拨弦", 4},
            {"pizz", 4},
            {"长笛", 5},
            {"chang di", 5},
            {"changdi", 5},
            {"flute", 5},
            {"fl", 5},
            {"双簧管", 6},
            {"shuang huang guan", 6},
            {"shuanghuangguan", 6},
            {"oboe", 6},
            {"ob", 6},
            {"单簧管", 7},
            {"dan huang guan", 7},
            {"danhuangguan", 7},
            {"clarinet", 7},
            {"cl", 7},
            {"横笛", 8},
            {"heng di", 8},
            {"hengdi", 8},
            {"piccolo", 8},
            {"fife", 8},
            {"picc", 8},
            {"排箫", 9},
            {"pai xiao", 9},
            {"paixiao", 9},
            {"panflute", 9},
            {"panpipe", 9},
            {"箫", 9},
            {"定音鼓", 10},
            {"ding yin gu", 10},
            {"dingyingu", 10},
            {"timpani", 10},
            {"timp", 10},
            {"邦戈鼓", 11},
            {"bang ge gu", 11},
            {"banggegu", 11},
            {"bongo", 11},
            {"低音鼓", 12},
            {"di yin gu", 12},
            {"diyingu", 12},
            {"bassdrum", 12},
            {"bd", 12},
            {"底鼓", 12},
            {"小军鼓", 13},
            {"xiao jun gu", 13},
            {"xiaojungu", 13},
            {"snare", 13},
            {"jungu", 13},
            {"军鼓", 13},
            {"sd", 13},
            {"镲", 14},
            {"cha", 14},
            {"cymbal", 14},
            {"擦", 14},
            {"cym", 14},
            {"小号", 15},
            {"xiao hao", 15},
            {"xiaohao", 15},
            {"trumpet", 15},
            {"tpt", 15},
            {"长号", 16},
            {"chang hao", 16},
            {"changhao", 16},
            {"trombone", 16},
            {"tbn", 16},
            {"大号", 17},
            {"da hao", 17},
            {"dahao", 17},
            {"tuba", 17},
            {"tba", 17},
            {"圆号", 18},
            {"yuan hao", 18},
            {"yuanhao", 18},
            {"horn", 18},
            {"frenchhorn", 18},
            {"hn", 18},
            {"萨克斯管", 19},
            {"sa ke si guan", 19},
            {"sakesiguan", 19},
            {"altosax", 19},
            {"saxophone", 19},
            {"saxo", 19},
            {"萨克斯", 19},
            {"sax", 19},
            {"小提琴", 20},
            {"xiao ti qin", 20},
            {"xiaotiqin", 20},
            {"violin", 20},
            {"vln", 20},
            {"中提琴", 21},
            {"zhong ti qin", 21},
            {"zhongtiqin", 21},
            {"viola", 21},
            {"vla", 21},
            {"大提琴", 22},
            {"da ti qin", 22},
            {"datiqin", 22},
            {"cello", 22},
            {"低音", 22},
            {"vc", 22},
            {"低音提琴", 23},
            {"di yin ti qin", 23},
            {"diyintiqin", 23},
            {"contrabass", 23},
            {"double bass", 23},
            {"bass", 23},
            {"doublebass", 23},
            {"cb", 23},
            {"电吉他,过载", 24},
            {"guo zai", 24},
            {"guozai", 24},
            {"overdriven", 24},
            {"过载", 24},
            {"OdGuitar", 24},
            {"egod", 24},
            {"ele_overdriven", 24},
            {"ele_over", 24},
            {"电吉他,清音", 25},
            {"qing yin", 25},
            {"qingyin", 25},
            {"clean", 25},
            {"清音", 25},
            {"cleanguitar", 25},
            {"guitarclean", 25},
            {"egcl", 25},
            {"ele_clean", 25},
            {"电吉他,闷音", 26},
            {"men yin", 26},
            {"menyin", 26},
            {"mute", 26},
            {"闷音", 26},
            {"muteguitar", 26},
            {"egm", 26},
            {"ele_mute", 26},
            {"电吉他,重力和弦", 27},
            {"zhong li he xian", 27},
            {"zhonglihexian", 27},
            {"powerchord", 27},
            {"重力", 27},
            {"GGuitar", 27},
            {"egpc", 27},
            {"ele_powerchord", 27},
            {"电吉他,特殊奏法", 28},
            {"te shu zou fa", 28},
            {"teshuzoufa", 28},
            {"special", 28},
            {"ele_special", 28},
            {"特殊", 28},
            {"egfx", 28}
        };

        public int IntruID { get; set; }

        public override string ToString()
        {
            if (InstrumentName.Keys.Contains(IntruID))
                return InstrumentName[IntruID];
            return InstrumentName[0];
        }

        /// <summary>
        /// </summary>
        /// <param name="name">alias to instrucode </param>
        /// <returns>convert success</returns>
        public bool AliasToCode(string name)
        {
            var clearedName = name.ToLower();
            clearedName = Regex.Replace(clearedName, @"\[.*\]|\{.*\}|\(.*\)|【.*】|（.*）", ""); //FIlter []【】（）(){}


            if (InstrumentAlias.ContainsKey(clearedName))
            {
                IntruID = InstrumentAlias[clearedName];
                return true;
            }

            IntruID = 0;
            return false;
        }
    }
}