namespace Daigassou
{
    partial class ConfigForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.minEventNum = new System.Windows.Forms.NumericUpDown();
            this.chordEventNum = new System.Windows.Forms.NumericUpDown();
            this.cbAutoChord = new System.Windows.Forms.CheckBox();
            this.tbNtpServer = new System.Windows.Forms.TextBox();
            this.hotKeyControl1 = new BondTech.HotkeyManagement.Win.HotKeyControl();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.快捷键绑定 = new System.Windows.Forms.GroupBox();
            this.hotKeyControl4 = new BondTech.HotkeyManagement.Win.HotKeyControl();
            this.hotKeyControl3 = new BondTech.HotkeyManagement.Win.HotKeyControl();
            this.hotKeyControl2 = new BondTech.HotkeyManagement.Win.HotKeyControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.minEventNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chordEventNum)).BeginInit();
            this.快捷键绑定.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "两音符最小间隔";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 81);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "和弦解析最小间隔";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 119);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "等比例琶音";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 156);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "NTP服务器";
            // 
            // minEventNum
            // 
            this.minEventNum.Location = new System.Drawing.Point(148, 37);
            this.minEventNum.Name = "minEventNum";
            this.minEventNum.Size = new System.Drawing.Size(120, 27);
            this.minEventNum.TabIndex = 12;
            this.minEventNum.ValueChanged += new System.EventHandler(this.MinEventNum_NumChanged);
            // 
            // chordEventNum
            // 
            this.chordEventNum.Location = new System.Drawing.Point(148, 79);
            this.chordEventNum.Name = "chordEventNum";
            this.chordEventNum.Size = new System.Drawing.Size(120, 27);
            this.chordEventNum.TabIndex = 13;
            this.chordEventNum.ValueChanged += new System.EventHandler(this.ChordEventNum_NumChanged);
            // 
            // cbAutoChord
            // 
            this.cbAutoChord.AutoSize = true;
            this.cbAutoChord.Location = new System.Drawing.Point(148, 119);
            this.cbAutoChord.Name = "cbAutoChord";
            this.cbAutoChord.Size = new System.Drawing.Size(58, 24);
            this.cbAutoChord.TabIndex = 14;
            this.cbAutoChord.Text = "开启";
            this.cbAutoChord.UseVisualStyleBackColor = true;
            this.cbAutoChord.CheckedChanged += new System.EventHandler(this.CbAutoChord_CheckedChangeEvent);
            // 
            // tbNtpServer
            // 
            this.tbNtpServer.Location = new System.Drawing.Point(148, 149);
            this.tbNtpServer.Name = "tbNtpServer";
            this.tbNtpServer.Size = new System.Drawing.Size(142, 27);
            this.tbNtpServer.TabIndex = 16;
            this.tbNtpServer.TextChanged += new System.EventHandler(this.TbNtpServer_TextChanged);
            // 
            // hotKeyControl1
            // 
            this.hotKeyControl1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hotKeyControl1.Location = new System.Drawing.Point(144, 37);
            this.hotKeyControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hotKeyControl1.Name = "hotKeyControl1";
            this.hotKeyControl1.Size = new System.Drawing.Size(150, 25);
            this.hotKeyControl1.TabIndex = 17;
            this.hotKeyControl1.ToolTip = null;
            this.hotKeyControl1.HotKeyIsSet += new BondTech.HotkeyManagement.Win.HotKeyIsSetEventHandler(this.HotKeyControl1_HotKeyIsSet);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 38);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 20);
            this.label6.TabIndex = 18;
            this.label6.Text = "开始演奏";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 68);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 20);
            this.label7.TabIndex = 19;
            this.label7.Text = "结束演奏";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 97);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 20);
            this.label8.TabIndex = 20;
            this.label8.Text = "向上移调";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 127);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 20);
            this.label9.TabIndex = 21;
            this.label9.Text = "向下移调";
            // 
            // 快捷键绑定
            // 
            this.快捷键绑定.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.快捷键绑定.Controls.Add(this.hotKeyControl4);
            this.快捷键绑定.Controls.Add(this.hotKeyControl3);
            this.快捷键绑定.Controls.Add(this.hotKeyControl2);
            this.快捷键绑定.Controls.Add(this.label6);
            this.快捷键绑定.Controls.Add(this.label9);
            this.快捷键绑定.Controls.Add(this.hotKeyControl1);
            this.快捷键绑定.Controls.Add(this.label8);
            this.快捷键绑定.Controls.Add(this.label7);
            this.快捷键绑定.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.快捷键绑定.Location = new System.Drawing.Point(0, 192);
            this.快捷键绑定.Name = "快捷键绑定";
            this.快捷键绑定.Size = new System.Drawing.Size(315, 208);
            this.快捷键绑定.TabIndex = 22;
            this.快捷键绑定.TabStop = false;
            this.快捷键绑定.Text = "快捷键绑定";
            // 
            // hotKeyControl4
            // 
            this.hotKeyControl4.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.hotKeyControl4.Location = new System.Drawing.Point(144, 124);
            this.hotKeyControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hotKeyControl4.Name = "hotKeyControl4";
            this.hotKeyControl4.Size = new System.Drawing.Size(150, 25);
            this.hotKeyControl4.TabIndex = 24;
            this.hotKeyControl4.ToolTip = null;
            // 
            // hotKeyControl3
            // 
            this.hotKeyControl3.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.hotKeyControl3.Location = new System.Drawing.Point(144, 96);
            this.hotKeyControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hotKeyControl3.Name = "hotKeyControl3";
            this.hotKeyControl3.Size = new System.Drawing.Size(150, 25);
            this.hotKeyControl3.TabIndex = 23;
            this.hotKeyControl3.ToolTip = null;
            // 
            // hotKeyControl2
            // 
            this.hotKeyControl2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.hotKeyControl2.Location = new System.Drawing.Point(144, 67);
            this.hotKeyControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hotKeyControl2.Name = "hotKeyControl2";
            this.hotKeyControl2.Size = new System.Drawing.Size(150, 25);
            this.hotKeyControl2.TabIndex = 22;
            this.hotKeyControl2.ToolTip = null;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbNtpServer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbAutoChord);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.chordEventNum);
            this.groupBox1.Controls.Add(this.minEventNum);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 192);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(315, 400);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.快捷键绑定);
            this.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ConfigForm";
            this.Text = "莫古莫古嘭嘭";
            ((System.ComponentModel.ISupportInitialize)(this.minEventNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chordEventNum)).EndInit();
            this.快捷键绑定.ResumeLayout(false);
            this.快捷键绑定.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown minEventNum;
        private System.Windows.Forms.NumericUpDown chordEventNum;
        private System.Windows.Forms.CheckBox cbAutoChord;
        private System.Windows.Forms.TextBox tbNtpServer;
        private BondTech.HotkeyManagement.Win.HotKeyControl hotKeyControl1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox 快捷键绑定;
        private BondTech.HotkeyManagement.Win.HotKeyControl hotKeyControl4;
        private BondTech.HotkeyManagement.Win.HotKeyControl hotKeyControl3;
        private BondTech.HotkeyManagement.Win.HotKeyControl hotKeyControl2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}