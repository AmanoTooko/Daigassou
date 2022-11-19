using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Daigassou.Utils;
using Daigassou.Controller;
using Sunny.UI;
using System.Xml.Linq;


namespace Daigassou.Forms
{
    public partial class PlayPage : UIPage
    {
        MidiFileParser midiFileParser;
        private Instrument instru;
        public PlayPage()
        {
            InitializeComponent();
            midiFileParser = new MidiFileParser();
            instru = new Instrument();
        }
        public override void Init()
        {
            base.Init();
            uiLine1.ForeColor = Color.FromArgb(255, 113, 128);
            uiLine2.ForeColor = Color.FromArgb(255, 113, 128);
        }

        public override void Final()
        {
            base.Final();

        }

        private void PlayForm_ReceiveParams(object sender, UIPageParamsArgs e)
        {
            Console.Write(e.Value);
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            
            this.SendParamToPage((int) PageID.SettingPage,"测试+1");
        }

        private void uiTrackBar1_ValueChanged(object sender, EventArgs e)

        {
            var speed = tbSpeed.Value > 50 ? 1 + (tbSpeed.Value - 50) / 50.0 : 0.5 + (tbSpeed.Value) / 100.0;
            lblSpeed.Text = $"当前速度 {speed.ToString("F2")} 倍";
        }

        private void tbKey_ValueChanged(object sender, EventArgs e)
        {
           
            lblKey.Text = $"当前音高  {tbKey.Value}";

            
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                //发送midi信息
                tbFilename.Text = openFileDialog1.FileName.Split("\\").Last();

                SendParamToPage((int)PageID.MultiPlayPage,
                    new CommObject() {eventId = eventCata.MIDI_FILE_NAME, payload = openFileDialog1.FileName});
                SendParamToPage((int)PageID.PreviewPlayPage,
                    new CommObject() { eventId = eventCata.MIDI_FILE_NAME, payload = openFileDialog1.FileName });
                //开始读取track信息

                midiFileParser.OpenFile(openFileDialog1.FileName);
                var trackNameList = midiFileParser.GetTrackNames();

                for (int i = 0; i < trackNameList.Count; i++)
                {
                   var name = trackNameList[i];
                    if (instru.AliasToCode(name))
                    {
                        trackNameList[i] = $"[{instru.ToString()}]{name}";
                    }
                }
                
                cbTrackname.DataSource = trackNameList;
            }
        }

        private void cbTrackname_SelectedIndexChanged(object sender, EventArgs e)
        {
            SendParamToPage((int)PageID.MultiPlayPage,
                new CommObject() { eventId = eventCata.TRACK_FILE_NAME, payload = $"{cbTrackname.SelectedIndex}|{cbTrackname.SelectedItem}" });
            SendParamToPage((int)PageID.PreviewPlayPage,
                new CommObject() { eventId = eventCata.TRACK_FILE_NAME, payload = $"{cbTrackname.SelectedIndex}|{cbTrackname.SelectedItem}" });
        }
    }
}
