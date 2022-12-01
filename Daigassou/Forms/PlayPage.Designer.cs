namespace Daigassou.Forms
{
    partial class PlayPage
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
            this.uiLine1 = new Sunny.UI.UILine();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.btnOpenFile = new Sunny.UI.UISymbolButton();
            this.uiSymbolLabel1 = new Sunny.UI.UISymbolLabel();
            this.cbTrackname = new Sunny.UI.UIComboBox();
            this.tbFilename = new Sunny.UI.UITextBox();
            this.uiLine2 = new Sunny.UI.UILine();
            this.tbSpeed = new Sunny.UI.UITrackBar();
            this.uiSymbolLabel2 = new Sunny.UI.UISymbolLabel();
            this.uiSymbolLabel3 = new Sunny.UI.UISymbolLabel();
            this.tbKey = new Sunny.UI.UITrackBar();
            this.tbMidiProcess = new Sunny.UI.UITrackBar();
            this.btnConfirmSpeed = new Sunny.UI.UISymbolButton();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.btnBackward = new Sunny.UI.UISymbolButton();
            this.btnStart = new Sunny.UI.UISymbolButton();
            this.btnStop = new Sunny.UI.UISymbolButton();
            this.btnForward = new Sunny.UI.UISymbolButton();
            this.lblProcess = new Sunny.UI.UILabel();
            this.lblKey = new Sunny.UI.UILabel();
            this.lblSpeed = new Sunny.UI.UILabel();
            this.btnConfirmKey = new Sunny.UI.UISymbolButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.uiPanel1.SuspendLayout();
            this.uiPanel2.SuspendLayout();
            this.SuspendLayout();
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
            this.uiLine1.TabIndex = 1;
            this.uiLine1.Text = "Midi乐谱选择";
            this.uiLine1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLine1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.btnOpenFile);
            this.uiPanel1.Controls.Add(this.uiSymbolLabel1);
            this.uiPanel1.Controls.Add(this.cbTrackname);
            this.uiPanel1.Controls.Add(this.tbFilename);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.uiPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel1.Location = new System.Drawing.Point(0, 29);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.uiPanel1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.uiPanel1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uiPanel1.Size = new System.Drawing.Size(510, 110);
            this.uiPanel1.Style = Sunny.UI.UIStyle.Colorful;
            this.uiPanel1.TabIndex = 5;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenFile.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnOpenFile.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnOpenFile.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnOpenFile.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnOpenFile.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnOpenFile.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpenFile.Location = new System.Drawing.Point(18, 16);
            this.btnOpenFile.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Radius = 1;
            this.btnOpenFile.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnOpenFile.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnOpenFile.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnOpenFile.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnOpenFile.Size = new System.Drawing.Size(105, 35);
            this.btnOpenFile.Style = Sunny.UI.UIStyle.Colorful;
            this.btnOpenFile.Symbol = 361564;
            this.btnOpenFile.TabIndex = 22;
            this.btnOpenFile.Text = "导入文件";
            this.btnOpenFile.TipsFont = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpenFile.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // uiSymbolLabel1
            // 
            this.uiSymbolLabel1.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolLabel1.Location = new System.Drawing.Point(18, 67);
            this.uiSymbolLabel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolLabel1.Name = "uiSymbolLabel1";
            this.uiSymbolLabel1.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.uiSymbolLabel1.Size = new System.Drawing.Size(105, 35);
            this.uiSymbolLabel1.Style = Sunny.UI.UIStyle.Colorful;
            this.uiSymbolLabel1.Symbol = 61641;
            this.uiSymbolLabel1.TabIndex = 22;
            this.uiSymbolLabel1.Text = "选择轨道";
            this.uiSymbolLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cbTrackname
            // 
            this.cbTrackname.DataSource = null;
            this.cbTrackname.FillColor = System.Drawing.Color.White;
            this.cbTrackname.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.cbTrackname.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbTrackname.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(242)))), ((int)(((byte)(238)))));
            this.cbTrackname.ItemRectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.cbTrackname.ItemSelectBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.cbTrackname.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.cbTrackname.Location = new System.Drawing.Point(129, 67);
            this.cbTrackname.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbTrackname.MinimumSize = new System.Drawing.Size(63, 0);
            this.cbTrackname.Name = "cbTrackname";
            this.cbTrackname.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cbTrackname.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.cbTrackname.Size = new System.Drawing.Size(345, 34);
            this.cbTrackname.Style = Sunny.UI.UIStyle.Colorful;
            this.cbTrackname.TabIndex = 8;
            this.cbTrackname.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbTrackname.Watermark = "轨道名";
            this.cbTrackname.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.cbTrackname.SelectedIndexChanged += new System.EventHandler(this.cbTrackname_SelectedIndexChanged);
            // 
            // tbFilename
            // 
            this.tbFilename.ButtonFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.tbFilename.ButtonFillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.tbFilename.ButtonFillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.tbFilename.ButtonRectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.tbFilename.ButtonRectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.tbFilename.ButtonRectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.tbFilename.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbFilename.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.tbFilename.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbFilename.Location = new System.Drawing.Point(129, 16);
            this.tbFilename.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbFilename.MinimumSize = new System.Drawing.Size(1, 16);
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.tbFilename.ScrollBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.tbFilename.ShowText = false;
            this.tbFilename.Size = new System.Drawing.Size(345, 34);
            this.tbFilename.Style = Sunny.UI.UIStyle.Colorful;
            this.tbFilename.TabIndex = 6;
            this.tbFilename.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbFilename.Watermark = "打开的文件名";
            this.tbFilename.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLine2
            // 
            this.uiLine2.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiLine2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.uiLine2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLine2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.uiLine2.Location = new System.Drawing.Point(0, 139);
            this.uiLine2.Margin = new System.Windows.Forms.Padding(0);
            this.uiLine2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLine2.Name = "uiLine2";
            this.uiLine2.Size = new System.Drawing.Size(510, 29);
            this.uiLine2.Style = Sunny.UI.UIStyle.Colorful;
            this.uiLine2.TabIndex = 6;
            this.uiLine2.Text = "播放选项控制";
            this.uiLine2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLine2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // tbSpeed
            // 
            this.tbSpeed.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.tbSpeed.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSpeed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.tbSpeed.Location = new System.Drawing.Point(127, 12);
            this.tbSpeed.MinimumSize = new System.Drawing.Size(1, 1);
            this.tbSpeed.Name = "tbSpeed";
            this.tbSpeed.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.tbSpeed.Size = new System.Drawing.Size(307, 29);
            this.tbSpeed.Style = Sunny.UI.UIStyle.Colorful;
            this.tbSpeed.TabIndex = 7;
            this.tbSpeed.Text = "uiTrackBar1";
            this.tbSpeed.Value = 50;
            this.tbSpeed.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.tbSpeed.ValueChanged += new System.EventHandler(this.uiTrackBar1_ValueChanged);
            // 
            // uiSymbolLabel2
            // 
            this.uiSymbolLabel2.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolLabel2.Location = new System.Drawing.Point(17, 12);
            this.uiSymbolLabel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolLabel2.Name = "uiSymbolLabel2";
            this.uiSymbolLabel2.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.uiSymbolLabel2.Size = new System.Drawing.Size(105, 35);
            this.uiSymbolLabel2.Style = Sunny.UI.UIStyle.Colorful;
            this.uiSymbolLabel2.Symbol = 363244;
            this.uiSymbolLabel2.SymbolOffset = new System.Drawing.Point(4, 0);
            this.uiSymbolLabel2.SymbolSize = 26;
            this.uiSymbolLabel2.TabIndex = 8;
            this.uiSymbolLabel2.Text = "调整速度";
            this.uiSymbolLabel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiSymbolLabel3
            // 
            this.uiSymbolLabel3.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolLabel3.Location = new System.Drawing.Point(17, 61);
            this.uiSymbolLabel3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolLabel3.Name = "uiSymbolLabel3";
            this.uiSymbolLabel3.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.uiSymbolLabel3.Size = new System.Drawing.Size(105, 35);
            this.uiSymbolLabel3.Style = Sunny.UI.UIStyle.Colorful;
            this.uiSymbolLabel3.Symbol = 361698;
            this.uiSymbolLabel3.SymbolOffset = new System.Drawing.Point(4, 0);
            this.uiSymbolLabel3.SymbolSize = 26;
            this.uiSymbolLabel3.TabIndex = 9;
            this.uiSymbolLabel3.Text = "调整音高";
            this.uiSymbolLabel3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // tbKey
            // 
            this.tbKey.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbKey.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.tbKey.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.tbKey.Location = new System.Drawing.Point(127, 61);
            this.tbKey.Maximum = 12;
            this.tbKey.Minimum = -12;
            this.tbKey.MinimumSize = new System.Drawing.Size(1, 1);
            this.tbKey.Name = "tbKey";
            this.tbKey.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.tbKey.Size = new System.Drawing.Size(307, 29);
            this.tbKey.Style = Sunny.UI.UIStyle.Colorful;
            this.tbKey.TabIndex = 12;
            this.tbKey.Text = "uiTrackBar2";
            this.tbKey.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.tbKey.ValueChanged += new System.EventHandler(this.tbKey_ValueChanged);
            // 
            // tbMidiProcess
            // 
            this.tbMidiProcess.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.tbMidiProcess.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMidiProcess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.tbMidiProcess.Location = new System.Drawing.Point(18, 102);
            this.tbMidiProcess.MinimumSize = new System.Drawing.Size(1, 1);
            this.tbMidiProcess.Name = "tbMidiProcess";
            this.tbMidiProcess.ReadOnly = true;
            this.tbMidiProcess.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.tbMidiProcess.Size = new System.Drawing.Size(456, 29);
            this.tbMidiProcess.Style = Sunny.UI.UIStyle.Colorful;
            this.tbMidiProcess.TabIndex = 13;
            this.tbMidiProcess.Text = "uiTrackBar3";
            this.tbMidiProcess.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnConfirmSpeed
            // 
            this.btnConfirmSpeed.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnConfirmSpeed.Enabled = false;
            this.btnConfirmSpeed.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnConfirmSpeed.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnConfirmSpeed.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnConfirmSpeed.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnConfirmSpeed.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnConfirmSpeed.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirmSpeed.Location = new System.Drawing.Point(438, 12);
            this.btnConfirmSpeed.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnConfirmSpeed.Name = "btnConfirmSpeed";
            this.btnConfirmSpeed.Radius = 25;
            this.btnConfirmSpeed.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnConfirmSpeed.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnConfirmSpeed.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnConfirmSpeed.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnConfirmSpeed.Size = new System.Drawing.Size(30, 30);
            this.btnConfirmSpeed.Style = Sunny.UI.UIStyle.Colorful;
            this.btnConfirmSpeed.TabIndex = 20;
            this.btnConfirmSpeed.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirmSpeed.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnConfirmSpeed.Click += new System.EventHandler(this.btnConfirmSpeed_Click);
            // 
            // uiPanel2
            // 
            this.uiPanel2.Controls.Add(this.btnBackward);
            this.uiPanel2.Controls.Add(this.btnStart);
            this.uiPanel2.Controls.Add(this.btnStop);
            this.uiPanel2.Controls.Add(this.btnForward);
            this.uiPanel2.Controls.Add(this.lblProcess);
            this.uiPanel2.Controls.Add(this.lblKey);
            this.uiPanel2.Controls.Add(this.lblSpeed);
            this.uiPanel2.Controls.Add(this.btnConfirmKey);
            this.uiPanel2.Controls.Add(this.uiSymbolLabel2);
            this.uiPanel2.Controls.Add(this.btnConfirmSpeed);
            this.uiPanel2.Controls.Add(this.tbSpeed);
            this.uiPanel2.Controls.Add(this.uiSymbolLabel3);
            this.uiPanel2.Controls.Add(this.tbKey);
            this.uiPanel2.Controls.Add(this.tbMidiProcess);
            this.uiPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.uiPanel2.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.uiPanel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel2.Location = new System.Drawing.Point(0, 168);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel2.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.uiPanel2.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uiPanel2.Size = new System.Drawing.Size(510, 182);
            this.uiPanel2.Style = Sunny.UI.UIStyle.Colorful;
            this.uiPanel2.TabIndex = 21;
            this.uiPanel2.Text = null;
            this.uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
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
            this.btnBackward.Location = new System.Drawing.Point(173, 137);
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
            this.btnBackward.TabIndex = 45;
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
            this.btnStart.Location = new System.Drawing.Point(216, 137);
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
            this.btnStart.TabIndex = 44;
            this.btnStart.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
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
            this.btnStop.Location = new System.Drawing.Point(259, 137);
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
            this.btnStop.SymbolOffset = new System.Drawing.Point(0, 1);
            this.btnStop.TabIndex = 43;
            this.btnStop.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
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
            this.btnForward.Location = new System.Drawing.Point(302, 137);
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
            this.btnForward.TabIndex = 42;
            this.btnForward.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnForward.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // lblProcess
            // 
            this.lblProcess.BackColor = System.Drawing.Color.Transparent;
            this.lblProcess.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProcess.Location = new System.Drawing.Point(343, 125);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(172, 21);
            this.lblProcess.Style = Sunny.UI.UIStyle.Colorful;
            this.lblProcess.TabIndex = 46;
            this.lblProcess.Text = "停止播放 00:00/00:00";
            this.lblProcess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProcess.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // lblKey
            // 
            this.lblKey.BackColor = System.Drawing.Color.Transparent;
            this.lblKey.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblKey.Location = new System.Drawing.Point(212, 86);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(138, 17);
            this.lblKey.Style = Sunny.UI.UIStyle.Colorful;
            this.lblKey.TabIndex = 23;
            this.lblKey.Text = "当前音高 +0 ";
            this.lblKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblKey.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // lblSpeed
            // 
            this.lblSpeed.BackColor = System.Drawing.Color.Transparent;
            this.lblSpeed.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSpeed.Location = new System.Drawing.Point(212, 40);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(138, 17);
            this.lblSpeed.Style = Sunny.UI.UIStyle.Colorful;
            this.lblSpeed.TabIndex = 22;
            this.lblSpeed.Text = "当前速度 1.00 倍";
            this.lblSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSpeed.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnConfirmKey
            // 
            this.btnConfirmKey.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmKey.Enabled = false;
            this.btnConfirmKey.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnConfirmKey.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnConfirmKey.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnConfirmKey.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnConfirmKey.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnConfirmKey.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirmKey.Location = new System.Drawing.Point(438, 61);
            this.btnConfirmKey.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnConfirmKey.Name = "btnConfirmKey";
            this.btnConfirmKey.Radius = 30;
            this.btnConfirmKey.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnConfirmKey.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnConfirmKey.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnConfirmKey.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnConfirmKey.Size = new System.Drawing.Size(30, 30);
            this.btnConfirmKey.Style = Sunny.UI.UIStyle.Colorful;
            this.btnConfirmKey.TabIndex = 21;
            this.btnConfirmKey.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirmKey.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnConfirmKey.Click += new System.EventHandler(this.btnConfirmKey_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "此处广告位招租";
            this.openFileDialog1.Filter = "mid 文件|*.mid|midi文件|*.midi";
            // 
            // PlayPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(510, 350);
            this.Controls.Add(this.uiPanel2);
            this.Controls.Add(this.uiLine2);
            this.Controls.Add(this.uiPanel1);
            this.Controls.Add(this.uiLine1);
            this.Name = "PlayPage";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.Style = Sunny.UI.UIStyle.Colorful;
            this.Text = "PlayForm";
            this.ReceiveParams += new Sunny.UI.OnReceiveParams(this.PlayForm_ReceiveParams);
            this.uiPanel1.ResumeLayout(false);
            this.uiPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UILine uiLine1;
        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UIComboBox cbTrackname;
        private Sunny.UI.UITextBox tbFilename;
        private Sunny.UI.UILine uiLine2;
        private Sunny.UI.UITrackBar tbSpeed;
        private Sunny.UI.UISymbolLabel uiSymbolLabel2;
        private Sunny.UI.UISymbolLabel uiSymbolLabel3;
        private Sunny.UI.UITrackBar tbKey;
        private Sunny.UI.UITrackBar tbMidiProcess;
        private Sunny.UI.UISymbolButton btnConfirmSpeed;
        private Sunny.UI.UIPanel uiPanel2;
        private Sunny.UI.UISymbolButton btnConfirmKey;
        private Sunny.UI.UISymbolButton btnOpenFile;
        private Sunny.UI.UISymbolLabel uiSymbolLabel1;
        private Sunny.UI.UILabel lblSpeed;
        private Sunny.UI.UILabel lblKey;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Sunny.UI.UISymbolButton btnBackward;
        private Sunny.UI.UISymbolButton btnStart;
        private Sunny.UI.UISymbolButton btnStop;
        private Sunny.UI.UISymbolButton btnForward;
        private Sunny.UI.UILabel lblProcess;
    }
}