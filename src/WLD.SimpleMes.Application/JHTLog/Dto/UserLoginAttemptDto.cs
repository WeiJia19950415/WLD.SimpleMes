using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace WLD.SimpleMes.Log.Dto
{
    /// <summary>
    /// 用户登录记录
    /// </summary>
    [AutoMapFrom(typeof(UserLoginAttempt))]
    public class UserLoginAttemptDto : EntityDto<long>
    {
        /// <summary>
        /// 所属租户ID
        /// </summary>
        public int? TenantId { get; set; }
        /// <summary>
        /// 所属租户
        /// </summary>
        public string TenancyName { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long? UserId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserNameOrEmailAddress { get; set; }
        /// <summary>
        /// 用户地址
        /// </summary>
        public string ClientIpAddress { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 浏览器信息
        /// </summary>
        public string BrowserInfo { get; set; }

        public DateTime CreationTime { get; set; }

        public string CreationTimeStr { get { return this.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"); } }

        public AbpLoginResultType Result { get; set; }

        public string ResultStr { get { return this.Result.ToString(); } }
    }
}

