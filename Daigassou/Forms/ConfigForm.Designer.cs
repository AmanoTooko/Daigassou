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
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNtpServer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbAutoChord = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chordEventNum = new System.Windows.Forms.NumericUpDown();
            this.minEventNum = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbUsingAnalysis = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbPcap = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.hotKeyControl5 = new Daigassou.HotKeyControl();
            this.hotKeyControl4 = new Daigassou.HotKeyControl();
            this.hotKeyControl3 = new Daigassou.HotKeyControl();
            this.hotKeyControl1 = new Daigassou.HotKeyControl();
            this.label11 = new System.Windows.Forms.Label();
            this.hotKeyControl2 = new Daigassou.HotKeyControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpKey = new System.Windows.Forms.TabPage();
            this.tpPlaySetting = new System.Windows.Forms.TabPage();
            this.tbLyric = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.cbSuffix = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbLrcEnable = new System.Windows.Forms.CheckBox();
            this.nudPort = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.chordEventNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minEventNum)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpKey.SuspendLayout();
            this.tpPlaySetting.SuspendLayout();
            this.tbLyric.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 18);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 28);
            this.label6.TabIndex = 42;
            this.label6.Text = "开始演奏";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.ForeColor = System.Drawing.Color.Gray;
            this.label9.Location = new System.Drawing.Point(15, 117);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 28);
            this.label9.TabIndex = 45;
            this.label9.Text = "向下移调";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 84);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 28);
            this.label8.TabIndex = 44;
            this.label8.Text = "向上移调";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 51);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 28);
            this.label7.TabIndex = 43;
            this.label7.Text = "结束演奏";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 28);
            this.label1.TabIndex = 33;
            this.label1.Text = "音符间最小间隔";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 57);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 28);
            this.label2.TabIndex = 34;
            this.label2.Text = "和弦解析最小间隔";
            // 
            // tbNtpServer
            // 
            this.tbNtpServer.ForeColor = System.Drawing.Color.Gray;
            this.tbNtpServer.Location = new System.Drawing.Point(146, 130);
            this.tbNtpServer.Name = "tbNtpServer";
            this.tbNtpServer.Size = new System.Drawing.Size(142, 35);
            this.tbNtpServer.TabIndex = 40;
            this.tbNtpServer.TextChanged += new System.EventHandler(this.TbNtpServer_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 210);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 28);
            this.label3.TabIndex = 35;
            this.label3.Text = "和弦平均化";
            // 
            // cbAutoChord
            // 
            this.cbAutoChord.AutoSize = true;
            this.cbAutoChord.Location = new System.Drawing.Point(146, 209);
            this.cbAutoChord.Name = "cbAutoChord";
            this.cbAutoChord.Size = new System.Drawing.Size(80, 32);
            this.cbAutoChord.TabIndex = 39;
            this.cbAutoChord.Text = "开启";
            this.cbAutoChord.UseVisualStyleBackColor = true;
            this.cbAutoChord.CheckedChanged += new System.EventHandler(this.CbAutoChord_CheckedChangeEvent);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 135);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 28);
            this.label5.TabIndex = 36;
            this.label5.Text = "NTP服务器";
            // 
            // chordEventNum
            // 
            this.chordEventNum.ForeColor = System.Drawing.Color.Gray;
            this.chordEventNum.Location = new System.Drawing.Point(146, 54);
            this.chordEventNum.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.chordEventNum.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.chordEventNum.Name = "chordEventNum";
            this.chordEventNum.Size = new System.Drawing.Size(120, 35);
            this.chordEventNum.TabIndex = 38;
            this.chordEventNum.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.chordEventNum.ValueChanged += new System.EventHandler(this.ChordEventNum_NumChanged);
            // 
            // minEventNum
            // 
            this.minEventNum.ForeColor = System.Drawing.Color.Gray;
            this.minEventNum.Location = new System.Drawing.Point(146, 12);
            this.minEventNum.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.minEventNum.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.minEventNum.Name = "minEventNum";
            this.minEventNum.Size = new System.Drawing.Size(120, 35);
            this.minEventNum.TabIndex = 37;
            this.minEventNum.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.minEventNum.ValueChanged += new System.EventHandler(this.MinEventNum_NumChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cbUsingAnalysis);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.cbPcap);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbNtpServer);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cbAutoChord);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.minEventNum);
            this.panel1.Controls.Add(this.chordEventNum);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(308, 258);
            this.panel1.TabIndex = 50;
            // 
            // cbUsingAnalysis
            // 
            this.cbUsingAnalysis.AutoSize = true;
            this.cbUsingAnalysis.Location = new System.Drawing.Point(146, 92);
            this.cbUsingAnalysis.Name = "cbUsingAnalysis";
            this.cbUsingAnalysis.Size = new System.Drawing.Size(80, 32);
            this.cbUsingAnalysis.TabIndex = 58;
            this.cbUsingAnalysis.Text = "开启";
            this.cbUsingAnalysis.UseVisualStyleBackColor = true;
            this.cbUsingAnalysis.CheckedChanged += new System.EventHandler(this.cbUsingAnalysis_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(4, 93);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(138, 28);
            this.label14.TabIndex = 57;
            this.label14.Text = "使用和弦解析";
            // 
            // cbPcap
            // 
            this.cbPcap.AutoSize = true;
            this.cbPcap.Location = new System.Drawing.Point(146, 171);
            this.cbPcap.Name = "cbPcap";
            this.cbPcap.Size = new System.Drawing.Size(80, 32);
            this.cbPcap.TabIndex = 56;
            this.cbPcap.Text = "开启";
            this.cbPcap.UseVisualStyleBackColor = true;
            this.cbPcap.CheckedChanged += new System.EventHandler(this.cbPcap_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 172);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(139, 28);
            this.label13.TabIndex = 55;
            this.label13.Text = "使用winpcap";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = global::Daigassou.Properties.Resources.c_about;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Location = new System.Drawing.Point(247, 195);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(63, 62);
            this.panel4.TabIndex = 54;
            this.panel4.Click += new System.EventHandler(this.panel4_Click);
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            this.panel4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel4_MouseClick);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.hotKeyControl5);
            this.panel3.Controls.Add(this.hotKeyControl4);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.hotKeyControl3);
            this.panel3.Controls.Add(this.hotKeyControl1);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.hotKeyControl2);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(308, 258);
            this.panel3.TabIndex = 51;
            // 
            // hotKeyControl5
            // 
            this.hotKeyControl5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hotKeyControl5.ForceModifiers = false;
            this.hotKeyControl5.Location = new System.Drawing.Point(131, 151);
            this.hotKeyControl5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hotKeyControl5.Name = "hotKeyControl5";
            this.hotKeyControl5.Size = new System.Drawing.Size(150, 31);
            this.hotKeyControl5.TabIndex = 48;
            this.hotKeyControl5.ToolTip = null;
            this.hotKeyControl5.HotKeyIsSet += new BondTech.HotkeyManagement.Win.HotKeyIsSetEventHandler(this.HotKeyControl1_HotKeyIsSet);
            this.hotKeyControl5.HotKeyIsReset += new System.EventHandler(this.HotKeyControl1_HotKeyIsReset);
            // 
            // hotKeyControl4
            // 
            this.hotKeyControl4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hotKeyControl4.ForceModifiers = false;
            this.hotKeyControl4.Location = new System.Drawing.Point(131, 117);
            this.hotKeyControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hotKeyControl4.Name = "hotKeyControl4";
            this.hotKeyControl4.Size = new System.Drawing.Size(150, 31);
            this.hotKeyControl4.TabIndex = 48;
            this.hotKeyControl4.ToolTip = null;
            this.hotKeyControl4.HotKeyIsSet += new BondTech.HotkeyManagement.Win.HotKeyIsSetEventHandler(this.HotKeyControl1_HotKeyIsSet);
            this.hotKeyControl4.HotKeyIsReset += new System.EventHandler(this.HotKeyControl1_HotKeyIsReset);
            // 
            // hotKeyControl3
            // 
            this.hotKeyControl3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hotKeyControl3.ForceModifiers = false;
            this.hotKeyControl3.Location = new System.Drawing.Point(131, 84);
            this.hotKeyControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hotKeyControl3.Name = "hotKeyControl3";
            this.hotKeyControl3.Size = new System.Drawing.Size(150, 31);
            this.hotKeyControl3.TabIndex = 47;
            this.hotKeyControl3.ToolTip = null;
            this.hotKeyControl3.HotKeyIsSet += new BondTech.HotkeyManagement.Win.HotKeyIsSetEventHandler(this.HotKeyControl1_HotKeyIsSet);
            this.hotKeyControl3.HotKeyIsReset += new System.EventHandler(this.HotKeyControl1_HotKeyIsReset);
            // 
            // hotKeyControl1
            // 
            this.hotKeyControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hotKeyControl1.ForceModifiers = false;
            this.hotKeyControl1.Location = new System.Drawing.Point(131, 18);
            this.hotKeyControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hotKeyControl1.Name = "hotKeyControl1";
            this.hotKeyControl1.Size = new System.Drawing.Size(150, 31);
            this.hotKeyControl1.TabIndex = 41;
            this.hotKeyControl1.ToolTip = null;
            this.hotKeyControl1.HotKeyIsSet += new BondTech.HotkeyManagement.Win.HotKeyIsSetEventHandler(this.HotKeyControl1_HotKeyIsSet);
            this.hotKeyControl1.HotKeyIsReset += new System.EventHandler(this.HotKeyControl1_HotKeyIsReset);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.ForeColor = System.Drawing.Color.Gray;
            this.label11.Location = new System.Drawing.Point(15, 150);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 28);
            this.label11.TabIndex = 45;
            this.label11.Text = "暂停演奏";
            // 
            // hotKeyControl2
            // 
            this.hotKeyControl2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hotKeyControl2.ForceModifiers = false;
            this.hotKeyControl2.Location = new System.Drawing.Point(131, 51);
            this.hotKeyControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hotKeyControl2.Name = "hotKeyControl2";
            this.hotKeyControl2.Size = new System.Drawing.Size(150, 31);
            this.hotKeyControl2.TabIndex = 46;
            this.hotKeyControl2.ToolTip = null;
            this.hotKeyControl2.HotKeyIsSet += new BondTech.HotkeyManagement.Win.HotKeyIsSetEventHandler(this.HotKeyControl1_HotKeyIsSet);
            this.hotKeyControl2.HotKeyIsReset += new System.EventHandler(this.HotKeyControl1_HotKeyIsReset);
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::Daigassou.Properties.Resources.c_about;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(247, 195);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(63, 62);
            this.panel2.TabIndex = 53;
            this.panel2.Click += new System.EventHandler(this.Panel2_Click);
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpKey);
            this.tabControl1.Controls.Add(this.tpPlaySetting);
            this.tabControl1.Controls.Add(this.tbLyric);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(322, 305);
            this.tabControl1.TabIndex = 53;
            // 
            // tpKey
            // 
            this.tpKey.Controls.Add(this.panel3);
            this.tpKey.Location = new System.Drawing.Point(4, 37);
            this.tpKey.Name = "tpKey";
            this.tpKey.Padding = new System.Windows.Forms.Padding(3);
            this.tpKey.Size = new System.Drawing.Size(314, 264);
            this.tpKey.TabIndex = 0;
            this.tpKey.Text = "快捷键绑定";
            this.tpKey.UseVisualStyleBackColor = true;
            // 
            // tpPlaySetting
            // 
            this.tpPlaySetting.Controls.Add(this.panel1);
            this.tpPlaySetting.Location = new System.Drawing.Point(4, 37);
            this.tpPlaySetting.Name = "tpPlaySetting";
            this.tpPlaySetting.Padding = new System.Windows.Forms.Padding(3);
            this.tpPlaySetting.Size = new System.Drawing.Size(314, 264);
            this.tpPlaySetting.TabIndex = 1;
            this.tpPlaySetting.Text = "播放参数";
            this.tpPlaySetting.UseVisualStyleBackColor = true;
            // 
            // tbLyric
            // 
            this.tbLyric.Controls.Add(this.panel5);
            this.tbLyric.Controls.Add(this.label10);
            this.tbLyric.Controls.Add(this.cbSuffix);
            this.tbLyric.Controls.Add(this.label4);
            this.tbLyric.Controls.Add(this.label12);
            this.tbLyric.Controls.Add(this.cbLrcEnable);
            this.tbLyric.Controls.Add(this.nudPort);
            this.tbLyric.Location = new System.Drawing.Point(4, 37);
            this.tbLyric.Name = "tbLyric";
            this.tbLyric.Padding = new System.Windows.Forms.Padding(3);
            this.tbLyric.Size = new System.Drawing.Size(314, 264);
            this.tbLyric.TabIndex = 2;
            this.tbLyric.Text = "歌词设置";
            this.tbLyric.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = global::Daigassou.Properties.Resources.c_about;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Location = new System.Drawing.Point(238, 181);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(63, 62);
            this.panel5.TabIndex = 55;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 98);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 28);
            this.label10.TabIndex = 49;
            this.label10.Text = "消息类别";
            // 
            // cbSuffix
            // 
            this.cbSuffix.ForeColor = System.Drawing.Color.Gray;
            this.cbSuffix.FormattingEnabled = true;
            this.cbSuffix.Items.AddRange(new object[] {
            "/说话频道",
            "/小队频道",
            "/感情表现 "});
            this.cbSuffix.Location = new System.Drawing.Point(144, 95);
            this.cbSuffix.Name = "cbSuffix";
            this.cbSuffix.Size = new System.Drawing.Size(121, 36);
            this.cbSuffix.TabIndex = 48;
            this.cbSuffix.SelectedIndexChanged += new System.EventHandler(this.cbSuffix_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 58);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 28);
            this.label4.TabIndex = 41;
            this.label4.Text = "鲶鱼精端口号";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 21);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 28);
            this.label12.TabIndex = 43;
            this.label12.Text = "歌词功能";
            // 
            // cbLrcEnable
            // 
            this.cbLrcEnable.AutoSize = true;
            this.cbLrcEnable.Location = new System.Drawing.Point(145, 21);
            this.cbLrcEnable.Name = "cbLrcEnable";
            this.cbLrcEnable.Size = new System.Drawing.Size(80, 32);
            this.cbLrcEnable.TabIndex = 47;
            this.cbLrcEnable.Text = "开启";
            this.cbLrcEnable.UseVisualStyleBackColor = true;
            this.cbLrcEnable.CheckedChanged += new System.EventHandler(this.cbLrcEnable_CheckedChanged);
            // 
            // nudPort
            // 
            this.nudPort.ForeColor = System.Drawing.Color.Gray;
            this.nudPort.Location = new System.Drawing.Point(144, 56);
            this.nudPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudPort.Name = "nudPort";
            this.nudPort.Size = new System.Drawing.Size(120, 35);
            this.nudPort.TabIndex = 45;
            this.nudPort.Value = new decimal(new int[] {
            2345,
            0,
            0,
            0});
            this.nudPort.ValueChanged += new System.EventHandler(this.nudPort_ValueChanged);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(322, 305);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.ForeColor = System.Drawing.Color.Gray;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ConfigForm";
            this.Text = "莫古莫古嘭嘭";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.chordEventNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minEventNum)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpKey.ResumeLayout(false);
            this.tpPlaySetting.ResumeLayout(false);
            this.tbLyric.ResumeLayout(false);
            this.tbLyric.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal HotKeyControl hotKeyControl4;
        internal HotKeyControl hotKeyControl3;
        internal HotKeyControl hotKeyControl2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        internal HotKeyControl hotKeyControl1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbNtpServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbAutoChord;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown chordEventNum;
        private System.Windows.Forms.NumericUpDown minEventNum;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        internal HotKeyControl hotKeyControl5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpKey;
        private System.Windows.Forms.TabPage tpPlaySetting;
        private System.Windows.Forms.TabPage tbLyric;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbSuffix;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox cbLrcEnable;
        private System.Windows.Forms.NumericUpDown nudPort;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox cbPcap;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox cbUsingAnalysis;
        private System.Windows.Forms.Label label14;
    }
}