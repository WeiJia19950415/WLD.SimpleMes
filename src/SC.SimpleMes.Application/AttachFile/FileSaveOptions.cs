namespace SC.SimpleMes.AttachFile
{
    public class FileSaveOptions
    {
        public FileSaveOptions()
        {
            AllowedExtension = ".txt,.jpeg,.jpg,.png,.bitimage,.doc,.docx,.xls,.xlsx,.doc,.docx,.pdf,.ppt,.pptx,.zip,,rar";
            AllowedFileSzie = 52428800;// 默认50M
            SaveStragety = "yyyyMM";
        }

        /// <summary>
        /// 允许的后缀
        /// </summary>
        public string AllowedExtension { get; set; }
        /// <summary>
        /// 允许上传的文大小
        /// </summary>
        public long AllowedFileSzie { get; set; }

        /// <summary>
        /// 默认文件保存的路径
        /// </summary>
        public string DeafaultSavePath { get; set; }
        /// <summary>
        /// 默认文件保存的路径
        /// </summary>
        public string DeafaultSaveDomain { get; set; }

        /// <summary>
        /// 保存策略 支持YYYYMM;YYYY;YYYYMMDD;
        /// </summary>
        public string SaveStragety { get; set; }

        /// <summary>
        /// 最大的文件数量
        /// </summary>
        public int MaxFileCount { get; set; }
        /// <summary>
        /// 用户人脸识别头像存放地址
        /// </summary>
        public string UserFaceSavePath { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserHeadImageSavePath { get; set; }

        /// <summary>
        /// 应用图片地址
        /// </summary>
        public string FlatFromImageSavePath { get; set; }

        /// <summary>
        /// 租户图片
        /// </summary>
        public string TenantImageSavePath { get; set; }

        /// <summary>
        /// 图片保存的最大值 单位KB
        /// </summary>
        public int MaxImageSize { get; set; }

        public string[] AllowedExtensions
        {
            get
            {
                return AllowedExtension.Split(',');
            }
        }
    }
}

