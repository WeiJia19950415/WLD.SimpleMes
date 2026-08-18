namespace WLD.SimpleMes.WinformClient
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            mesNotify = new NotifyIcon(components);
            ctxShowSetting = new ContextMenuStrip(components);
            settingMenuItem = new ToolStripMenuItem();
            minMenuItem = new ToolStripMenuItem();
            退出ToolStripMenuItem = new ToolStripMenuItem();
            checkUpdateItem = new ToolStripMenuItem();
            qrCodeDetect = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            ctxShowSetting.SuspendLayout();
            SuspendLayout();
            // 
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Dock = DockStyle.Fill;
            webView.Location = new Point(0, 0);
            webView.Name = "webView";
            webView.Size = new Size(800, 450);
            webView.TabIndex = 0;
            webView.ZoomFactor = 1D;
            // 
            // mesNotify
            // 
            mesNotify.ContextMenuStrip = ctxShowSetting;
            mesNotify.Icon = (Icon)resources.GetObject("mesNotify.Icon");
            mesNotify.Text = "V-Liquid";
            mesNotify.Visible = true;
            mesNotify.MouseDoubleClick += mesNotify_MouseDoubleClick;
            // 
            // ctxShowSetting
            // 
            ctxShowSetting.Items.AddRange(new ToolStripItem[] { settingMenuItem, minMenuItem, 退出ToolStripMenuItem, checkUpdateItem, qrCodeDetect });
            ctxShowSetting.Name = "ctxShowSetting";
            ctxShowSetting.Size = new Size(181, 136);
            ctxShowSetting.Text = "设置";
            ctxShowSetting.DoubleClick += ctxShowSetting_DoubleClick;
            // 
            // settingMenuItem
            // 
            settingMenuItem.Name = "settingMenuItem";
            settingMenuItem.Size = new Size(180, 22);
            settingMenuItem.Text = "设置";
            settingMenuItem.Click += settingMenuItem_Click;
            // 
            // minMenuItem
            // 
            minMenuItem.Name = "minMenuItem";
            minMenuItem.Size = new Size(180, 22);
            minMenuItem.Text = "隐藏窗口";
            minMenuItem.Click += minMenuItem_Click;
            // 
            // 退出ToolStripMenuItem
            // 
            退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            退出ToolStripMenuItem.Size = new Size(180, 22);
            退出ToolStripMenuItem.Text = "退出";
            退出ToolStripMenuItem.Click += 退出ToolStripMenuItem_Click;
            // 
            // checkUpdateItem
            // 
            checkUpdateItem.Name = "checkUpdateItem";
            checkUpdateItem.Size = new Size(180, 22);
            checkUpdateItem.Text = "检查版本";
            checkUpdateItem.Click += checkUpdateItem_Click;
            // 
            // qrCodeDetect
            // 
            qrCodeDetect.Name = "qrCodeDetect";
            qrCodeDetect.Size = new Size(180, 22);
            qrCodeDetect.Text = "二维码识别";
            qrCodeDetect.Click += qrCodeDetect_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(webView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "MainForm";
            Text = "全钒液流电池生产执行系统";
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            ctxShowSetting.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private NotifyIcon mesNotify;
        private ContextMenuStrip ctxShowSetting;
        private ToolStripMenuItem settingMenuItem;
        private ToolStripMenuItem minMenuItem;
        private ToolStripMenuItem 退出ToolStripMenuItem;
        private ToolStripMenuItem checkUpdateItem;
        private ToolStripMenuItem qrCodeDetect;
    }
}