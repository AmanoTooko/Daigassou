using System;

namespace RainbowMage.OverlayPlugin
{
  public class MaxFrameRateChangedEventArgs : EventArgs
  {
    public int NewFrameRate { get; private set; }

    public MaxFrameRateChangedEventArgs(int frameRate)
    {
      this.NewFrameRate = frameRate;
    }
  }
}
