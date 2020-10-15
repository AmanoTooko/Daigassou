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
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Core;

namespace Daigassou
{
    public partial class MainForm : Form
    {
        [DllImport("winmm.dll")] internal static extern uint timeBeginPeriod(uint period);
        [DllImport("winmm.dll")] internal static extern uint timeEndPeriod(uint period);
        private readonly KeyController kc = new KeyController();
        private readonly KeyBindFormOld keyForm37 = new KeyBindFormOld();
        private readonly KeyBindForm8Key keyForm13 = new KeyBindForm8Key();
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
        private int trackLock = 0;
        private bool netMidiFlag = false;
        public MainForm()
        {
            InitializeComponent();
            formUpdate();
            KeyBinding.LoadConfig();
            timeBeginPeriod(1);
            
            Task.Run((Action)(() =>
            {
                CommonUtilities.GetLatestVersion();
                this.TimeSync();
            }));
            
            Text += $@" Ver{Assembly.GetExecutingAssembly().GetName().Version} ";
            cbMidiKeyboard.DataSource = KeyboardUtilities.GetKeyboardList();
            kc.stopHandler += StopKeyPlay;
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
                MessageBox.Show(new Form() { TopMost = true },
                    $"部分快捷键注册失败,程序可能无法正常运行。\r\n请检查是否有其他程序占用。\r\n点击下方小齿轮重新配置快捷键",
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
                btn13Key.ForeColor = Color.WhiteSmoke;
                btn37Key.ForeColor = Color.WhiteSmoke;
                btn13Key.BackColor = Color.FromArgb(255, 110, 128);
                btn37Key.BackColor = Color.Silver;
                btnSwitch.BackgroundImage = Resources.a0;
                btn13Key.Enabled = true;
                btn37Key.Enabled = false;
            }
            else
            {
                btn13Key.ForeColor = Color.WhiteSmoke;
                btn37Key.ForeColor = Color.WhiteSmoke;
                btn37Key.BackColor = Color.FromArgb(255, 110, 128);
                btn13Key.BackColor = Color.Silver;
                btnSwitch.BackgroundImage = Resources.a1;
                btn13Key.Enabled = false;
                btn37Key.Enabled = true;
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
            lyricPoster.LrcStop();
            mtk.midiPlay?.Stop();
            btnSyncReady.BackColor = Color.FromArgb(255, 110, 128);
            btnSyncReady.Text = "准备好了";
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
            ParameterController.GetInstance().Pitch = 0;
            if (midFileDiag.FileName==string.Empty)
            {
                Log.overlayLog($"错误：没有Midi文件");
                MessageBox.Show(new Form() { TopMost = true }, "没有midi你演奏个锤锤？", "喵喵喵？", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }

            if (_runningTask==null||
                _runningTask.ThreadState!= System.Threading.ThreadState.Running&&
                _runningTask.ThreadState != System.Threading.ThreadState.Suspended)
            {
                _runningTask?.Abort();

                this.kc.isPlayingFlag = true;
                btnSyncReady.BackColor = Color.Aquamarine;
                btnSyncReady.Text = "中断演奏";
                var Interval = interval < 1000 ? 1000 : interval;
                var sub = (long) (1000 - interval);
                int bpm = 120;
                //timer1.Start();
                var sw = new Stopwatch();
                sw.Start();
                Log.overlayLog($"文件名：{Path.GetFileName(midFileDiag.FileName)}");
                Log.overlayLog($"定时：{Interval}毫秒后演奏");
                if (ParameterController.GetInstance().isEnsembleSync)
                {
                    System.Threading.Timer timer1 = new System.Threading.Timer((TimerCallback)(x => this.kc.KeyboardPress(48)), new object(), Interval - 4000, 0);
                    System.Threading.Timer timer2 = new System.Threading.Timer((TimerCallback)(x => this.kc.KeyboardRelease(48)), new object(), Interval - 3950, 0);
                    Log.overlayLog($"定时：同步音按下");
                }
                if(netMidiFlag)
                {
                    keyPlayLists = mtk.netmidi?.Tracks[trackComboBox.SelectedIndex].notes;
                    bpm = mtk.netmidi.BPM;
                }
                else
                {
                    
                    OpenFile(midFileDiag.FileName);
                    bpm = mtk.GetBpm();
                    mtk.GetTrackManagers();
                    keyPlayLists = mtk.ArrangeKeyPlaysNew((double)(bpm / nudBpm.Value));
                }


                lyricPoster.LrcStart(midFileDiag.FileName.Replace(".mid", ".mml").Replace(".mml", ".lrc"), interval);
                File.WriteAllText($"1.txt", JsonConvert.SerializeObject(keyPlayLists));
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
                if (Settings.Default.isUsingAnalysis||netMidiFlag==true)
                    _runningTask = createPerformanceTask(cts.Token, interval - (int)sw.ElapsedMilliseconds);//minus bug?
                else
                    _runningTask = createPerformanceTaskOriginal(cts.Token, (double)(nudBpm.Value / bpm));
                _runningTask.Priority = ThreadPriority.Highest;

            }


        }

        private void OpenFile(string fileName)
        {
            switch (Path.GetExtension(fileName))
            {
                case ".mid":
                    mtk.OpenFile(fileName);
                    break;
                case ".mml":
                    var buffer = MmlMidiConventer.mmlRead(fileName);
                    if (buffer != null)
                        mtk.OpenFile(buffer);
                    break;
                default:
                    break;
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
        private Thread createPerformanceTaskOriginal(CancellationToken token,double speed)
        {
            ParameterController.GetInstance().InternalOffset = (int)numericUpDown2.Value;
            ParameterController.GetInstance().Offset = 0;
            
            Thread thread = new Thread(
                () => {
                    KeyboardUtilities.kc = kc;
                    kc.isRunningFlag = true;
                    mtk.PlaybackWithoutAnalysis(speed, P_EventPlayed, cts.Token);
                  
                    _runningFlag = false;
                }
                );
            thread.Start();
            return thread;

        }

        private void P_EventPlayed(object sender, MidiEventPlayedEventArgs e)
        {
            switch (e.Event)
            {
                case NoteOnEvent keyon:
                    KeyboardUtilities.NoteOn(keyon);
                    
                    break;
                case NoteOffEvent keyoff:
                    KeyboardUtilities.NoteOff(keyoff);
                    
                    break;
            }
        }


        private void trackComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mtk.Index = trackComboBox.SelectedIndex;
        }


        private void selectFileButton_Click(object sender, EventArgs e)
        {
            if (midFileDiag.ShowDialog() == DialogResult.OK)
            {
                OpenFile(midFileDiag.FileName);
            }
                
            else
                return;
            netMidiFlag = false;
            pathTextBox.Text = Path.GetFileName( midFileDiag.FileName);
            Log.overlayLog($"打开文件：{Path.GetFileName(midFileDiag.FileName)}");
            radioButton2.Checked = true;
            _tmpScore = mtk.GetTrackManagers(); //note tracks
            var bpm = mtk.GetBpm();
            var tmp = new List<string>();

            if (_tmpScore != null)
                for (var i = 0; i < _tmpScore.Count; i++)
                    tmp.Add($"track_{i}:{_tmpScore[i]}");


            trackComboBox.DataSource = tmp;
            trackComboBox.SelectedIndex =Math.Min(tmp.Count-1,trackLock);
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
            ParameterController.GetInstance().Pitch = EnumPitchOffset.OctaveLower - mtk.Offset;
            mtk.Offset = EnumPitchOffset.OctaveLower;
            Log.overlayLog($"[移调] 当前 {ParameterController.GetInstance().Pitch}");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ParameterController.GetInstance().Pitch = EnumPitchOffset.None - mtk.Offset;
            mtk.Offset = EnumPitchOffset.None;
            Log.overlayLog($"[移调] 当前 {ParameterController.GetInstance().Pitch}");

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            ParameterController.GetInstance().Pitch = EnumPitchOffset.OctaveHigher - mtk.Offset;
            mtk.Offset = EnumPitchOffset.OctaveHigher;
            Log.overlayLog($"[移调] 当前 {ParameterController.GetInstance().Pitch}");
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            mtk.Bpm = (int) nudBpm.Value;
            
        }

        private void SyncButton_Click(object sender, EventArgs e)
        {
            var interval = dtpSyncTime.Value - DateTime.Now;
            if (kc.isRunningFlag)
            {
                
                StopKeyPlay();
            }
            else
            {
                StartKeyPlayback((int)interval.TotalMilliseconds + (int)numericUpDown2.Value);
            }
            
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
            keyForm13.ShowDialog();
        }

       


        private void btnKeyboardConnect_Click(object sender, EventArgs e)
        {
            if (cbMidiKeyboard.SelectedItem != null)
                if (cbMidiKeyboard.Enabled)
                {
                    if (KeyboardUtilities.Connect(cbMidiKeyboard.SelectedItem.ToString(), kc) == 0)
                    {
                        cbMidiKeyboard.Enabled = false;
                        btnKeyboardConnect.BackColor = Color.Aquamarine;
                        btnKeyboardConnect.Text = "断开连接";
                    }
                }
                else
                {
                    KeyboardUtilities.Disconnect();
                    cbMidiKeyboard.Enabled = true;
                    cbMidiKeyboard.DataSource = KeyboardUtilities.GetKeyboardList();
                    btnKeyboardConnect.BackColor = Color.FromArgb(255,110,128);
                    btnKeyboardConnect.Text = "开始连接";
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

        private void btn37key_Click(object sender, EventArgs e)
        {
            keyForm37.ShowDialog();
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
        private void Playback_Finished(object sender, EventArgs e)
        {
            this.Invoke(new Action(()=> { btnStop_Click(sender, e); }));
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (mtk.PlaybackStart((int) nudBpm.Value,Playback_Finished) == 0)
            {
                mtk.PlaybackPercentSet(tbMidiProcess.Value);
                btnPlay.BackgroundImage = Resources.c_play_1;
                btnPause.BackgroundImage = Resources.c_pause;
                btnStop.BackgroundImage = Resources.c_stop;
                tbMidiProcess.Visible = true;
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
                tbMidiProcess.Value = 0;
                tbMidiProcess.Visible = false;
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
                (sender as Button).Text = "网络同步";
                (sender as Button).BackColor = Color.FromArgb(255, 110, 128);
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
                        MessageBox.Show(new Form() { TopMost = true }, "你开游戏了吗？", "喵喵喵？", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                }
                catch (Exception exception)
                {
                }
            }
        }
        private delegate void remotePlay(int time, string name);
        private delegate void updateForm(string text);
        
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
                else if (e.Mode==2)
                {
                    var n = new updateForm(upform);
                    this.Invoke(n, e.Text);
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

            dtpSyncTime.Value = dt;
             SyncButton_Click(new object(), new EventArgs());
            //var msTime = (dt - DateTime.Now).TotalMilliseconds;
            //StartKeyPlayback((int)msTime + (int)numericUpDown2.Value);
            Log.overlayLog($"网络控制：{name.Trim().Replace("\0", string.Empty)}发起倒计时，目标时间:{dt.ToString("HH:mm:ss")}");
            //tlblTime.Text = $"{name.Trim().Replace("\0",string.Empty)}发起倒计时:{msTime}毫秒";



        }
        private void upform(string text)
        {
            this.Text= $@"[{text}]大合奏！ Ver{Assembly.GetExecutingAssembly().GetName().Version} ";
        }
        private void NetStop(int time, string name)
        {
            StopKeyPlay();
            Log.overlayLog($"网络控制：{name.Trim().Replace("\0", string.Empty)}停止了演奏");
            tlblTime.Text = $"{name.Trim().Replace("\0", string.Empty)}停止了演奏";
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            if (_playingFlag)
            {
                timeLabel.Text = mtk.PlaybackInfo();
                var per= mtk.PlaybackPercentGet();
                tbMidiProcess.Value = per;
                
            }
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
                    dtpSyncTime.Value = dt;
                }
                catch (Exception)
                {
                }
            }
            else if (!e.Alt && !e.Shift && e.Control && e.KeyCode == Keys.C)
            {
                var targetTime = dtpSyncTime.Value.ToString("HH:mm:ss");
                Clipboard.SetDataObject(targetTime);
            }
        }


        private void TrackComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.Control && e.Shift && e.KeyCode == Keys.S) mtk.SaveToFile();
            else if (e.Alt && e.Control && e.Shift && e.KeyCode == Keys.W) mtk.SaveJsonToFile();
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
                MessageBox.Show(new Form() { TopMost = true }, "悬浮窗库文件似乎不完全", "你是只傻肥！", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }
            
        }

        private void btnSyncReady_MouseClick(object sender, MouseEventArgs e)
        {
            SyncButton_Click(sender, e);
        }

        private void label4_DoubleClick(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            var tempTime = DateTime.Now.AddMinutes(2);
            DateTime targetTime;
            if (tempTime.Minute%2==1)
            {
                targetTime = new DateTime(tempTime.Year, tempTime.Month, tempTime.Day, tempTime.Hour, tempTime.Minute - 1, 0);
            }
            else
            {
                targetTime = new DateTime(tempTime.Year, tempTime.Month, tempTime.Day, tempTime.Hour, tempTime.Minute , 0);

            }
            dtpSyncTime.Value = targetTime;
            SyncButton_Click(sender, e);
        }

        private void btnSyncReady_Click(object sender, EventArgs e)
        {

        }

        private void tbMidiProcess_Scroll(object sender, EventArgs e)
        {
            mtk.PlaybackPercentSet(tbMidiProcess.Value);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if(trackLock!=0)
            {
                trackLock = 0;
                label2.BackColor = Color.White;
            }
            else
            {
                trackLock = trackComboBox.SelectedIndex;
                label2.BackColor = Color.FromArgb(255, 110, 128);
            }    
        }

        private void label1_Click(object sender, EventArgs e)
        {
            var s = new SongSelect();
            string id="";
            s.Getid += (SongSelect.IdSelector)(x =>
            {
                id = x;
            });
            s.ShowDialog();
            if (id!=null)
            {
                var jsonscore=NetMidiDownload.DownloadMidi(id);
                try
                {
                    KeyplayClass keyplay = JsonConvert.DeserializeObject<KeyplayClass>(jsonscore);




                    pathTextBox.Text = keyplay.Filename;
                    Log.overlayLog($"网络文件：{Path.GetFileName(midFileDiag.FileName)}");
                    radioButton2.Checked = true;

                    var bpm = keyplay.BPM;
                    var tmp = new List<string>();

                    if (keyplay.Tracks?.Length != 0)
                        for (var i = 0; i < keyplay.Tracks.Length; i++)
                            tmp.Add($"track_{i}:{keyplay.Tracks[i].name}");


                    trackComboBox.DataSource = tmp;
                    trackComboBox.SelectedIndex = Math.Min(tmp.Count - 1, trackLock);
                    //TODO: if source midi not imported successfully will cause error
                    //TODO: Enhancement issue#14 lock track selection
                    if (bpm >= nudBpm.Maximum)
                        nudBpm.Value = nudBpm.Maximum;
                    else if (bpm <= nudBpm.Minimum)
                        nudBpm.Value = nudBpm.Minimum;
                    else
                        nudBpm.Value = bpm;
                    mtk.netmidi = keyplay;
                    netMidiFlag = true;

                }
                catch (Exception ee)
                {

                   throw ee ;
                }
            }
            
        }
    }
}