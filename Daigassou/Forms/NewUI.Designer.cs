using System.Drawing;

namespace Daigassou
{
    partial class NewUI
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
            this.ucPanelTitle1 = new HZH_Controls.Controls.UCPanelTitle();
            this.ucPanelTitle2 = new HZH_Controls.Controls.UCPanelTitle();
            this.SuspendLayout();
            // 
            // ucPanelTitle1
            // 
            this.ucPanelTitle1.BackColor = System.Drawing.Color.Transparent;
            this.ucPanelTitle1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.ucPanelTitle1.ConerRadius = 10;
            this.ucPanelTitle1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucPanelTitle1.FillColor = System.Drawing.Color.White;
            this.ucPanelTitle1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucPanelTitle1.IsCanExpand = true;
            this.ucPanelTitle1.IsExpand = false;
            this.ucPanelTitle1.IsRadius = true;
            this.ucPanelTitle1.IsShowRect = true;
            this.ucPanelTitle1.Location = new System.Drawing.Point(0, 524);
            this.ucPanelTitle1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucPanelTitle1.Name = "ucPanelTitle1";
            this.ucPanelTitle1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.ucPanelTitle1.RectWidth = 1;
            this.ucPanelTitle1.Size = new System.Drawing.Size(654, 171);
            this.ucPanelTitle1.TabIndex = 0;
            this.ucPanelTitle1.Title = "面板";
            // 
            // ucPanelTitle2
            // 
            this.ucPanelTitle2.BackColor = System.Drawing.Color.Transparent;
            this.ucPanelTitle2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.ucPanelTitle2.ConerRadius = 10;
            this.ucPanelTitle2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucPanelTitle2.FillColor = System.Drawing.Color.White;
            this.ucPanelTitle2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucPanelTitle2.IsCanExpand = true;
            this.ucPanelTitle2.IsExpand = false;
            this.ucPanelTitle2.IsRadius = true;
            this.ucPanelTitle2.IsShowRect = true;
            this.ucPanelTitle2.Location = new System.Drawing.Point(0, 353);
            this.ucPanelTitle2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucPanelTitle2.Name = "ucPanelTitle2";
            this.ucPanelTitle2.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.ucPanelTitle2.RectWidth = 1;
            this.ucPanelTitle2.Size = new System.Drawing.Size(654, 171);
            this.ucPanelTitle2.TabIndex = 1;
            this.ucPanelTitle2.Title = "面板";
            this.ucPanelTitle2.Load += new System.EventHandler(this.UcPanelTitle2_Load);
            this.ucPanelTitle2.Click += UcPanelTitle2_Click;
            // 
            // NewUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 695);
            this.Controls.Add(this.ucPanelTitle2);
            this.Controls.Add(this.ucPanelTitle1);
            this.Name = "NewUI";
            this.Text = "NewUI";
            this.ResumeLayout(false);

        }

        private void UcPanelTitle2_Click(object sender, System.EventArgs e)
        {
            ucPanelTitle2.FillColor = Color.AliceBlue;
        }

        #endregion

        private HZH_Controls.Controls.UCPanelTitle ucPanelTitle1;
        private HZH_Controls.Controls.UCPanelTitle ucPanelTitle2;
    }
}