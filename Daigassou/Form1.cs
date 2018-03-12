using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Daigassou
{
    public partial class Form1 : Form
    {
        private MidiToKey mtk;
        private List<string> tmpScore;
        public Form1()
        {
            InitializeComponent();
            
            HotKeys.Regist(new Window().Handle,(int)HotKeys.HotkeyModifiers.None,Keys.F4, testFunction);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mtk=new MidiToKey();
            mtk.OpenFile(@"C:\Users\tooko\Documents\MuseScore2\乐谱\小星星.mid");
            tmpScore  = mtk.getTrackManagers();
            List<string> tmp=new List<string>();
            for (int i = 0; i < tmpScore.Count; i++)
            {
                tmp.Add("track"+i.ToString());
            }

            trackComboBox.DataSource = tmp;



        }

        private void testFunction()
        {
            Queue<KeyPlayList> a=new Queue<KeyPlayList>();
            a.Enqueue(new KeyPlayList(Keys.W,1));
            a.Enqueue(new KeyPlayList(Keys.S, 1));
            a.Enqueue(new KeyPlayList(Keys.Space, 2));

            Task t=new  Task(() => { KeyController.KeyPlayBack(a, 75); });
            t.Start();
            
        }

        private void trackComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            noteTextBox.Text = tmpScore[trackComboBox.SelectedIndex];
        }
    }
}
