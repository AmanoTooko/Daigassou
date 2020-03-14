using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commons.Music.Midi.Mml;
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
			var text = File.ReadAllText(mmlPath);
			if(text.Contains("MML@"))
			{
				unsafe
				{
					try
					{
						fixed (byte* b1 = (Encoding.Default.GetBytes(mmlPath)),
						b2 = Encoding.Default.GetBytes(midiPath))
						{
							ret = c.convert((sbyte*)b1, (sbyte*)b2);
						}
					}
					catch (Exception)
					{

						return null;
					}


				}
			}
			else
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
				catch (Exception e)
				{
					return null;

				}
			}
			if (ret == 0)
			{

				var mmlOut = File.ReadAllBytes(midiPath);
				File.Delete(midiPath);
				return mmlOut;

			}
			return null;
			
		}
	}
}
