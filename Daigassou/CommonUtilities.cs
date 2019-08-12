using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Daigassou
{
    internal class CommonUtilities
    {
        private const string LatestApiAddress =
            "https://raw.githubusercontent.com/AmanoTooko/Daigassou/master/Daigassou/Version.ORZ";

        public static async void GetLatestVersion()
        {
            var wc = new WebClient();
            try
            {
                var nowVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                var newVersion = await wc.DownloadStringTaskAsync(LatestApiAddress);
                if (nowVersion != newVersion)
                    if (MessageBox.Show($"检测到新版本{newVersion}已经发布，点击确定下载最新版哦！\r\n 当然就算你点了取消，这个提示每次打开还会出现的哦！", "哇——更新啦！",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Information) == DialogResult.OK)
                        Process.Start("https://github.com/AmanoTooko/Daigassou/releases");
            }
            catch (Exception e)
            {
            }
        }

        public static void WriteLog(string msg)
        {
            System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("O")}\t\t\t" + $"{msg}");
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SystemTime
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMiliseconds;
        }

        public class SetSystemDateTime
        {
            [DllImport("Kernel32.dll")]
            public static extern bool SetLocalTime(ref SystemTime sysTime);

            public static bool SetLocalTimeByStr(DateTime dt)
            {
                bool flag = false;
                SystemTime sysTime = new SystemTime();
                sysTime.wYear = Convert.ToUInt16(dt.Year);
                sysTime.wMonth = Convert.ToUInt16(dt.Month);
                sysTime.wDay = Convert.ToUInt16(dt.Day);
                sysTime.wHour = Convert.ToUInt16(dt.Hour);
                sysTime.wMinute = Convert.ToUInt16(dt.Minute);
                sysTime.wSecond = Convert.ToUInt16(dt.Second);
                sysTime.wMiliseconds = Convert.ToUInt16(dt.Millisecond);
                try
                {
                    flag = SetSystemDateTime.SetLocalTime(ref sysTime);
                }
                catch (Exception e)
                {
                    WriteLog("Failed to set system date time with exception "+e.Message);
                }

                return flag;
            }
        }
    }
}