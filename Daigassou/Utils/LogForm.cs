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

        public static void B(byte[] text)
        {
            Debug(text.ToString());
        }
    }
}
