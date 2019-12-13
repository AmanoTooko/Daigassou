namespace Daigassou
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.RadioButton radioButton3;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.trackComboBox = new System.Windows.Forms.ComboBox();
            this.gBMidiFile = new System.Windows.Forms.GroupBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnFileSelect = new System.Windows.Forms.Button();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnTimeSync = new System.Windows.Forms.Button();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSyncReady = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.midFileDiag = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gBParameterSetting = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.nudBpm = new System.Windows.Forms.NumericUpDown();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbMidiKeyboard = new System.Windows.Forms.ComboBox();
            this.gBKeySetting = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.btn22key = new System.Windows.Forms.Button();
            this.btn8key = new System.Windows.Forms.Button();
            this.gBKeyboardSetting = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnKeyboardConnect = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.timeLabel = new System.Windows.Forms.Label();
            this.lblMidiName = new System.Windows.Forms.Label();
            this.lblPlay = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tlblTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.timeStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.playTimer = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            radioButton3 = new System.Windows.Forms.RadioButton();
            this.gBMidiFile.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.gBParameterSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBpm)).BeginInit();
            this.gBKeySetting.SuspendLayout();
            this.gBKeyboardSetting.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Font = new System.Drawing.Font("微软雅黑", 12F);
            radioButton3.Location = new System.Drawing.Point(263, 70);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new System.Drawing.Size(69, 25);
            radioButton3.TabIndex = 7;
            radioButton3.Text = "高8度";
            this.toolTip1.SetToolTip(radioButton3, "点这里是设置音高的");
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // trackComboBox
            // 
            this.trackComboBox.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.trackComboBox.ForeColor = System.Drawing.Color.Gray;
            this.trackComboBox.FormattingEnabled = true;
            this.trackComboBox.Location = new System.Drawing.Point(93, 66);
            this.trackComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.trackComboBox.Name = "trackComboBox";
            this.trackComboBox.Size = new System.Drawing.Size(243, 28);
            this.trackComboBox.TabIndex = 1;
            this.toolTip1.SetToolTip(this.trackComboBox, "点这里选择音轨");
            this.trackComboBox.SelectedIndexChanged += new System.EventHandler(this.trackComboBox_SelectedIndexChanged);
            this.trackComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TrackComboBox_KeyDown);
            // 
            // gBMidiFile
            // 
            this.gBMidiFile.Controls.Add(this.panel6);
            this.gBMidiFile.Controls.Add(this.btnFileSelect);
            this.gBMidiFile.Controls.Add(this.pathTextBox);
            this.gBMidiFile.Controls.Add(this.label1);
            this.gBMidiFile.Controls.Add(this.label2);
            this.gBMidiFile.Controls.Add(this.trackComboBox);
            this.gBMidiFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.gBMidiFile.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gBMidiFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(75)))), ((int)(((byte)(107)))));
            this.gBMidiFile.Location = new System.Drawing.Point(0, 13);
            this.gBMidiFile.Name = "gBMidiFile";
            this.gBMidiFile.Size = new System.Drawing.Size(357, 111);
            this.gBMidiFile.TabIndex = 3;
            this.gBMidiFile.TabStop = false;
            this.gBMidiFile.Text = "Midi乐谱选择";
            // 
            // panel6
            // 
            this.panel6.Location = new System.Drawing.Point(-1, 99);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1346, 13);
            this.panel6.TabIndex = 19;
            // 
            // btnFileSelect
            // 
            this.btnFileSelect.BackColor = System.Drawing.Color.White;
            this.btnFileSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFileSelect.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFileSelect.ForeColor = System.Drawing.Color.Black;
            this.btnFileSelect.Location = new System.Drawing.Point(291, 31);
            this.btnFileSelect.Name = "btnFileSelect";
            this.btnFileSelect.Size = new System.Drawing.Size(45, 26);
            this.btnFileSelect.TabIndex = 7;
            this.btnFileSelect.Text = "···";
            this.btnFileSelect.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.btnFileSelect, "点这里选择Midi文件");
            this.btnFileSelect.UseVisualStyleBackColor = false;
            this.btnFileSelect.Click += new System.EventHandler(this.selectFileButton_Click);
            // 
            // pathTextBox
            // 
            this.pathTextBox.Enabled = false;
            this.pathTextBox.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pathTextBox.ForeColor = System.Drawing.Color.Gray;
            this.pathTextBox.Location = new System.Drawing.Point(93, 31);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(192, 26);
            this.pathTextBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(16, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "导入Mid";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(16, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "选择音轨";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.btnTimeSync);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btnSyncReady);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(75)))), ((int)(((byte)(107)))));
            this.groupBox1.Location = new System.Drawing.Point(0, 323);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(357, 135);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "同步演奏";
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(0, 123);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1346, 13);
            this.panel3.TabIndex = 16;
            // 
            // btnTimeSync
            // 
            this.btnTimeSync.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnTimeSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimeSync.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnTimeSync.ForeColor = System.Drawing.Color.White;
            this.btnTimeSync.Location = new System.Drawing.Point(264, 53);
            this.btnTimeSync.Name = "btnTimeSync";
            this.btnTimeSync.Size = new System.Drawing.Size(81, 29);
            this.btnTimeSync.TabIndex = 6;
            this.btnTimeSync.Text = "开启抓包";
            this.toolTip1.SetToolTip(this.btnTimeSync, "开启或停止抓包模式\r\n点击后会自动与服务器同步本地时钟\r\n并且抓取第一个游戏进程的演奏数据包\r\n点一次就行了！");
            this.btnTimeSync.UseVisualStyleBackColor = false;
            this.btnTimeSync.Click += new System.EventHandler(this.BtnTimeSync_Click);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.numericUpDown2.Location = new System.Drawing.Point(107, 90);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(100, 26);
            this.numericUpDown2.TabIndex = 5;
            this.toolTip1.SetToolTip(this.numericUpDown2, "海外党适用\r\n当队员们与服务器延迟过大的时候\r\n用于补正Ping值\r\n设置方法为全员平均Ping值-每个人的Ping值");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.label8.ForeColor = System.Drawing.Color.Gray;
            this.label8.Location = new System.Drawing.Point(16, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "网络延迟";
            // 
            // btnSyncReady
            // 
            this.btnSyncReady.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnSyncReady.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSyncReady.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnSyncReady.ForeColor = System.Drawing.Color.White;
            this.btnSyncReady.Location = new System.Drawing.Point(264, 88);
            this.btnSyncReady.Name = "btnSyncReady";
            this.btnSyncReady.Size = new System.Drawing.Size(81, 29);
            this.btnSyncReady.TabIndex = 3;
            this.btnSyncReady.Text = "准备好了";
            this.toolTip1.SetToolTip(this.btnSyncReady, "点击后进游戏等待就可以了\r\n点一次就行了嗷！");
            this.btnSyncReady.UseVisualStyleBackColor = false;
            this.btnSyncReady.Click += new System.EventHandler(this.SyncButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(16, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "演奏时间";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("微软雅黑", 10.5F);
            this.dateTimePicker1.CustomFormat = "HH-mm-ss";
            this.dateTimePicker1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker1.Location = new System.Drawing.Point(107, 55);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(100, 26);
            this.dateTimePicker1.TabIndex = 1;
            this.toolTip1.SetToolTip(this.dateTimePicker1, "合奏用\r\n与队友们设定同样的时间后\r\n点击[准备好了]，就可以自动合奏\r\n*时间支持复制粘贴*");
            this.dateTimePicker1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DateTimePicker1_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(14, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "通过定时来同步的演奏吧";
            // 
            // midFileDiag
            // 
            this.midFileDiag.FileName = "openFileDialog1";
            this.midFileDiag.Filter = "mid文件|*.mid";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // gBParameterSetting
            // 
            this.gBParameterSetting.Controls.Add(this.panel5);
            this.gBParameterSetting.Controls.Add(radioButton3);
            this.gBParameterSetting.Controls.Add(this.nudBpm);
            this.gBParameterSetting.Controls.Add(this.radioButton2);
            this.gBParameterSetting.Controls.Add(this.radioButton1);
            this.gBParameterSetting.Controls.Add(this.label5);
            this.gBParameterSetting.Controls.Add(this.label7);
            this.gBParameterSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.gBParameterSetting.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.gBParameterSetting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(75)))), ((int)(((byte)(107)))));
            this.gBParameterSetting.Location = new System.Drawing.Point(0, 124);
            this.gBParameterSetting.Name = "gBParameterSetting";
            this.gBParameterSetting.Size = new System.Drawing.Size(357, 109);
            this.gBParameterSetting.TabIndex = 5;
            this.gBParameterSetting.TabStop = false;
            this.gBParameterSetting.Text = "播放属性设置";
            // 
            // panel5
            // 
            this.panel5.Location = new System.Drawing.Point(-8, 98);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1346, 13);
            this.panel5.TabIndex = 18;
            // 
            // nudBpm
            // 
            this.nudBpm.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.nudBpm.ForeColor = System.Drawing.Color.Gray;
            this.nudBpm.Location = new System.Drawing.Point(182, 33);
            this.nudBpm.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.nudBpm.Minimum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.nudBpm.Name = "nudBpm";
            this.nudBpm.Size = new System.Drawing.Size(154, 29);
            this.nudBpm.TabIndex = 9;
            this.toolTip1.SetToolTip(this.nudBpm, "点这里可以设置速度");
            this.nudBpm.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.nudBpm.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.radioButton2.Location = new System.Drawing.Point(188, 70);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(60, 25);
            this.radioButton2.TabIndex = 6;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "原始";
            this.toolTip1.SetToolTip(this.radioButton2, "点这里是设置音高的");
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.radioButton1.Location = new System.Drawing.Point(107, 70);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(69, 25);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.Text = "低8度";
            this.toolTip1.SetToolTip(this.radioButton1, "点这里是设置音高的");
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(16, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "音高调整";
            this.toolTip1.SetToolTip(this.label5, "点这里是设置音高的");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Gray;
            this.label7.Location = new System.Drawing.Point(16, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(161, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "设置BPM，范围40~250";
            // 
            // cbMidiKeyboard
            // 
            this.cbMidiKeyboard.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbMidiKeyboard.ForeColor = System.Drawing.Color.Gray;
            this.cbMidiKeyboard.FormattingEnabled = true;
            this.cbMidiKeyboard.Location = new System.Drawing.Point(87, 34);
            this.cbMidiKeyboard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbMidiKeyboard.Name = "cbMidiKeyboard";
            this.cbMidiKeyboard.Size = new System.Drawing.Size(161, 28);
            this.cbMidiKeyboard.TabIndex = 8;
            this.toolTip1.SetToolTip(this.cbMidiKeyboard, "如果你有Midi键盘可以在这里连接\r\n没有的就不要凑热闹了！\r\n樱桃键盘不行！Filco也不行！\r\nHHKB也不行！王总不是钱的问题！\r\n");
            this.cbMidiKeyboard.SelectedIndexChanged += new System.EventHandler(this.cbMidiKeyboard_SelectedIndexChanged);
            // 
            // gBKeySetting
            // 
            this.gBKeySetting.Controls.Add(this.panel4);
            this.gBKeySetting.Controls.Add(this.btnSwitch);
            this.gBKeySetting.Controls.Add(this.btn22key);
            this.gBKeySetting.Controls.Add(this.btn8key);
            this.gBKeySetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.gBKeySetting.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.gBKeySetting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(75)))), ((int)(((byte)(107)))));
            this.gBKeySetting.Location = new System.Drawing.Point(0, 233);
            this.gBKeySetting.Name = "gBKeySetting";
            this.gBKeySetting.Size = new System.Drawing.Size(357, 90);
            this.gBKeySetting.TabIndex = 10;
            this.gBKeySetting.TabStop = false;
            this.gBKeySetting.Text = "游戏键位设置";
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(0, 80);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1346, 13);
            this.panel4.TabIndex = 17;
            // 
            // btnSwitch
            // 
            this.btnSwitch.BackColor = System.Drawing.Color.Transparent;
            this.btnSwitch.BackgroundImage = global::Daigassou.Properties.Resources.a0;
            this.btnSwitch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwitch.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnSwitch.ForeColor = System.Drawing.Color.White;
            this.btnSwitch.Location = new System.Drawing.Point(128, 38);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(95, 36);
            this.btnSwitch.TabIndex = 6;
            this.btnSwitch.UseVisualStyleBackColor = false;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // btn22key
            // 
            this.btn22key.BackColor = System.Drawing.Color.Transparent;
            this.btn22key.BackgroundImage = global::Daigassou.Properties.Resources.kb0;
            this.btn22key.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn22key.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn22key.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btn22key.ForeColor = System.Drawing.Color.White;
            this.btn22key.Location = new System.Drawing.Point(229, 38);
            this.btn22key.Name = "btn22key";
            this.btn22key.Size = new System.Drawing.Size(95, 36);
            this.btn22key.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btn22key, "点这里就可以设置键位了\r\n嗨呀气死我了你们不看教程的吗！");
            this.btn22key.UseVisualStyleBackColor = false;
            this.btn22key.Click += new System.EventHandler(this.btn22key_Click);
            // 
            // btn8key
            // 
            this.btn8key.BackColor = System.Drawing.Color.Transparent;
            this.btn8key.BackgroundImage = global::Daigassou.Properties.Resources.ka1;
            this.btn8key.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn8key.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn8key.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btn8key.ForeColor = System.Drawing.Color.White;
            this.btn8key.Location = new System.Drawing.Point(18, 38);
            this.btn8key.Name = "btn8key";
            this.btn8key.Size = new System.Drawing.Size(95, 36);
            this.btn8key.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btn8key, "点这里就可以设置键位了\r\n嗨呀气死我了你们不看教程的吗！\r\n");
            this.btn8key.UseVisualStyleBackColor = false;
            this.btn8key.Click += new System.EventHandler(this.button4_Click);
            // 
            // gBKeyboardSetting
            // 
            this.gBKeyboardSetting.Controls.Add(this.panel2);
            this.gBKeyboardSetting.Controls.Add(this.btnKeyboardConnect);
            this.gBKeyboardSetting.Controls.Add(this.label9);
            this.gBKeyboardSetting.Controls.Add(this.cbMidiKeyboard);
            this.gBKeyboardSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.gBKeyboardSetting.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.gBKeyboardSetting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(75)))), ((int)(((byte)(107)))));
            this.gBKeyboardSetting.Location = new System.Drawing.Point(0, 458);
            this.gBKeyboardSetting.Name = "gBKeyboardSetting";
            this.gBKeyboardSetting.Size = new System.Drawing.Size(357, 79);
            this.gBKeyboardSetting.TabIndex = 11;
            this.gBKeyboardSetting.TabStop = false;
            this.gBKeyboardSetting.Text = "Midi键盘选择";
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 72);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1346, 13);
            this.panel2.TabIndex = 15;
            // 
            // btnKeyboardConnect
            // 
            this.btnKeyboardConnect.BackColor = System.Drawing.Color.Transparent;
            this.btnKeyboardConnect.BackgroundImage = global::Daigassou.Properties.Resources.btn1;
            this.btnKeyboardConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnKeyboardConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyboardConnect.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnKeyboardConnect.ForeColor = System.Drawing.Color.White;
            this.btnKeyboardConnect.Location = new System.Drawing.Point(263, 28);
            this.btnKeyboardConnect.Name = "btnKeyboardConnect";
            this.btnKeyboardConnect.Size = new System.Drawing.Size(82, 34);
            this.btnKeyboardConnect.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnKeyboardConnect, "如果你有Midi键盘可以在这里连接\r\n没有的就不要凑热闹了！\r\n樱桃键盘不行！Filco也不行！\r\nHHKB也不行！王总不是钱的问题！");
            this.btnKeyboardConnect.UseVisualStyleBackColor = false;
            this.btnKeyboardConnect.Click += new System.EventHandler(this.btnKeyboardConnect_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.Gray;
            this.label9.Location = new System.Drawing.Point(16, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "键盘选择";
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(75)))), ((int)(((byte)(107)))));
            this.button1.Location = new System.Drawing.Point(0, 657);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(357, 36);
            this.button1.TabIndex = 13;
            this.button1.Text = "关于大合奏";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.timeLabel);
            this.groupBox2.Controls.Add(this.lblMidiName);
            this.groupBox2.Controls.Add(this.lblPlay);
            this.groupBox2.Controls.Add(this.btnStop);
            this.groupBox2.Controls.Add(this.btnPause);
            this.groupBox2.Controls.Add(this.btnPlay);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(75)))), ((int)(((byte)(107)))));
            this.groupBox2.Location = new System.Drawing.Point(0, 537);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(357, 134);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Midi轨道试听";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.timeLabel.ForeColor = System.Drawing.Color.Gray;
            this.timeLabel.Location = new System.Drawing.Point(277, 96);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(29, 19);
            this.timeLabel.TabIndex = 27;
            this.timeLabel.Text = "     ";
            // 
            // lblMidiName
            // 
            this.lblMidiName.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.lblMidiName.ForeColor = System.Drawing.Color.Gray;
            this.lblMidiName.Location = new System.Drawing.Point(1, 63);
            this.lblMidiName.Name = "lblMidiName";
            this.lblMidiName.Size = new System.Drawing.Size(356, 19);
            this.lblMidiName.TabIndex = 26;
            this.lblMidiName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPlay
            // 
            this.lblPlay.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblPlay.Location = new System.Drawing.Point(127, 33);
            this.lblPlay.Name = "lblPlay";
            this.lblPlay.Size = new System.Drawing.Size(100, 22);
            this.lblPlay.TabIndex = 25;
            this.lblPlay.Text = "试听未开始";
            this.lblPlay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Transparent;
            this.btnStop.BackgroundImage = global::Daigassou.Properties.Resources.c_stop;
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.ForeColor = System.Drawing.Color.Transparent;
            this.btnStop.Location = new System.Drawing.Point(91, 88);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(40, 40);
            this.btnStop.TabIndex = 24;
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackgroundImage = global::Daigassou.Properties.Resources.c_pause;
            this.btnPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.ForeColor = System.Drawing.Color.Transparent;
            this.btnPause.Location = new System.Drawing.Point(219, 88);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(40, 40);
            this.btnPause.TabIndex = 23;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.AutoSize = true;
            this.btnPlay.BackColor = System.Drawing.Color.White;
            this.btnPlay.BackgroundImage = global::Daigassou.Properties.Resources.c_play;
            this.btnPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.ForeColor = System.Drawing.Color.White;
            this.btnPlay.Location = new System.Drawing.Point(155, 88);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(40, 40);
            this.btnPlay.TabIndex = 22;
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(357, 13);
            this.panel1.TabIndex = 14;
            // 
            // tlblTime
            // 
            this.tlblTime.BackColor = System.Drawing.Color.Transparent;
            this.tlblTime.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tlblTime.ForeColor = System.Drawing.Color.Gray;
            this.tlblTime.Name = "tlblTime";
            this.tlblTime.Size = new System.Drawing.Size(68, 21);
            this.tlblTime.Text = "时钟未同步";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlblTime,
            this.timeStripStatus,
            this.toolStripSplitButton1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 693);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(357, 26);
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // timeStripStatus
            // 
            this.timeStripStatus.BackColor = System.Drawing.Color.Transparent;
            this.timeStripStatus.Name = "timeStripStatus";
            this.timeStripStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.timeStripStatus.Size = new System.Drawing.Size(56, 21);
            this.timeStripStatus.Text = "20:00:00";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.Image = global::Daigassou.Properties.Resources.s2;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(36, 24);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            this.toolStripSplitButton1.ButtonClick += new System.EventHandler(this.ToolStripSplitButton1_ButtonClick);
            // 
            // playTimer
            // 
            this.playTimer.Enabled = true;
            this.playTimer.Tick += new System.EventHandler(this.PlayTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(357, 719);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gBKeyboardSetting);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gBKeySetting);
            this.Controls.Add(this.gBParameterSetting);
            this.Controls.Add(this.gBMidiFile);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "大合奏!";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gBMidiFile.ResumeLayout(false);
            this.gBMidiFile.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.gBParameterSetting.ResumeLayout(false);
            this.gBParameterSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBpm)).EndInit();
            this.gBKeySetting.ResumeLayout(false);
            this.gBKeyboardSetting.ResumeLayout(false);
            this.gBKeyboardSetting.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox trackComboBox;
        private System.Windows.Forms.GroupBox gBMidiFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFileSelect;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog midFileDiag;
        private System.Windows.Forms.Button btnSyncReady;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gBParameterSetting;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudBpm;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button btn8key;
        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbMidiKeyboard;
        private System.Windows.Forms.GroupBox gBKeySetting;
        private System.Windows.Forms.GroupBox gBKeyboardSetting;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnKeyboardConnect;
        private System.Windows.Forms.Button btn22key;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnTimeSync;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Label lblMidiName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripStatusLabel tlblTime;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Timer playTimer;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.ToolStripStatusLabel timeStripStatus;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
    }
}

