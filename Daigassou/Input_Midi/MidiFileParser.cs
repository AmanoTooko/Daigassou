using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.Tools;

namespace Daigassou.Controller
{
    public class MidiFileParser
    {
        private readonly bool autoChord;
        private readonly uint MIN_DELAY_TIME_MS_CHORD;
        private readonly uint MIN_DELAY_TIME_MS_EVENT;
        private readonly List<TimedObjectsManager<Note>> tracks;
        public int Index;
        private MidiFile midi;

        private OutputDevice outputDevice;
        private Playback playback;
        private TempoMap Tmap;
        private List<TrackChunk> trunks;


        public MidiFileParser()
        {
            tracks = new List<TimedObjectsManager<Note>>();
            Bpm = 80;
            MIN_DELAY_TIME_MS_EVENT = Properties.Settings.Default.MinChordMs;
            MIN_DELAY_TIME_MS_CHORD = Properties.Settings.Default.MinChordMs;
            autoChord = true;//.Settings.Default.isAutoChord;
        }

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
                InvalidSystemCommonEventParameterValuePolicy = InvalidSystemCommonEventParameterValuePolicy.SnapToLimits
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
        ///     Get a String list of track name from trackmanager
        /// </summary>
        /// <returns></returns>
        public List<string> GetTrackNames()
        {
            try
            {
                tracks.Clear();
                var score = new List<string>();
                trunks = new List<TrackChunk>();
                foreach (var track in midi.GetTrackChunks())
                    if (track.ManageNotes().Objects.Count() != 0)
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
        ///     和弦处理，主要逻辑：
        ///     | ===
        ///     | ===
        ///     | ===
        ///     1. 将N个Note组成的chord拆成 从Chord.time起，每隔LENGTH-N时间演奏一个note
        ///     |     =
        ///     |   ==
        ///     | ===
        ///     2. 在chord.time前后按照最小时间均匀分布N个note
        ///     | =
        ///     中
        ///     =|
        /// </summary>
        public void PreProcessChord()
        {
            using (var chordManager = new ChordsManager(trunks.ElementAt(Index).Events))
            {
                foreach (var chord in chordManager.Chords)
                    if (chord.Notes.Count() > 1)
                    {
                        var count = 0;
                        if (autoChord)
                        {
                            var autoTick = chord.Length / (chord.Notes.Count() + 1);
                            foreach (var note in chord.Notes.OrderBy(x => x.NoteNumber))
                            {
                                note.Time += count * autoTick;
                                note.Length = autoTick;
                                count++;
                            }
                        }
                        else
                        {
                            var lenoffset =
                                LengthConverter.ConvertFrom(new MetricTimeSpan(MIN_DELAY_TIME_MS_CHORD * 1000),
                                    chord.Time, Tmap);
                            var startTime = chord.Time - lenoffset * (chord.Notes.Count() - 1) / 2 < 0
                                ? 0
                                : chord.Time - lenoffset * (chord.Notes.Count() - 1) / 2;
                            var len = chord.Length / chord.Notes.Count();
                            foreach (var note in chord.Notes.OrderBy(x => x.NoteNumber))
                            {
                                note.Length = len;
                                note.Time = startTime + count * lenoffset;
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
            using (var notesManager = trunks.ElementAt(Index).ManageNotes())
            {
                Note lastNote = null;
                foreach (var note in notesManager.Objects)
                {
                    var lastNoteStartTime = lastNote?.TimeAs(TimeSpanType.Metric, Tmap) as MetricTimeSpan;
                    var lastNoteLength = lastNote?.LengthAs(TimeSpanType.Metric, Tmap) as MetricTimeSpan;
                    var currentNoteLength = note.TimeAs(TimeSpanType.Metric, Tmap) as MetricTimeSpan;

                    if (lastNote != null &&
                        lastNoteStartTime.TotalMicroseconds + lastNoteLength.TotalMicroseconds +
                        MIN_DELAY_TIME_MS_EVENT * 1000 > currentNoteLength.TotalMicroseconds)
                    {
                        var minTick = LengthConverter.ConvertFrom(new MetricTimeSpan(MIN_DELAY_TIME_MS_EVENT * 1000),
                            lastNote.Time, Tmap);
                        if (lastNoteLength.TotalMicroseconds < MIN_DELAY_TIME_MS_EVENT * 1000 ||
                            note.Time - lastNote.Time <= minTick)
                        {
                            ///  ===       =>     ===
                            ///======   =>   ={}
                            lastNote.Length =
                                LengthConverter.ConvertFrom(new MetricTimeSpan(MIN_DELAY_TIME_MS_EVENT * 1000),
                                    lastNote.Time, Tmap);
                            
                        }
                        else
                        {
                            ///
                            ///            ===    =>               ===
                            ///=======        =>   ======
                            ///
                            var noteSpan = note.TimeAs(TimeSpanType.Metric, Tmap)
                                .Subtract(lastNoteStartTime, TimeSpanMode.TimeTime);
                            lastNote.Length = LengthConverter.ConvertFrom(
                                noteSpan.Subtract(new MetricTimeSpan(MIN_DELAY_TIME_MS_EVENT * 1000),
                                    TimeSpanMode.LengthLength), lastNote.Time, Tmap);
                        }
                    }

                    lastNote = note;
                }
            }
        }

        /// <summary>
        ///     清除水果或其他软件产生的duration小于10ms的音
        /// </summary>
        public void PreProcessNoise()
        {
            using (var eventsManager = trunks.ElementAt(Index).Events.ManageNotes())
            {
                foreach (var @event in eventsManager.Objects)
                    if ((@event.LengthAs(TimeSpanType.Metric, Tmap) as MetricTimeSpan).TotalMicroseconds < 10000.0)
                        eventsManager.Objects.Remove(@event);
            }
        }

        public void SaveToFile()
        {
            // PreProcessTempoMap();
            for (var i = 0; i < trunks.Count; i++)
            {
                Index = i;
                PreProcessNoise();
                PreProcessChord();
                PreProcessEvents();
            }

            var stfd = new SaveFileDialog();
            stfd.Filter = "Midi文件|*.mid";
            if (stfd.ShowDialog() == DialogResult.OK)
                midi.Write(stfd.FileName, false, MidiFileFormat.MultiTrack,
                    new WritingSettings {TextEncoding = Encoding.Default});
        }

        public Queue<KeyPlayList> ArrangeKeyPlays(double speed)
        {
            var trunkEvents = trunks.ElementAt(Index).Events;

            var ticksPerQuarterNote = Convert.ToInt64(midi.TimeDivision.ToString()
                .TrimEnd(" ticks/qnote".ToCharArray()));

            var nowTimeMs = 0.0;
            var retKeyPlayLists = new Queue<KeyPlayList>();
            PreProcessNoise();
            PreProcessSpeed(speed);
            PreProcessChord();
            PreProcessEvents();
            using (var timedEvent = trunkEvents.ManageTimedEvents())
            {
                foreach (var ev in timedEvent.Objects)
                    switch (ev.Event)
                    {
                        case NoteOnEvent @event:
                        {
                            var noteNumber = (int) @event.NoteNumber;
                            nowTimeMs = (ev.TimeAs(TimeSpanType.Metric, Tmap) as MetricTimeSpan).TotalMicroseconds /
                                        1000.0;
                            retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOn,
                                noteNumber, nowTimeMs));
                        }
                            break;
                        case NoteOffEvent @event:
                        {
                            var noteNumber = (int) @event.NoteNumber;
                            nowTimeMs = (ev.TimeAs(TimeSpanType.Metric, Tmap) as MetricTimeSpan).TotalMicroseconds /
                                        1000.0;
                            retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
                                noteNumber, nowTimeMs));
                        }
                            break;
                    }
            }

            return retKeyPlayLists;
        }

        public Playback GetPlayback()
        {
            if (Properties.Settings.Default.isUsingAnalysis)
            {
                PreProcessNoise();
                PreProcessChord();
                PreProcessEvents();
            }

            var playback = new Playback(trunks.ElementAt(Index).GetNotes(), Tmap);
            return playback;
        }

        public Playback GetPlaybackForAll()
        {
            if (Properties.Settings.Default.isUsingAnalysis)
            {
                for (int i = 0; i < trunks.Count; i++)
                {
                    Index = i;
                    PreProcessNoise();
                    PreProcessChord();
                    PreProcessEvents();
                }

            }

            var playback = midi.GetPlayback();
            return playback;
        }

        public void PreProcessSpeed(double speed)
        {
            using (var eventsManager = trunks.ElementAt(Index).Events.ManageTimedEvents())
            {
                foreach (var @event in eventsManager.Objects) @event.Time = (long) (@event.Time * speed);
            }
        }

        //public void PreProcessTempoMap()
        //{
        //    foreach (var trackChunk in trunks)
        //        using (var eventsManager = trackChunk.Events.ManageTimedEvents())
        //        {
        //            foreach (var tmpChange in Tmap.GetTempoChanges())
        //            {
        //                var ev = new SetTempoEvent(tmpChange.Value.MicrosecondsPerQuarterNote);
        //                eventsManager.Events.AddEvent(ev, tmpChange.Time);
        //            }
        //        }
        //}

        //public int GetBpm()
        //{
        //    var bpm = (int)Tmap.Tempo.AtTime(0).BeatsPerMinute;
        //    return bpm;
        //}

        #region MidiFilePlayBack

        public int PlaybackPause()
        {
            if (playback == null) return -1;

            if (!playback.IsRunning) return -2;
            playback.Stop();
            return 0;
        }

        //public int PlaybackStart(int BPM, EventHandler playbackFinishHandler)
        //{
        //    if (midi == null) return -1;

        //    if (OutputDevice.GetDevicesCount() == 0) return -2;
        //    if (outputDevice == null && (outputDevice = OutputDevice.GetByName("Microsoft GS Wavetable Synth")) == null)
        //        outputDevice = OutputDevice.GetAll().ElementAt(0);
        //    if (playback == null)
        //        playback = new Playback(trunks.ElementAt(Index).Events, midi.GetTempoMap(), outputDevice);
        //    playback.Speed = (double)BPM / GetBpm();
        //    playback.InterruptNotesOnStop = true;
        //    playback.Start();
        //    playback.Finished += playbackFinishHandler;

        //    return 0;
        //}


        //public int PlaybackPercentGet()
        //{
        //    if (playback.IsRunning)
        //    {
        //        var cur = (MidiTimeSpan)playback.GetCurrentTime(TimeSpanType.Midi);
        //        var dur = (MidiTimeSpan)playback.GetDuration(TimeSpanType.Midi);

        //        return (int)(cur.TimeSpan * 100 / dur.TimeSpan);
        //    }
        //    return 0;
        //}
        //public void PlaybackPercentSet(int process)
        //{
        //    if (playback.IsRunning)
        //    {
        //        MidiTimeSpan dur = (MidiTimeSpan)playback.GetDuration(TimeSpanType.Midi);

        //        var tar = new MidiTimeSpan(dur.TimeSpan * process / 100);
        //        playback.MoveToTime(tar);

        //    }
        //}
        //public string PlaybackInfo()
        //{
        //    var ret = "";
        //    var expression = @"\d*:(?<time>.+):\d+";
        //    if (playback.IsRunning)
        //    {
        //        var cur = playback.GetCurrentTime(TimeSpanType.Metric);
        //        var dur = playback.GetDuration(TimeSpanType.Metric);

        //        ret = Regex.Match(cur.ToString(), expression).Groups["time"].Value + "/" +
        //              Regex.Match(dur.ToString(), expression).Groups["time"].Value;
        //    }
        //    return ret;
        //}

        //public int PlaybackRestart()
        //{
        //    if (midi == null) return -1;
        //    if (playback == null) return -2;

        //    playback.Stop();
        //    playback.Dispose();
        //    playback = null;
        //    return 0;
        //}

        #endregion
    }
}