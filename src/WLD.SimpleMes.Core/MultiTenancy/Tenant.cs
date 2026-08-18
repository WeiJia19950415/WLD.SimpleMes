using Abp.MultiTenancy;
using WLD.SimpleMes.Authorization.Users;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WLD.SimpleMes.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
        /// <summary>
        /// 统一社会信用代码
        /// </summary>
        [MaxLength(200)]
        public string UniformSocialCreditCode { get; set; }
        /// <summary>
        /// Logo地址
        /// </summary>
        [MaxLength(200)]
        public string LogoImage { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        [MaxLength(200)]
        public string ContactName { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        [MaxLength(50)]
        public string ContactPhone { get; set; }
        /// <summary>
        /// 联系人邮箱
        /// </summary>
        [MaxLength(200)]
        public string ContactEmail { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        [MaxLength(200)]
        public string Address { get; set; }
        /// <summary>
        /// 归属区域
        /// </summary>
        [MaxLength(50)]
        public string AreaCode { get; set; }

        /// <summary>
        /// 官网
        /// </summary>
        [MaxLength(200)]
        public string OfficialWebsite { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        public bool IsAuthentication { get; set; }
        /// <summary>
        /// 公司简介
        /// </summary>
        [MaxLength(2000)]
        public string BriefIntroduction { get; set; }
        /// <summary>
        /// 公司规模
        /// </summary>
        public TenantScaleEnum? TenantScale { get; set; }
    }
    public enum TenantScaleEnum
    {
        [Description("50人以内")]
        InFifty = 1,
        [Description("50~100人")]
        FiftyToOneHundred = 2,
        [Description("100~500人")]
        OneHundredToFiveHundred = 3,
        [Description("500~1000人")]
        FiveHundredToOneThousand = 4,
        [Description("1000人以上")]
        OutOneThousand = 5
    }
}

