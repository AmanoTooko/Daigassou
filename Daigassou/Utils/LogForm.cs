using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Daigassou;
namespace Daigassou.Utils
{
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
        }
    }

    public static class Log
    {
        private static LogForm logform { get; set; }
        private static DateTime lastTime;
        public static RainbowMage.OverlayPlugin.LabelOverlayConfig log;
        public static bool isBeta=false;
        public static void overlayLog(string text)
        {
            if (log!=null)
            {
                log.Text = string.Format($"[{DateTime.Now.ToString("HH:mm:ss.fff")}] {text}");
                Console.WriteLine(string.Format($"[{DateTime.Now.ToString("HH:mm:ss.fff")}] {text}"));
            }
        }

        public static void overlayProcess(string process)
        {
            if (log != null)
            {
                log.Process = process;
            }
        }
        public static void Debug(string text)
        {
            Console.WriteLine(text);
            //output(Color.Blue, text);
        }

        private static void output(Color c, string s)
        {
            logform?.Invoke(new Action(() =>
            {
                logform.LogTextBox.SelectionColor = c; 
                logform.LogTextBox.AppendText(s);
                logform.LogTextBox.SelectionColor = logform.LogTextBox.ForeColor;
                
            }));
        }

        public static void I(string text)
        {
            Debug(text);
        }
        public static void E(string text)
        {
            Debug(text);
        }
        public static void Ex(Exception e,string text)
        {
            Debug(text);
        }
        public static void S(string text)
        {
            Debug(text);
        }

        public static void B(byte[] text,bool isoffset)
        {
            var sb = new StringBuilder();
            var delaytime = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == 0xFF)
                {
                    delaytime += Convert.ToInt32(text[i + 1]);
                }
            }

            if (isoffset)
            {
                if ((DateTime.Now - lastTime).Milliseconds - delaytime > 150)
                {
                    Console.WriteLine("???");
                }
                sb.Append($"{DateTime.Now.ToString("O")}   {text.Length} Bytes {delaytime} ms Interval {(DateTime.Now - lastTime).Milliseconds} ms");
                sb.AppendLine();lastTime = DateTime.Now;
            }
            else
            {

                sb.Append($"{DateTime.Now.ToString("O")}   {text.Length} Bytes");

            }

            for (var i = 0; i < text.Length; i++)
            {
                //if (i != 0)
                //{
                //    if (i % 16 == 0)
                //    {
                //        sb.AppendLine();
                //    }
                //    else if (i % 8 == 0)
                //    {
                //        sb.Append(' ', 2);
                //    }
                //    else
                //    {
                //        sb.Append(' ');
                //    }
                //}
                sb.Append(' ');
                sb.Append(text[i].ToString("X2"));
            }

            //Log.overlayLog(sb.ToString());
            Debug(sb.ToString());
        }
    }
}
