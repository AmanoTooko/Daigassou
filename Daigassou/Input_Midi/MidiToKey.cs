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

        public MidiToKey()
        {
            tracks = new List<NotesManager>();
            Bpm = 80;
            Offset = EnumPitchOffset.None;
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
                foreach (var ev in trunkEvents)
                    switch (ev)
                    {
                        case NoteOnEvent _:
                        {
                            var @event = ev as NoteOnEvent;
                            if (@event != null && @event.DeltaTime == 0) continue;
                            if (@event.NoteNumber >= 84 || @event.NoteNumber <= 48) continue;
                            retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOn,
                                (int) (@event.NoteNumber + Offset), (int) (tickbase * @event.DeltaTime)));
                        }
                            break;
                        case NoteOffEvent _:
                        {
                            var @event = ev as NoteOffEvent;
                            if (@event != null && @event.DeltaTime == 0) continue;
                            if (@event.NoteNumber >= 84 || @event.NoteNumber <= 48) continue;
                            retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff,
                                (int) (@event.NoteNumber + Offset), (int) (tickbase * @event.DeltaTime)));
                        }
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