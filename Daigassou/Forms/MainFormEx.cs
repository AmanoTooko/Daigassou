using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Daigassou.Utils;
using Sunny.UI;

using Newtonsoft.Json.Linq;

using BondTech.HotkeyManagement.Win;
using Melanchall.DryWetMidi.Interaction;
using WK.Libraries.HotkeyListenerNS;
using HotkeyEventArgs = NHotkey.HotkeyEventArgs;
using AutoUpdaterDotNET;
using Newtonsoft.Json;

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

            Utils.Utils.TimeSync();

            checkFileNameChanged();
            
            Utils.Utils.CheckForUpdate(AutoUpdaterOnParseUpdateInfoEvent);
            
            
            toolStripStatusLabel1.Text = "当前版本： "+System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            
            

        }
        private void AutoUpdaterOnParseUpdateInfoEvent(ParseUpdateInfoEventArgs args)
        {
            dynamic json = JsonConvert.DeserializeObject(args.RemoteData);
#if !DEBUG 
            
            NetworkParser.opcodeDict["countDownPacket"] = json.opcode.countDownPacket;
            NetworkParser.opcodeDict["ensembleStopPacket"] = json.opcode.ensembleStopPacket;
            NetworkParser.opcodeDict["partyStopPacket"] = json.opcode.partyStopPacket;
            NetworkParser.opcodeDict["ensembleStartPacket"] = json.opcode.ensembleStartPacket;
            NetworkParser.opcodeDict["InstruSendingPacket"] = json.opcode.InstruSendingPacket;
#endif
            args.UpdateInfo = new UpdateInfoEventArgs
            {
                CurrentVersion = json.version,
                ChangelogURL = json.changelog,
                DownloadURL = json.url,
                Mandatory = new Mandatory
                {
                    Value = json.mandatory.value,
                    UpdateMode = json.mandatory.mode,
                    MinimumVersion = json.mandatory.minVersion
                },
                CheckSum = new CheckSum
                {
                    Value = json.checksum.value,
                    HashingAlgorithm = json.checksum.hashingAlgorithm
                }
            };
            toolStripStatusLabel3.Text = "|更新检查完毕";
        }
        private void checkFileNameChanged()
        {
            var targetName = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName),
                "Daigassou.exe");

            if (File.Exists(targetName) && Process.GetCurrentProcess().MainModule?.FileName != "Daigassou.exe")
            {
                var v = FileVersionInfo.GetVersionInfo(targetName).FileVersion;
                if (v.ToString() != System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString())
                {
                    UIMessageDialog.ShowWarningDialog(this,"更新提示","因当前文件名被修改，更新后请打开Daigassou.exe！");
                    
                }

                

            }

            
        }

        private void HotkeyUtils_HotKeyPressed(object sender, HotkeyEventArgs e)
        {
            switch (e.Name)
            {
                case "Start":
                    Utils.Utils.SendMessageToAll(eventCata.MIDI_CONTROL_START_KEY,"");
                    SendParamToPage((int)PageID.SoloPlayPage, new CommObject() { eventId = eventCata.MIDI_CONTROL_START_KEY, payload = 1000 });
                    break;
                case "Stop":
                    Utils.Utils.SendMessageToAll(eventCata.MIDI_CONTROL_STOP, "");
                    SendParamToPage((int)PageID.SoloPlayPage, new CommObject() { eventId = eventCata.MIDI_CONTROL_STOP, payload = null });
                    break;
                case "PitchUp":
                    Utils.Utils.SendMessageToAll(eventCata.MIDI_CONTROL_PITCHUP, "");
                    SendParamToPage((int)PageID.SoloPlayPage, new CommObject() { eventId = eventCata.MIDI_CONTROL_PITCHUP, payload = null });
                    break;
                case "PitchDown":
                    Utils.Utils.SendMessageToAll(eventCata.MIDI_CONTROL_PITCHDOWN, "");
                    SendParamToPage((int)PageID.SoloPlayPage, new CommObject() { eventId = eventCata.MIDI_CONTROL_PITCHDOWN, payload = null });
                    break;

            }
            
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

        protected override void WndProc(ref Message m)

        {

            switch (m.Msg)

            {

                case WM_COPYDATA:

                    Utils.Utils.COPYDATASTRUCT cdata = new Utils.Utils.COPYDATASTRUCT();

                    Type mytype = cdata.GetType();

                    cdata = (Utils.Utils.COPYDATASTRUCT)m.GetLParam(mytype);

                    SendParamToPage((int)PageID.SoloPlayPage,
                        new CommObject() {eventId = eventCata.MIDI_FILE_NAME_CROSS,payload= cdata.lpData });




                    break;
                case 0x3378:
                    var result = (eventCata) m.LParam.ToInt32();
                    SendParamToPage((int)PageID.SoloPlayPage, new CommObject() {eventId = result, payload = null});
                    break;
                default:
                    
                    base.WndProc(ref m);

                    break;

            }

        }

        private void MainFormEx_Load(object sender, EventArgs e)
        {

                HotkeyUtils.GetInstance(this).InitHotKey();
                HotkeyUtils.GetInstance(this).HotKeyHandler += HotkeyUtils_HotKeyPressed;
         

        }
    }
}