// Decompiled with JetBrains decompiler
// Type: RainbowMage.OverlayPlugin.Util
// Assembly: OverlayPlugin.Core, Version=0.3.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 8E4CC27A-B72E-4CED-BAB0-55402D96E0C0
// Assembly location: C:\Program Files (x86)\ACT国服整合\Plugins\OverlayPlugin\OverlayPlugin.Core.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RainbowMage.OverlayPlugin
{
  internal static class Util
  {
    public static string CreateJsonSafeString(string str)
    {
      return str.Replace("\"", "\\\"").Replace("'", "\\'").Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t");
    }

    public static string ReplaceNaNString(string str, string replace)
    {
      return str.Replace(double.NaN.ToString(), replace);
    }

    public static bool IsOnScreen(Form form)
    {
      foreach (Screen allScreen in Screen.AllScreens)
      {
        if (allScreen.WorkingArea.IntersectsWith(new Rectangle(form.Left, form.Top, form.Width, form.Height)))
          return true;
      }
      return false;
    }

    public static void HidePreview(Form form)
    {
      uint num = NativeMethods.GetWindowLong(form.Handle, -20) | 128;
      NativeMethods.SetWindowLongA(form.Handle, -20, (IntPtr) num);
    }

    public static string GetHotkeyString(Keys modifier, Keys key, string defaultText = "")
    {
      StringBuilder stringBuilder = new StringBuilder();
      if ((modifier & Keys.Shift) == Keys.Shift)
        stringBuilder.Append("Shift + ");
      if ((modifier & Keys.Control) == Keys.Control)
        stringBuilder.Append("Ctrl + ");
      if ((modifier & Keys.Alt) == Keys.Alt)
        stringBuilder.Append("Alt + ");
      if ((modifier & Keys.LWin) == Keys.LWin || (modifier & Keys.RWin) == Keys.RWin)
        stringBuilder.Append("Win + ");
      stringBuilder.Append(Enum.ToObject(typeof (Keys), (object) key).ToString());
      return stringBuilder.ToString();
    }

    public static Keys RemoveModifiers(Keys keyCode, Keys modifiers)
    {
      Keys keys1 = keyCode;
      foreach (Keys keys2 in new List<Keys>()
      {
        Keys.ControlKey,
        Keys.LControlKey,
        Keys.Alt,
        Keys.ShiftKey,
        Keys.Shift,
        Keys.LShiftKey,
        Keys.RShiftKey,
        Keys.Control,
        Keys.LWin,
        Keys.RWin
      })
      {
        if (keys1.HasFlag((Enum) keys2) && keys1 == keys2)
          keys1 &= ~keys2;
      }
      return keys1;
    }
  }
}
