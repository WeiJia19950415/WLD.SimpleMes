using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.WinformClient.Model
{
    public class SettingModel
    {
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private SettingModel()
        {
            FixedVersionPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FixedWebView2");
            if (Environment.Is64BitProcess)
            {
                InstallPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MicrosoftEdgeWebView2RuntimeInstallerX64.exe");
            }
            else
            {
                InstallPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MicrosoftEdgeWebView2RuntimeInstallerX86.exe");
            }

        }

        private static SettingModel _instance;
        public static SettingModel Instance { get { return _instance; } }
        static SettingModel()
        {
            _instance = LoadFromConfig();
        }

        public const string WebView2RuntimeDownLoadPath = "https://developer.microsoft.com/zh-cn/microsoft-edge/webview2/";

        /// <summary>
        /// https://developer.microsoft.com/zh-cn/microsoft-edge/webview2/
        /// 下载后安装的浏览器位置
        /// </summary>
        public string FixedVersionPath { get; set; }

        /// <summary>
        ///  长青版安装软件路径
        /// </summary>
        public string InstallPath { get; set; }

        /// <summary>
        /// 用户数据路径
        /// </summary>
        public string UserDataPath
        {
            get
            {
                return  $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SimplMesEdge\\Profile";
            }
        }

        /// <summary>
        /// 退出时是否清理缓存
        /// </summary>
        public bool IsClearCacheWhenQuit { get; set; }

        /// <summary>
        /// 启动地址
        /// </summary>
        public string StartPathUrl { get; set; }

        public List<string> ConfigStartPathUrls { get; set; }

        /// <summary>
        /// 是否使用固定版本
        /// </summary>
        public bool IsUseFixedVersion { get; set; }

        /// <summary>
        /// 客户端版本信息文件
        /// </summary>
        public string ServerVersionInfoPath { get; set; }

        /// <summary>
        /// 开机检查版本
        /// </summary>
        public bool StartCheckUpdate { get; set; }

        #region 打印机信息
        /// <summary>
        /// 打印机连接
        /// </summary>
        public string PringName { get; set; }

        /// <summary>
        /// 标签纸宽度
        /// </summary>
        public string LableWidth { get; set; }


        /// <summary>
        /// 标签纸宽度
        /// </summary>
        public string LableHeight { get; set; }


        /// <summary>
        /// 标签纸宽度
        /// </summary>
        public string PrintType { get; set; }
        #endregion


        /// <summary>
        /// 定时检查更新(分钟)
        /// </summary>
        public int TimlyCheckUpdateFrequency { get; set; }

        public string ServerDomain { get; set; }

        private static SettingModel LoadFromConfig()
        {
            SettingModel settingModel = new SettingModel();
            var keysInfo = ConfigurationManager.AppSettings.AllKeys;
            foreach (var key in keysInfo)
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings[key]))
                {
                    continue;
                }

                if (string.Equals(key, "FixedVersionPath"))
                {
                    settingModel.FixedVersionPath = ConfigurationManager.AppSettings[key];
                }

                if (string.Equals(key, "InstallPath"))
                {
                    settingModel.InstallPath = ConfigurationManager.AppSettings[key];
                }

                //if (string.Equals(key, "UserDataPath"))
                //{
                //    settingModel.UserDataPath = ConfigurationManager.AppSettings[key];
                //}

                if (string.Equals(key, "IsClearCacheWhenQuit") && string.Equals(ConfigurationManager.AppSettings[key], bool.TrueString))
                {
                    settingModel.IsClearCacheWhenQuit = true;
                }

                if (string.Equals(key, "IsUseFixedVersion") && string.Equals(ConfigurationManager.AppSettings[key], bool.TrueString))
                {
                    settingModel.IsUseFixedVersion = true;
                }

                if (string.Equals(key, "StartCheckUpdate") && string.Equals(ConfigurationManager.AppSettings[key], bool.TrueString))
                {
                    settingModel.StartCheckUpdate = true;
                }

                if (string.Equals(key, "StartPathUrl"))
                {
                    settingModel.StartPathUrl = ConfigurationManager.AppSettings[key];
                }

                if (string.Equals(key, "ServerVersionInfoPath"))
                {
                    settingModel.ServerVersionInfoPath = ConfigurationManager.AppSettings[key];
                }

                if (string.Equals(key, "TimlyCheckUpdateFrequency"))
                {
                    settingModel.TimlyCheckUpdateFrequency = int.Parse(ConfigurationManager.AppSettings[key]);
                }

                if (string.Equals(key, "PringName"))
                {
                    settingModel.PringName = ConfigurationManager.AppSettings[key];
                }

                if (string.Equals(key, "LableWidth"))
                {
                    settingModel.LableWidth = ConfigurationManager.AppSettings[key];
                }

                if (string.Equals(key, "LableHeight"))
                {
                    settingModel.LableHeight = ConfigurationManager.AppSettings[key];
                }

                if (string.Equals(key, "PrintType"))
                {
                    settingModel.PrintType = ConfigurationManager.AppSettings[key];
                }

                if (string.Equals(key, "StartPathUrl"))
                {
                    settingModel.StartPathUrl = ConfigurationManager.AppSettings[key];
                }

                if (string.Equals(key, "PringName"))
                {
                    settingModel.PringName = ConfigurationManager.AppSettings[key];
                }

                if (string.Equals(key, "ServerDomain"))
                {
                    settingModel.ServerDomain = ConfigurationManager.AppSettings[key];
                }

                if (string.Equals(key, "ConfigStartPathUrls"))
                {
                    settingModel.ConfigStartPathUrls = ConfigurationManager.AppSettings[key].Split(",").ToList();
                }


            }

            return settingModel;
        }

        public void SaveInfo()
        {
            System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var keysInfo = ConfigurationManager.AppSettings.AllKeys;
            foreach (var key in keysInfo)
            {
                if (string.Equals(key, "FixedVersionPath"))
                {
                    configuration.AppSettings.Settings[key].Value = this.FixedVersionPath;
                }

                if (string.Equals(key, "InstallPath"))
                {
                    configuration.AppSettings.Settings[key].Value = this.InstallPath;
                }

                if (string.Equals(key, "StartPathUrl"))
                {
                    configuration.AppSettings.Settings[key].Value = this.StartPathUrl;

                }

                //if (string.Equals(key, "UserDataPath"))
                //{
                //    configuration.AppSettings.Settings[key].Value = this.UserDataPath;

                //}

                if (string.Equals(key, "IsClearCacheWhenQuit"))
                {
                    configuration.AppSettings.Settings[key].Value = this.IsClearCacheWhenQuit.ToString();

                }

                if (string.Equals(key, "IsUseFixedVersion"))
                {
                    configuration.AppSettings.Settings[key].Value = this.IsUseFixedVersion.ToString();
                }

                if (string.Equals(key, "StartCheckUpdate"))
                {
                    configuration.AppSettings.Settings[key].Value = this.StartCheckUpdate.ToString();
                }

                if (string.Equals(key, "ServerVersionInfoPath"))
                {
                    configuration.AppSettings.Settings[key].Value = this.ServerVersionInfoPath.ToString();
                }


                if (string.Equals(key, "TimlyCheckUpdateFrequency"))
                {
                    configuration.AppSettings.Settings[key].Value = this.TimlyCheckUpdateFrequency.ToString();
                }

                if (string.Equals(key, "StartPathUrl"))
                {
                    configuration.AppSettings.Settings[key].Value = this.StartPathUrl.ToString();
                }

                if (string.Equals(key, "PringName"))
                {
                    configuration.AppSettings.Settings[key].Value = this.PringName.ToString();
                }

                if (string.Equals(key, "ServerDomain"))
                {
                    configuration.AppSettings.Settings[key].Value = this.ServerDomain.ToString();
                }
            }


            configuration.Save(ConfigurationSaveMode.Modified, true);
        }
    }
}
