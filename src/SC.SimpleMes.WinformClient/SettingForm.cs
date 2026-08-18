using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SC.SimpleMes.WinformClient.HostObject;
using SC.SimpleMes.WinformClient.Model;

namespace SC.SimpleMes.WinformClient
{
    public partial class SettingForm : Form
    {
        private readonly FolderBrowserDialog folderBrowserDialog;
        public SettingForm()
        {
            InitializeComponent();
            this.folderBrowserDialog = new FolderBrowserDialog();
            this.Load += SettingForm_Load;

        }

        private void SettingForm_Load(object? sender, EventArgs e)
        {
            txtUserDataPath.Text = SettingModel.Instance.UserDataPath;
            cbCleaerCache.Checked = SettingModel.Instance.IsClearCacheWhenQuit;
            cmbStartUrl.SelectedText = SettingModel.Instance.StartPathUrl;
            foreach (var item in PrinterSettings.InstalledPrinters)
            {
                this.cbPrinterList.Items.Add(item);
            }

            this.cbPrinterList.SelectedValue = SettingModel.Instance.PringName;
            this.cmbStartUrl.Items.Clear();
            foreach (var item in SettingModel.Instance.ConfigStartPathUrls)
            {
                this.cmbStartUrl.Items.Add(item);

            }

#if !DEBUG
    this.button1.Visible = false;
#endif
        }

        private void btnSelectFilePath_Click(object sender, EventArgs e)
        {
            var filePath = folderBrowserDialog.ShowDialog(this);
            if (filePath == DialogResult.OK && !string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
            {
                txtUserDataPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //SettingModel.Instance.UserDataPath = txtUserDataPath.Text;
            SettingModel.Instance.IsClearCacheWhenQuit = cbCleaerCache.Checked;
            SettingModel.Instance.SaveInfo();
            this.Close();
        }

        private void cbPrinterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPrinterList.SelectedIndex > 0)
            {
                SettingModel.Instance.PringName = cbPrinterList.SelectedItem.ToString();
            }
        }

        private void cmbStartUrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            SettingModel.Instance.StartPathUrl = cmbStartUrl.SelectedItem.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SVNProcessInfoHelper sVNProcessInfoHelper = new SVNProcessInfoHelper();

            sVNProcessInfoHelper.Add(@"E:\系统部\MES\");
            sVNProcessInfoHelper.Commit("", "测试内容");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenCvVidoeCapture windowCaptur = new OpenCvVidoeCapture();
            windowCaptur.Show();
        }
    }
}
