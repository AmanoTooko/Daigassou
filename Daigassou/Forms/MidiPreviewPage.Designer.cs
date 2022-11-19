namespace Daigassou.Forms
{
    partial class MidiPreviewPage
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
            this.uiLinkLabel1 = new Sunny.UI.UILinkLabel();
            this.uiLine2 = new Sunny.UI.UILine();
            this.lblScoreName = new Sunny.UI.UILabel();
            this.lblTrackName = new Sunny.UI.UILabel();
            this.uiSymbolLabel2 = new Sunny.UI.UISymbolLabel();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.uiSymbolLabel1 = new Sunny.UI.UISymbolLabel();
            this.swAll = new Sunny.UI.UISwitch();
            this.btnBackward = new Sunny.UI.UISymbolButton();
            this.btnStart = new Sunny.UI.UISymbolButton();
            this.btnStop = new Sunny.UI.UISymbolButton();
            this.tbMidiProcess = new Sunny.UI.UITrackBar();
            this.btnForward = new Sunny.UI.UISymbolButton();
            this.lblProcess = new Sunny.UI.UILabel();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.uiLine1 = new Sunny.UI.UILine();
            this.cbMidiDevice = new Sunny.UI.UIComboBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiPanel1.SuspendLayout();
            this.uiPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiLinkLabel1
            // 
            this.uiLinkLabel1.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(155)))), ((int)(((byte)(40)))));
            this.uiLinkLabel1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.uiLinkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.uiLinkLabel1.Location = new System.Drawing.Point(358, 48);
            this.uiLinkLabel1.Name = "uiLinkLabel1";
            this.uiLinkLabel1.Size = new System.Drawing.Size(140, 23);
            this.uiLinkLabel1.Style = Sunny.UI.UIStyle.Colorful;
            this.uiLinkLabel1.TabIndex = 35;
            this.uiLinkLabel1.TabStop = true;
            this.uiLinkLabel1.Text = "在线音色试听";
            this.uiLinkLabel1.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.uiLinkLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiLinkLabel1.Click += new System.EventHandler(this.uiLinkLabel1_Click);
            // 
            // uiLine2
            // 
            this.uiLine2.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiLine2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.uiLine2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLine2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.uiLine2.Location = new System.Drawing.Point(0, 70);
            this.uiLine2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLine2.Name = "uiLine2";
            this.uiLine2.Size = new System.Drawing.Size(510, 33);
            this.uiLine2.Style = Sunny.UI.UIStyle.Colorful;
            this.uiLine2.TabIndex = 41;
            this.uiLine2.Text = "Midi播放控制";
            this.uiLine2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLine2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // lblScoreName
            // 
            this.lblScoreName.BackColor = System.Drawing.Color.Transparent;
            this.lblScoreName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblScoreName.Location = new System.Drawing.Point(12, 3);
            this.lblScoreName.Name = "lblScoreName";
            this.lblScoreName.Size = new System.Drawing.Size(349, 17);
            this.lblScoreName.Style = Sunny.UI.UIStyle.Colorful;
            this.lblScoreName.TabIndex = 24;
            this.lblScoreName.Text = "乐谱名：妹有乐谱";
            this.lblScoreName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblScoreName.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // lblTrackName
            // 
            this.lblTrackName.BackColor = System.Drawing.Color.Transparent;
            this.lblTrackName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTrackName.Location = new System.Drawing.Point(12, 21);
            this.lblTrackName.Name = "lblTrackName";
            this.lblTrackName.Size = new System.Drawing.Size(349, 26);
            this.lblTrackName.Style = Sunny.UI.UIStyle.Colorful;
            this.lblTrackName.TabIndex = 25;
            this.lblTrackName.Text = "轨道名：[钢琴]Piano ";
            this.lblTrackName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTrackName.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiSymbolLabel2
            // 
            this.uiSymbolLabel2.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolLabel2.Location = new System.Drawing.Point(12, 0);
            this.uiSymbolLabel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolLabel2.Name = "uiSymbolLabel2";
            this.uiSymbolLabel2.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.uiSymbolLabel2.Size = new System.Drawing.Size(105, 35);
            this.uiSymbolLabel2.Style = Sunny.UI.UIStyle.Colorful;
            this.uiSymbolLabel2.Symbol = 363244;
            this.uiSymbolLabel2.SymbolOffset = new System.Drawing.Point(4, 0);
            this.uiSymbolLabel2.SymbolSize = 26;
            this.uiSymbolLabel2.TabIndex = 28;
            this.uiSymbolLabel2.Text = "输出设备";
            this.uiSymbolLabel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.uiLabel1);
            this.uiPanel1.Controls.Add(this.uiSymbolLabel1);
            this.uiPanel1.Controls.Add(this.swAll);
            this.uiPanel1.Controls.Add(this.btnBackward);
            this.uiPanel1.Controls.Add(this.btnStart);
            this.uiPanel1.Controls.Add(this.btnStop);
            this.uiPanel1.Controls.Add(this.tbMidiProcess);
            this.uiPanel1.Controls.Add(this.btnForward);
            this.uiPanel1.Controls.Add(this.lblProcess);
            this.uiPanel1.Controls.Add(this.uiLinkLabel1);
            this.uiPanel1.Controls.Add(this.uiSymbolLabel2);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.uiPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel1.Location = new System.Drawing.Point(0, 103);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.uiPanel1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uiPanel1.Size = new System.Drawing.Size(510, 247);
            this.uiPanel1.Style = Sunny.UI.UIStyle.Colorful;
            this.uiPanel1.TabIndex = 42;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiSymbolLabel1
            // 
            this.uiSymbolLabel1.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolLabel1.Location = new System.Drawing.Point(3, 41);
            this.uiSymbolLabel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolLabel1.Name = "uiSymbolLabel1";
            this.uiSymbolLabel1.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.uiSymbolLabel1.Size = new System.Drawing.Size(158, 35);
            this.uiSymbolLabel1.Style = Sunny.UI.UIStyle.Colorful;
            this.uiSymbolLabel1.Symbol = 363244;
            this.uiSymbolLabel1.SymbolOffset = new System.Drawing.Point(4, 0);
            this.uiSymbolLabel1.SymbolSize = 26;
            this.uiSymbolLabel1.TabIndex = 43;
            this.uiSymbolLabel1.Text = "播放所有轨道";
            this.uiSymbolLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // swAll
            // 
            this.swAll.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.swAll.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.swAll.Location = new System.Drawing.Point(176, 42);
            this.swAll.MinimumSize = new System.Drawing.Size(1, 1);
            this.swAll.Name = "swAll";
            this.swAll.Size = new System.Drawing.Size(75, 29);
            this.swAll.Style = Sunny.UI.UIStyle.Colorful;
            this.swAll.TabIndex = 42;
            this.swAll.Text = "uiSwitch1";
            this.swAll.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.swAll.ValueChanged += new Sunny.UI.UISwitch.OnValueChanged(this.swAll_ValueChanged);
            // 
            // btnBackward
            // 
            this.btnBackward.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackward.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnBackward.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnBackward.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnBackward.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnBackward.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnBackward.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBackward.Location = new System.Drawing.Point(171, 142);
            this.btnBackward.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Radius = 35;
            this.btnBackward.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnBackward.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnBackward.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnBackward.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnBackward.Size = new System.Drawing.Size(35, 35);
            this.btnBackward.Style = Sunny.UI.UIStyle.Colorful;
            this.btnBackward.Symbol = 61514;
            this.btnBackward.SymbolOffset = new System.Drawing.Point(-2, 0);
            this.btnBackward.TabIndex = 40;
            this.btnBackward.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBackward.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnBackward.Click += new System.EventHandler(this.btnBackward_Click);
            // 
            // btnStart
            // 
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnStart.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnStart.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnStart.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnStart.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.Location = new System.Drawing.Point(214, 142);
            this.btnStart.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnStart.Name = "btnStart";
            this.btnStart.Radius = 35;
            this.btnStart.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnStart.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnStart.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnStart.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnStart.Size = new System.Drawing.Size(35, 35);
            this.btnStart.Style = Sunny.UI.UIStyle.Colorful;
            this.btnStart.Symbol = 61515;
            this.btnStart.SymbolOffset = new System.Drawing.Point(2, 1);
            this.btnStart.TabIndex = 39;
            this.btnStart.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnStart.Click += new System.EventHandler(this.uiSymbolButton6_Click);
            // 
            // btnStop
            // 
            this.btnStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStop.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnStop.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnStop.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnStop.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnStop.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnStop.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.Location = new System.Drawing.Point(257, 142);
            this.btnStop.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnStop.Name = "btnStop";
            this.btnStop.Radius = 35;
            this.btnStop.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnStop.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnStop.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnStop.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnStop.Size = new System.Drawing.Size(35, 35);
            this.btnStop.Style = Sunny.UI.UIStyle.Colorful;
            this.btnStop.Symbol = 61517;
            this.btnStop.SymbolOffset = new System.Drawing.Point(1, 1);
            this.btnStop.TabIndex = 38;
            this.btnStop.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnStop.Click += new System.EventHandler(this.uiSymbolButton5_Click);
            // 
            // tbMidiProcess
            // 
            this.tbMidiProcess.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.tbMidiProcess.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMidiProcess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.tbMidiProcess.Location = new System.Drawing.Point(16, 107);
            this.tbMidiProcess.MinimumSize = new System.Drawing.Size(1, 1);
            this.tbMidiProcess.Name = "tbMidiProcess";
            this.tbMidiProcess.ReadOnly = true;
            this.tbMidiProcess.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.tbMidiProcess.Size = new System.Drawing.Size(456, 29);
            this.tbMidiProcess.Style = Sunny.UI.UIStyle.Colorful;
            this.tbMidiProcess.TabIndex = 36;
            this.tbMidiProcess.Text = "uiTrackBar3";
            this.tbMidiProcess.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnForward
            // 
            this.btnForward.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnForward.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnForward.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnForward.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnForward.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnForward.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnForward.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnForward.Location = new System.Drawing.Point(300, 142);
            this.btnForward.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnForward.Name = "btnForward";
            this.btnForward.Radius = 35;
            this.btnForward.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnForward.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnForward.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnForward.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnForward.Size = new System.Drawing.Size(35, 35);
            this.btnForward.Style = Sunny.UI.UIStyle.Colorful;
            this.btnForward.Symbol = 61518;
            this.btnForward.SymbolOffset = new System.Drawing.Point(2, 0);
            this.btnForward.TabIndex = 37;
            this.btnForward.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnForward.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // lblProcess
            // 
            this.lblProcess.BackColor = System.Drawing.Color.Transparent;
            this.lblProcess.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProcess.Location = new System.Drawing.Point(341, 139);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(172, 21);
            this.lblProcess.Style = Sunny.UI.UIStyle.Colorful;
            this.lblProcess.TabIndex = 41;
            this.lblProcess.Text = "停止播放 00:00/00:00";
            this.lblProcess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProcess.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiPanel2
            // 
            this.uiPanel2.Controls.Add(this.lblScoreName);
            this.uiPanel2.Controls.Add(this.lblTrackName);
            this.uiPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiPanel2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.uiPanel2.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.uiPanel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel2.Location = new System.Drawing.Point(0, 29);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.uiPanel2.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uiPanel2.Size = new System.Drawing.Size(510, 41);
            this.uiPanel2.Style = Sunny.UI.UIStyle.Colorful;
            this.uiPanel2.TabIndex = 43;
            this.uiPanel2.Text = null;
            this.uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLine1
            // 
            this.uiLine1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiLine1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.uiLine1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLine1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.uiLine1.Location = new System.Drawing.Point(0, 0);
            this.uiLine1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Size = new System.Drawing.Size(510, 29);
            this.uiLine1.Style = Sunny.UI.UIStyle.Colorful;
            this.uiLine1.TabIndex = 40;
            this.uiLine1.Text = "Midi乐谱信息";
            this.uiLine1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLine1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cbMidiDevice
            // 
            this.cbMidiDevice.DataSource = null;
            this.cbMidiDevice.FillColor = System.Drawing.Color.White;
            this.cbMidiDevice.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.cbMidiDevice.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbMidiDevice.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(242)))), ((int)(((byte)(238)))));
            this.cbMidiDevice.ItemRectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.cbMidiDevice.ItemSelectBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.cbMidiDevice.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.cbMidiDevice.Location = new System.Drawing.Point(124, 103);
            this.cbMidiDevice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbMidiDevice.MinimumSize = new System.Drawing.Size(63, 0);
            this.cbMidiDevice.Name = "cbMidiDevice";
            this.cbMidiDevice.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cbMidiDevice.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.cbMidiDevice.Size = new System.Drawing.Size(265, 34);
            this.cbMidiDevice.Style = Sunny.UI.UIStyle.Colorful;
            this.cbMidiDevice.TabIndex = 42;
            this.cbMidiDevice.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbMidiDevice.Watermark = "Midi输入设备";
            this.cbMidiDevice.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.cbMidiDevice.SelectedIndexChanged += new System.EventHandler(this.cbMidiDevice_SelectedIndexChanged);
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(12, 215);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(426, 23);
            this.uiLabel1.Style = Sunny.UI.UIStyle.Colorful;
            this.uiLabel1.TabIndex = 44;
            this.uiLabel1.Text = "本地音色试听预计下个节气更新";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // MidiPreviewPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(510, 350);
            this.Controls.Add(this.cbMidiDevice);
            this.Controls.Add(this.uiPanel1);
            this.Controls.Add(this.uiLine2);
            this.Controls.Add(this.uiPanel2);
            this.Controls.Add(this.uiLine1);
            this.Name = "MidiPreviewPage";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.Style = Sunny.UI.UIStyle.Colorful;
            this.Text = "MidiPreviewPage";
            this.ReceiveParams += new Sunny.UI.OnReceiveParams(this.MidiPreviewPage_ReceiveParams);
            this.uiPanel1.ResumeLayout(false);
            this.uiPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILinkLabel uiLinkLabel1;
        private Sunny.UI.UILine uiLine2;
        private Sunny.UI.UILabel lblScoreName;
        private Sunny.UI.UILabel lblTrackName;
        private Sunny.UI.UISymbolLabel uiSymbolLabel2;
        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UIPanel uiPanel2;
        private Sunny.UI.UILine uiLine1;
        private Sunny.UI.UISymbolButton btnBackward;
        private Sunny.UI.UISymbolButton btnStart;
        private Sunny.UI.UISymbolButton btnStop;
        private Sunny.UI.UITrackBar tbMidiProcess;
        private Sunny.UI.UISymbolButton btnForward;
        private Sunny.UI.UILabel lblProcess;
        private Sunny.UI.UISwitch swAll;
        private Sunny.UI.UIComboBox cbMidiDevice;
        private Sunny.UI.UISymbolLabel uiSymbolLabel1;
        private Sunny.UI.UILabel uiLabel1;
    }
}