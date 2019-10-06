using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Daigassou.Input_Midi
{
    public class BackgroundKey
    {
        const int WmKeydown = 0x0100;
        const int WmKeyup = 0x0101;
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll", EntryPoint = "PostMessage", CallingConvention = CallingConvention.Winapi)]
        public static extern bool PostMessage(IntPtr hwnd, uint msg, uint wParam, uint lParam);

        public static IEnumerable<IntPtr> GetPids()
        {
            foreach (var p in Process.GetProcesses())
            {
                if (string.Equals(p.ProcessName, "ffxiv", StringComparison.Ordinal)
                    || string.Equals(p.ProcessName, "ffxiv_dx11", StringComparison.Ordinal))
                {
                    yield return p.MainWindowHandle;
                }
                p.Dispose();
            }
        }

        private static IntPtr _gameIntPtr;
        public void Init(IntPtr gameIntPtr)
        {
            _gameIntPtr = _gameIntPtr;
        }

        public void BackgroundKeyPress(Keys viKeys)
        {
            if (_gameIntPtr != IntPtr.Zero) 
                SendMessage(_gameIntPtr, WmKeydown, (int)viKeys, 0);
        }

        public void BackgroundKeyRelease(Keys viKeys)
        {
            if (_gameIntPtr != IntPtr.Zero)
                SendMessage(_gameIntPtr, WmKeyup, (int)viKeys, 0);
        }
    }
}
