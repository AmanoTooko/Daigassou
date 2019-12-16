
using System;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace RainbowMage.OverlayPlugin
{
  [Serializable]
  public abstract class OverlayConfigBase : IOverlayConfig
  {
    private bool isVisible;
    private bool isClickThru;
    private string url;
    private int maxFrameRate;
    private bool globalHotkeyEnabled;
    private Keys globalHotkey;
    private Keys globalHotkeyModifiers;
    private GlobalHotkeyType globalHotkeyType;
    private bool isLocked;

    public event EventHandler<VisibleStateChangedEventArgs> VisibleChanged;

    public event EventHandler<ThruStateChangedEventArgs> ClickThruChanged;

    public event EventHandler<UrlChangedEventArgs> UrlChanged;

    public event EventHandler<MaxFrameRateChangedEventArgs> MaxFrameRateChanged;

    public event EventHandler<GlobalHotkeyEnabledChangedEventArgs> GlobalHotkeyEnabledChanged;

    public event EventHandler<GlobalHotkeyChangedEventArgs> GlobalHotkeyChanged;

    public event EventHandler<GlobalHotkeyChangedEventArgs> GlobalHotkeyModifiersChanged;

    public event EventHandler<LockStateChangedEventArgs> LockChanged;

    public event EventHandler<GlobalHotkeyTypeChangedEventArgs> GlobalHotkeyTypeChanged;

    [XmlElement("Name")]
    public string Name { get; set; }

    [XmlElement("IsVisible")]
    public bool IsVisible
    {
      get
      {
        return this.isVisible;
      }
      set
      {
        if (this.isVisible == value)
          return;
        this.isVisible = value;
        if (this.VisibleChanged == null)
          return;
        this.VisibleChanged((object) this, new VisibleStateChangedEventArgs(this.isVisible));
      }
    }

    [XmlElement("IsClickThru")]
    public bool IsClickThru
    {
      get
      {
        return this.isClickThru;
      }
      set
      {
        if (this.isClickThru == value)
          return;
        this.isClickThru = value;
        if (this.ClickThruChanged == null)
          return;
        this.ClickThruChanged((object) this, new ThruStateChangedEventArgs(this.isClickThru));
      }
    }

    [XmlElement("Position")]
    public System.Drawing.Point Position { get; set; }

    [XmlElement("Size")]
    public System.Drawing.Size Size { get; set; }

    [XmlElement("Url")]
    public string Url
    {
      get
      {
        return this.url;
      }
      set
      {
        if (!(this.url != value))
          return;
        this.url = value;
        if (this.UrlChanged == null)
          return;
        this.UrlChanged((object) this, new UrlChangedEventArgs(this.url));
      }
    }

    [XmlElement("MaxFrameRate")]
    public int MaxFrameRate
    {
      get
      {
        return this.maxFrameRate;
      }
      set
      {
        if (this.maxFrameRate == value)
          return;
        this.maxFrameRate = value;
        if (this.MaxFrameRateChanged == null)
          return;
        this.MaxFrameRateChanged((object) this, new MaxFrameRateChangedEventArgs(this.maxFrameRate));
      }
    }

    [XmlElement("GlobalHotkeyEnabled")]
    public bool GlobalHotkeyEnabled
    {
      get
      {
        return this.globalHotkeyEnabled;
      }
      set
      {
        if (this.globalHotkeyEnabled == value)
          return;
        this.globalHotkeyEnabled = value;
        if (this.GlobalHotkeyEnabledChanged == null)
          return;
        this.GlobalHotkeyEnabledChanged((object) this, new GlobalHotkeyEnabledChangedEventArgs(this.globalHotkeyEnabled));
      }
    }

    [XmlElement("GlobalHotkey")]
    public Keys GlobalHotkey
    {
      get
      {
        return this.globalHotkey;
      }
      set
      {
        if (this.globalHotkey == value)
          return;
        this.globalHotkey = value;
        if (this.GlobalHotkeyChanged == null)
          return;
        this.GlobalHotkeyChanged((object) this, new GlobalHotkeyChangedEventArgs(this.globalHotkey));
      }
    }

    [XmlElement("GlobalHotkeyModifiers")]
    public Keys GlobalHotkeyModifiers
    {
      get
      {
        return this.globalHotkeyModifiers;
      }
      set
      {
        if (this.globalHotkeyModifiers == value)
          return;
        this.globalHotkeyModifiers = value;
        if (this.GlobalHotkeyModifiersChanged == null)
          return;
        this.GlobalHotkeyModifiersChanged((object) this, new GlobalHotkeyChangedEventArgs(this.globalHotkeyModifiers));
      }
    }

    [XmlElement("GlobalHotkeyType")]
    public GlobalHotkeyType GlobalHotkeyType
    {
      get
      {
        return this.globalHotkeyType;
      }
      set
      {
        if (this.globalHotkeyType == value)
          return;
        this.globalHotkeyType = value;
        if (this.GlobalHotkeyTypeChanged == null)
          return;
        this.GlobalHotkeyTypeChanged((object) this, new GlobalHotkeyTypeChangedEventArgs(this.globalHotkeyType));
      }
    }

    [XmlElement("IsLocked")]
    public bool IsLocked
    {
      get
      {
        return this.isLocked;
      }
      set
      {
        if (this.isLocked == value)
          return;
        this.isLocked = value;
        if (this.LockChanged == null)
          return;
        this.LockChanged((object) this, new LockStateChangedEventArgs(this.isLocked));
      }
    }

    protected OverlayConfigBase(string name)
    {
      this.Name = name;
      this.IsVisible = true;
      this.IsClickThru = false;
      this.Position = new System.Drawing.Point(20, 20);
      this.Size = new System.Drawing.Size(300, 300);
      this.Url = "";
      this.MaxFrameRate = 30;
      this.globalHotkeyEnabled = false;
      this.GlobalHotkey = Keys.None;
      this.globalHotkeyModifiers = Keys.None;
      this.globalHotkeyType = GlobalHotkeyType.ToggleVisible;
    }

    [XmlIgnore]
    public abstract System.Type OverlayType { get; }
  }
}
