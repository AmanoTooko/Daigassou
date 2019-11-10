using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Daigassou.Input_Midi
{
    public class BackgroundKey
    {
        private const int WmKeydown = 0x0100;
        private const int WmKeyup = 0x0101;

        private static IntPtr _gameIntPtr;

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
                    yield return p.MainWindowHandle;
                p.Dispose();
            }
        }

        public void Init(IntPtr gameIntPtr)
        {
            _gameIntPtr = gameIntPtr;
        }

        public void BackgroundKeyPress(Keys viKeys)
        {
            if (_gameIntPtr != IntPtr.Zero)
                SendMessage(_gameIntPtr, WmKeydown, (int) viKeys, 0);
        }

        public void BackgroundKeyRelease(Keys viKeys)
        {
            if (_gameIntPtr != IntPtr.Zero)
                SendMessage(_gameIntPtr, WmKeyup, (int) viKeys, 0);
        }
    }
}