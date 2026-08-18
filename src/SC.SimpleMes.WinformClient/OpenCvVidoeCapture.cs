using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace SC.SimpleMes.WinformClient
{
    public partial class OpenCvVidoeCapture : Form
    {
        private readonly VideoCapture capture;
        private readonly BackgroundWorker backgroundWorker1;
        private readonly WeChatQRCode qRCodeDetector;
        const string detector_prototxt_path = "opencvsharp/detect.prototxt";
        const string detector_caffe_model_path = "opencvsharp/detect.caffemodel";
        const string prototxt_path = "opencvsharp/sr.prototxt";
        const string caffe_model_path = "opencvsharp/sr.caffemodel";

        public OpenCvVidoeCapture()
        {
            InitializeComponent();
            qRCodeDetector = OpenCvSharp.WeChatQRCode.Create(detector_prototxt_path, detector_caffe_model_path, prototxt_path, caffe_model_path);

            backgroundWorker1 = new BackgroundWorker() { WorkerReportsProgress = true };
            try
            {
                capture = new VideoCapture("http://192.168.17.11:4747/video");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void OpenCvVidoeCapture_Load(object sender, EventArgs e)
        {
            capture.Open(0, VideoCaptureAPIs.ANY);
            if (!capture.IsOpened())
            {
                Close();
                return;
            }

            ClientSize = new System.Drawing.Size(capture.FrameWidth, capture.FrameHeight);
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            
            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerAsync();
        }

        public class QrDecodeProgressChangedEventArgs
        {
            public Bitmap ImgInfo { get; set; }

            public string[] DecodeReuslt { get; set; }
        }
        private void BackgroundWorker1_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            var frameBitmap = (QrDecodeProgressChangedEventArgs)e.UserState;
            videoShowPicBox.Image?.Dispose();
            videoShowPicBox.Image = frameBitmap.ImgInfo;
            if (frameBitmap.DecodeReuslt != null && frameBitmap.DecodeReuslt.Length > 0)
            {
                richTextBox1.Clear();
                // 调用网页方法赋值控件
                backgroundWorker1.ProgressChanged -= BackgroundWorker1_ProgressChanged;
                richTextBox1.AppendText(string.Join($"{Environment.NewLine}", frameBitmap.DecodeReuslt));
            }
        }

        private void BackgroundWorker1_DoWork(object? sender, DoWorkEventArgs e)
        {
            var bgWorker = (BackgroundWorker)sender;

            while (!bgWorker.CancellationPending)
            {
                using (var frameMat = capture.RetrieveMat())
                {
                    Mat[] mats;
                    Point2f[] point2F;
                    string[] results;
                    qRCodeDetector.DetectAndDecode(frameMat, out mats, out results);

                    if (mats != null && mats.Length > 0)
                    {
                        foreach (var item in mats)
                        {
                            Cv2.Rectangle(frameMat, item.BoundingRect(), Scalar.Blue);
                        }
                    }

                    var frameBitmap = BitmapConverter.ToBitmap(frameMat);
                    bgWorker.ReportProgress(0, new QrDecodeProgressChangedEventArgs
                    {
                        ImgInfo = frameBitmap,
                        DecodeReuslt = results
                    });
                }

                Thread.Sleep(100);
            }
        }

        private void VideoCaptureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            backgroundWorker1.CancelAsync();
            capture.Dispose();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
        }
    }
}
