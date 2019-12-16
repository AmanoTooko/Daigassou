

using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace RainbowMage.OverlayPlugin
{
    public class TextChangedEventArgs : EventArgs
    {
        public string Text { get; private set; }

        public TextChangedEventArgs(string text)
        {
            this.Text = text;
        }
    }
    public class KeyPressedEventArgs : EventArgs
  {
    private ModifierKeys _modifier;
    private Keys _key;

    internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
    {
      this._modifier = modifier;
      this._key = key;
    }

    public ModifierKeys Modifier
    {
      get
      {
        return this._modifier;
      }
    }

    public Keys Key
    {
      get
      {
        return this._key;
      }
    }
  }
}
