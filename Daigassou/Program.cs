using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using BondTech.HotkeyManagement.Win;
using Daigassou.Forms;

namespace Daigassou
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]


        static void Main() 
        {

            try
            {
              
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                #region 应用程序的主入口点
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainFormEx());
                #endregion
            }
            catch (Exception ex)
            {
                if (ex is HotKeyAlreadyRegisteredException)
                {
                    MessageBox.Show(@"快捷键似乎注册失败了，是否已经被占用？", @"快捷键无法注册", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string str = GetExceptionMsg(ex, string.Empty);
                    MessageBox.Show(str, @"系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            MessageBox.Show(str, @"系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //LogManager.WriteLog(str);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            MessageBox.Show(str, @"系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //LogManager.WriteLog(str);
        }

        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("*************************软件内部出错*************************");
            sb.AppendLine("****************请将当前界面截图并反馈给我们****************");
            sb.AppendLine("【异常时间】：" + DateTime.Now.ToString());
            sb.AppendLine("【软件版本】：" + $"Ver{ Assembly.GetExecutingAssembly().GetName().Version}");
            if (ex != null)
            {                
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }
    }
}
