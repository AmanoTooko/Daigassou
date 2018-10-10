using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Daigassou
{
    class CommonUtilities
    {
        private const string LatestApiAddress = "https://raw.githubusercontent.com/AmanoTooko/Daigassou/master/Version.ORZ";

        static public async void GetLatestVersion()
        {
            WebClient wc = new WebClient();

            string nowVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string newVersion = await wc.DownloadStringTaskAsync(LatestApiAddress);
            if (nowVersion != newVersion)
            {
                MessageBox.Show($"检测到新版本{newVersion}已经发布，请下载最新版！", "版本更新", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            

        }
    }
}
