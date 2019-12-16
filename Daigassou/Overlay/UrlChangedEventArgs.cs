using System;

namespace RainbowMage.OverlayPlugin
{
  public class UrlChangedEventArgs : EventArgs
  {
    public string NewUrl { get; private set; }

    public UrlChangedEventArgs(string url)
    {
      this.NewUrl = url;
    }
  }
}
