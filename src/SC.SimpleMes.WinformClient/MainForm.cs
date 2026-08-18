using AutoUpdaterDotNET;
using Microsoft.Web.WebView2.Core;
using SC.SimpleMes.WinformClient.HostObject;
using SC.SimpleMes.WinformClient.Model;

namespace SC.SimpleMes.WinformClient
{
    public partial class MainForm : Form
    {




        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.FormClosing += MainForm_FormClosing;
            foreach (ToolStripItem item in this.ctxShowSetting.Items)
            {
#if !DEBUG
                if (string.Equals(item.Text, "二维码识别"))
                {
                    item.Visible = false;
                }
#endif
            }

        }

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.minMenuItem.Text = "显示窗口";
#if !DEBUG
            e.Cancel = true;
#endif


        }

        private async void Form1_Load(object? sender, EventArgs e)
        {
            webView.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted;
            webView.CreationProperties = new Microsoft.Web.WebView2.WinForms.CoreWebView2CreationProperties()
            {
                UserDataFolder = SettingModel.Instance.UserDataPath,
                Language = "Zh-Cn",
                // BrowserExecutableFolder=Settmo "", //指定特定版本
            };
            await webView.EnsureCoreWebView2Async();
            InitWebView();
            webView.CoreWebView2.Navigate(SettingModel.Instance.ServerDomain + SettingModel.Instance.StartPathUrl);
            webView.CoreWebView2.AddHostObjectToScript("TscHostObject", new TSCHostObject());
        }

        private void InitWebView()
        {
            webView.CoreWebView2.ContextMenuRequested += CoreWebView2_ContextMenuRequested;
        }


        #region webView2 菜单配置

        private void CoreWebView2_ContextMenuRequested(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2ContextMenuRequestedEventArgs e)
        {
            e.MenuItems.Clear();
            var reloadItem = webView.CoreWebView2.Environment.CreateContextMenuItem("刷新", null, CoreWebView2ContextMenuItemKind.Command);
            reloadItem.CustomItemSelected += ReloadItem_CustomItemSelected;
            e.MenuItems.Add(reloadItem);
            var backItem = webView.CoreWebView2.Environment.CreateContextMenuItem("返回", null, CoreWebView2ContextMenuItemKind.Command);
            backItem.CustomItemSelected += BackItem_CustomItemSelected;
            e.MenuItems.Add(backItem);
            var checkItem = webView.CoreWebView2.Environment.CreateContextMenuItem("检查", null, CoreWebView2ContextMenuItemKind.Command);
            checkItem.CustomItemSelected += CheckItem_CustomItemSelected; ;
            e.MenuItems.Add(checkItem);

            var settingItem = webView.CoreWebView2.Environment.CreateContextMenuItem("设置", null, CoreWebView2ContextMenuItemKind.Command);
            settingItem.CustomItemSelected += SettingItem_CustomItemSelected;
            e.MenuItems.Add(settingItem);
        }

        private void SettingItem_CustomItemSelected(object? sender, object e)
        {
            this.TopMost = false;
            new SettingForm().ShowDialog();
        }

        private void CheckItem_CustomItemSelected(object? sender, object e)
        {
            webView.CoreWebView2.OpenDevToolsWindow();
            this.TopMost = false;
        }

        private void BackItem_CustomItemSelected(object? sender, object e)
        {
            webView.CoreWebView2.GoBack();
        }

        private void ReloadItem_CustomItemSelected(object? sender, object e)
        {
            webView.CoreWebView2.Reload();
        }

        #endregion

        private void WebView_CoreWebView2InitializationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {

        }

        private void settingMenuItem_Click(object sender, EventArgs e)
        {
            SettingForm settingForm = new SettingForm();
            settingForm.ShowDialog();
        }

        private void minMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.minMenuItem.Text = "显示窗口";
                this.Hide(); ;
            }
            else
            {
                this.minMenuItem.Text = "隐藏窗口";
                this.Show();
                this.BringToFront();
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(-1);
        }

        private void checkUpdateItem_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                AutoUpdater.Start(SettingModel.Instance.ServerVersionInfoPath);
            });
        }

        private void ctxShowSetting_DoubleClick(object sender, EventArgs e)
        {
            Program.winformApplication.ShowMainForm();
        }

        private void mesNotify_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Program.winformApplication.ShowMainForm();
        }

        private void qrCodeDetect_Click(object sender, EventArgs e)
        {
            OpenCvVidoeCapture openCvVidoeCapture = new OpenCvVidoeCapture();
            openCvVidoeCapture.Show();
        }
    }
}