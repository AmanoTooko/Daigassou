using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BondTech.HotkeyManagement.Win;
using Daigassou.Input_Midi;
using Daigassou.Properties;
using Daigassou.Utils;
using Newtonsoft.Json;
using RainbowMage.OverlayPlugin;
using Daigassou.Forms;
namespace Daigassou
{
    public partial class MainForm : Form
    {
        [DllImport("winmm.dll")] internal static extern uint timeBeginPeriod(uint period);
        [DllImport("winmm.dll")] internal static extern uint timeEndPeriod(uint period);
        private readonly KeyController kc = new KeyController();
        private readonly KeyBindFormOld keyForm22 = new KeyBindFormOld();
        private readonly KeyBindForm8Key keyForm8 = new KeyBindForm8Key();
        private readonly MidiToKey mtk = new MidiToKey();
        private bool _playingFlag;
        private bool _runningFlag;
        private Thread _runningTask;
        private List<string> _tmpScore;
        private CancellationTokenSource cts = new CancellationTokenSource();
        internal HotKeyManager hkm;
        private ArrayList hotkeysArrayList;
        private int pauseTime = 0;
        private bool isCaptureFlag;
        private Queue<KeyPlayList> keyPlayLists;
        private NetworkClass net;
        
        public MainForm()
        {
            InitializeComponent();
            formUpdate();
            
            KeyBinding.LoadConfig();
            timeBeginPeriod(1);
            ThreadPool.SetMaxThreads(25, 50);
            Task.Run((Action)(() =>
            {
                CommonUtilities.GetLatestVersion();
                this.TimeSync();
            }));

            Text += $@" Ver{Assembly.GetExecutingAssembly().GetName().Version}";
            cbMidiKeyboard.DataSource = KeyboardUtilities.GetKeyboardList();
        }

        private void InitHotKeyBiding()
        {
            try
            {


                hkm = new HotKeyManager(this);
                hkm.Enabled = false;
                if (KeyBinding.hotkeyArrayList == null || KeyBinding.hotkeyArrayList.Count < 5)
                {
                   
                    hotkeysArrayList = new ArrayList();
                    hotkeysArrayList.Clear();
                    hotkeysArrayList.Add(
                        new GlobalHotKey(
                            "Start", Modifiers.Control, Keys.F10, true));
                    hotkeysArrayList.Add(
                        new GlobalHotKey(
                            "Stop", Modifiers.Control, Keys.F11, true));
                    hotkeysArrayList.Add(
                        new GlobalHotKey(
                            "PitchUp", Modifiers.Control, Keys.F8, true));
                    hotkeysArrayList.Add(
                        new GlobalHotKey(
                            "PitchDown", Modifiers.Control, Keys.F9, true));
                    hotkeysArrayList.Add(
                        new GlobalHotKey(
                            "Pause", Modifiers.Control, Keys.F12, true));
                    KeyBinding.hotkeyArrayList = hotkeysArrayList;
                }
                else
                {
                    hotkeysArrayList = KeyBinding.hotkeyArrayList;
                }

                {
                    ((GlobalHotKey) hotkeysArrayList[0]).HotKeyPressed += Start_HotKeyPressed;
                    ((GlobalHotKey) hotkeysArrayList[1]).HotKeyPressed += Stop_HotKeyPressed;
                    ((GlobalHotKey) hotkeysArrayList[2]).HotKeyPressed += PitchUp_HotKeyPressed;
                    ((GlobalHotKey) hotkeysArrayList[3]).HotKeyPressed += PitchDown_HotKeyPressed;
                    ((GlobalHotKey) hotkeysArrayList[4]).HotKeyPressed += Pause_HotKeyPressed;
                }
                var ret = true;
                foreach (GlobalHotKey k in hotkeysArrayList)
                {

                    if (k.Enabled)
                    {
                        try
                        {
                            hkm.AddGlobalHotKey(k);
                        }
                        catch (Exception e)
                        {
                            
                            ret = false;
                        }

                    }

                }

                if (ret == false)
                {
                    
                    throw new Exception();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(
                    $"一些快捷键无法注册,程序可能无法正常运行。\r\n请检查是否有其他程序占用。\r\n点击下方小齿轮重新配置快捷键+{JsonConvert.SerializeObject(hotkeysArrayList)}",
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                hkm.Enabled = true;
            }
        }

        private void Pause_HotKeyPressed(object sender, GlobalHotKeyEventArgs e)
        {
            if (!this.kc.isRunningFlag || !this.kc.isPlayingFlag)
                return;
            Daigassou.Utils.Log.overlayLog("快捷键：演奏暂停");
            this.pauseTime = Environment.TickCount;
            this.kc.isPlayingFlag = false;
        }

        private void formUpdate()
        {
            if (Settings.Default.IsEightKeyLayout)
            {
                btn8key.BackgroundImage = Resources.ka1;
                btn22key.BackgroundImage = Resources.kb0;
                btnSwitch.BackgroundImage = Resources.a0;
                btn8key.Enabled = true;
                btn22key.Enabled = false;
            }
            else
            {
                btn8key.BackgroundImage = Resources.ka0;
                btn22key.BackgroundImage = Resources.kb1;
                btnSwitch.BackgroundImage = Resources.a1;
                btn8key.Enabled = false;
                btn22key.Enabled = true;
            }
        }

        private void Start_HotKeyPressed(object sender, GlobalHotKeyEventArgs e)
        {
            if (!this.kc.isRunningFlag)
            {
                Daigassou.Utils.Log.overlayLog("快捷键：演奏开始");
                this.StartKeyPlayback(1000);
            }
            else
            {
                Daigassou.Utils.Log.overlayLog("快捷键：演奏恢复");
                this.kc.isPlayingFlag = true;
                this.kc.pauseOffset += Environment.TickCount - this.pauseTime;
            }
        }

        private void Stop_HotKeyPressed(object sender, GlobalHotKeyEventArgs e)
        {
            Log.overlayLog($"快捷键：演奏停止");
            StopKeyPlay();

            

        }
        private void StopKeyPlay()
        {
            _runningFlag = false;            
            _runningTask?.Abort();
            while (_runningTask!=null&&
                _runningTask.ThreadState != System.Threading.ThreadState.Stopped &&
                _runningTask.ThreadState != System.Threading.ThreadState.Aborted) Thread.Sleep(1);
            kc.ResetKey();
        }
        private void PitchUp_HotKeyPressed(object sender, GlobalHotKeyEventArgs e)
        {

                ParameterController.GetInstance().Pitch += 1;
                Log.overlayLog($"快捷键：向上移调 当前 {ParameterController.GetInstance().Pitch}");
                
        }

        private void PitchDown_HotKeyPressed(object sender, GlobalHotKeyEventArgs e)
        {

                ParameterController.GetInstance().Pitch -= 1;
                Log.overlayLog($"快捷键：向下移调 当前 {ParameterController.GetInstance().Pitch}");
            
                
        }


        private void StartKeyPlayback(int interval)
        {
            kc.isPlayingFlag = false;
            kc.isRunningFlag = false;
            kc.pauseOffset = 0;
            if (Path.GetExtension(midFileDiag.FileName) != ".mid" && Path.GetExtension(midFileDiag.FileName) != ".midi")
            {
                Log.overlayLog($"错误：没有Midi文件");
                MessageBox.Show("没有midi你演奏个锤锤？", "喵喵喵？", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }

            if (_runningTask==null||
                _runningTask.ThreadState!= System.Threading.ThreadState.Running&&
                _runningTask.ThreadState != System.Threading.ThreadState.Suspended)
            {
                _runningTask?.Abort();

                this.kc.isPlayingFlag = true;
                var Interval = interval < 1000 ? 1000 : interval;
                var sub = (long) (1000 - interval);

                //timer1.Start();
                var sw = new Stopwatch();
                sw.Start();
                Log.overlayLog($"文件名：{Path.GetFileName(midFileDiag.FileName)}");
                Log.overlayLog($"定时：{Interval}毫秒后演奏");
                mtk.OpenFile(midFileDiag.FileName);
                mtk.GetTrackManagers();
                keyPlayLists = mtk.ArrangeKeyPlaysNew((double)(mtk.GetBpm() / nudBpm.Value));
                if (interval<0)
                {
                    var keyPlay=keyPlayLists.Where((x)=>x.TimeMs> sub);
                    keyPlayLists=new Queue<KeyPlayList>();
                    foreach (KeyPlayList kp in keyPlay)
                    {
                        kp.TimeMs -= sub;
                        keyPlayLists.Enqueue(kp);
                    }
                }
                sw.Stop();
                _runningFlag = true;
                cts = new CancellationTokenSource();
                _runningTask = createPerformanceTask(cts.Token, interval-(int)sw.ElapsedMilliseconds);//minus bug?
                _runningTask.Priority = ThreadPriority.Highest;

            }


        }

        private Thread createPerformanceTask(CancellationToken token,int startOffset)
        {
            ParameterController.GetInstance().InternalOffset = (int)numericUpDown2.Value;
            ParameterController.GetInstance().Offset = 0;
            Thread thread = new Thread(
                () => {
                    kc.KeyPlayBack(keyPlayLists, 1, cts.Token, startOffset);
                    _runningFlag = false;
                }
                );
            thread.Start();
            return thread;

        }

        private void trackComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mtk.Index = trackComboBox.SelectedIndex;
        }


        private void selectFileButton_Click(object sender, EventArgs e)
        {
            if (midFileDiag.ShowDialog() == DialogResult.OK)
                mtk.OpenFile(midFileDiag.FileName);
            else
                return;

            pathTextBox.Text = Path.GetFileName( midFileDiag.FileName);
            Log.overlayLog($"打开文件：{Path.GetFileName(midFileDiag.FileName)}");
            _tmpScore = mtk.GetTrackManagers(); //note tracks
            var bpm = mtk.GetBpm();
            var tmp = new List<string>();

            if (_tmpScore != null)
                for (var i = 0; i < _tmpScore.Count; i++)
                    tmp.Add($"track_{i}:{_tmpScore[i]}");


            trackComboBox.DataSource = tmp;
            trackComboBox.SelectedIndex =0;
            //TODO: if source midi not imported successfully will cause error
            //TODO: Enhancement issue#14 lock track selection
            if (bpm >= nudBpm.Maximum)
                nudBpm.Value = nudBpm.Maximum;
            else if (bpm <= nudBpm.Minimum)
                nudBpm.Value = nudBpm.Minimum;
            else
                nudBpm.Value = bpm;
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            mtk.Offset = EnumPitchOffset.OctaveLower;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            mtk.Offset = EnumPitchOffset.None;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            mtk.Offset = EnumPitchOffset.OctaveHigher;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            mtk.Bpm = (int) nudBpm.Value;
            
        }

        private void SyncButton_Click(object sender, EventArgs e)
        {
            var interval = dateTimePicker1.Value - DateTime.Now;
            StartKeyPlayback((int) interval.TotalMilliseconds + (int) numericUpDown2.Value);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            KeyboardUtilities.Disconnect();
            hkm.Enabled = false;
            ArrayList tmp=new ArrayList();
            foreach (GlobalHotKey a in hkm.EnumerateGlobalHotKeys)
            {
                tmp.Add(a);
            }

            foreach (GlobalHotKey VARIABLE in tmp)
            {
                hkm.RemoveGlobalHotKey(VARIABLE);
            }
            hkm.Dispose();
            if (a!=null)
            {
                if (a.f!=null)
                {
                    a.f.Dispose();
                    RainbowMage.HtmlRenderer.Renderer.Shutdown();
                }
                
            }
            StopKeyPlay();
            timeEndPeriod(1);
            
        }

        private void keyForm13Button_Click(object sender, EventArgs e)
        {
            keyForm8.ShowDialog();
        }

       


        private void btnKeyboardConnect_Click(object sender, EventArgs e)
        {
            if (cbMidiKeyboard.SelectedItem != null)
                if (cbMidiKeyboard.Enabled)
                {
                    if (KeyboardUtilities.Connect(cbMidiKeyboard.SelectedItem.ToString(), kc) == 0)
                    {
                        cbMidiKeyboard.Enabled = false;
                        btnKeyboardConnect.BackgroundImage = Resources.btn2;
                    }
                }
                else
                {
                    KeyboardUtilities.Disconnect();
                    cbMidiKeyboard.Enabled = true;
                    cbMidiKeyboard.DataSource = KeyboardUtilities.GetKeyboardList();
                    btnKeyboardConnect.BackgroundImage = Resources.btn1;
                }
        }


        protected override void WndProc(ref Message m)
        {
            try
            {
                const int WM_DEVICECHANGE = 0x0219;
                switch (m.Msg)
                {
                    case WM_DEVICECHANGE:
                        cbMidiKeyboard.DataSource = KeyboardUtilities.GetKeyboardList();
                        break;
                }
            }

            catch (Exception ex)
            {
            }

            base.WndProc(ref m);
        }

        private void cbMidiKeyboard_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cbMidiKeyboard.DataSource = KeyboardUtilities.GetKeyboardList();
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            if (Settings.Default.IsEightKeyLayout)
                Settings.Default.IsEightKeyLayout = false;
            else
                Settings.Default.IsEightKeyLayout = true;

            Settings.Default.Save();
            formUpdate();
        }

        private void btn22key_Click(object sender, EventArgs e)
        {
            keyForm22.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            new AboutForm(kc).ShowDialog();
        }


        private void TimeSync()
        {
            double error;
            try
            {
                Log.overlayLog($"时间同步：NTP请求发送");
                var offset = new NtpClient(Settings.Default.NtpServer).GetOffset(out error);
                Log.overlayLog($"时间同步：与北京时间相差{offset.Milliseconds}毫秒");
                //TODO:error handler
                if (CommonUtilities.SetSystemDateTime.SetLocalTimeByStr(
                    DateTime.Now.AddMilliseconds(offset.TotalMilliseconds * -0.5)))
                    tlblTime.Text = "本地时钟已同步";
            }
            catch (Exception e)
            {
                tlblTime.Text = "设置时间出错";
                
            }

           
                
        }


        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (mtk.PlaybackStart((int) nudBpm.Value) == 0)
            {
                btnPlay.BackgroundImage = Resources.c_play_1;
                btnPause.BackgroundImage = Resources.c_pause;
                btnStop.BackgroundImage = Resources.c_stop;

                lblPlay.Text = "正在试听";
                lblMidiName.Text = Path.GetFileNameWithoutExtension(midFileDiag.FileName);
                _playingFlag = true;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (mtk.PlaybackRestart() == 0)
            {
                btnPlay.BackgroundImage = Resources.c_play;
                btnPause.BackgroundImage = Resources.c_pause;
                btnStop.BackgroundImage = Resources.c_stop_1;
                lblPlay.Text = "试听已停止";
                timeLabel.Text = "";
                _playingFlag = false;
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (mtk.PlaybackPause() == 0)
            {
                btnPlay.BackgroundImage = Resources.c_play;
                btnPause.BackgroundImage = Resources.c_pause_1;
                btnStop.BackgroundImage = Resources.c_stop;
                lblPlay.Text = "试听暂停";
                _playingFlag = false;
            }
        }

        private void BtnTimeSync_Click(object sender, EventArgs e)
        {

            if (isCaptureFlag)
            {
                net._shouldStop = true;                
                isCaptureFlag = false;
                (sender as Button).Text = "开始同步";
                (sender as Button).BackColor = Color.FromArgb(255, 128, 128);
            }
            else
            {
                TimeSync();
                
                
                net = new NetworkClass();
                net.Play += Net_Play;
                try
                {
                    List<Process> ffxivProcess = FFProcess.FindFFXIVProcess();
                    if (ffxivProcess.Count == 1)
                    {
                        Task.Run((Action)(() => this.net.Run((uint)FFProcess.FindFFXIVProcess().First<Process>().Id)));
                        this.isCaptureFlag = true;
                        (sender as Button).Text = "停止同步";
                        (sender as Button).BackColor = Color.Aquamarine;
                    }
                    else if (ffxivProcess.Count >= 2)
                    {
                        uint id = 0;
                        PidSelect pidSelect = new PidSelect();
                        pidSelect.GetPid += (PidSelect.PidSelector)(x => id = (uint)x);
                        int num = (int)pidSelect.ShowDialog();
                        Task.Run((Action)(() => this.net.Run(id)));
                        this.isCaptureFlag = true;
                        (sender as Button).Text = "停止同步";
                        (sender as Button).BackColor = Color.Aquamarine;
                    }
                    else
                    {
                        MessageBox.Show("你开游戏了吗？", "喵喵喵？", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                }
                catch (Exception exception)
                {
                }
            }
        }
        private delegate void remotePlay(int time, string name);
        private void Net_Play(object sender, PlayEvent e)
        {
            if (this.InvokeRequired)
            {
                if (e.Mode==0)
                {
                    var n = new remotePlay(NetPlay);
                    this.Invoke(n, e.Time, e.Text);
                }
                else if (e.Mode == 1)
                {
                    var n = new remotePlay(NetStop);
                    this.Invoke(n, e.Time, e.Text);
                }
                
            }
            else
            {
                if (e.Mode == 0)
                {
                    NetPlay(e.Time, e.Text);
                }
                else if (e.Mode == 1)
                {
                    NetStop(e.Time, e.Text);
                }
                
            }
            
        }

        private void NetPlay(int time,string name)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            DateTime dt = startTime.AddSeconds(time);

            dateTimePicker1.Value = dt;
            var msTime = (dt - DateTime.Now).TotalMilliseconds;
            StartKeyPlayback((int)msTime + (int)numericUpDown2.Value);
            Log.overlayLog($"网络控制：{name.Trim().Replace("\0", string.Empty)}发起倒计时，目标时间:{dt.ToString("HH:mm:ss")}");
            tlblTime.Text = $"{name.Trim().Replace("\0",string.Empty)}发起倒计时:{msTime}毫秒";
            //if (ParameterController.GetInstance().isEnsembleSync)
            //{
            //    System.Threading.Timer timer1 = new System.Threading.Timer((TimerCallback)(x => this.kc.KeyboardPress(48)), new object(), 2000, 0);
            //    System.Threading.Timer timer2 = new System.Threading.Timer((TimerCallback)(x => this.kc.KeyboardRelease(48)), new object(), 2050, 0);

            //}


        }

        private void NetStop(int time, string name)
        {
            StopKeyPlay();
            Log.overlayLog($"网络控制：{name.Trim().Replace("\0", string.Empty)}停止了演奏");
            tlblTime.Text = $"{name.Trim().Replace("\0", string.Empty)}停止了演奏";
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            if (_playingFlag) timeLabel.Text = mtk.PlaybackInfo();

            timeStripStatus.Text = DateTime.Now.ToString("T");
        }


        private void DateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && !e.Shift && e.Control && e.KeyCode == Keys.V)
            {
                var targetTime = Convert.ToString(Clipboard.GetDataObject().GetData(DataFormats.Text));
                try
                {
                    var dt = DateTime.ParseExact(targetTime, "HH:mm:ss", CultureInfo.CurrentCulture);
                    dateTimePicker1.Value = dt;
                }
                catch (Exception)
                {
                }
            }
            else if (!e.Alt && !e.Shift && e.Control && e.KeyCode == Keys.C)
            {
                var targetTime = dateTimePicker1.Value.ToString("HH:mm:ss");
                Clipboard.SetDataObject(targetTime);
            }
        }


        private void TrackComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.Control && e.Shift && e.KeyCode == Keys.S) mtk.SaveToFile();
        }

        private void ToolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            
            var form=new ConfigForm(hotkeysArrayList,kc,hkm);
            form.ShowDialog();
        }



        private void MainForm_Load(object sender, EventArgs e)
        {
            InitHotKeyBiding();
        }

        private StatusOverlay.OverlayControl a;
        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            try
            {
                if (a != null)
                {
                    a.config.IsVisible = !a.config.IsVisible;
                }
                else
                {
                    a = new StatusOverlay.OverlayControl();
                    a.InitializeOverlays(PointToScreen(new Point(this.Width, this.Height - 150)));
                    Log.log = a.config;
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("悬浮窗库文件似乎不完全", "你是只傻肥！", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }
            
        }
}
}