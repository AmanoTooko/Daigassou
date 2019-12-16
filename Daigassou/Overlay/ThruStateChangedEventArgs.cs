// Decompiled with JetBrains decompiler
// Type: RainbowMage.OverlayPlugin.ThruStateChangedEventArgs
// Assembly: OverlayPlugin.Core, Version=0.3.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 8E4CC27A-B72E-4CED-BAB0-55402D96E0C0
// Assembly location: C:\Program Files (x86)\ACT国服整合\Plugins\OverlayPlugin\OverlayPlugin.Core.dll

using System;

namespace RainbowMage.OverlayPlugin
{
  public class ThruStateChangedEventArgs : EventArgs
  {
    public bool IsClickThru { get; private set; }

    public ThruStateChangedEventArgs(bool isClickThru)
    {
      this.IsClickThru = isClickThru;
    }
  }
}
