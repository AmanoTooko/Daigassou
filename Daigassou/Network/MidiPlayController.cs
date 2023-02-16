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

        private readonly object playLock = new object();
        private int _offset;
        private int _pitch;
        private double _speed;
        private bool isRunning;
        public ProcessKeyController keyPlayer;
        private Playback playback;
        public Playback_Finished_Notice Playback_Finished_Notification;
        private Task playtask;
        public Process Process;

        public MidiPlayController()
        {
            keyPlayer = ProcessKeyController.GetInstance();
        }

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
            var totalMilliseconds =
                (int) ((MetricTimeSpan) playback.GetCurrentTime(TimeSpanType.Metric)).TotalMilliseconds;
            var str1 = totalMilliseconds.ToString();
            totalMilliseconds = (int) ((MetricTimeSpan) playback.GetDuration(TimeSpanType.Metric)).TotalMilliseconds;
            var str2 = totalMilliseconds.ToString();
            return str1 + "//" + str2;
        }

        private void resetSetting()
        {
            _pitch = 0;
            _offset = 0;
            _speed = 0.0;
            isRunning = false;
            if (playback?.OutputDevice == null)
                keyPlayer.ReleaseAllKey();
            else
            {
                playback?.OutputDevice.Dispose();
                playback.OutputDevice = null;

            }
                
        }

        public void SetPlayback(Playback pb)
        {
            playback = pb;
            playback.InterruptNotesOnStop = false;
            playback.Finished += Playback_Finished;
            playback.EventPlayed += Playback_EventPlayed;
        }

        public void SetPlaybackForPreview(Playback pb, OutputDevice output)
        {
            playback = pb;
            playback.InterruptNotesOnStop = false;
            playback.Finished += Playback_Finished;
            playback.OutputDevice = output;
        }

        public bool CheckPlaybackValid()
        {
            return playback == null;
        }

        private void Playback_Finished(object sender, EventArgs e)
        {
            resetSetting();
            if (Playback_Finished_Notification == null)
                return;
            Playback_Finished_Notification();
        }

        public void SetSpeed(double speed)
        {
            if (playback == null)
                return;
            if (playback.IsRunning)
            {
                playback.Stop();
                playback.Speed = speed;
                playback.Start();
            }
            else
            {
                playback.Speed = speed;
            }
        }

        public async void StartPlay(int startOffset)
        {
            isRunning = true;
            if (playback == null)
                return;
            await Task.Delay(startOffset);
            lock (playLock)
            {
                if (!isRunning)
                    return;
                playback.Start();
            }
        }

        public void StopPlay()
        {
            lock (playLock)
            {
                playback?.Stop();
                (playback?.OutputDevice as OutputDevice)?.TurnAllNotesOff();
                playback?.MoveToStart();
                resetSetting();
            }
        }

        public void PausePlay()
        {
            if ((MetricTimeSpan)playback.GetCurrentTime(TimeSpanType.Metric) ==new MetricTimeSpan(0))
            {
                isRunning = false;
            }

            playback?.Stop();
            (playback?.OutputDevice as OutputDevice)?.TurnAllNotesOff();
        }

        public void SetPreviewOffset(int offset)
        {
            if (!isRunning || playback == null)
                return;
            if (offset > 0)
            {
                playback.Stop();
                (playback?.OutputDevice as OutputDevice)?.TurnAllNotesOff();
                playback.MoveForward(new MetricTimeSpan(offset * 1000));
                playback.Start();
            }
            else
            {
                playback.Stop();
                (playback?.OutputDevice as OutputDevice)?.TurnAllNotesOff();
                playback?.MoveBack(new MetricTimeSpan(offset * -1000));
                playback?.Start();
            }
        }

        public void SetOffset(int offset)
        {
            if (!isRunning || playback == null)
                return;
            if (offset > 0)
            {
                playback.Stop();
                (playback?.OutputDevice as OutputDevice)?.TurnAllNotesOff();
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
                        (playback?.OutputDevice as OutputDevice)?.TurnAllNotesOff();
                        Thread.Sleep(-offset);
                        playback.Start();
                    }
                }).Start();
            }
        }

        public void SetPitch(int p)
        {
            if (p > 24 || p < -24)
                return;
            _pitch = p;
        }

        private void Playback_EventPlayed(object sender, MidiEventPlayedEventArgs e)
        {
            switch (e.Event.EventType)
            {
                case MidiEventType.NoteOff:
                    keyPlayer.ReleaseKeyBoardByPitch((byte) ((NoteEvent) e.Event).NoteNumber + _pitch);
                    break;
                case MidiEventType.NoteOn:
                    keyPlayer.PressKeyBoardByPitch((byte) ((NoteEvent) e.Event).NoteNumber + _pitch);
                    break;
            }
        }
    }
}