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
            this.selectFileButton = new System.Windows.Forms.Button();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.SyncButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.midFileDiag = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gBParameterSetting = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.cbMidiKeyboard = new System.Windows.Forms.ComboBox();
            this.gBKeySetting = new System.Windows.Forms.GroupBox();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.btn22key = new System.Windows.Forms.Button();
            this.btn8key = new System.Windows.Forms.Button();
            this.gBKeyboardSetting = new System.Windows.Forms.GroupBox();
            this.btnKeyboardConnect = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            radioButton3 = new System.Windows.Forms.RadioButton();
            this.gBMidiFile.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.gBParameterSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.gBKeySetting.SuspendLayout();
            this.gBKeyboardSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Font = new System.Drawing.Font("微软雅黑", 12F);
            radioButton3.Location = new System.Drawing.Point(263, 57);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new System.Drawing.Size(69, 25);
            radioButton3.TabIndex = 7;
            radioButton3.Text = "高8度";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // trackComboBox
            // 
            this.trackComboBox.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.trackComboBox.FormattingEnabled = true;
            this.trackComboBox.Location = new System.Drawing.Point(93, 57);
            this.trackComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.trackComboBox.Name = "trackComboBox";
            this.trackComboBox.Size = new System.Drawing.Size(243, 28);
            this.trackComboBox.TabIndex = 1;
            this.trackComboBox.SelectedIndexChanged += new System.EventHandler(this.trackComboBox_SelectedIndexChanged);
            // 
            // gBMidiFile
            // 
            this.gBMidiFile.Controls.Add(this.selectFileButton);
            this.gBMidiFile.Controls.Add(this.pathTextBox);
            this.gBMidiFile.Controls.Add(this.label1);
            this.gBMidiFile.Controls.Add(this.label2);
            this.gBMidiFile.Controls.Add(this.trackComboBox);
            this.gBMidiFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.gBMidiFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gBMidiFile.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gBMidiFile.ForeColor = System.Drawing.Color.OrangeRed;
            this.gBMidiFile.Location = new System.Drawing.Point(0, 0);
            this.gBMidiFile.Name = "gBMidiFile";
            this.gBMidiFile.Size = new System.Drawing.Size(351, 99);
            this.gBMidiFile.TabIndex = 3;
            this.gBMidiFile.TabStop = false;
            this.gBMidiFile.Text = "Midi乐谱选择";
            // 
            // selectFileButton
            // 
            this.selectFileButton.BackColor = System.Drawing.Color.White;
            this.selectFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectFileButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.selectFileButton.ForeColor = System.Drawing.Color.Black;
            this.selectFileButton.Location = new System.Drawing.Point(291, 22);
            this.selectFileButton.Name = "selectFileButton";
            this.selectFileButton.Size = new System.Drawing.Size(45, 26);
            this.selectFileButton.TabIndex = 7;
            this.selectFileButton.Text = "···";
            this.selectFileButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.selectFileButton.UseVisualStyleBackColor = false;
            this.selectFileButton.Click += new System.EventHandler(this.selectFileButton_Click);
            // 
            // pathTextBox
            // 
            this.pathTextBox.Enabled = false;
            this.pathTextBox.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pathTextBox.Location = new System.Drawing.Point(93, 22);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(192, 26);
            this.pathTextBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(16, 25);
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
            this.label2.Location = new System.Drawing.Point(16, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "选择音轨";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.SyncButton);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 15.25F);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.groupBox1.Location = new System.Drawing.Point(0, 564);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 134);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "同步演奏";
            this.groupBox1.Visible = false;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.numericUpDown2.Location = new System.Drawing.Point(102, 104);
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
            this.numericUpDown2.Size = new System.Drawing.Size(120, 29);
            this.numericUpDown2.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label8.Location = new System.Drawing.Point(14, 106);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 21);
            this.label8.TabIndex = 4;
            this.label8.Text = "网络延迟";
            // 
            // SyncButton
            // 
            this.SyncButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.SyncButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SyncButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.SyncButton.ForeColor = System.Drawing.Color.White;
            this.SyncButton.Location = new System.Drawing.Point(261, 103);
            this.SyncButton.Name = "SyncButton";
            this.SyncButton.Size = new System.Drawing.Size(81, 29);
            this.SyncButton.TabIndex = 3;
            this.SyncButton.Text = "准备好了";
            this.SyncButton.UseVisualStyleBackColor = false;
            this.SyncButton.Click += new System.EventHandler(this.SyncButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(14, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 21);
            this.label4.TabIndex = 2;
            this.label4.Text = "演奏时间";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "HH-mm-ss";
            this.dateTimePicker1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker1.Location = new System.Drawing.Point(102, 68);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(119, 29);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(14, 45);
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
            this.gBParameterSetting.Controls.Add(radioButton3);
            this.gBParameterSetting.Controls.Add(this.numericUpDown1);
            this.gBParameterSetting.Controls.Add(this.radioButton2);
            this.gBParameterSetting.Controls.Add(this.radioButton1);
            this.gBParameterSetting.Controls.Add(this.label5);
            this.gBParameterSetting.Controls.Add(this.label7);
            this.gBParameterSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.gBParameterSetting.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.gBParameterSetting.ForeColor = System.Drawing.Color.OrangeRed;
            this.gBParameterSetting.Location = new System.Drawing.Point(0, 99);
            this.gBParameterSetting.Name = "gBParameterSetting";
            this.gBParameterSetting.Size = new System.Drawing.Size(351, 87);
            this.gBParameterSetting.TabIndex = 5;
            this.gBParameterSetting.TabStop = false;
            this.gBParameterSetting.Text = "播放属性设置";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.numericUpDown1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.numericUpDown1.Location = new System.Drawing.Point(182, 22);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(154, 29);
            this.numericUpDown1.TabIndex = 9;
            this.numericUpDown1.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.radioButton2.Location = new System.Drawing.Point(188, 57);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(60, 25);
            this.radioButton2.TabIndex = 6;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "原始";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.radioButton1.Location = new System.Drawing.Point(107, 57);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(69, 25);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.Text = "低8度";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(16, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "音高调整";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Gray;
            this.label7.Location = new System.Drawing.Point(16, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(161, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "设置BPM，范围60~180";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("微软雅黑", 9.25F);
            this.linkLabel1.Location = new System.Drawing.Point(280, 370);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(56, 19);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "About...";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // cbMidiKeyboard
            // 
            this.cbMidiKeyboard.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbMidiKeyboard.FormattingEnabled = true;
            this.cbMidiKeyboard.Location = new System.Drawing.Point(87, 24);
            this.cbMidiKeyboard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbMidiKeyboard.Name = "cbMidiKeyboard";
            this.cbMidiKeyboard.Size = new System.Drawing.Size(177, 28);
            this.cbMidiKeyboard.TabIndex = 8;
            this.cbMidiKeyboard.SelectedIndexChanged += new System.EventHandler(this.cbMidiKeyboard_SelectedIndexChanged);
            // 
            // gBKeySetting
            // 
            this.gBKeySetting.Controls.Add(this.btnSwitch);
            this.gBKeySetting.Controls.Add(this.btn22key);
            this.gBKeySetting.Controls.Add(this.btn8key);
            this.gBKeySetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.gBKeySetting.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.gBKeySetting.ForeColor = System.Drawing.Color.OrangeRed;
            this.gBKeySetting.Location = new System.Drawing.Point(0, 186);
            this.gBKeySetting.Name = "gBKeySetting";
            this.gBKeySetting.Size = new System.Drawing.Size(351, 76);
            this.gBKeySetting.TabIndex = 10;
            this.gBKeySetting.TabStop = false;
            this.gBKeySetting.Text = "游戏键位设置";
            // 
            // btnSwitch
            // 
            this.btnSwitch.BackColor = System.Drawing.Color.Transparent;
            this.btnSwitch.BackgroundImage = global::Daigassou.Properties.Resources.a0;
            this.btnSwitch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwitch.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnSwitch.ForeColor = System.Drawing.Color.White;
            this.btnSwitch.Location = new System.Drawing.Point(126, 28);
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
            this.btn22key.Location = new System.Drawing.Point(227, 28);
            this.btn22key.Name = "btn22key";
            this.btn22key.Size = new System.Drawing.Size(95, 36);
            this.btn22key.TabIndex = 5;
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
            this.btn8key.Location = new System.Drawing.Point(16, 28);
            this.btn8key.Name = "btn8key";
            this.btn8key.Size = new System.Drawing.Size(95, 36);
            this.btn8key.TabIndex = 4;
            this.btn8key.UseVisualStyleBackColor = false;
            this.btn8key.Click += new System.EventHandler(this.button4_Click);
            // 
            // gBKeyboardSetting
            // 
            this.gBKeyboardSetting.Controls.Add(this.btnKeyboardConnect);
            this.gBKeyboardSetting.Controls.Add(this.label9);
            this.gBKeyboardSetting.Controls.Add(this.cbMidiKeyboard);
            this.gBKeyboardSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.gBKeyboardSetting.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.gBKeyboardSetting.ForeColor = System.Drawing.Color.OrangeRed;
            this.gBKeyboardSetting.Location = new System.Drawing.Point(0, 262);
            this.gBKeyboardSetting.Name = "gBKeyboardSetting";
            this.gBKeyboardSetting.Size = new System.Drawing.Size(351, 67);
            this.gBKeyboardSetting.TabIndex = 11;
            this.gBKeyboardSetting.TabStop = false;
            this.gBKeyboardSetting.Text = "Midi键盘选择";
            // 
            // btnKeyboardConnect
            // 
            this.btnKeyboardConnect.BackColor = System.Drawing.Color.Transparent;
            this.btnKeyboardConnect.BackgroundImage = global::Daigassou.Properties.Resources.btn1;
            this.btnKeyboardConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnKeyboardConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyboardConnect.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnKeyboardConnect.ForeColor = System.Drawing.Color.White;
            this.btnKeyboardConnect.Location = new System.Drawing.Point(273, 23);
            this.btnKeyboardConnect.Name = "btnKeyboardConnect";
            this.btnKeyboardConnect.Size = new System.Drawing.Size(72, 29);
            this.btnKeyboardConnect.TabIndex = 7;
            this.btnKeyboardConnect.UseVisualStyleBackColor = false;
            this.btnKeyboardConnect.Click += new System.EventHandler(this.btnKeyboardConnect_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.Gray;
            this.label9.Location = new System.Drawing.Point(16, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "键盘选择";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(351, 421);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.gBKeyboardSetting);
            this.Controls.Add(this.gBKeySetting);
            this.Controls.Add(this.gBParameterSetting);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gBMidiFile);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "大合奏！[喵？]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gBMidiFile.ResumeLayout(false);
            this.gBMidiFile.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.gBParameterSetting.ResumeLayout(false);
            this.gBParameterSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.gBKeySetting.ResumeLayout(false);
            this.gBKeyboardSetting.ResumeLayout(false);
            this.gBKeyboardSetting.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox trackComboBox;
        private System.Windows.Forms.GroupBox gBMidiFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button selectFileButton;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog midFileDiag;
        private System.Windows.Forms.Button SyncButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gBParameterSetting;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button btn8key;
        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbMidiKeyboard;
        private System.Windows.Forms.GroupBox gBKeySetting;
        private System.Windows.Forms.GroupBox gBKeyboardSetting;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnKeyboardConnect;
        private System.Windows.Forms.Button btn22key;
        private System.Windows.Forms.Button btnSwitch;
    }
}

