namespace Daigassou.Forms
{
    partial class HotKeyBindingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.hotKeyControl4 = new Daigassou.HotKeyControl();
            this.hotKeyControl3 = new Daigassou.HotKeyControl();
            this.hotKeyControl2 = new Daigassou.HotKeyControl();
            this.hotKeyControl1 = new Daigassou.HotKeyControl();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.btnConfirm = new Sunny.UI.UISymbolButton();
            this.btnReset = new Sunny.UI.UISymbolButton();
            this.uiPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiPanel1
            // 
            this.uiPanel1.BackColor = System.Drawing.Color.Transparent;
            this.uiPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uiPanel1.Controls.Add(this.hotKeyControl4);
            this.uiPanel1.Controls.Add(this.hotKeyControl3);
            this.uiPanel1.Controls.Add(this.hotKeyControl2);
            this.uiPanel1.Controls.Add(this.hotKeyControl1);
            this.uiPanel1.Controls.Add(this.uiLabel5);
            this.uiPanel1.Controls.Add(this.uiLabel4);
            this.uiPanel1.Controls.Add(this.uiLabel3);
            this.uiPanel1.Controls.Add(this.uiLabel2);
            this.uiPanel1.Controls.Add(this.uiLabel1);
            this.uiPanel1.Controls.Add(this.btnConfirm);
            this.uiPanel1.Controls.Add(this.btnReset);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.uiPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel1.Location = new System.Drawing.Point(0, 35);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.uiPanel1.Size = new System.Drawing.Size(369, 299);
            this.uiPanel1.Style = Sunny.UI.UIStyle.Colorful;
            this.uiPanel1.TabIndex = 0;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // hotKeyControl4
            // 
            this.hotKeyControl4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hotKeyControl4.Location = new System.Drawing.Point(115, 165);
            this.hotKeyControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hotKeyControl4.Name = "hotKeyControl4";
            this.hotKeyControl4.Size = new System.Drawing.Size(207, 23);
            this.hotKeyControl4.TabIndex = 47;
            this.hotKeyControl4.ToolTip = null;
            this.hotKeyControl4.HotKeyIsSet += new BondTech.HotkeyManagement.Win.HotKeyIsSetEventHandler(this.hotKeyControl4_HotKeyIsSet);
            this.hotKeyControl4.HotKeyIsReset += new System.EventHandler(this.hotKeyControl4_HotKeyIsReset);
            // 
            // hotKeyControl3
            // 
            this.hotKeyControl3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hotKeyControl3.Location = new System.Drawing.Point(115, 120);
            this.hotKeyControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hotKeyControl3.Name = "hotKeyControl3";
            this.hotKeyControl3.Size = new System.Drawing.Size(207, 23);
            this.hotKeyControl3.TabIndex = 46;
            this.hotKeyControl3.ToolTip = null;
            this.hotKeyControl3.HotKeyIsSet += new BondTech.HotkeyManagement.Win.HotKeyIsSetEventHandler(this.hotKeyControl4_HotKeyIsSet);
            this.hotKeyControl3.HotKeyIsReset += new System.EventHandler(this.hotKeyControl4_HotKeyIsReset);
            // 
            // hotKeyControl2
            // 
            this.hotKeyControl2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hotKeyControl2.Location = new System.Drawing.Point(115, 76);
            this.hotKeyControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hotKeyControl2.Name = "hotKeyControl2";
            this.hotKeyControl2.Size = new System.Drawing.Size(207, 23);
            this.hotKeyControl2.TabIndex = 45;
            this.hotKeyControl2.ToolTip = null;
            this.hotKeyControl2.HotKeyIsSet += new BondTech.HotkeyManagement.Win.HotKeyIsSetEventHandler(this.hotKeyControl4_HotKeyIsSet);
            this.hotKeyControl2.HotKeyIsReset += new System.EventHandler(this.hotKeyControl4_HotKeyIsReset);
            // 
            // hotKeyControl1
            // 
            this.hotKeyControl1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hotKeyControl1.Location = new System.Drawing.Point(115, 31);
            this.hotKeyControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hotKeyControl1.Name = "hotKeyControl1";
            this.hotKeyControl1.Size = new System.Drawing.Size(207, 23);
            this.hotKeyControl1.TabIndex = 44;
            this.hotKeyControl1.ToolTip = null;
            this.hotKeyControl1.HotKeyIsSet += new BondTech.HotkeyManagement.Win.HotKeyIsSetEventHandler(this.hotKeyControl4_HotKeyIsSet);
            this.hotKeyControl1.HotKeyIsReset += new System.EventHandler(this.hotKeyControl4_HotKeyIsReset);
            // 
            // uiLabel5
            // 
            this.uiLabel5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel5.Location = new System.Drawing.Point(24, 165);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(100, 23);
            this.uiLabel5.Style = Sunny.UI.UIStyle.Colorful;
            this.uiLabel5.TabIndex = 43;
            this.uiLabel5.Text = "向下移调";
            this.uiLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel5.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel4
            // 
            this.uiLabel4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel4.Location = new System.Drawing.Point(24, 120);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(100, 23);
            this.uiLabel4.Style = Sunny.UI.UIStyle.Colorful;
            this.uiLabel4.TabIndex = 43;
            this.uiLabel4.Text = "向上移调";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel3
            // 
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel3.Location = new System.Drawing.Point(24, 76);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(100, 23);
            this.uiLabel3.Style = Sunny.UI.UIStyle.Colorful;
            this.uiLabel3.TabIndex = 43;
            this.uiLabel3.Text = "结束演奏";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.Location = new System.Drawing.Point(24, 31);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(100, 23);
            this.uiLabel2.Style = Sunny.UI.UIStyle.Colorful;
            this.uiLabel2.TabIndex = 43;
            this.uiLabel2.Text = "开始演奏";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(24, 31);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(100, 23);
            this.uiLabel1.Style = Sunny.UI.UIStyle.Colorful;
            this.uiLabel1.TabIndex = 43;
            this.uiLabel1.Text = "开始演奏";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnConfirm.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnConfirm.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnConfirm.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnConfirm.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnConfirm.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.Location = new System.Drawing.Point(210, 238);
            this.btnConfirm.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Radius = 15;
            this.btnConfirm.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnConfirm.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnConfirm.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnConfirm.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnConfirm.Size = new System.Drawing.Size(112, 28);
            this.btnConfirm.Style = Sunny.UI.UIStyle.Colorful;
            this.btnConfirm.TabIndex = 41;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnReset
            // 
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnReset.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnReset.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnReset.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnReset.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnReset.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReset.Location = new System.Drawing.Point(29, 238);
            this.btnReset.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnReset.Name = "btnReset";
            this.btnReset.Radius = 15;
            this.btnReset.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnReset.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnReset.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnReset.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnReset.Size = new System.Drawing.Size(112, 28);
            this.btnReset.Style = Sunny.UI.UIStyle.Colorful;
            this.btnReset.Symbol = 61473;
            this.btnReset.TabIndex = 40;
            this.btnReset.Text = "重置";
            this.btnReset.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReset.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // HotKeyBindingForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(369, 334);
            this.ControlBoxFillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.Controls.Add(this.uiPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HotKeyBindingForm";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ShowRadius = false;
            this.ShowRect = false;
            this.ShowShadow = true;
            this.Style = Sunny.UI.UIStyle.Colorful;
            this.Text = "按键绑定";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.TopMost = true;
            this.ZoomScaleRect = new System.Drawing.Rectangle(19, 19, 800, 450);
            this.ReceiveParams += new Sunny.UI.OnReceiveParams(this.HotKeyBindingForm_ReceiveParams);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KeyBindingForm_FormClosing);
            this.uiPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UISymbolButton btnConfirm;
        private Sunny.UI.UISymbolButton btnReset;
        private Sunny.UI.UILabel uiLabel5;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILabel uiLabel1;
        private HotKeyControl hotKeyControl4;
        private HotKeyControl hotKeyControl3;
        private HotKeyControl hotKeyControl2;
        private HotKeyControl hotKeyControl1;
    }
}