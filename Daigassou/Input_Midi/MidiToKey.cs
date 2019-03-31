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
        private readonly int MIN_DELAY_TIME_MS;
        private readonly List<NotesManager> tracks;
        public int Index = 0;
        private MidiFile midi;
        private TempoMap Tmap;
        private List<TrackChunk> trunks;

        private OutputDevice outputDevice = OutputDevice.GetAll().ElementAt(0);
        private Playback playback ;

        public MidiToKey()
        {
            tracks = new List<NotesManager>();
            Bpm = 80;
            Offset = EnumPitchOffset.None;
            MIN_DELAY_TIME_MS = 75;
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
                            
                            if (tickBase * @event.DeltaTime < MIN_DELAY_TIME_MS && isLastOnEvent==true)
                            {
                                if (nowPitch == @event.NoteNumber)
                                    retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
                                        noteNumber, MIN_DELAY_TIME_MS));
                                retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOn,
                                    noteNumber, MIN_DELAY_TIME_MS));
                            }
                            else
                            {
                                if (nowPitch == @event.NoteNumber)
                                    retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
                                        noteNumber, MIN_DELAY_TIME_MS));
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
                            
                            if (tickBase * @event.DeltaTime < MIN_DELAY_TIME_MS&&isLastOnEvent==true&& nowPitch == @event.NoteNumber)
                                retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
                                    noteNumber, MIN_DELAY_TIME_MS));
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
                        tickBase = 60000 / (float)midi.GetTempoMap().Tempo.AtTime(chord.Notes.First().Time).BeatsPerMinute /
                                   ticksPerQuarterNote;
                        var minTick = (long)(MIN_DELAY_TIME_MS / tickBase);
                        foreach (var note in chord.Notes)
                        {
                            note.Time += (long)(count * minTick);
                            note.Length = note.Length - (count * minTick) > minTick ? note.Length - (count * minTick) : minTick;
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
                    tickBase = 60000 / (float)midi.GetTempoMap().Tempo.AtTime(@event.Time).BeatsPerMinute /
                               ticksPerQuarterNote;
                    var minTick = (long)(MIN_DELAY_TIME_MS / tickBase);
                    switch (@event.Event)
                    {
                        case NoteOnEvent noteOnEvent:
                            if (eventOffTimeArray[noteOnEvent.NoteNumber] != null && eventOffTimeArray[noteOnEvent.NoteNumber].Time > @event.Time + minTick)
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
        public Queue<KeyPlayList> ArrangeKeyPlaysNew(int index)
        {
            var trunkEvents = trunks.ElementAt(index).Events;

            var ticksPerQuarterNote = Convert.ToInt64(midi.TimeDivision.ToString()
                .TrimEnd(" ticks/qnote".ToCharArray()));
            var tickBase = 60000 / (float)Bpm / ticksPerQuarterNote;//duplicate code need to be delete
            var nowTimeMs = 0;
            var retKeyPlayLists = new Queue<KeyPlayList>();
            PreProcessChord();
            PreProcessEvents();
            using (var timedEvent = trunkEvents.ManageTimedEvents())
            {
                foreach (var ev in timedEvent.Events)
                {
                    tickBase = 60000 / (float)midi.GetTempoMap().Tempo.AtTime(ev.Time).BeatsPerMinute /
                               ticksPerQuarterNote;
                    switch (ev.Event)
                    {
                        case NoteOnEvent @event:
                        {
                            
                            var noteNumber = (int)(@event.NoteNumber + Offset);
                            retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOn,
                                noteNumber, (int)(tickBase * @event.DeltaTime)));
                        }
                            break;
                        case NoteOffEvent @event:
                        {
                            var noteNumber = (int)(@event.NoteNumber + Offset);
                            retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
                                noteNumber, (int)(tickBase * @event.DeltaTime)));
                        }
                            break;
                        default:
                            break;
                    }
                }
            }
            return retKeyPlayLists;
        }

        public void PlaybackPause()
        {
            playback.Stop();
        }

        public void PlaybackStart()
        {
            if (playback==null)
            {
                playback = new Playback(trunks.ElementAt(Index).Events, midi.GetTempoMap(), outputDevice);
            }
            playback.Start();
        }

        public void PlaybackRestart()
        {
            playback.Stop();
            playback.Dispose();
            playback= new Playback(trunks.ElementAt(Index).Events, midi.GetTempoMap(), outputDevice);
        }
        public int GetBpm()
        {
            var bpm = 80;
            bpm = (int) Tmap.Tempo.AtTime(0).BeatsPerMinute;
            return bpm;
        }
    }
}