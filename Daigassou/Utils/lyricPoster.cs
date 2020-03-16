using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;

namespace Daigassou.Utils
{
    public static class lyricPoster
    {
        public class lyricLine
        {
            public string text = "";
            public int startTimeMs = 0;
            public lyricLine(string _time, string _text)
            {
                text = _text;
                var result=Regex.Match(_time, @"(?<min>\d+):(?<sec>\d+).(?<hm>\d+)");
                startTimeMs += Convert.ToInt32(result.Groups["min"].Value) * 60000 + Convert.ToInt32(result.Groups["sec"].Value) * 1000 + Convert.ToInt32(result.Groups["hm"].Value) * 10;
            } 
    }
        public static uint port=2345;
        public static string suffix = "/s";
        public static string url = $"http://127.0.0.1:{port}/command";
        public static Thread LrcThread;
        public static bool IsLrcEnable=false;
        internal static Queue<lyricLine> AnalyzeLrc(string path)
        {
            Queue<lyricLine> ret = new Queue<lyricLine>();
            var text = File.ReadAllText(path);
            var reg = new Regex(@"\[(?<time>.*)\](?<lyric>.*)\r\n");
            var c = reg.Matches(text);
            foreach (Match item in c)
            {
                ret.Enqueue(new lyricLine(item.Groups["time"].Value, item.Groups["lyric"].Value));
            }
            return ret;
        }
        public static void  PostJson(string text)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://127.0.0.1:{port}/command");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write($"/s ♪ {text} ♪");
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                httpWebRequest.GetResponse();
            }
            catch (Exception ex)
            {

            }
        }
        public static void LrcStart(string path, int startOffset)
        {
            Queue<lyricLine> lyric;

            try
            {
                if (File.Exists(path)&& IsLrcEnable)
                {
                    lyric = AnalyzeLrc(path);
                    LrcThread = new Thread(
                            () => {RunningLrc(lyric, startOffset);}
                            );
                    Log.overlayLog("【歌词播放】歌词导入成功，开始播放");
                    LrcThread.Start();
                }
                else
                {
                    Log.overlayLog("【歌词播放】找不到lrc文件");
                    return;
                }
            }
            catch (Exception)
            {
                Log.overlayLog("【歌词播放】解析出错");
                throw;
            }


        }
        public static void LrcStop()
        {
            if(LrcThread!=null)
            {
                LrcThread.Abort();
            }
        }
        public static void RunningLrc(Queue<lyricLine> lyric, int startOffset)
        {

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                while (lyric.Any<lyricLine>())
                {
                    lyricLine text = lyric.Dequeue();
                    double num1 = (double)startOffset + text.startTimeMs;
                    while (true)
                    {
                        if ((double)ParameterController.GetInstance().Offset + num1 > (double)stopwatch.ElapsedMilliseconds)
                            Thread.Sleep(1);
                        else
                            break;
                    }
                    PostJson(text.text);

                }
            }

            

        }
    }

