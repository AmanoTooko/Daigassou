using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
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
    }
}