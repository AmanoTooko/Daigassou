using System;
using System.Windows.Forms;

namespace RainbowMage.OverlayPlugin
{
  public class GlobalHotkeyChangedEventArgs : EventArgs
  {
    public Keys NewHotkey { get; private set; }

    public GlobalHotkeyChangedEventArgs(Keys hotkey)
    {
      this.NewHotkey = hotkey;
    }
  }
}
