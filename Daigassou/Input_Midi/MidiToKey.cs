using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public int Index = 0;
        private MidiFile midi;
        private TempoMap Tmap;
        private readonly List<NotesManager> tracks;
        private readonly int MAX_DELAY_TICK;

        public MidiToKey()
        {
            tracks = new List<NotesManager>();
            Bpm = 80;
            Offset = EnumPitchOffset.None;
            MAX_DELAY_TICK = 30;
        }

        public EnumPitchOffset Offset { get; set; }
        public int Bpm { get; set; }

        public void OpenFile(string path)
        {
            midi = MidiFile.Read(path);
            Tmap = midi.GetTempoMap();
        }

        /// <summary>
        ///     Get a String list of Note name from noteManager
        /// </summary>
        /// <returns></returns>
        public List<string> GetTrackManagers()
        {
            tracks.Clear();
            var score = new List<string>();
            foreach (var track in midi.GetTrackChunks()) tracks.Add(track.ManageNotes());


            foreach (var noteManager in tracks)
            {
                var track = new StringBuilder("");
                foreach (var note in noteManager.Notes) track.Append(note + " ");

                score.Add(track.ToString());
            }

            return score;
        }

        public int GetTimerTick()
        {
            return 0;
        }

        public Queue<KeyPlayList> ArrangeKeyPlays(int index)
        {
            try
            {
                var trunkEvents = midi.GetTrackChunks().ElementAt(index).Events;
                var retKeyPlayLists = new Queue<KeyPlayList>();
                var tickbase = 60000 / (float) Bpm /
                               Convert.ToDouble(midi.TimeDivision.ToString()
                                   .TrimEnd(" ticks/qnote".ToCharArray()));
                var isLastOffEvent = true;
                foreach (var ev in trunkEvents)
                    switch (ev)
                    {
                        case NoteOnEvent _:
                        {
                            
                            var @event = ev as NoteOnEvent;
                            
                            //if (@event == null ) continue;
                            var notenumber = (int)(@event.NoteNumber + Offset);
                                if (notenumber >= 84 || notenumber <= 48) continue;
                            if (isLastOffEvent == true&& (tickbase * @event.DeltaTime)<MAX_DELAY_TICK)
                            {
                                retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOn,
                                    notenumber, MAX_DELAY_TICK));
                                
                            }
                            else
                            {
                                retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOn,
                                    notenumber, (int)(tickbase * @event.DeltaTime)));

                            }

                            isLastOffEvent = false;

                        }
                            break;
                        case NoteOffEvent _:
                        {
                            var @event = ev as NoteOffEvent;
                           
                            if (@event != null && @event.DeltaTime == 0) continue;
                            var notenumber = (int)(@event.NoteNumber + Offset);
                                if (notenumber >= 84 || notenumber <= 48) continue;
                            retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
                                (int) (notenumber), (int) (tickbase * @event.DeltaTime)));
                            isLastOffEvent = true;
                        }
                            break;
                        default:
                            isLastOffEvent = false;
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
    }
}