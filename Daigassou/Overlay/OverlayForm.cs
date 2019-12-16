// Decompiled with JetBrains decompiler
// Type: RainbowMage.OverlayPlugin.OverlayForm
// Assembly: OverlayPlugin.Core, Version=0.3.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 8E4CC27A-B72E-4CED-BAB0-55402D96E0C0
// Assembly location: C:\Program Files (x86)\ACT国服整合\Plugins\OverlayPlugin\OverlayPlugin.Core.dll

using RainbowMage.HtmlRenderer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;
using Xilium.CefGlue;

namespace RainbowMage.OverlayPlugin
{
  public class OverlayForm : Form
  {
    private static object xivProcLocker = new object();
    private static TimeSpan tryInterval = new TimeSpan(0, 0, 15);
    private object surfaceBufferLocker = new object();
    private DIBitmap surfaceBuffer;
    private int maxFrameRate;
    private System.Threading.Timer zorderCorrector;
    private bool terminated;
    private bool shiftKeyPressed;
    private bool altKeyPressed;
    private bool controlKeyPressed;
    private string url;
    private bool isClickThru;
    private bool isDragging;
    private System.Drawing.Point offset;
    private static Process xivProc;
    private static DateTime lastTry;
    private IContainer components;

    public Renderer Renderer { get; private set; }

    public string Url
    {
      get
      {
        return this.url;
      }
      set
      {
        this.url = value;
        this.UpdateRender();
      }
    }

    public bool IsClickThru
    {
      get
      {
        return this.isClickThru;
      }
      set
      {
        if (this.isClickThru == value)
          return;
        this.isClickThru = value;
        this.UpdateMouseClickThru();
      }
    }

    public bool IsLoaded { get; private set; }

    public bool Locked { get; set; }

    public OverlayForm(string overlayVersion, string overlayName, string url, int maxFrameRate = 30)
    {
      this.InitializeComponent();
      this.Renderer = new Renderer(overlayVersion, overlayName);
      Renderer.Initialize();
      this.maxFrameRate = maxFrameRate;
      
      this.Renderer.Render += new EventHandler<RenderEventArgs>(this.renderer_Render);
      this.MouseWheel += new MouseEventHandler(this.OverlayForm_MouseWheel);
      this.url = url;
      Util.HidePreview((Form) this);
    }

    public void Reload()
    {
      this.Renderer.Reload();
    }

    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams createParams = base.CreateParams;
        createParams.ExStyle = createParams.ExStyle | 8 | 524288 | 134217728;
        createParams.ClassStyle |= 512;
        return createParams;
      }
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (m.Msg == 132 && !this.Locked)
      {
        System.Drawing.Point client = this.PointToClient(new System.Drawing.Point(m.LParam.ToInt32() & (int) ushort.MaxValue, m.LParam.ToInt32() >> 16));
        int x = client.X;
        System.Drawing.Size clientSize = this.ClientSize;
        int num1 = clientSize.Width - 16;
        if (x >= num1)
        {
          int y = client.Y;
          clientSize = this.ClientSize;
          int num2 = clientSize.Height - 16;
          if (y >= num2)
          {
            m.Result = (IntPtr) 17;
            return;
          }
        }
      }
      if (m.Msg != 256 && m.Msg != 257 && (m.Msg != 258 && m.Msg != 260) && (m.Msg != 261 && m.Msg != 262))
        return;
      this.OnKeyEvent(ref m);
    }

    private void UpdateLayeredWindowBitmap()
    {
      if (this.surfaceBuffer.IsDisposed || this.terminated)
        return;
      using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
      {
        IntPtr hdc = graphics.GetHdc();
        IntPtr hgdiobj = NativeMethods.SelectObject(this.surfaceBuffer.DeviceContext, this.surfaceBuffer.Handle);
        NativeMethods.BlendFunction pBlend = new NativeMethods.BlendFunction()
        {
          BlendOp = 0,
          BlendFlags = 0,
          SourceConstantAlpha = byte.MaxValue,
          AlphaFormat = 1
        };
        NativeMethods.Point point = new NativeMethods.Point();
        point.X = this.Left;
        point.Y = this.Top;
        NativeMethods.Point pptDst = point;
        NativeMethods.Size pSize = new NativeMethods.Size()
        {
          Width = this.surfaceBuffer.Width,
          Height = this.surfaceBuffer.Height
        };
        point = new NativeMethods.Point();
        point.X = 0;
        point.Y = 0;
        NativeMethods.Point pptSrc = point;
        IntPtr handle = IntPtr.Zero;
        try
        {
          if (!this.terminated)
          {
            if (this.InvokeRequired)
              this.Invoke((Action) (() => handle = this.Handle));
            else
              handle = this.Handle;
            NativeMethods.UpdateLayeredWindow(handle, hdc, ref pptDst, ref pSize, this.surfaceBuffer.DeviceContext, ref pptSrc, 0, ref pBlend, 2U);
          }
        }
        catch (ObjectDisposedException ex)
        {
          return;
        }
        NativeMethods.SelectObject(this.surfaceBuffer.DeviceContext, hgdiobj);
        graphics.ReleaseHdc(hdc);
      }
    }

    private void UpdateMouseClickThru()
    {
      if (!this.IsLoaded)
        return;
      if (this.isClickThru)
        this.EnableMouseClickThru();
      else
        this.DisableMouseClickThru();
    }

    private void EnableMouseClickThru()
    {
      NativeMethods.SetWindowLong(this.Handle, -20, NativeMethods.GetWindowLong(this.Handle, -20) | 32);
    }

    private void DisableMouseClickThru()
    {
      NativeMethods.SetWindowLong(this.Handle, -20, (int)NativeMethods.GetWindowLong(this.Handle, -20) & -33);
    }

    private void renderer_Render(object sender, RenderEventArgs e)
    {
      if (this.terminated)
        return;
      try
      {
        if (this.surfaceBuffer != null && (this.surfaceBuffer.Width != e.Width || this.surfaceBuffer.Height != e.Height))
        {
          this.surfaceBuffer.Dispose();
          this.surfaceBuffer = (DIBitmap) null;
        }
        if (this.surfaceBuffer == null)
          this.surfaceBuffer = new DIBitmap(e.Width, e.Height);
        this.surfaceBuffer.SetSurfaceData(e.Buffer, (uint) (e.Width * e.Height * 4));
        this.UpdateLayeredWindowBitmap();
      }
      catch
      {
      }
    }

    private void UpdateRender()
    {
      if (this.Renderer == null)
        return;
      this.Renderer.BeginRender(this.Width, this.Height, this.Url, this.maxFrameRate);
    }

    private void OverlayForm_Load(object sender, EventArgs e)
    {
      this.IsLoaded = true;
      this.UpdateMouseClickThru();
      this.zorderCorrector = new System.Threading.Timer((TimerCallback) (state =>
      {
        if (!this.Visible || this.IsOverlaysGameWindow())
          return;
        this.EnsureTopMost();
      }), (object) null, 0, 1000);
    }

    private void OverlayForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      this.Renderer.EndRender();
      this.terminated = true;
    }

    protected override void Dispose(bool disposing)
    {
      if (this.zorderCorrector != null)
        this.zorderCorrector.Dispose();
      if (this.Renderer != null)
      {
        this.Renderer.Dispose();
        this.Renderer = (Renderer) null;
      }
      if (this.surfaceBuffer != null)
        this.surfaceBuffer.Dispose();
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void OverlayForm_Resize(object sender, EventArgs e)
    {
      if (this.Renderer == null)
        return;
      this.Renderer.Resize(this.Width, this.Height);
    }

    private void OverlayForm_MouseDown(object sender, MouseEventArgs e)
    {
      if (!this.Locked)
      {
        this.isDragging = true;
        this.offset = e.Location;
      }
      this.Renderer.SendMouseUpDown(e.X, e.Y, this.GetMouseButtonType(e), false);
    }

    private void OverlayForm_MouseMove(object sender, MouseEventArgs e)
    {
      if (this.isDragging)
      {
        System.Drawing.Point screen = this.PointToScreen(e.Location);
        this.Location = new System.Drawing.Point(screen.X - this.offset.X, screen.Y - this.offset.Y);
      }
      else
        this.Renderer.SendMouseMove(e.X, e.Y, this.GetMouseButtonType(e));
    }

    private void OverlayForm_MouseUp(object sender, MouseEventArgs e)
    {
      this.isDragging = false;
      this.Renderer.SendMouseUpDown(e.X, e.Y, this.GetMouseButtonType(e), true);
    }

    private void OverlayForm_MouseWheel(object sender, MouseEventArgs e)
    {
      int mouseEventFlags = (int) this.GetMouseEventFlags(e);
      this.Renderer.SendMouseWheel(e.X, e.Y, e.Delta, this.shiftKeyPressed);
    }

    private CefMouseButtonType GetMouseButtonType(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
        return CefMouseButtonType.Left;
      if (e.Button == MouseButtons.Middle)
        return CefMouseButtonType.Middle;
      return e.Button == MouseButtons.Right ? CefMouseButtonType.Right : CefMouseButtonType.Left;
    }

    private CefEventFlags GetMouseEventFlags(MouseEventArgs e)
    {
      CefEventFlags cefEventFlags = CefEventFlags.None;
      if (e.Button == MouseButtons.Left)
        cefEventFlags |= CefEventFlags.LeftMouseButton;
      else if (e.Button == MouseButtons.Middle)
        cefEventFlags |= CefEventFlags.MiddleMouseButton;
      else if (e.Button == MouseButtons.Right)
        cefEventFlags |= CefEventFlags.RightMouseButton;
      if (this.shiftKeyPressed)
        cefEventFlags |= CefEventFlags.ShiftDown;
      if (this.altKeyPressed)
        cefEventFlags |= CefEventFlags.AltDown;
      if (this.controlKeyPressed)
        cefEventFlags |= CefEventFlags.ControlDown;
      return cefEventFlags;
    }

    private bool IsOverlaysGameWindow()
    {
      IntPtr gameWindowHandle = OverlayForm.GetGameWindowHandle();
      for (IntPtr hWnd = this.Handle; hWnd != IntPtr.Zero; hWnd = NativeMethods.GetWindow(hWnd, 3U))
      {
        if (hWnd == gameWindowHandle)
          return false;
      }
      return true;
    }

    private void EnsureTopMost()
    {
      NativeMethods.SetWindowPos(this.Handle, NativeMethods.HWND_TOPMOST, 0, 0, 0, 0, 19U);
    }

    private static IntPtr GetGameWindowHandle()
    {
      lock (OverlayForm.xivProcLocker)
      {
        try
        {
          if (OverlayForm.xivProc != null && OverlayForm.xivProc.HasExited)
            OverlayForm.xivProc = (Process) null;
          if (OverlayForm.xivProc == null && DateTime.Now - OverlayForm.lastTry > OverlayForm.tryInterval)
          {
            OverlayForm.xivProc = ((IEnumerable<Process>) Process.GetProcessesByName("ffxiv")).FirstOrDefault<Process>();
            if (OverlayForm.xivProc == null)
              OverlayForm.xivProc = ((IEnumerable<Process>) Process.GetProcessesByName("ffxiv_dx11")).FirstOrDefault<Process>();
            OverlayForm.lastTry = DateTime.Now;
          }
          if (OverlayForm.xivProc != null)
            return OverlayForm.xivProc.MainWindowHandle;
        }
        catch (Win32Exception ex)
        {
        }
        return IntPtr.Zero;
      }
    }

    private void OverlayForm_KeyDown(object sender, KeyEventArgs e)
    {
      this.altKeyPressed = e.Alt;
      this.shiftKeyPressed = e.Shift;
      this.controlKeyPressed = e.Control;
    }

    private void OverlayForm_KeyUp(object sender, KeyEventArgs e)
    {
      this.altKeyPressed = e.Alt;
      this.shiftKeyPressed = e.Shift;
      this.controlKeyPressed = e.Control;
    }

    private void OnKeyEvent(ref Message m)
    {
      CefKeyEvent keyEvent = new CefKeyEvent();
      keyEvent.WindowsKeyCode = m.WParam.ToInt32();
      keyEvent.NativeKeyCode = (int) m.LParam.ToInt64();
      keyEvent.IsSystemKey = m.Msg == 262 || m.Msg == 260 || m.Msg == 261;
      keyEvent.EventType = m.Msg == 256 || m.Msg == 260 ? CefKeyEventType.RawKeyDown : (m.Msg == 257 || m.Msg == 261 ? CefKeyEventType.KeyUp : CefKeyEventType.Char);
      keyEvent.Modifiers = this.GetKeyboardModifiers(ref m);
      if (this.Renderer == null)
        return;
      this.Renderer.SendKeyEvent(keyEvent);
    }

    private CefEventFlags GetKeyboardModifiers(ref Message m)
    {
      CefEventFlags cefEventFlags = CefEventFlags.None;
      if (this.IsKeyDown(Keys.Shift))
        cefEventFlags |= CefEventFlags.ShiftDown;
      if (this.IsKeyDown(Keys.Control))
        cefEventFlags |= CefEventFlags.ControlDown;
      if (this.IsKeyDown(Keys.Menu))
        cefEventFlags |= CefEventFlags.AltDown;
      if (this.IsKeyToggled(Keys.NumLock))
        cefEventFlags |= CefEventFlags.NumLockOn;
      if (this.IsKeyToggled(Keys.Capital))
        cefEventFlags |= CefEventFlags.CapsLockOn;
      switch ((Keys) (int) m.WParam)
      {
        case Keys.Clear:
        case Keys.NumPad0:
        case Keys.NumPad1:
        case Keys.NumPad2:
        case Keys.NumPad3:
        case Keys.NumPad4:
        case Keys.NumPad5:
        case Keys.NumPad6:
        case Keys.NumPad7:
        case Keys.NumPad8:
        case Keys.NumPad9:
        case Keys.Multiply:
        case Keys.Add:
        case Keys.Subtract:
        case Keys.Decimal:
        case Keys.Divide:
        case Keys.NumLock:
          cefEventFlags |= CefEventFlags.IsKeyPad;
          break;
        case Keys.Return:
          if ((m.LParam.ToInt64() >> 48 & 256L) != 0L)
          {
            cefEventFlags |= CefEventFlags.IsKeyPad;
            break;
          }
          break;
        case Keys.Prior:
        case Keys.Next:
        case Keys.End:
        case Keys.Home:
        case Keys.Left:
        case Keys.Up:
        case Keys.Right:
        case Keys.Down:
        case Keys.Insert:
        case Keys.Delete:
          if ((m.LParam.ToInt64() >> 48 & 256L) == 0L)
          {
            cefEventFlags |= CefEventFlags.IsKeyPad;
            break;
          }
          break;
        case Keys.LWin:
          cefEventFlags |= CefEventFlags.IsLeft;
          break;
        case Keys.RWin:
          cefEventFlags |= CefEventFlags.IsRight;
          break;
        case Keys.Shift:
          if (this.IsKeyDown(Keys.LShiftKey))
          {
            cefEventFlags |= CefEventFlags.IsLeft;
            break;
          }
          cefEventFlags |= CefEventFlags.IsRight;
          break;
        case Keys.Control:
          if (this.IsKeyDown(Keys.LControlKey))
          {
            cefEventFlags |= CefEventFlags.IsLeft;
            break;
          }
          cefEventFlags |= CefEventFlags.IsRight;
          break;
        case Keys.Alt:
          if (this.IsKeyDown(Keys.LMenu))
          {
            cefEventFlags |= CefEventFlags.IsLeft;
            break;
          }
          cefEventFlags |= CefEventFlags.IsRight;
          break;
      }
      return cefEventFlags;
    }

    private bool IsKeyDown(Keys key)
    {
      return ((uint) NativeMethods.GetKeyState((int) key) & 32768U) > 0U;
    }

    private bool IsKeyToggled(Keys key)
    {
      return ((int) NativeMethods.GetKeyState((int) key) & 1) == 1;
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(394, 242);
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (OverlayForm);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (OverlayForm);
      this.FormClosed += new FormClosedEventHandler(this.OverlayForm_FormClosed);
      this.Load += new EventHandler(this.OverlayForm_Load);
      this.KeyDown += new KeyEventHandler(this.OverlayForm_KeyDown);
      this.KeyUp += new KeyEventHandler(this.OverlayForm_KeyUp);
      this.MouseDown += new MouseEventHandler(this.OverlayForm_MouseDown);
      this.MouseMove += new MouseEventHandler(this.OverlayForm_MouseMove);
      this.MouseUp += new MouseEventHandler(this.OverlayForm_MouseUp);
      this.Resize += new EventHandler(this.OverlayForm_Resize);
      this.ResumeLayout(false);
    }
  }
}
