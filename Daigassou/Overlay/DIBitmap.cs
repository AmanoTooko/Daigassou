// Decompiled with JetBrains decompiler
// Type: RainbowMage.OverlayPlugin.DIBitmap
// Assembly: OverlayPlugin.Core, Version=0.3.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 8E4CC27A-B72E-4CED-BAB0-55402D96E0C0
// Assembly location: C:\Program Files (x86)\ACT国服整合\Plugins\OverlayPlugin\OverlayPlugin.Core.dll

using System;
using System.Runtime.InteropServices;

namespace RainbowMage.OverlayPlugin
{
  internal class DIBitmap : IDisposable
  {
    public int Width { get; private set; }

    public int Height { get; private set; }

    public IntPtr Bits { get; private set; }

    public IntPtr Handle { get; private set; }

    public IntPtr DeviceContext { get; private set; }

    public bool IsDisposed { get; private set; }

    public DIBitmap(int width, int height)
    {
      this.IsDisposed = false;
      this.Width = width;
      this.Height = height;
      this.DeviceContext = NativeMethods.CreateCompatibleDC(NativeMethods.CreateCompatibleDC(IntPtr.Zero));
      NativeMethods.BitmapInfo pbmi = new NativeMethods.BitmapInfo();
      pbmi.bmiHeader.biSize = (uint) Marshal.SizeOf((object) pbmi);
      pbmi.bmiHeader.biBitCount = (ushort) 32;
      pbmi.bmiHeader.biPlanes = (ushort) 1;
      pbmi.bmiHeader.biWidth = width;
      pbmi.bmiHeader.biHeight = -height;
      IntPtr ppvBits;
      this.Handle = NativeMethods.CreateDIBSection(this.DeviceContext, ref pbmi, 0U, out ppvBits, IntPtr.Zero, 0U);
      this.Bits = ppvBits;
    }

    public void SetSurfaceData(IntPtr srcSurfaceData, uint count)
    {
      NativeMethods.CopyMemory(this.Bits, srcSurfaceData, count);
    }

    public void Dispose()
    {
      if (this.Handle != IntPtr.Zero)
        NativeMethods.DeleteObject(this.Handle);
      if (this.DeviceContext != IntPtr.Zero)
        NativeMethods.DeleteDC(this.DeviceContext);
      this.IsDisposed = true;
    }
  }
}
