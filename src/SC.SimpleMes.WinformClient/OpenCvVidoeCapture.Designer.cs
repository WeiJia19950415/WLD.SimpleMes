namespace SC.SimpleMes.WinformClient
{
    partial class OpenCvVidoeCapture
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
            btnCapture = new Button();
            videoShowPicBox = new PictureBox();
            button1 = new Button();
            lblResult = new Label();
            richTextBox1 = new RichTextBox();
            comboBox1 = new ComboBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)videoShowPicBox).BeginInit();
            SuspendLayout();
            // 
            // btnCapture
            // 
            btnCapture.Location = new Point(528, 22);
            btnCapture.Name = "btnCapture";
            btnCapture.Size = new Size(75, 23);
            btnCapture.TabIndex = 0;
            btnCapture.Text = "测试截屏";
            btnCapture.UseVisualStyleBackColor = true;
            btnCapture.Click += btnCapture_Click;
            // 
            // videoShowPicBox
            // 
            videoShowPicBox.BackColor = SystemColors.ActiveCaptionText;
            videoShowPicBox.Dock = DockStyle.Left;
            videoShowPicBox.Location = new Point(0, 0);
            videoShowPicBox.Name = "videoShowPicBox";
            videoShowPicBox.Size = new Size(522, 450);
            videoShowPicBox.TabIndex = 1;
            videoShowPicBox.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(609, 22);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Location = new Point(528, 126);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(56, 17);
            lblResult.TabIndex = 3;
            lblResult.Text = "识别结果";
            // 
            // richTextBox1
            // 
            richTextBox1.Dock = DockStyle.Bottom;
            richTextBox1.Location = new Point(522, 146);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(323, 304);
            richTextBox1.TabIndex = 4;
            richTextBox1.Text = "";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(609, 60);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(208, 25);
            comboBox1.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(528, 63);
            label1.Name = "label1";
            label1.Size = new Size(68, 17);
            label1.TabIndex = 6;
            label1.Text = "摄像头名称";
            // 
            // OpenCvVidoeCapture
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(845, 450);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(richTextBox1);
            Controls.Add(lblResult);
            Controls.Add(button1);
            Controls.Add(videoShowPicBox);
            Controls.Add(btnCapture);
            Name = "OpenCvVidoeCapture";
            Text = "OpenCvVidoeCapture";
            Load += OpenCvVidoeCapture_Load;
            ((System.ComponentModel.ISupportInitialize)videoShowPicBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCapture;
        private PictureBox videoShowPicBox;
        private Button button1;
        private Label lblResult;
        private RichTextBox richTextBox1;
        private ComboBox comboBox1;
        private Label label1;
    }
}