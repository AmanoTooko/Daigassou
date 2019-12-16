
using System;

namespace RainbowMage.OverlayPlugin
{
  public class SortKeyChangedEventArgs : EventArgs
  {
    public string NewSortKey { get; private set; }

    public SortKeyChangedEventArgs(string newSortKey)
    {
      this.NewSortKey = newSortKey;
    }
  }
}
