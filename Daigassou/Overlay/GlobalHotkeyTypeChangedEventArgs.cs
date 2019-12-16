
using System;

namespace RainbowMage.OverlayPlugin
{
  public class GlobalHotkeyTypeChangedEventArgs : EventArgs
  {
    public GlobalHotkeyType NewHotkeyType { get; private set; }

    public GlobalHotkeyTypeChangedEventArgs(GlobalHotkeyType newHotkeyType)
    {
      this.NewHotkeyType = newHotkeyType;
    }
  }
}
