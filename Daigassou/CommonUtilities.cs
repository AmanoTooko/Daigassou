using System;
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
                    MessageBox.Show($"检测到新版本{newVersion}已经发布，请下载最新版！", "更新啦！", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
            }
        }
    }
}