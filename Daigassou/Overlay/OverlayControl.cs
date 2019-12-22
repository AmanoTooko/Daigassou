using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Xml.Serialization;
using RainbowMage.OverlayPlugin;
using RainbowMage.HtmlRenderer;

namespace RainbowMage.OverlayPlugin
{
    [Serializable]
    public class LabelOverlayConfig : OverlayConfigBase
    {
        private string text;
        private string process;

        [XmlElement("Text")]
        public string Text
        {
            get { return this.text; }
            set
            {
                if (!(this.text != value))
                    return;
                this.text = value;
                if (this.TextChanged == null)
                    return;
                this.TextChanged((object) this, new TextChangedEventArgs(this.text));
            }
        }

        public string Process
        {
            get { return this.process; }
            set
            {
                if (!(this.process != value))
                    return;
                this.process = value;
                if (this.processChanged == null)
                    return;
                this.processChanged((object)this, new TextChangedEventArgs(this.process));
            }
        }


        public event EventHandler<TextChangedEventArgs> TextChanged;
        public event EventHandler<TextChangedEventArgs> processChanged;

        public LabelOverlayConfig(string name)
            : base(name)
        {
            this.Text = "";

        }

        private LabelOverlayConfig()
            : base((string) null)
        {
        }

        public override Type OverlayType
        {
            get { return typeof(StatusOverlay); }
        }
    }

    public class StatusOverlay : OverlayBase<LabelOverlayConfig>
    {
        public StatusOverlay(LabelOverlayConfig config)
            : base(config, config.Name)
        {
            this.timer.Stop();
            config.TextChanged += (EventHandler<TextChangedEventArgs>) ((o, e) => this.UpdateOverlayText());
            config.processChanged += (EventHandler<TextChangedEventArgs>)((o, e) => this.UpdateOverlayProcess());
        }

        private void UpdateOverlayProcess()
        {
            try
            {
                string dispatcherScript = this.CreateEventDispatcherScript(CreateJsonProcess());
                if (this.Overlay != null && this.Overlay.Renderer != null && this.Overlay.Renderer.Browser != null)
                    this.Overlay.Renderer.ExecuteScript(dispatcherScript);
                else
                    this.Log(RainbowMage.OverlayPlugin.LogLevel.Error, "更新: 浏览器未准备好");
            }
            catch (Exception ex)
            {
                
            }
        }
        private void UpdateOverlayText()
        {
            try
            {
                string dispatcherScript = this.CreateEventDispatcherScript(CreateJsonLog());
                if (this.Overlay != null && this.Overlay.Renderer != null && this.Overlay.Renderer.Browser != null)
                    this.Overlay.Renderer.ExecuteScript(dispatcherScript);
                else
                    this.Log(RainbowMage.OverlayPlugin.LogLevel.Error, "更新: 浏览器未准备好");
            }
            catch (Exception ex)
            {
                this.Log(RainbowMage.OverlayPlugin.LogLevel.Error, "更新: {1}", (object) this.Name, (object) ex);
            }
        }

        private string CreateEventDispatcherScript(string json)
        {
            return string.Format("document.dispatchEvent(new CustomEvent('onOverlayDataUpdate', {{ detail: {0} }}));",
                json);
        }

        internal string CreateJsonLog()
        {
            return string.Format("{{ log: \"{0}\"}}",
                (object) Util.CreateJsonSafeString(this.Config.Text));
        }
        internal string CreateJsonProcess()
        {
            return string.Format("{{process: \"{0}\"}}",
                (object)Util.CreateJsonSafeString(this.Config.Process)
                );
        }
        protected override void Update()
        {
        }

        public class OverlayControl
        {
            public StatusOverlay f;
            public LabelOverlayConfig config =new LabelOverlayConfig("喵");
            public OverlayControl()
            {
                
            }
            public void InitializeOverlays(Point p)
            {
             
                    
                    config.IsClickThru = false;
                    config.Url = "http://overlay.ffxiv.cat:8088/index.html";
                    config.MaxFrameRate = 60;
                    config.IsVisible = true;
                    config.Size=new Size(250,150);
                    config.Position = p;
                    f=new StatusOverlay((LabelOverlayConfig) config);
                    f.Start();
                
                
            }

            internal void RegisterOverlay(IOverlay overlay)
            {
                
                //overlay.Start();
                
            }

            internal void RemoveOverlay(IOverlay overlay)
            {
               // overlay.Dispose();
                
            }

        }
    }
}
