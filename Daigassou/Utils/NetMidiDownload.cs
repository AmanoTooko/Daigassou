using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Daigassou.Utils
{
    class NetMidiDownload
    {
        private static string url = "http://midi.ffxiv.cat:8808/song/dl/";
        public static string DownloadMidi(string id)
        {



                try
                {
                    var request = (HttpWebRequest)WebRequest.Create(url+id);
                request.Headers.Add("Accept-Encoding", "gzip,deflate");
                    var response = (HttpWebResponse)request.GetResponse();
                using (GZipStream stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        var responseString = reader.ReadToEnd();
                        return responseString;

                    }
                }
                
                    
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e);
                    throw e;
                }



            
        }
    }
}
