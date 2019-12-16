using System;

namespace RainbowMage.OverlayPlugin
{
  public class GlobalHotkeyEnabledChangedEventArgs : EventArgs
  {
    public bool NewGlobalHotkeyEnabled { get; private set; }

    public GlobalHotkeyEnabledChangedEventArgs(bool globalHotkeyEnabled)
    {
      this.NewGlobalHotkeyEnabled = globalHotkeyEnabled;
    }
  }
}
