namespace MidiKeyBoardTest
{
    partial class 一个测试midi键盘连接的小工具
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(一个测试midi键盘连接的小工具));
            this.btnKeyboardConnect = new System.Windows.Forms.Button();
            this.cbMidiKeyboard = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnKeyboardConnect
            // 
            this.btnKeyboardConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnKeyboardConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnKeyboardConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyboardConnect.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnKeyboardConnect.ForeColor = System.Drawing.Color.Black;
            this.btnKeyboardConnect.Location = new System.Drawing.Point(212, 23);
            this.btnKeyboardConnect.Name = "btnKeyboardConnect";
            this.btnKeyboardConnect.Size = new System.Drawing.Size(72, 29);
            this.btnKeyboardConnect.TabIndex = 9;
            this.btnKeyboardConnect.Text = "连接";
            this.btnKeyboardConnect.UseVisualStyleBackColor = false;
            this.btnKeyboardConnect.Click += new System.EventHandler(this.btnKeyboardConnect_Click);
            // 
            // cbMidiKeyboard
            // 
            this.cbMidiKeyboard.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbMidiKeyboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.cbMidiKeyboard.FormattingEnabled = true;
            this.cbMidiKeyboard.Location = new System.Drawing.Point(26, 24);
            this.cbMidiKeyboard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbMidiKeyboard.Name = "cbMidiKeyboard";
            this.cbMidiKeyboard.Size = new System.Drawing.Size(177, 28);
            this.cbMidiKeyboard.TabIndex = 10;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 76);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(640, 403);
            this.richTextBox1.TabIndex = 11;
            this.richTextBox1.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbMidiKeyboard);
            this.panel1.Controls.Add(this.btnKeyboardConnect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 76);
            this.panel1.TabIndex = 12;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // 一个测试midi键盘连接的小工具
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 479);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "一个测试midi键盘连接的小工具";
            this.Text = "一个测试midi键盘连接的小工具";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnKeyboardConnect;
        private System.Windows.Forms.ComboBox cbMidiKeyboard;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
    }
}

