using RainbowMage.HtmlRenderer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Input;
//using UpdateHelper;

namespace RainbowMage.OverlayPlugin
{
  public abstract class OverlayBase<TConfig> : IOverlay, IDisposable where TConfig : OverlayConfigBase
  {
   
    protected System.Timers.Timer timer;
    protected System.Timers.Timer xivWindowTimer;

    public event EventHandler<LogEventArgs> OnLog;

    public string Name { get; private set; }

    public OverlayForm Overlay { get; private set; }

    public TConfig Config { get; private set; }

    public IPluginConfig PluginConfig { get; set; }

    protected OverlayBase(TConfig config, string name)
    {
      this.Config = config;
      this.Name = name;
      this.InitializeOverlay();
      this.InitializeTimer();
      this.InitializeConfigHandlers();
    }

    public void Start()
    {
      this.timer.Start();
      this.xivWindowTimer.Start();
    }

    public void Stop()
    {
      this.timer.Stop();
      this.xivWindowTimer.Stop();
    }

    protected virtual void InitializeOverlay()
    {
      try
      {
        this.Overlay = new OverlayForm(Assembly.GetExecutingAssembly().GetName().Version.ToString(), this.Name, "about:blank", this.Config.MaxFrameRate);
        if (!Util.IsOnScreen((Form) this.Overlay))
          this.Overlay.StartPosition = FormStartPosition.WindowsDefaultLocation;
        else
          this.Overlay.Location = this.Config.Position;
        this.Overlay.Text = this.Name;
        this.Overlay.Size = this.Config.Size;
        this.Overlay.IsClickThru = this.Config.IsClickThru;
        this.Overlay.Renderer.BrowserError += (EventHandler<BrowserErrorEventArgs>) ((o, e) => this.Log(LogLevel.Error, "浏览器错误: {0}, {1}, {2}", (object) e.ErrorCode, (object) e.ErrorText, (object) e.Url));
        this.Overlay.Renderer.BrowserLoad += (EventHandler<BrowserLoadEventArgs>) ((o, e) =>
        {
          this.Log(LogLevel.Debug, "浏览器读取: {0}: {1}", (object) e.HttpStatusCode, (object) e.Url);
          this.NotifyOverlayState();
        });
        this.Overlay.Renderer.BrowserConsoleLog += (EventHandler<BrowserConsoleLogEventArgs>) ((o, e) => this.Log(LogLevel.Info, "浏览器控制: {0} (目标: {1}, 行: {2})", (object) e.Message, (object) e.Source, (object) e.Line));
        this.Config.UrlChanged += (EventHandler<UrlChangedEventArgs>) ((o, e) => this.Navigate(e.NewUrl));
        if (this.CheckUrl(this.Config.Url))
          this.Navigate(this.Config.Url);
        else
          this.Navigate("about:blank");
        this.Overlay.Show();
        this.Overlay.Visible = this.Config.IsVisible;
        this.Overlay.Locked = this.Config.IsLocked;
      }
      catch (Exception ex)
      {
        this.Log(LogLevel.Error, "初始化美化窗口: {0}", (object) this.Name, (object) ex);
      }
    }

    private ModifierKeys GetModifierKey(Keys modifier)
    {
      ModifierKeys modifierKeys = (ModifierKeys) 0;
      if ((modifier & Keys.Shift) == Keys.Shift)
        modifierKeys |= ModifierKeys.Shift;
      if ((modifier & Keys.Control) == Keys.Control)
        modifierKeys |= ModifierKeys.Control;
      if ((modifier & Keys.Alt) == Keys.Alt)
        modifierKeys |= ModifierKeys.Alt;
      if ((modifier & Keys.LWin) == Keys.LWin || (modifier & Keys.RWin) == Keys.RWin)
        modifierKeys |= ModifierKeys.Windows;
      return modifierKeys;
    }

    private bool CheckUrl(string url)
    {
      try
      {
        Uri uri = new Uri(url);
        if (uri.Scheme == "file")
        {
          if (!File.Exists(uri.LocalPath))
          {
            this.Log(LogLevel.Warning, "初始化美化窗口: 本地文件 {0} 不存在!", (object) uri.LocalPath);
            return false;
          }
        }
      }
      catch (Exception ex)
      {
        this.Log(LogLevel.Error, "初始化美化窗口: URI解析错误! 请核对URL. (Config.Url = {0}): {1}", (object) this.Config.Url, (object) ex);
        return false;
      }
      return true;
    }

    protected virtual void InitializeTimer()
    {
      this.timer = new System.Timers.Timer();
      this.timer.Interval = 1000.0;
      this.timer.Elapsed += (ElapsedEventHandler) ((o, e) =>
      {
        try
        {
          this.Update();
        }
        catch (Exception ex)
        {
          this.Log(LogLevel.Error, "更新: {0}", (object) ex.ToString());
        }
      });
      this.xivWindowTimer = new System.Timers.Timer();
      this.xivWindowTimer.Interval = 1000.0;
      this.xivWindowTimer.Elapsed += (ElapsedEventHandler) ((o, e) =>
      {
        try
        {
          if (!this.Config.IsVisible || !this.PluginConfig.HideOverlaysWhenNotActive)
            return;
          IntPtr foregroundWindow = NativeMethods.GetForegroundWindow();
          if (foregroundWindow == IntPtr.Zero)
            return;
          uint lpdwProcessId;
          int windowThreadProcessId = (int) NativeMethods.GetWindowThreadProcessId(foregroundWindow, out lpdwProcessId);
          string fileName = Process.GetProcessById((int) lpdwProcessId).MainModule.FileName;
          if (Path.GetFileName(fileName.ToString()) == "ffxiv.exe" || Path.GetFileName(fileName.ToString()) == "ffxiv_dx11.exe" || fileName.ToString() == Process.GetCurrentProcess().MainModule.FileName)
            this.Overlay.Visible = true;
          else
            this.Overlay.Visible = false;
        }
        catch (Exception ex)
        {
          this.Log(LogLevel.Error, "最终幻想14窗口监视: {0}", (object) ex.ToString());
        }
      });
    }

    protected virtual void InitializeConfigHandlers()
    {
      this.Config.VisibleChanged += (EventHandler<VisibleStateChangedEventArgs>) ((o, e) => this.Overlay.Visible = e.IsVisible);
      this.Config.ClickThruChanged += (EventHandler<ThruStateChangedEventArgs>) ((o, e) => this.Overlay.IsClickThru = e.IsClickThru);
      this.Config.LockChanged += (EventHandler<LockStateChangedEventArgs>) ((o, e) =>
      {
        this.Overlay.Locked = e.IsLocked;
        this.NotifyOverlayState();
      });
    }

    protected abstract void Update();

    public virtual void Dispose()
    {
      try
      {
        if (this.timer != null)
          this.timer.Stop();
        if (this.xivWindowTimer != null)
          this.xivWindowTimer.Stop();
        if (this.Overlay != null)
        {
          this.Overlay.Close();
          this.Overlay.Dispose();
        }

      }
      catch (Exception ex)
      {
        this.Log(LogLevel.Error, "清除: {0}", (object) ex);
      }
    }

    public virtual void Navigate(string url)
    {
      this.Overlay.Url = url;
    }

    protected void Log(LogLevel level, string message)
    {
      if (this.OnLog == null)
        return;
      this.OnLog((object) this, new LogEventArgs(level, string.Format("{0}: {1}", (object) this.Name, (object) message)));
    }

    protected void Log(LogLevel level, string format, params object[] args)
    {
      this.Log(level, string.Format(format, args));
    }

    public void SavePositionAndSize()
    {
      this.Config.Position = this.Overlay.Location;
      this.Config.Size = this.Overlay.Size;
    }

    private void NotifyOverlayState()
    {
      string script = string.Format("document.dispatchEvent(new CustomEvent('onOverlayStateUpdate', {{ detail: {{ isLocked: {0} }} }}));", this.Config.IsLocked ? (object) "true" : (object) "false");
      if (this.Overlay == null || this.Overlay.Renderer == null || this.Overlay.Renderer.Browser == null)
        return;
      this.Overlay.Renderer.ExecuteScript(script);
    }

    public void SendMessage(string message)
    {
      string script = string.Format("document.dispatchEvent(new CustomEvent('onBroadcastMessageReceive', {{ detail: {{ message: \"{0}\" }} }}));", (object) Util.CreateJsonSafeString(message));
      if (this.Overlay == null || this.Overlay.Renderer == null || this.Overlay.Renderer.Browser == null)
        return;
      this.Overlay.Renderer.ExecuteScript(script);
    }

    public virtual void OverlayMessage(string message)
    {
    }
        internal static class Util
        {
            public static string CreateJsonSafeString(string str)
            {
                return str.Replace("\"", "\\\"").Replace("'", "\\'").Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t");
            }

            public static string ReplaceNaNString(string str, string replace)
            {
                return str.Replace(double.NaN.ToString(), replace);
            }

            public static bool IsOnScreen(Form form)
            {
                foreach (Screen allScreen in Screen.AllScreens)
                {
                    if (allScreen.WorkingArea.IntersectsWith(new Rectangle(form.Left, form.Top, form.Width, form.Height)))
                        return true;
                }
                return false;
            }

            public static void HidePreview(Form form)
            {
                uint num = NativeMethods.GetWindowLong(form.Handle, -20) | 128;
                NativeMethods.SetWindowLongA(form.Handle, -20, (IntPtr)num);
            }

            public static string GetHotkeyString(Keys modifier, Keys key, string defaultText = "")
            {
                StringBuilder stringBuilder = new StringBuilder();
                if ((modifier & Keys.Shift) == Keys.Shift)
                    stringBuilder.Append("Shift + ");
                if ((modifier & Keys.Control) == Keys.Control)
                    stringBuilder.Append("Ctrl + ");
                if ((modifier & Keys.Alt) == Keys.Alt)
                    stringBuilder.Append("Alt + ");
                if ((modifier & Keys.LWin) == Keys.LWin || (modifier & Keys.RWin) == Keys.RWin)
                    stringBuilder.Append("Win + ");
                stringBuilder.Append(Enum.ToObject(typeof(Keys), (object)key).ToString());
                return stringBuilder.ToString();
            }

            public static Keys RemoveModifiers(Keys keyCode, Keys modifiers)
            {
                Keys keys1 = keyCode;
                foreach (Keys keys2 in new List<Keys>()
      {
        Keys.ControlKey,
        Keys.LControlKey,
        Keys.Alt,
        Keys.ShiftKey,
        Keys.Shift,
        Keys.LShiftKey,
        Keys.RShiftKey,
        Keys.Control,
        Keys.LWin,
        Keys.RWin
      })
                {
                    if (keys1.HasFlag((Enum)keys2) && keys1 == keys2)
                        keys1 &= ~keys2;
                }
                return keys1;
            }
        }

        
    }
    public static class NativeMethods
    {
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        public static readonly IntPtr HWND_TOP = new IntPtr(0);
        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_LAYERED = 524288;
        public const int WS_EX_TOOLWINDOW = 128;
        public const int WS_EX_TRANSPARENT = 32;
        public const int LWA_ALPHA = 2;
        public const int LWA_COLORKEY = 1;
        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;
        public const uint SWP_NOSIZE = 1;
        public const uint SWP_NOMOVE = 2;
        public const uint TOPMOST_FLAGS = 3;
        public const int ERROR_INSUFFICIENT_BUFFER = 122;
        public const byte AC_SRC_ALPHA = 1;
        public const byte AC_SRC_OVER = 0;
        public const int ULW_ALPHA = 2;
        public const int DIB_RGB_COLORS = 0;
        public const uint GW_HWNDPREV = 3;
        public const uint SWP_NOACTIVATE = 16;
        public const int WM_KEYDOWN = 256;
        public const int WM_KEYUP = 257;
        public const int WM_CHAR = 258;
        public const int WM_SYSKEYDOWN = 260;
        public const int WM_SYSKEYUP = 261;
        public const int WM_SYSCHAR = 262;

        [DllImport("user32")]
        public static extern bool UpdateLayeredWindow(
          IntPtr hWnd,
          IntPtr hdcDst,
          [In] ref NativeMethods.Point pptDst,
          [In] ref NativeMethods.Size pSize,
          IntPtr hdcSrc,
          [In] ref NativeMethods.Point pptSrc,
          int crKey,
          [In] ref NativeMethods.BlendFunction pBlend,
          uint dwFlags);

        [DllImport("gdi32")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32")]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        [DllImport("gdi32")]
        public static extern IntPtr CreateDIBSection(
          IntPtr hdc,
          [In] ref NativeMethods.BitmapInfo pbmi,
          uint iUsage,
          out IntPtr ppvBits,
          IntPtr hSection,
          uint dwOffset);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


        [DllImport("kernel32")]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("kernel32.dll")]
        public static extern void SetLastError(int dwErrorCode);

        private static int ToIntPtr32(IntPtr intPtr)
        {
            return (int)intPtr.ToInt64();
        }

        public static IntPtr SetWindowLongA(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            IntPtr num1 = IntPtr.Zero;
            NativeMethods.SetLastError(0);
            int lastWin32Error;
            if (IntPtr.Size == 4)
            {
                int num2 = NativeMethods.SetWindowLong(hWnd, nIndex, NativeMethods.ToIntPtr32(dwNewLong));
                lastWin32Error = Marshal.GetLastWin32Error();
                num1 = new IntPtr(num2);
            }
            else
            {
                num1 = NativeMethods.SetWindowLongPtr(hWnd, nIndex, dwNewLong);
                lastWin32Error = Marshal.GetLastWin32Error();
            }
            if (num1 == IntPtr.Zero && lastWin32Error != 0)
                throw new Win32Exception(lastWin32Error);
            return num1;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);



        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);



        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int GetModuleFileName(IntPtr hModule, StringBuilder lpFilename, int nSize);

        [DllImport("user32.dll")]
        public static extern short GetKeyState(int nVirtKey);

        public struct BitmapInfo
        {
            public NativeMethods.BitmapInfoHeader bmiHeader;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.Struct)]
            public NativeMethods.RgbQuad[] bmiColors;
        }

        public struct BitmapInfoHeader
        {
            public uint biSize;
            public int biWidth;
            public int biHeight;
            public ushort biPlanes;
            public ushort biBitCount;
            public NativeMethods.BitmapCompressionMode biCompression;
            public uint biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public uint biClrUsed;
            public uint biClrImportant;

            public void Init()
            {
                this.biSize = (uint)Marshal.SizeOf((object)this);
            }
        }

        public struct BlendFunction
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        public struct Point
        {
            public int X;
            public int Y;
        }

        public struct Size
        {
            public int Width;
            public int Height;
        }

        public struct RgbQuad
        {
            public byte rgbBlue;
            public byte rgbGreen;
            public byte rgbRed;
            public byte rgbReserved;
        }

        public enum BitmapCompressionMode : uint
        {
            BI_RGB,
            BI_RLE8,
            BI_RLE4,
            BI_BITFIELDS,
            BI_JPEG,
            BI_PNG,
        }
        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod", SetLastError = true)]
        internal static extern uint TimeBeginPeriod(uint uMilliseconds);

        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod", SetLastError = true)]
        internal static extern uint TimeEndPeriod(uint uMilliseconds);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowPos(
          IntPtr hWnd,
          IntPtr hWndInsertAfter,
          int X,
          int Y,
          int cx,
          int cy,
          uint uFlags);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int SetWindowLong(IntPtr Handle, int nIndex, uint dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ShowWindow(IntPtr Handle, uint nCmdShow);

        [DllImport("kernel32.dll")]
        internal static extern bool ReadProcessMemory(
          IntPtr hProcess,
          IntPtr lpBaseAddress,
          byte[] lpBuffer,
          IntPtr nSize,
          ref IntPtr lpNumberOfBytesRead);
    }
}
