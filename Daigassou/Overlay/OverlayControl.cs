using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;
using RainbowMage.OverlayPlugin;
using RainbowMage.HtmlRenderer;

namespace RainbowMage.OverlayPlugin
{
    [Serializable]
    public class LabelOverlayConfig : OverlayConfigBase
    {
        private string text;


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



        public event EventHandler<TextChangedEventArgs> TextChanged;


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
            get { return typeof(标签美化窗口); }
        }
    }

    public class 标签美化窗口 : OverlayBase<LabelOverlayConfig>
    {
        public 标签美化窗口(LabelOverlayConfig config)
            : base(config, config.Name)
        {
            this.timer.Stop();
            config.TextChanged += (EventHandler<TextChangedEventArgs>) ((o, e) => this.UpdateOverlayText());
           
        }

        private void UpdateOverlayText()
        {
            try
            {
                string dispatcherScript = this.CreateEventDispatcherScript();
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

        private string CreateEventDispatcherScript()
        {
            return string.Format("document.dispatchEvent(new CustomEvent('onOverlayDataUpdate', {{ detail: {0} }}));",
                (object) this.CreateJson());
        }

        internal string CreateJson()
        {
            return string.Format("{{ text: \"{0}\", isHTML: {1} }}",
                (object) Util.CreateJsonSafeString(this.Config.Text),
                "true");
        }

        protected override void Update()
        {
        }

        public class OverlayControl
        {
            public 标签美化窗口 f;


            public OverlayControl()
            {
                
            }
            public void InitializeOverlays()
            {
             
                    var config =new LabelOverlayConfig("标签");
                    config.IsClickThru = false;
                    config.Url = "http://www.baidu.com";
                    config.MaxFrameRate = 60;
                    config.IsVisible = true;
                    config.Size=new Size(500,500);
                    f=new 标签美化窗口((LabelOverlayConfig) config);
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
