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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.minEventNum = new System.Windows.Forms.NumericUpDown();
            this.chordEventNum = new System.Windows.Forms.NumericUpDown();
            this.cbAutoChord = new System.Windows.Forms.CheckBox();
            this.cbBackgroundKey = new System.Windows.Forms.CheckBox();
            this.tbNtpServer = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.minEventNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chordEventNum)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "MinEventMs";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 128);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "ChordEventMs";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 192);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "等比例琶音";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 252);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "后台播放";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 302);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "NTP服务器";
            // 
            // minEventNum
            // 
            this.minEventNum.Location = new System.Drawing.Point(178, 64);
            this.minEventNum.Name = "minEventNum";
            this.minEventNum.Size = new System.Drawing.Size(120, 31);
            this.minEventNum.TabIndex = 12;
            this.minEventNum.ValueChanged += new System.EventHandler(this.MinEventNum_NumChanged);
            // 
            // chordEventNum
            // 
            this.chordEventNum.Location = new System.Drawing.Point(178, 128);
            this.chordEventNum.Name = "chordEventNum";
            this.chordEventNum.Size = new System.Drawing.Size(120, 31);
            this.chordEventNum.TabIndex = 13;
            this.chordEventNum.ValueChanged += new System.EventHandler(this.ChordEventNum_NumChanged);
            // 
            // cbAutoChord
            // 
            this.cbAutoChord.AutoSize = true;
            this.cbAutoChord.Location = new System.Drawing.Point(178, 197);
            this.cbAutoChord.Name = "cbAutoChord";
            this.cbAutoChord.Size = new System.Drawing.Size(72, 29);
            this.cbAutoChord.TabIndex = 14;
            this.cbAutoChord.Text = "开启";
            this.cbAutoChord.UseVisualStyleBackColor = true;
            this.cbAutoChord.CheckedChanged += new System.EventHandler(this.CbAutoChord_CheckedChangeEvent);
            // 
            // cbBackgroundKey
            // 
            this.cbBackgroundKey.AutoSize = true;
            this.cbBackgroundKey.Location = new System.Drawing.Point(178, 248);
            this.cbBackgroundKey.Name = "cbBackgroundKey";
            this.cbBackgroundKey.Size = new System.Drawing.Size(72, 29);
            this.cbBackgroundKey.TabIndex = 15;
            this.cbBackgroundKey.Text = "开启";
            this.cbBackgroundKey.UseVisualStyleBackColor = true;
            this.cbBackgroundKey.CheckedChanged += new System.EventHandler(this.CbBackgroundKey_CheckedChangeEvent);
            // 
            // tbNtpServer
            // 
            this.tbNtpServer.Location = new System.Drawing.Point(178, 302);
            this.tbNtpServer.Name = "tbNtpServer";
            this.tbNtpServer.Size = new System.Drawing.Size(100, 31);
            this.tbNtpServer.TabIndex = 16;
            this.tbNtpServer.TextChanged += new System.EventHandler(this.TbNtpServer_TextChanged);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(331, 363);
            this.Controls.Add(this.tbNtpServer);
            this.Controls.Add(this.cbBackgroundKey);
            this.Controls.Add(this.cbAutoChord);
            this.Controls.Add(this.chordEventNum);
            this.Controls.Add(this.minEventNum);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ConfigForm";
            this.Text = "ConfigForm";
            ((System.ComponentModel.ISupportInitialize)(this.minEventNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chordEventNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown minEventNum;
        private System.Windows.Forms.NumericUpDown chordEventNum;
        private System.Windows.Forms.CheckBox cbAutoChord;
        private System.Windows.Forms.CheckBox cbBackgroundKey;
        private System.Windows.Forms.TextBox tbNtpServer;
    }
}