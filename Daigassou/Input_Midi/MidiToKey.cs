using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Melanchall.DryWetMidi;
using Melanchall.DryWetMidi.Smf;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Smf.Interaction;


namespace Daigassou
{
    internal enum EnumPitchOffset:int
    {
        OctaveLower = -12,
        None = 0,
        OctaveHigher = +12
    }

    internal class MidiToKey
    {
        private MidiFile midi;
        private List<NotesManager> tracks;
        private TempoMap Tmap;
        public EnumPitchOffset Offset { get; set; }
        public int Bpm { get; set; }
        public int Index = 0;
        
        public MidiToKey()
        {
            tracks = new List<NotesManager>();
            Bpm = 80;
            Offset = EnumPitchOffset.None;
        }

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
            List<string> score = new List<string>();
            foreach (var track in midi.GetTrackChunks())
            {
                
                tracks.Add(track.ManageNotes());
            }


            foreach (var noteManager in tracks)
            {
                StringBuilder track = new StringBuilder("");
                foreach (var note in noteManager.Notes)
                {
                    track.Append(note.ToString() + " ");
                }

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
                Queue<KeyPlayList> retKeyPlayLists = new Queue<KeyPlayList>();
                var tickbase = (int) ((60000 / (float) Bpm) /
                                   Convert.ToDouble(midi.TimeDivision.ToString()
                                       .TrimEnd(" ticks/qnote".ToCharArray())) );
                foreach (var ev in trunkEvents)
                {
                   
                    switch (ev)
                    {
                        case NoteOnEvent _:
                        {
                            
                            var @event = ev as NoteOnEvent;
                            if (@event != null && @event.DeltaTime==0)
                            {
                                continue;
                            }
                            if (@event.NoteNumber >= 84 || @event.NoteNumber <= 48)
                            {
                                continue;
                            }
                            retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOn, (int) (@event.NoteNumber+ Offset), tickbase*@event.DeltaTime));




                            }
                            break;
                        case NoteOffEvent _:
                        {
                            var @event = ev as NoteOffEvent;
                            if (@event != null && @event.DeltaTime == 0)
                            {
                                continue;
                            }
                                if (@event.NoteNumber >= 84 || @event.NoteNumber <= 48)
                            {
                                continue;
                            }
                            retKeyPlayLists.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff, (int) (@event.NoteNumber+ Offset), tickbase * @event.DeltaTime));
                            }
                            break;
                    }
                   
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