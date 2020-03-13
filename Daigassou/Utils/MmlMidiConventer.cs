using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commons.Music.Midi.Mml;
using Melanchall.DryWetMidi.Smf;
using Melanchall.DryWetMidi.Common;
using mml2MidiDotNet;
namespace Daigassou.Utils
{
    class MmlMidiConventer
    {
		public static byte[] mmlRead(string mmlPath)
		{

			mml2midi c = new mml2midi();
			var ret = 0;
			var midiPath = mmlPath.Replace(".mml", ".mid");
			unsafe
			{
				fixed (byte* b1 = (Encoding.Default.GetBytes(mmlPath)),
					b2 = Encoding.Default.GetBytes(midiPath))
				{
					ret = c.convert((sbyte*)b1, (sbyte*)b2);
				}
				
			}
			if (ret != 0)
			{
				try
				{
					var sources = new List<MmlInputSource>();
					sources.Add(new MmlInputSource("fake.mml", new StringReader(File.ReadAllText(mmlPath))));
					using (var outs = new MemoryStream())
					{
						new MmlCompiler().Compile(false, sources, null, outs, false);
						return outs.ToArray();
					}
				}
				catch (Exception)
				{
					return null;

				}

				
			}
			else
			{
				var mmlOut = File.ReadAllBytes(midiPath);
				File.Delete(midiPath);
				return mmlOut;
			}
			
		}
	}
}
