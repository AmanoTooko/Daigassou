// Decompiled with JetBrains decompiler
// Type: BondTech.HotkeyManagement.Win.HotKeyControl
// Assembly: HotkeyManagement, Version=1.7.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C3A4DAEA-C646-4A6B-8B8A-1058CD30A130
// Assembly location: D:\dev\Daigassou\packages\HotkeyManagement.WinForms.1.7.0\lib\HotkeyManagement.dll

using BondTech.HotkeyManagement.Win.Properties;
using BondTech.HotkeyManagement.Win;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Daigassou
{
    [DefaultProperty("ForceModifiers")]
    [ToolboxBitmap(typeof(HotKeyControl), "HotKeyControl.png")]
    [DefaultEvent("HotKeyIsSet")]
    public class HotKeyControl : UserControl
    {
        private IContainer components = (IContainer)null;
        private bool forcemodifier = true;
        private Button ResetButton;
        private TextBox TextBox;
        private System.Windows.Forms.ToolTip ToolTipProvider;
        private bool KeyisSet;
        private string tooltip;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = (IContainer)new Container();
            this.ResetButton = new Button();
            this.TextBox = new TextBox();
            this.ToolTipProvider = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            this.ResetButton.BackColor = Color.Transparent;
            this.ResetButton.Dock = DockStyle.Right;
            this.ResetButton.FlatAppearance.BorderSize = 0;
            this.ResetButton.FlatStyle = FlatStyle.Flat;
            this.ResetButton.BackgroundImage = global::HotkeyControl.Properties.Resources.icons8_reset_64;
            this.ResetButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ResetButton.Location = new Point(227, 0);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new Size(28, 26);
            this.ResetButton.TabIndex = 0;
            this.ResetButton.TabStop = false;
            this.ToolTipProvider.SetToolTip((Control)this.ResetButton, "重置快捷键");
            this.ResetButton.UseVisualStyleBackColor = false;
            this.ResetButton.Visible = false;
            this.ResetButton.Click += new EventHandler(this.ResetButton_Click);
            this.TextBox.Dock = DockStyle.Fill;
            this.TextBox.Location = new Point(0, 0);
            this.TextBox.Name = "TextBox";
            this.TextBox.ShortcutsEnabled = false;
            this.TextBox.Size = new Size(227, 23);
            this.TextBox.TabIndex = 0;
            this.TextBox.TextChanged += new EventHandler(this.TextBox_TextChanged);
            this.TextBox.KeyDown += new KeyEventHandler(this.TextBox_KeyDown);
            this.TextBox.KeyUp += new KeyEventHandler(this.TextBox_KeyUp);
            this.TextBox.Leave += new EventHandler(this.TextBox_Leave);
            this.AutoScaleDimensions = new SizeF(7f, 16f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add((Control)this.TextBox);
            this.Controls.Add((Control)this.ResetButton);
            this.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.Margin = new Padding(3, 4, 3, 4);
            this.Name = nameof(HotKeyControl);
            this.Size = new Size((int)byte.MaxValue, 26);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        [DllImport("user32.dll", EntryPoint = "SendMessageW")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        [DefaultValue(true)]
        [Description("Specifies that the control should force the user to use a modifier.")]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool ForceModifiers
        {
            get
            {
                return this.forcemodifier;
            }
            set
            {
                this.forcemodifier = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get
            {
                return this.TextBox.Text;
            }
            set
            {
                this.TextBox.Text = value;
            }
        }

        [Browsable(false)]
        public Keys UserKey
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Text) && this.Text != Keys.None.ToString())
                    return (Keys)HotKeyShared.ParseShortcut(this.Text).GetValue(1);
                return Keys.None;
            }
        }

        [Browsable(false)]
        public Modifiers UserModifier
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Text) && this.Text != Keys.None.ToString())
                    return (Modifiers)HotKeyShared.ParseShortcut(this.Text).GetValue(0);
                return Modifiers.None;
            }
        }

        public string ToolTip
        {
            get
            {
                return this.tooltip;
            }
            set
            {
                this.ToolTipProvider.SetToolTip((Control)this.TextBox, value);
                this.tooltip = value;
            }
        }

        [Description("Raised when a valid key is set")]
        public event HotKeyIsSetEventHandler HotKeyIsSet;
        [Description("Raised when a key is reset")]
        public event EventHandler HotKeyIsReset;
        public HotKeyControl()
        {
            this.InitializeComponent();
        }

        private void updateWatermark()
        {
            if (!this.IsHandleCreated)
                return;
            IntPtr hglobalUni = Marshal.StringToHGlobalUni("在此输入快捷键");
            HotKeyControl.SendMessage(this.TextBox.Handle, 5377, (IntPtr)1, hglobalUni);
            Marshal.FreeHGlobal(hglobalUni);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.updateWatermark();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (this.Height == this.TextBox.Height)
                return;
            this.Height = this.TextBox.Height;
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            if (!this.Text.Trim().EndsWith("+"))
                return;
            this.Text = string.Empty;
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (!this.KeyisSet)
                this.Text = string.Empty;
            else if (this.HotKeyIsSet != null)
            {
                HotKeyIsSetEventArgs e1 = new HotKeyIsSetEventArgs(this.UserKey, this.UserModifier);
                this.HotKeyIsSet((object)this, e1);
                if (e1.Cancel)
                {
                    this.KeyisSet = false;
                    this.Text = string.Empty;
                }
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            this.Text = string.Empty;
            this.KeyisSet = false;
            if (e.Modifiers == Keys.None && this.forcemodifier)
            {
                int num = (int)MessageBox.Show("快捷键必须包含Ctrl或者Alt或Shift");
                this.Text = string.Empty;
            }
            else
            {
                string str1 = e.Modifiers.ToString();
                char[] chArray = new char[1] { ',' };
                foreach (string str2 in str1.Split(chArray))
                {
                    if (str2 != Keys.None.ToString())
                    {
                        HotKeyControl hotKeyControl = this;
                        hotKeyControl.Text = hotKeyControl.Text + str2 + " + ";
                    }
                }
                if (e.KeyCode == Keys.ShiftKey | e.KeyCode == Keys.ControlKey | e.KeyCode == Keys.Menu)
                {
                    this.KeyisSet = false;
                }
                else
                {
                    this.Text += e.KeyCode.ToString();
                    this.KeyisSet = true;
                }
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.Text == string.Empty)
                this.ResetButton.Visible = false;
            else
                this.ResetButton.Visible = true;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            if (this.HotKeyIsSet != null)
                this.HotKeyIsReset((object)this, new EventArgs());
            this.TextBox.Text = string.Empty;
        }
    }
}
