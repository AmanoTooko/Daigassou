using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;

namespace DaigassouDX.Controller
{
    public class MidiPlayController
    {
        public delegate void Playback_Finished_Notice();

        private int _offset;
        private int _pitch;
        private double _speed;
        private bool isRunning;
        public ProcessKeyController keyPlayer;
        private Playback playback;
        public Playback_Finished_Notice Playback_Finished_Notification;
        private readonly object playLock = new object();
        private Task playtask;
        public Process Process;

        public int Pitch
        {
            get => _pitch;
            set
            {
                _pitch = value;
                SetPitch(_pitch);
            }
        }

        public int Offset
        {
            get => _offset;
            set
            {
                _offset = value;
                SetOffset(_offset);
            }
        }

        public double Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                SetSpeed(_speed);
            }
        }

        public string GetProcess()
        {
            
            return (int)((MetricTimeSpan)playback.GetCurrentTime(TimeSpanType.Metric)).TotalMilliseconds + "//" + (int)((MetricTimeSpan)playback.GetDuration(TimeSpanType.Metric)).TotalMilliseconds;
        }
        public void update11()
        {
            
        }
        private void resetSetting()
        {
            _pitch = 0;
            _offset = 0;
            _speed = 0;
            isRunning = false;
            if (playback.OutputDevice == null)
            {
                keyPlayer.ReleaseAllKey();
            }
            else
            {
                playback.OutputDevice.Dispose();
            }
            
        }

        public void SetPlayback(Playback pb)
        {
            playback = pb;
            playback.InterruptNotesOnStop = false;
            playback.Finished += Playback_Finished;
            playback.EventPlayed += Playback_EventPlayed;
        }
        public void SetPlaybackForPreview(Playback pb,OutputDevice output)
        {
            playback = pb;
            playback.InterruptNotesOnStop = false;
            playback.Finished += Playback_Finished; 
            playback.OutputDevice = output;
        }
        public bool CheckPlaybackValid()
        {
            if (playback == null) return false;
            return true;
        }

        private void Playback_Finished(object sender, EventArgs e)
        {
            resetSetting();
            if (Playback_Finished_Notification != null) Playback_Finished_Notification();
        }

        public void SetSpeed(double speed)
        {
            playback.Speed = speed;
        }


        /// 功能清单
        /// 1. 播放 O 暂停 O 断点播 三角（合奏有可能无法保证同步） 
        /// 2. 任意点开始播放O / 跳转O /进度条拖放 O note on持续问题已解决
        /// 3. 属性控制：延迟调整 通过move位置 三角 move后静音？ 音调O 速度O
        /// 4. Midi键盘输入统一 三角，但可以重新开一个
        public async void StartPlay(int startOffset)
        {
            isRunning = true;
            if (playback != null)
            {
                await Task.Delay(startOffset);
                lock (playLock)
                {
                    if (isRunning) playback.Start();
                }
            }
        }

        public void StopPlay()
        {
            lock (playLock)
            {
                playback?.Stop();
                playback?.MoveToStart();
                resetSetting();
            }
        }


        public void PausePlay()
        {
            playback.Stop();
        }

        public void SetPreviewOffset(int offset)
        {
            if (isRunning)
            {
                if (offset > 0)
                {
                    playback.Stop();
                    //off掉key
                    playback.MoveForward(new MetricTimeSpan(offset * 1000));
                    playback.Start();
                }
                else
                {

                    playback.Stop();
                    //off掉key
                    playback.MoveBack(new MetricTimeSpan(offset * -1000));
                    playback.Start();
                }
            }
        }
        public void SetOffset(int offset)
        {
            if (isRunning)
            {
                if (offset > 0)
                {
                    playback.Stop();
                    //off掉key
                    playback.MoveForward(new MetricTimeSpan(offset * 1000));
                    playback.Start();
                }
                else
                {

                    new Task(() =>
                    {
                        lock (playLock)
                        {
                            playback.Stop();
                            Thread.Sleep(0 - offset);
                            playback.Start();
                        }
                    }).Start();
                }
            }
        }
        public void SetPitch(int p)
        {
            if (p <= 24 && p >= -24) _pitch = p;
        }


        private void Playback_EventPlayed(object sender, MidiEventPlayedEventArgs e)
        {
            switch (e.Event.EventType)
            {
                case MidiEventType.NoteOff:
                    keyPlayer.ReleaseKeyBoardByPitch(((NoteOffEvent) e.Event).NoteNumber + _pitch);
                    break;
                case MidiEventType.NoteOn:
                    keyPlayer.PressKeyBoardByPitch(((NoteOnEvent) e.Event).NoteNumber + _pitch);
                    break;
            }
        }
    }
}