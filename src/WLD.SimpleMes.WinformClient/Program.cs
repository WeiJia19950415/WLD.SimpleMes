using AutoUpdaterDotNET;
using Microsoft.VisualBasic.ApplicationServices;
using WLD.SimpleMes.WinformClient.Model;

namespace WLD.SimpleMes.WinformClient
{
    public class WinformApplication : WindowsFormsApplicationBase
    {
        public WinformApplication()
        {
            IsSingleInstance = true;
        }

        protected override void OnCreateMainForm()
        {
            MainForm = new MainForm();
        }

        protected override void OnStartupNextInstance(StartupNextInstanceEventArgs eventArgs)
        {

            this.MainForm.Show();
        }

        protected override bool OnStartup(StartupEventArgs eventArgs)
        {
            return base.OnStartup(eventArgs);
        }

        public void ShowMainForm()
        {
            this.MainForm.BringToFront();
            this.MainForm.Show();
        }
    }


    internal static class Program
    {
        public static WinformApplication winformApplication;
        static System.Windows.Forms.Timer checkUpdateTimer = null;
        static NLog.Logger logger;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.ThreadException += Application_ThreadException;
            logger = NLog.LogManager.GetCurrentClassLogger();

            CheckSeting();

            CheckUpdate();

            winformApplication = new WinformApplication();
            winformApplication.UnhandledException += WinformApplication_UnhandledException;
            winformApplication.Shutdown += WinformApplication_Shutdown;
            winformApplication.Run(Environment.GetCommandLineArgs());

        }

        private static void WinformApplication_Shutdown(object sender, EventArgs e)
        {
            if (checkUpdateTimer != null)
            {
                checkUpdateTimer.Stop();
                checkUpdateTimer = null;
            }
        }

        private static void WinformApplication_UnhandledException(object sender, Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs e)
        {
            e.ExitApplication = false;
            logger.Warn(e.Exception as Exception, $"未处理异常");
        }

        private static void CheckUpdate()
        {
            if (SettingModel.Instance.StartCheckUpdate)
            {
                AutoUpdater.RunUpdateAsAdmin = true;
                AutoUpdater.ReportErrors = false;

                AutoUpdater.Start(SettingModel.Instance.ServerVersionInfoPath);
            }

            if (SettingModel.Instance.TimlyCheckUpdateFrequency > 0)
            {
                checkUpdateTimer = new System.Windows.Forms.Timer();
                checkUpdateTimer.Interval = SettingModel.Instance.TimlyCheckUpdateFrequency * 60 * 1000;
                checkUpdateTimer.Tick += Timer_Tick;
                checkUpdateTimer.Start();
            }
        }

        private static void CheckSeting()
        {
            if (string.IsNullOrEmpty(SettingModel.Instance.UserDataPath))
            {
                new SettingForm().ShowDialog();
            }

            if (SettingModel.Instance.IsClearCacheWhenQuit && !string.IsNullOrEmpty(SettingModel.Instance.UserDataPath))
            {
                if (Directory.Exists(SettingModel.Instance.UserDataPath))
                {
                    System.IO.Directory.Delete(SettingModel.Instance.UserDataPath, true);
                }

                Directory.CreateDirectory(SettingModel.Instance.UserDataPath);
            }
        }


        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            logger.Warn(e.Exception as Exception, $"未处理异常{e.Exception.Message}");
        }

        private static void Timer_Tick(object? sender, EventArgs e)
        {
            AutoUpdater.Start(SettingModel.Instance.ServerVersionInfoPath);
        }
    }
}