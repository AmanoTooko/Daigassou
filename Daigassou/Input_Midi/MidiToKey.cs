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
        private readonly int MIN_DELAY_TICK;
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
            MIN_DELAY_TICK = 75;
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
                            
                            if (tickBase * @event.DeltaTime < MIN_DELAY_TICK && isLastOnEvent==true)
                            {
                                if (nowPitch == @event.NoteNumber)
                                    retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
                                        noteNumber, MIN_DELAY_TICK));
                                retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOn,
                                    noteNumber, MIN_DELAY_TICK));
                            }
                            else
                            {
                                if (nowPitch == @event.NoteNumber)
                                    retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
                                        noteNumber, MIN_DELAY_TICK));
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
                            
                            if (tickBase * @event.DeltaTime < MIN_DELAY_TICK&&isLastOnEvent==true&& nowPitch == @event.NoteNumber)
                                retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
                                    noteNumber, MIN_DELAY_TICK));
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

        public void PreProcessMidi(int index)
        {
            var trunk = trunks.ElementAt(index);
            var trunkEvents = trunks.ElementAt(index).Events;
            var retKeyPlayLists = new Queue<KeyPlayList>();
            var tickBase = 60000 / (float)Bpm /
                           Convert.ToDouble(midi.TimeDivision.ToString()
                               .TrimEnd(" ticks/qnote".ToCharArray()));
            var manager = new TimedEventsManager(trunkEvents);
            var notesManager = new NotesManager(trunkEvents);

            var tempo =midi.GetTempoMap().Tempo.AtTime(0);//get tempo?
            
            var isLastOnEvent = false;
            var nowPitch = 0;

            using (var outputDevice = OutputDevice.GetAll().ElementAt(0))
            using (var playback = new Playback(trunkEvents, TempoMap.Default, outputDevice))
            {
                playback.Start();
            }
            //foreach (var ev in trunkEvents)
            //    switch (ev)
            //    {
            //        case NoteOnEvent onEvent:
            //            {
            //                var @event = onEvent;


            //                var noteNumber = (int)(@event.NoteNumber + Offset);

            //                if (tickBase * @event.DeltaTime < MIN_DELAY_TICK && isLastOnEvent == true)
            //                {
            //                    if (nowPitch == @event.NoteNumber)
            //                        retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
            //                            noteNumber, MIN_DELAY_TICK));
            //                    retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOn,
            //                        noteNumber, MIN_DELAY_TICK));
            //                }
            //                else
            //                {
            //                    if (nowPitch == @event.NoteNumber)
            //                        retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
            //                            noteNumber, MIN_DELAY_TICK));
            //                    retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOn,
            //                        noteNumber, (int)(tickBase * @event.DeltaTime)));
            //                }

            //                isLastOnEvent = true;
            //                nowPitch = @event.NoteNumber;
            //            }
            //            break;
            //        case NoteOffEvent offEvent:
            //            {
            //                var @event = offEvent;


            //                var noteNumber = (int)(@event.NoteNumber + Offset);

            //                if (tickBase * @event.DeltaTime < MIN_DELAY_TICK && isLastOnEvent == true && nowPitch == @event.NoteNumber)
            //                    retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
            //                        noteNumber, MIN_DELAY_TICK));
            //                else
            //                    retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
            //                        noteNumber, (int)(tickBase * @event.DeltaTime)));
            //                isLastOnEvent = false;
            //                if (nowPitch == @event.NoteNumber) nowPitch = 0;
            //            }
            //            break;
            //        default:
            //            isLastOnEvent = false;
            //            nowPitch = 0;
            //            break;
            //    }

            
        }
        public void ArrangeKeyPlaysNew(int index)
        {
            

            var trunk = trunks.ElementAt(index);
            var trunkEvents = trunks.ElementAt(index).Events;
            var tickBase = 60000 / (float)Bpm /
                           Convert.ToDouble(midi.TimeDivision.ToString()
                               .TrimEnd(" ticks/qnote".ToCharArray()));
            var eventsManager = new TimedEventsManager(trunkEvents);
            var notesManager = new NotesManager(trunkEvents);


            var tempo = midi.GetTempoMap().Tempo.AtTime(0);//get tempo?

            var isLastOnEvent = false;
            var nowPitch = 0;

            playback = new Playback(trunkEvents, midi.GetTempoMap(), outputDevice);
            //var task = System.Threading.Tasks.Task.Run(() => { playback.Start(); });

            playback.Start();
            var retKeyPlayLists = ArrangeKeyPlays(index);
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