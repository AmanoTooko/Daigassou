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
    public partial class MuiltiPlayForm : UIPage
    {
        public MuiltiPlayForm()
        {
            InitializeComponent();
        }
        public override void Init()
        {
            base.Init();
            uiLine1.ForeColor = Color.FromArgb(255, 113, 128);
            uiLine2.ForeColor = Color.FromArgb(255, 113, 128);
            uiLine3.ForeColor = Color.FromArgb(255, 113, 128);
        }

        public override void Final()
        {
            base.Final();

        }

        private void uiSwitch1_ValueChanged(object sender, bool value)
        {
            uiPanel1.Enabled = false;
        }
    }
}