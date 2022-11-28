using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Daigassou.Utils;
using Sunny.UI;

namespace Daigassou.Forms
{
    public enum PageID
    {
        SoloPlayPage = 1001,
        MultiPlayPage = 1002,
        DevicePlayPage = 1003,
        PreviewPlayPage = 1004,
        SettingPage = 2001,
        KeyConfigPage = 3001
    }

    public partial class MainFormEx : UIForm
    {
        public MainFormEx()
        {
            InitializeComponent();
            //uiTabControl1.TabPages[0].Controls.Add(new PlayForm());
            var a = new PlayPage();
            UIStyles.InitColorful(Color.FromArgb(255, 141, 155), Color.White);
            AddPage(new PlayPage(), (int) PageID.SoloPlayPage);
            AddPage(new MuiltiPlayForm(), (int) PageID.MultiPlayPage);
            AddPage(new SettingPage(), (int) PageID.SettingPage);
            AddPage(new MidiDevicePage(), (int) PageID.DevicePlayPage);
            AddPage(new MidiPreviewPage(), (int) PageID.PreviewPlayPage);


            uiNavMenu1.SetNodePageIndex(uiNavMenu1.Nodes[0], (int) PageID.SoloPlayPage);
            uiNavMenu1.SetNodePageIndex(uiNavMenu1.Nodes[1], (int) PageID.MultiPlayPage);
            uiNavMenu1.SetNodePageIndex(uiNavMenu1.Nodes[2], (int) PageID.DevicePlayPage);
            uiNavMenu1.SetNodePageIndex(uiNavMenu1.Nodes[3], (int) PageID.SettingPage);
            uiNavMenu1.SetNodePageIndex(uiNavMenu1.Nodes[4], (int) PageID.PreviewPlayPage);
            //uiNavMenu1.SetNodePageIndex(uiNavMenu1.Nodes[1], (int)PageID.MultiPlayPage);
        }

        private void uiNavMenu1_MenuItemClick(TreeNode node, NavMenuItem item, int pageIndex)
        {
            //this.SendParamToPage(1002, "123");
            SelectPage(pageIndex);
        }

        private void MainFormEx_ReceiveParams(object sender, UIPageParamsArgs e)
        {
            UConsole.WriteConsole(e);
            this.Text = $"大合奏!!Ex - {Instrument.InstrumentName[Convert.ToInt32((e.Value as CommObject).payload)]}";
        }
        const int WM_COPYDATA = 0x004A;

        public struct COPYDATASTRUCT

        {

            public IntPtr dwData;

            public int cData;

            [MarshalAs(UnmanagedType.LPStr)]

            public string lpData;

        }
        protected override void WndProc(ref Message m)

        {

            switch (m.Msg)

            {

                case WM_COPYDATA:

                    COPYDATASTRUCT cdata = new COPYDATASTRUCT();

                    Type mytype = cdata.GetType();

                    cdata = (COPYDATASTRUCT)m.GetLParam(mytype);

                    SendParamToPage((int)PageID.SoloPlayPage,
                        new CommObject() {eventId = eventCata.MIDI_FILE_NAME_CROSS,payload= cdata.lpData });




                    break;

                default:

                    base.WndProc(ref m);

                    break;

            }

        }
    }
}