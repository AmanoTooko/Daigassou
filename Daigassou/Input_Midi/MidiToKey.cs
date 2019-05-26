using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Smf;
using Melanchall.DryWetMidi.Smf.Interaction;

namespace Daigassou
{
    internal enum EnumPitchOffset
    {
        OctaveLower = -12,
        None = 0,
        OctaveHigher = +12
    }

    internal class MidiPlaybackController
    {
        public int Index = 0;
        private MidiFile midi;
        private TempoMap Tmap;
        private List<TrackChunk> trunks;

        public MidiPlaybackController()
        {

        }

        public List<string> GetOutputDevicesNames()
        {
            var ret = new List<string>();
            foreach (var device in OutputDevice.GetAll())
            {
                ret.Add(device.Name);
            }
            return ret;
        }
        public void TrackPlayback(string outputDevice,int Index)
        {
            if (outputDevice!=null)
            {
                
            }
        }

        public void TrackPlaybackPause()
        {
            //cts = new CancellationTokenSource();
            //NewCancellableTask(cts.Token);
            //private Task NewCancellableTask(CancellationToken token)
            //{
            //    return Task.Run(() =>
            //    {
            //        mtk.ArrangeKeyPlaysNew(mtk.Index);
            //        //var keyPlayLists = mtk.ArrangeKeyPlays(mtk.Index);
            //        //KeyController.KeyPlayBack(keyPlayLists, 1, cts.Token);
            //        //_runningFlag = false;
            //    }, token);
            //}
        }
    }
    internal class MidiToKey
    {
        private readonly uint MIN_DELAY_TIME_MS_EVENT;
        private readonly uint MIN_DELAY_TIME_MS_CHORD;
        private readonly bool autoChord;
        private readonly List<NotesManager> tracks;
        public int Index = 0;
        private MidiFile midi;
        private TempoMap Tmap;
        private List<TrackChunk> trunks;

        private OutputDevice outputDevice;
        private Playback playback ;

        public MidiToKey()
        {
            tracks = new List<NotesManager>();
            Bpm = 80;
            Offset = EnumPitchOffset.None;
            MIN_DELAY_TIME_MS_EVENT = Properties.Settings.Default.MinEventMs;
            MIN_DELAY_TIME_MS_CHORD = Properties.Settings.Default.MinChordMs;
            autoChord = Properties.Settings.Default.AutoChord;
        }

        public EnumPitchOffset Offset { get; set; }
        public int Bpm { get; set; }

        public void OpenFile(string path)
        {
            midi = MidiFile.Read(path, new ReadingSettings
            {
                NoHeaderChunkPolicy = NoHeaderChunkPolicy.Ignore,
                NotEnoughBytesPolicy = NotEnoughBytesPolicy.Ignore,
                InvalidChannelEventParameterValuePolicy = InvalidChannelEventParameterValuePolicy.ReadValid,
                InvalidChunkSizePolicy = InvalidChunkSizePolicy.Ignore,
                InvalidMetaEventParameterValuePolicy = InvalidMetaEventParameterValuePolicy.SnapToLimits,
                MissedEndOfTrackPolicy = MissedEndOfTrackPolicy.Ignore,
                UnexpectedTrackChunksCountPolicy = UnexpectedTrackChunksCountPolicy.Ignore,
                ExtraTrackChunkPolicy = ExtraTrackChunkPolicy.Read,
                UnknownChunkIdPolicy = UnknownChunkIdPolicy.ReadAsUnknownChunk,
                SilentNoteOnPolicy = SilentNoteOnPolicy.NoteOff
            });
            Tmap = midi.GetTempoMap();
        }

        /// <summary>
        ///     Get a String list of Note name from noteManager
        /// </summary>
        /// <returns></returns>
        public List<string> GetTrackManagers()
        {
            try
            {
                tracks.Clear();
                var score = new List<string>();
                trunks = new List<TrackChunk>();
                foreach (var track in midi.GetTrackChunks())
                    if (track.ManageNotes().Notes.Count() != 0)
                    {
                        tracks.Add(track.ManageNotes());
                        trunks.Add(track);
                    }


                foreach (var noteManager in tracks)
                {
                    var track = new StringBuilder("");
                    foreach (var note in noteManager.Notes) track.Append(note + " ");
                    if (track.ToString() != string.Empty) score.Add(track.ToString());
                }

                return score;
            }
            catch (Exception e)
            {
                MessageBox.Show($"这个Midi文件读取出错！请使用其他软件重新保存。\r\n异常信息：{e.Message}\r\n 异常类型{e.GetType()}",
                    "读取错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public Queue<KeyPlayList> ArrangeKeyPlays(int index)
        {
            try
            {
                var trunkEvents = trunks.ElementAt(index).Events;
                var retKeyPlayLists = new Queue<KeyPlayList>();
                var tickBase = 60000 / (float) Bpm /
                               Convert.ToDouble(midi.TimeDivision.ToString()
                                   .TrimEnd(" ticks/qnote".ToCharArray()));
                var isLastOnEvent = false;
                var nowPitch = 0;
                foreach (var ev in trunkEvents)
                    switch (ev)
                    {
                        case NoteOnEvent onEvent:
                        {
                            var @event = onEvent;


                            var noteNumber = (int) (@event.NoteNumber + Offset);
                            
                            if (tickBase * @event.DeltaTime < MIN_DELAY_TIME_MS_EVENT && isLastOnEvent==true)
                            {
                                if (nowPitch == @event.NoteNumber)
                                    retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
                                        noteNumber, MIN_DELAY_TIME_MS_EVENT));
                                retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOn,
                                    noteNumber, MIN_DELAY_TIME_MS_EVENT));
                            }
                            else
                            {
                                if (nowPitch == @event.NoteNumber)
                                    retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
                                        noteNumber, MIN_DELAY_TIME_MS_EVENT));
                                retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOn,
                                    noteNumber, (int) (tickBase * @event.DeltaTime)));
                            }

                            isLastOnEvent = true;
                            nowPitch = @event.NoteNumber;
                        }
                            break;
                        case NoteOffEvent offEvent:
                        {
                            var @event = offEvent;


                            var noteNumber = (int) (@event.NoteNumber + Offset);
                            
                            if (tickBase * @event.DeltaTime < MIN_DELAY_TIME_MS_EVENT && isLastOnEvent==true&& nowPitch == @event.NoteNumber)
                                retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
                                    noteNumber, MIN_DELAY_TIME_MS_EVENT));
                            else
                                retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
                                    noteNumber, (int) (tickBase * @event.DeltaTime)));
                            isLastOnEvent = false;
                            if (nowPitch == @event.NoteNumber) nowPitch = 0;
                        }
                            break;
                        default:
                            isLastOnEvent = false;
                            nowPitch = 0;
                            break;
                    }

                return retKeyPlayLists;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 和弦处理，主要逻辑：遇到和弦时，从第一个和弦起，每个音符向后移位minTick数，同时Length也减少
        /// </summary>
        public void PreProcessChord()
        {
            var ticksPerQuarterNote = Convert.ToInt64(midi.TimeDivision.ToString()
                .TrimEnd(" ticks/qnote".ToCharArray()));
            var tickBase = 60000 / (float)Bpm / ticksPerQuarterNote;//duplicate code need to be delete

            using (var chordManager = trunks.ElementAt(Index).Events.ManageChords())
            {
                foreach (var chord in chordManager.Chords)
                {
                    if (chord.Notes.Count() > 1)
                    {
                        var count = 0;
                        if (autoChord)
                        {
                            var autoTick = chord.Notes.First().Length / (chord.Notes.Count()+1);
                            foreach (var note in chord.Notes.OrderBy(x => x.NoteNumber))
                            {
                                note.Time += (long)(count * autoTick);
                                note.Length = note.Length - (count * autoTick);
                                count++;
                            }
                        }
                        else//修改为等比例前后
                        {
                            tickBase = 60000 / (float)midi.GetTempoMap().Tempo.AtTime(chord.Notes.First().Time).BeatsPerMinute /
                                       ticksPerQuarterNote;
                            var minTick = (long)(MIN_DELAY_TIME_MS_CHORD / tickBase);
                            //original time is on the center of chord
                            double startOffset = (double)(chord.Notes.Count() - 1) / 2;
                            foreach (var note in chord.Notes.OrderBy(x => x.NoteNumber))
                            {
                                note.Time += (long)((count-startOffset) * minTick>0? (count - startOffset) * minTick : 0);
                                note.Length = note.Length - (count * minTick) > minTick ? note.Length - (count * minTick) : minTick;
                                count++;
                            }

                        }
                        
                    }

                }
            }
                


        }

        /// <summary>
        /// Event处理，主要逻辑：遇到同音符连续按下时，提前offevent
        /// </summary>
        public void PreProcessEvents()
        {
            var ticksPerQuarterNote = Convert.ToInt64(midi.TimeDivision.ToString()
                .TrimEnd(" ticks/qnote".ToCharArray()));
            var tickBase = 60000 / (float)Bpm / ticksPerQuarterNote;
            TimedEvent[] eventOffTimeArray = new TimedEvent[108];
            using (var eventsManager = trunks.ElementAt(Index).Events.ManageTimedEvents())
            {
                foreach (var @event in eventsManager.Events)
                {
                    tickBase = 60000 / (float)Tmap.Tempo.AtTime(@event.Time).BeatsPerMinute /
                               ticksPerQuarterNote;
                    var minTick = (long)(MIN_DELAY_TIME_MS_EVENT / tickBase);
                    switch (@event.Event)
                    {
                        case NoteOnEvent noteOnEvent:
                            if (eventOffTimeArray[noteOnEvent.NoteNumber] != null && eventOffTimeArray[noteOnEvent.NoteNumber].Time + minTick > @event.Time )
                            {
                                eventOffTimeArray[noteOnEvent.NoteNumber].Time -= minTick;//未加小于0的判断
                            }
                            break;
                        case NoteOffEvent noteOffEvent:
                            eventOffTimeArray[noteOffEvent.NoteNumber] = @event;
                            break;
                    }
                }
            }
                
        }
        public Queue<KeyPlayList> ArrangeKeyPlaysNew(double speed)
        {
            var trunkEvents = trunks.ElementAt(Index).Events;

            var ticksPerQuarterNote = Convert.ToInt64(midi.TimeDivision.ToString()
                .TrimEnd(" ticks/qnote".ToCharArray()));
            var tickBase = 60000 / (float)Bpm / ticksPerQuarterNote;//duplicate code need to be delete
            var nowTimeMs = 0;
            var retKeyPlayLists = new Queue<KeyPlayList>();
            PreProcessSpeed(speed);
            PreProcessChord();
            PreProcessEvents();
            using (var timedEvent = trunkEvents.ManageTimedEvents())
            {
                foreach (var ev in timedEvent.Events)
                {
                    
                    switch (ev.Event)
                    {
                        case NoteOnEvent @event:
                        {
                            tickBase = 60000 / (float)Tmap.Tempo.AtTime(ev.Time).BeatsPerMinute /
                                       ticksPerQuarterNote;
                                var noteNumber = (int)(@event.NoteNumber + Offset);
                            nowTimeMs += (int)(tickBase * @event.DeltaTime);
                                retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOn,
                                noteNumber, nowTimeMs));
                            
                        }
                            break;
                        case NoteOffEvent @event:
                        {
                            tickBase = 60000 / (float)Tmap.Tempo.AtTime(ev.Time).BeatsPerMinute /
                                       ticksPerQuarterNote;
                                var noteNumber = (int)(@event.NoteNumber + Offset);
                            nowTimeMs += (int)(tickBase * @event.DeltaTime);
                                retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
                                noteNumber, nowTimeMs));
                        }
                            break;
                        case SetTempoEvent @event:
                        {
                            tickBase = 60000 / (float)Tmap.Tempo.AtTime(ev.Time-1).BeatsPerMinute /
                                       ticksPerQuarterNote;
                            
                            nowTimeMs += (int)(tickBase * @event.DeltaTime);
                            
                        }
                            break;
                        default:
                            break;
                    }
                }
            }
            return retKeyPlayLists;
        }

        public void PreProcessSpeed(double speed)
        {
            using (var eventsManager = trunks.ElementAt(Index).Events.ManageTimedEvents())
            {
                foreach (var @event in eventsManager.Events)
                {
                    @event.Time = (long)(@event.Time*speed);
                }
            }
        }

        public int PlaybackPause()
        {
            if (playback==null)
            {
                return -1;
            }

            if (!playback.IsRunning)
            {
                return -2;
            }
            playback.Stop();
            return 0;
        }

        public int PlaybackStart(int BPM)
        {
            if (midi==null)
            {
                return -1;
            }

            if (OutputDevice.GetDevicesCount()==0)
            {
                return -2;
            }
            if (outputDevice==null&&(outputDevice = OutputDevice.GetByName("Microsoft GS Wavetable Synth"))==null)
            {
                outputDevice = OutputDevice.GetAll().ElementAt(0);
            }
            if (playback==null)
            {
                playback = new Playback(midi.GetTrackChunks().ElementAt(Index).Events, midi.GetTempoMap(), outputDevice);
            }
            playback.Speed = (double)BPM / GetBpm();
            playback.Start();
            
            
            return 0;
        }
        
        public int PlaybackRestart()
        {
            if (midi == null)
            {
                return -1;
            }
            if (playback==null)
            {
                return -2;
            }
            playback.Stop();
            playback.Dispose();
            playback = null;
            return 0;


        }
        public int GetBpm()
        {
            var bpm = 80;
            bpm = (int) Tmap.Tempo.AtTime(0).BeatsPerMinute;
            return bpm;
        }
    }
}