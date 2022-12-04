namespace Daigassou.Forms
{
    partial class MainFormEx
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFormEx));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("独奏", 0, 1);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("合奏", 2, 3);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("外接", 4, 5);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("设置", 6, 7);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("试听", 8, 9);
            this.uiStyleManager1 = new Sunny.UI.UIStyleManager(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.uiNavMenu1 = new Sunny.UI.UINavMenu();
            this.uiTabControl1 = new Sunny.UI.UITabControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiStyleManager1
            // 
            this.uiStyleManager1.DPIScale = true;
            this.uiStyleManager1.Style = Sunny.UI.UIStyle.Colorful;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ico_dz.png");
            this.imageList1.Images.SetKeyName(1, "ico_dz_select - light.png");
            this.imageList1.Images.SetKeyName(2, "ico_hz.png");
            this.imageList1.Images.SetKeyName(3, "ico_hz_select - light.png");
            this.imageList1.Images.SetKeyName(4, "ico_wj.png");
            this.imageList1.Images.SetKeyName(5, "ico_wj_select - light.png");
            this.imageList1.Images.SetKeyName(6, "ico_sz.png");
            this.imageList1.Images.SetKeyName(7, "ico_sz_select - light.png");
            this.imageList1.Images.SetKeyName(8, "icons8-ヘッドフォン-48-grey.png");
            this.imageList1.Images.SetKeyName(9, "icons8-ヘッドフォン-48.png");
            // 
            // uiNavMenu1
            // 
            this.uiNavMenu1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.uiNavMenu1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.uiNavMenu1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiNavMenu1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.uiNavMenu1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(241)))), ((int)(((byte)(242)))));
            this.uiNavMenu1.Font = new System.Drawing.Font("微软雅黑", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiNavMenu1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiNavMenu1.FullRowSelect = true;
            this.uiNavMenu1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.uiNavMenu1.ImageIndex = 0;
            this.uiNavMenu1.ImageList = this.imageList1;
            this.uiNavMenu1.ItemHeight = 70;
            this.uiNavMenu1.Location = new System.Drawing.Point(0, 36);
            this.uiNavMenu1.Margin = new System.Windows.Forms.Padding(0);
            this.uiNavMenu1.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.uiNavMenu1.Name = "uiNavMenu1";
            treeNode1.ImageIndex = 0;
            treeNode1.Name = "节点0";
            treeNode1.SelectedImageIndex = 1;
            treeNode1.Text = "独奏";
            treeNode2.ImageIndex = 2;
            treeNode2.Name = "节点1";
            treeNode2.SelectedImageIndex = 3;
            treeNode2.Text = "合奏";
            treeNode3.ImageIndex = 4;
            treeNode3.Name = "节点2";
            treeNode3.SelectedImageIndex = 5;
            treeNode3.Text = "外接";
            treeNode4.ImageIndex = 6;
            treeNode4.Name = "节点3";
            treeNode4.SelectedImageIndex = 7;
            treeNode4.Text = "设置";
            treeNode5.ImageIndex = 8;
            treeNode5.Name = "节点4";
            treeNode5.SelectedImageIndex = 9;
            treeNode5.Text = "试听";
            this.uiNavMenu1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            this.uiNavMenu1.ScrollBarColor = System.Drawing.Color.White;
            this.uiNavMenu1.ScrollBarHoverColor = System.Drawing.Color.White;
            this.uiNavMenu1.ScrollBarPressColor = System.Drawing.Color.White;
            this.uiNavMenu1.ScrollFillColor = System.Drawing.Color.White;
            this.uiNavMenu1.SecondBackColor = System.Drawing.Color.White;
            this.uiNavMenu1.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(248)))), ((int)(((byte)(249)))));
            this.uiNavMenu1.SelectedColor2 = System.Drawing.Color.White;
            this.uiNavMenu1.SelectedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.uiNavMenu1.SelectedHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.uiNavMenu1.SelectedImageIndex = 0;
            this.uiNavMenu1.ShowLines = false;
            this.uiNavMenu1.Size = new System.Drawing.Size(164, 351);
            this.uiNavMenu1.Style = Sunny.UI.UIStyle.Colorful;
            this.uiNavMenu1.TabControl = this.uiTabControl1;
            this.uiNavMenu1.TabIndex = 4;
            this.uiNavMenu1.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiNavMenu1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiNavMenu1.MenuItemClick += new Sunny.UI.UINavMenu.OnMenuItemClick(this.uiNavMenu1_MenuItemClick);
            // 
            // uiTabControl1
            // 
            this.uiTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.uiTabControl1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.uiTabControl1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTabControl1.Frame = null;
            this.uiTabControl1.ItemSize = new System.Drawing.Size(0, 1);
            this.uiTabControl1.Location = new System.Drawing.Point(164, 36);
            this.uiTabControl1.MainPage = "";
            this.uiTabControl1.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.uiTabControl1.Name = "uiTabControl1";
            this.uiTabControl1.SelectedIndex = 0;
            this.uiTabControl1.Size = new System.Drawing.Size(510, 351);
            this.uiTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.uiTabControl1.Style = Sunny.UI.UIStyle.Colorful;
            this.uiTabControl1.TabIndex = 5;
            this.uiTabControl1.TabSelectedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.uiTabControl1.TabSelectedHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.uiTabControl1.TabVisible = false;
            this.uiTabControl1.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTabControl1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(241)))), ((int)(((byte)(242)))));
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 387);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(674, 26);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(64, 20);
            this.toolStripStatusLabel1.Text = "Version";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(313, 20);
            this.toolStripStatusLabel2.Text = "开源免费，请勿购买。|请勿修改程序文件名！";
            // 
            // MainFormEx
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(674, 413);
            this.ControlBoxFillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.Controls.Add(this.uiTabControl1);
            this.Controls.Add(this.uiNavMenu1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainFormEx";
            this.Padding = new System.Windows.Forms.Padding(0, 36, 0, 0);
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.ShowTitleIcon = true;
            this.Style = Sunny.UI.UIStyle.Colorful;
            this.Text = "大合奏!Ex";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.TitleFont = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 800, 450);
            this.ReceiveParams += new Sunny.UI.OnReceiveParams(this.MainFormEx_ReceiveParams);
            this.Load += new System.EventHandler(this.MainFormEx_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Sunny.UI.UIStyleManager uiStyleManager1;
        private System.Windows.Forms.ImageList imageList1;
        private Sunny.UI.UINavMenu uiNavMenu1;
        private Sunny.UI.UITabControl uiTabControl1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    }
}