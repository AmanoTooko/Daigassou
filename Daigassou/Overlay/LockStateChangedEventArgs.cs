using System;

namespace RainbowMage.OverlayPlugin
{
  public class LockStateChangedEventArgs : EventArgs
  {
    public bool IsLocked { get; private set; }

    public LockStateChangedEventArgs(bool isLocked)
    {
      this.IsLocked = isLocked;
    }
  }
}
