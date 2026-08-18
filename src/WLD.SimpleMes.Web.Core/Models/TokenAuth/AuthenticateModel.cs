using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;

namespace WLD.SimpleMes.Models.TokenAuth
{
    public class AuthenticateModel
    {
        /// <summary>
        /// 用户帐号/邮箱/手机号码
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string Account { get; set; }

        /// <summary>
        /// 密码或动态验证码
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }

        /// <summary>
        /// 车间工位
        /// </summary>
        public long WorkStationId { get; set; }

        /// <summary>
        /// 登录方式 
        /// 0 帐号密码登录
        /// 1 动态手机验证码登录
        /// </summary>
        public int LoginWay { get; set; }

        /// <summary>
        /// 所在公司名称
        /// </summary>
        public string TeancyName { get; set; }

        /// <summary>
        /// 切换租户时，切换的租户ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 返回的连接
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}

