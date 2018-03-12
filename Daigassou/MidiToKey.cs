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
    class MidiToKey
    {
        private MidiFile midi;
        private List<NotesManager> tracks;

        public MidiToKey()
        {
            tracks=new  List<NotesManager>();
        }
        public void OpenFile(string path)
        {
           midi=MidiFile.Read(path);
            
        }

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
            TempoMap T = midi.GetTempoMap();
            return 0;
        }

        public void GetKeyPlays()
        {
            
        }
    }
}
