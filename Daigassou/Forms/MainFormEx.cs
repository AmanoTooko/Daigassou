﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;


namespace Daigassou.Forms
{
    public enum PageID:int
    {
     SoloPlayPage =1001,
     MultiPlayPage=1002,
     DevicePlayPage=1003,
     SettingPage=2001,
     KeyConfigPage=3001,



    }
    public partial class MainFormEx : UIForm
    {
        public MainFormEx()
        {
            InitializeComponent();
            //uiTabControl1.TabPages[0].Controls.Add(new PlayForm());
            var a = new PlayPage();
            
            AddPage(new PlayPage(), (int)PageID.SoloPlayPage);
            AddPage(new SettingPage(), (int) PageID.SettingPage);
            
            UIStyles.InitColorful(Color.FromArgb(255, 141, 155), Color.White);
            uiNavMenu1.SetNodePageIndex(uiNavMenu1.Nodes[0], (int)PageID.SoloPlayPage);
            uiNavMenu1.SetNodePageIndex(uiNavMenu1.Nodes[3], (int)PageID.SettingPage);

        }

        private void uiNavMenu1_MenuItemClick(TreeNode node, NavMenuItem item, int pageIndex)
        {
            //this.SendParamToPage(1002, "123");
            SelectPage(pageIndex);
        }
    }
}
