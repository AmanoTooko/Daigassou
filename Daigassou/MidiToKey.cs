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
    enum EnumPitchOffset:int
    {
        OctaveLower=-12,
        None=0,
        OctaveHigher=+12
    }
    class MidiToKey
    {
        private MidiFile midi;
        private List<NotesManager> tracks;
        private TempoMap Tmap;
        public EnumPitchOffset Offset { get; set; }
        public int Bpm { get; set; }
        public int Index = 0;
        public MidiToKey()
        {
            tracks=new  List<NotesManager>();
            Bpm = 80;
            Offset = EnumPitchOffset.None;
        }
        public void OpenFile(string path)
        {
           midi=MidiFile.Read(path);
            Tmap = midi.GetTempoMap();
        }
        /// <summary>
        /// Get a String list of Note name from noteManager
        /// </summary>
        /// <returns></returns>
        public List<string> getTrackManagers() 
        {
            List<string> score=new List<string>();
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

        public  Queue<KeyPlayList> ArrangeKeyPlays(int index)
        {
            try
            {
                NotesManager noteManager = tracks[index];
                Queue<KeyPlayList> retKeyPlayLists = new Queue<KeyPlayList>();
                long lastTime = -1;
                foreach (var note in noteManager.Notes)
                {
                    var timeInterval = note.Time - lastTime;
                    var noteNumber = note.NoteNumber + (int)Offset;
                    var tickCount = 0;
                    if (timeInterval == 0) continue;
                    if (noteNumber >= 84 || noteNumber <= 48) continue;
                    tickCount = (int)((60000 / (float)Bpm) / Convert.ToDouble(midi.TimeDivision.ToString().TrimEnd(" ticks/qnote".ToCharArray())) * timeInterval);
                    retKeyPlayLists.Enqueue(new KeyPlayList(KeyBinding.GetNoteToKey(noteNumber), tickCount));
                    lastTime = note.Time;
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
