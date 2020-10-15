using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Daigassou.Properties;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Standards;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.Threading;
using Newtonsoft.Json;
using Daigassou.Utils;

namespace Daigassou
{
    internal enum EnumPitchOffset
    {
        OctaveLower = -12,
        None = 0,
        OctaveHigher = +12
    }

    internal class MidiToKey
    {
        private readonly bool autoChord;
        private readonly uint MIN_DELAY_TIME_MS_CHORD;
        private readonly uint MIN_DELAY_TIME_MS_EVENT;
        private readonly List<NotesManager> tracks;
        public int Index = 0;
        private MidiFile midi;

        private OutputDevice outputDevice;
        private Playback playback;
        private TempoMap Tmap;
        private List<TrackChunk> trunks;
        public Playback midiPlay;
        private List<string> score = new List<string>();
        public KeyplayClass netmidi;
        public MidiToKey()
        {
            tracks = new List<NotesManager>();
            Bpm = 80;
            Offset = EnumPitchOffset.None;
            MIN_DELAY_TIME_MS_EVENT = Settings.Default.MinEventMs;
            MIN_DELAY_TIME_MS_CHORD = Settings.Default.MinChordMs;
            autoChord = Settings.Default.AutoChord;
        }

        public EnumPitchOffset Offset { get; set; }
        public int Bpm { get; set; }

        public void OpenFile(byte[] s)
        {
            
            midi = MidiFile.Read(new MemoryStream(s), new ReadingSettings
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
                SilentNoteOnPolicy = SilentNoteOnPolicy.NoteOff,
                TextEncoding = Encoding.Default,
                InvalidSystemCommonEventParameterValuePolicy=InvalidSystemCommonEventParameterValuePolicy.SnapToLimits,
                
            });
            Tmap = midi.GetTempoMap();
        }
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
                SilentNoteOnPolicy = SilentNoteOnPolicy.NoteOff,
                TextEncoding = Encoding.Default
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
                trunks = new List<TrackChunk>();
                score.Clear();
                foreach (var track in midi.GetTrackChunks())
                    if (track.ManageNotes().Notes.Count() != 0)
                    {
                        tracks.Add(track.ManageNotes());
                        trunks.Add(track);
                    }

                for (var i = 0; i < trunks.Count; i++)
                {
                    var name = "Untitled";
                    foreach (var trunkEvent in trunks[i].Events)
                        if (trunkEvent is SequenceTrackNameEvent)
                        {
                            var e = trunkEvent as SequenceTrackNameEvent;
                            name = e.Text;
                            break;
                        }

                    score.Add(name);
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

        /// <summary>
        ///     和弦处理，主要逻辑：遇到和弦时，从第一个和弦起，每个音符向后移位minTick数，同时Length也减少
        /// </summary>
        public void PreProcessChord()
        {
            var ticksPerQuarterNote = Convert.ToInt64(midi.TimeDivision.ToString()
                .TrimEnd(" ticks/qnote".ToCharArray()));
            var tickBase = 60000 / (float) Bpm / ticksPerQuarterNote; //duplicate code need to be delete

            using (var chordManager = new ChordsManager(trunks.ElementAt(Index).Events))
            {
                foreach (var chord in chordManager.Chords)
                    if (chord.Notes.Count() > 1)
                    {
                        var count = 0;
                        if (autoChord)//等分拆分
                        {
                            var autoTick = chord.Length / (chord.Notes.Count() + 1);
                            foreach (var note in chord.Notes.OrderBy(x => x.NoteNumber))
                            {
                                note.Time += count * autoTick;
                                note.Length = (long)((chord.Length - count * autoTick)<MIN_DELAY_TIME_MS_CHORD/tickBase? MIN_DELAY_TIME_MS_CHORD / tickBase: (chord.Length - count * autoTick));
                                count++;
                            }
                        }
                        else //前后最小值化
                        {
                            tickBase = 60000 / (float) Tmap.Tempo.AtTime(chord.Time).BeatsPerMinute /
                                       ticksPerQuarterNote;
                            var minTick = (long) (MIN_DELAY_TIME_MS_CHORD / tickBase);
                            //original time is on the center of chord
                            var startOffset = (double) (chord.Notes.Count() - 1) / 2;
                            long timeoffset = 0;
                            foreach (var note in chord.Notes.OrderBy(x => x.NoteNumber))
                            {
                                timeoffset = (long) ((count - startOffset) * minTick);
                                note.Length = note.Length - count * minTick > minTick
                                    ? note.Length - count * minTick
                                    : minTick;
                                note.Time = note.Time + timeoffset < 0 ? 0 : note.Time + timeoffset;
                                count++;
                            }
                        }
                    }
            }
        }

        /// <summary>
        ///     Event处理，主要逻辑：遇到同音符连续按下时，提前offevent
        /// </summary>
        public void PreProcessEvents()
        {
            var ticksPerQuarterNote = Convert.ToInt64(midi.TimeDivision.ToString()
                .TrimEnd(" ticks/qnote".ToCharArray()));
            var tickBase = 60000 / (float) Bpm / ticksPerQuarterNote;
            var eventOffTimeArray = new TimedEvent[127];

            using (var notesManager = trunks.ElementAt(Index).ManageNotes())
            {
                Note lastNote = null;
                foreach (var @note in notesManager.Notes)
                {
                    tickBase = 60000 / (float) Tmap.Tempo.AtTime(@note.Time).BeatsPerMinute /
                               ticksPerQuarterNote;
                    var minTick = (long) (85 / tickBase);

                    if(lastNote!=null&& (lastNote.Time+lastNote.Length+minTick> @note.Time))
                    {
                        lastNote.Length = lastNote.Length < minTick ?  minTick: lastNote.Length-minTick;
                    }
                    lastNote = @note;

                }
            }
        }
        /// <summary>
        /// 清除水果或其他软件产生的duration小于5的奇怪杂音
        /// </summary>
        public void PreProcessNoise()
        {
            var ticksPerQuarterNote = Convert.ToInt64(midi.TimeDivision.ToString()
               .TrimEnd(" ticks/qnote".ToCharArray()));
            var tickBase = 60000 / (float)Bpm / ticksPerQuarterNote;
            var eventOffTimeArray = new TimedEvent[127];
            using (var eventsManager = trunks.ElementAt(Index).Events.ManageNotes())
            {
                foreach (var @event in eventsManager.Notes)
                {
                    tickBase = 60000 / (float)Tmap.Tempo.AtTime(@event.Time).BeatsPerMinute /
                               ticksPerQuarterNote;
                    var minTick = (long)(MIN_DELAY_TIME_MS_EVENT / tickBase);
                    if (@event.Length < minTick / 2) 
                        eventsManager.Notes.Remove(@event);
                }
            }
        }
        public void SaveJsonToFile()
        {
            var result = new Daigassou.Utils.KeyplayClass();
            var stfd = new SaveFileDialog();
            var backup = Index;
            stfd.Filter = "Midi文件|*.mid";
            if (stfd.ShowDialog() == DialogResult.OK)
            {
                result.Filename = Path.GetFileNameWithoutExtension(stfd.FileName);
                result.BPM = GetBpm();
                result.Tracks = new Utils.KeyplayTrackClass[trunks.Count];
                for (int i = 0; i < trunks.Count; i++)
                {
                    result.Tracks[i] = new Utils.KeyplayTrackClass();
                    Index = i;
                    result.Tracks[i].name = score[i];
                    result.Tracks[i].notes = ArrangeKeyPlaysNew(1);
                }
                File.WriteAllText(stfd.FileName, JsonConvert.SerializeObject(result));
            }
                
            
        
        }

        public void SaveToFile()
        {
            PreProcessTempoMap();
            var backup = Index;
            for (var i = 0; i < trunks.Count; i++)
            {
                Index = i;
                PreProcessNoise();
                PreProcessChord();
                PreProcessEvents();

            }
            Index = backup;
            var stfd = new SaveFileDialog();
            stfd.Filter = "Midi文件|*.mid";
            if (stfd.ShowDialog() == DialogResult.OK)
                midi.Write(stfd.FileName, false, MidiFileFormat.MultiTrack,
                    new WritingSettings {TextEncoding = Encoding.Default});
        }
        
        public Queue<KeyPlayList> ArrangeKeyPlaysNew(double speed)
        {
            var trunkEvents = trunks.ElementAt(Index).Events;

            var ticksPerQuarterNote = Convert.ToInt64(midi.TimeDivision.ToString()
                .TrimEnd(" ticks/qnote".ToCharArray()));
            var tickBase = 60000 / (double) Bpm / ticksPerQuarterNote; //duplicate code need to be delete
            var nowTimeMs = 0.0;
            var retKeyPlayLists = new Queue<KeyPlayList>();
            
            PreProcessTempoMap();
            PreProcessNoise();
            PreProcessSpeed(speed);
            PreProcessChord();
            PreProcessEvents();
            using (var timedEvent = trunkEvents .ManageTimedEvents())
            {
                foreach (var ev in timedEvent.Events)
                    switch (ev.Event)
                    {
                        case NoteOnEvent @event:
                        {
                            var noteNumber = (int) (@event.NoteNumber + Offset);
                            nowTimeMs +=  (tickBase * @event.DeltaTime);
                            retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOn,
                                noteNumber, nowTimeMs));
                        }
                            break;
                        case NoteOffEvent @event:
                        {
                            var noteNumber = (int) (@event.NoteNumber + Offset);
                            nowTimeMs +=  (tickBase * @event.DeltaTime);
                            retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
                                noteNumber, nowTimeMs));
                        }
                            break;
                        case SetTempoEvent @event:
                        {
                            nowTimeMs +=  (tickBase * @event.DeltaTime);
                            tickBase = (double) @event.MicrosecondsPerQuarterNote / (1000.0 *ticksPerQuarterNote);
                        }
                            break;
                        default:
                        {
                            nowTimeMs += (tickBase * ev.Event.DeltaTime);
                        }
                            break;
                    }
            }

            return retKeyPlayLists;
        }

        public void PreProcessTempoMap()
        {
            foreach (var trackChunk in trunks)
                using (var eventsManager = trackChunk.Events.ManageTimedEvents())
                {
                    foreach (var tmpChange in Tmap.Tempo)
                    {
                        var ev = new SetTempoEvent(tmpChange.Value.MicrosecondsPerQuarterNote);
                        eventsManager.Events.AddEvent(ev, tmpChange.Time);
                    }
                }
        }

        public void PreProcessSpeed(double speed)
        {
            using (var eventsManager = trunks.ElementAt(Index).Events.ManageTimedEvents())
            {
                foreach (var @event in eventsManager.Events) @event.Time = (long) (@event.Time * speed);
            }
        }

        public int PlaybackPause()
        {
            if (playback == null) return -1;

            if (!playback.IsRunning) return -2;
            playback.Stop();
            return 0;
        }

        public int PlaybackStart(int BPM,EventHandler playbackFinishHandler)
        {
            if (midi == null) return -1;

            if (OutputDevice.GetDevicesCount() == 0) return -2;
            if (outputDevice == null && (outputDevice = OutputDevice.GetByName("Microsoft GS Wavetable Synth")) == null)
                outputDevice = OutputDevice.GetAll().ElementAt(0);
            if (playback == null)
                playback = new Playback(trunks.ElementAt(Index).Events, midi.GetTempoMap(), outputDevice);
            playback.Speed = (double) BPM / GetBpm();
            playback.InterruptNotesOnStop = true;
            playback.Start();
            playback.Finished += playbackFinishHandler;

            return 0;
        }

        public void PlaybackWithoutAnalysis(double speed,EventHandler<MidiEventPlayedEventArgs> P_EventPlayed, CancellationToken token)
        {
            midiPlay = trunks.ElementAt(Index).GetPlayback(midi.GetTempoMap());
            midiPlay.Speed = speed;
            midiPlay.EventPlayed += P_EventPlayed;
            midiPlay.Play();
            while(!token.IsCancellationRequested)
            {
                Thread.Sleep(1);
            }
            midiPlay.Stop();
        }



        public int PlaybackPercentGet()
        {
            if (playback.IsRunning)
            {
                var cur = (MidiTimeSpan)playback.GetCurrentTime(TimeSpanType.Midi);
                var dur = (MidiTimeSpan)playback.GetDuration(TimeSpanType.Midi);

                return (int)(cur.TimeSpan * 100 / dur.TimeSpan);
            }
            return 0;
        }
        public void PlaybackPercentSet(int process)
        {
            if (playback.IsRunning)
            {
                MidiTimeSpan dur = (MidiTimeSpan)playback.GetDuration(TimeSpanType.Midi);

                var tar = new MidiTimeSpan(dur.TimeSpan * process / 100);
                playback.MoveToTime(tar); 
                
            }
        }
        public string PlaybackInfo()
        {
            var ret = "";
            var expression = @"\d*:(?<time>.+):\d+";
            if (playback.IsRunning)
            {
                var cur = playback.GetCurrentTime(TimeSpanType.Metric);
                var dur = playback.GetDuration(TimeSpanType.Metric);

                ret = Regex.Match(cur.ToString(), expression).Groups["time"].Value + "/" +
                      Regex.Match(dur.ToString(), expression).Groups["time"].Value;
            }
            return ret;
        }

        public int PlaybackRestart()
        {
            if (midi == null) return -1;
            if (playback == null) return -2;
            
            playback.Stop();
            playback.Dispose();
            playback = null;
            return 0;
        }

        public int GetBpm()
        {
            var bpm = (int) Tmap.Tempo.AtTime(0).BeatsPerMinute;
            return bpm;
        }

    }
}