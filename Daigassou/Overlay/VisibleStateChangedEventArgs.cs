using System;

namespace RainbowMage.OverlayPlugin
{
  public class VisibleStateChangedEventArgs : EventArgs
  {
    public bool IsVisible { get; private set; }

    public VisibleStateChangedEventArgs(bool isVisible)
    {
      this.IsVisible = isVisible;
    }
  }
}
