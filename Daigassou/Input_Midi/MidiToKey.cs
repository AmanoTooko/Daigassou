using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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

    internal class MidiToKey
    {
        private readonly int MIN_DELAY_TICK;
        private readonly List<NotesManager> tracks;
        public int Index = 0;
        private MidiFile midi;
        private TempoMap Tmap;
        private List<TrackChunk> trunks;

        public MidiToKey()
        {
            tracks = new List<NotesManager>();
            Bpm = 80;
            Offset = EnumPitchOffset.None;
            MIN_DELAY_TICK = 50;
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

        public int GetTimerTick()
        {
            return 0;
        }

        public Queue<KeyPlayList> ArrangeKeyPlays(int index)
        {
            try
            {
                var trunkEvents = trunks.ElementAt(index).Events;
                var retKeyPlayLists = new Queue<KeyPlayList>();
                var tickbase = 60000 / (float) Bpm /
                               Convert.ToDouble(midi.TimeDivision.ToString()
                                   .TrimEnd(" ticks/qnote".ToCharArray()));
                var isLastOffEvent = true;
                var NowPitch = 0;
                foreach (var ev in trunkEvents)
                    switch (ev)
                    {
                        case NoteOnEvent _:
                        {
                            var @event = ev as NoteOnEvent;


                            var notenumber = (int) (@event.NoteNumber + Offset);
                            if (notenumber >= 84 || notenumber <= 48) continue;
                            if (tickbase * @event.DeltaTime < MIN_DELAY_TICK)
                            {
                                retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOn,
                                    notenumber, MIN_DELAY_TICK));
                            }
                            else
                            {
                                if (NowPitch == @event.NoteNumber)
                                    retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
                                        notenumber, MIN_DELAY_TICK));
                                retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOn,
                                    notenumber, (int) (tickbase * @event.DeltaTime)));
                            }

                            isLastOffEvent = false;
                            NowPitch = @event.NoteNumber;
                        }
                            break;
                        case NoteOffEvent _:
                        {
                            var @event = ev as NoteOffEvent;


                            var notenumber = (int) (@event.NoteNumber + Offset);
                            if (notenumber >= 84 || notenumber <= 48) continue;
                            if (tickbase * @event.DeltaTime < MIN_DELAY_TICK)
                                retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
                                    notenumber, MIN_DELAY_TICK));
                            else
                                retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
                                    notenumber, (int) (tickbase * @event.DeltaTime)));
                            isLastOffEvent = true;
                            if (NowPitch == @event.NoteNumber) NowPitch = 0;
                        }
                            break;
                        default:
                            isLastOffEvent = false;
                            NowPitch = 0;
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

        public int GetBpm()
        {
            var bpm = 80;
            bpm = (int) Tmap.Tempo.AtTime(0).BeatsPerMinute;
            return bpm;
        }
    }
}