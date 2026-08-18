namespace WLD.SimpleMes.WinformClient
{
    partial class SettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            label1 = new Label();
            label2 = new Label();
            txtUserDataPath = new TextBox();
            btnSelectFilePath = new Button();
            cmbStartUrl = new ComboBox();
            cbCleaerCache = new CheckBox();
            btnSave = new Button();
            btnCancel = new Button();
            label3 = new Label();
            cbPrinterList = new ComboBox();
            label4 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 35);
            label1.Name = "label1";
            label1.Size = new Size(80, 17);
            label1.TabIndex = 0;
            label1.Text = "用户文件路径";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 120);
            label2.Name = "label2";
            label2.Size = new Size(52, 17);
            label2.TabIndex = 1;
            label2.Text = "启 动 页";
            // 
            // txtUserDataPath
            // 
            txtUserDataPath.Location = new Point(98, 32);
            txtUserDataPath.Name = "txtUserDataPath";
            txtUserDataPath.Size = new Size(228, 23);
            txtUserDataPath.TabIndex = 3;
            // 
            // btnSelectFilePath
            // 
            btnSelectFilePath.Location = new Point(332, 32);
            btnSelectFilePath.Name = "btnSelectFilePath";
            btnSelectFilePath.Size = new Size(71, 23);
            btnSelectFilePath.TabIndex = 4;
            btnSelectFilePath.Text = "选择文件夹";
            btnSelectFilePath.UseVisualStyleBackColor = true;
            btnSelectFilePath.Click += btnSelectFilePath_Click;
            // 
            // cmbStartUrl
            // 
            cmbStartUrl.FormattingEnabled = true;
            cmbStartUrl.Items.AddRange(new object[] { "http://localhost:8080/#", "https://192.168.1.37:8015", "https://192.168.1.37:8015/index.html#/erpInStock", "https://192.168.1.37:8015/index.html#/printProductBn" });
            cmbStartUrl.Location = new Point(98, 117);
            cmbStartUrl.Name = "cmbStartUrl";
            cmbStartUrl.Size = new Size(305, 25);
            cmbStartUrl.TabIndex = 5;
            cmbStartUrl.SelectedIndexChanged += cmbStartUrl_SelectedIndexChanged;
            // 
            // cbCleaerCache
            // 
            cbCleaerCache.AutoSize = true;
            cbCleaerCache.Location = new Point(12, 214);
            cbCleaerCache.Name = "cbCleaerCache";
            cbCleaerCache.Size = new Size(111, 21);
            cbCleaerCache.TabIndex = 6;
            cbCleaerCache.Text = "启动时清空缓存";
            cbCleaerCache.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(98, 252);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 7;
            btnSave.Text = "保存";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(201, 252);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 169);
            label3.Name = "label3";
            label3.Size = new Size(68, 17);
            label3.TabIndex = 9;
            label3.Text = "默认打印机";
            // 
            // cbPrinterList
            // 
            cbPrinterList.FormattingEnabled = true;
            cbPrinterList.Location = new Point(97, 166);
            cbPrinterList.Name = "cbPrinterList";
            cbPrinterList.Size = new Size(306, 25);
            cbPrinterList.TabIndex = 10;
            cbPrinterList.SelectedIndexChanged += cbPrinterList_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 82);
            label4.Name = "label4";
            label4.Size = new Size(68, 17);
            label4.TabIndex = 11;
            label4.Text = "服务器地址";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(96, 77);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(307, 23);
            textBox1.TabIndex = 12;
            // 
            // button1
            // 
            button1.Location = new Point(302, 252);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 13;
            button1.Text = "测试SVN";
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            button1.Click += button1_Click;
            // 
            // SettingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(426, 287);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label4);
            Controls.Add(cbPrinterList);
            Controls.Add(label3);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(cbCleaerCache);
            Controls.Add(cmbStartUrl);
            Controls.Add(btnSelectFilePath);
            Controls.Add(txtUserDataPath);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SettingForm";
            Text = "系统设置";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtUserDataPath;
        private Button btnSelectFilePath;
        private ComboBox cmbStartUrl;
        private CheckBox cbCleaerCache;
        private Button btnSave;
        private Button btnCancel;
        private Label label3;
        private ComboBox cbPrinterList;
        private Label label4;
        private TextBox textBox1;
        private Button button1;
    }
}