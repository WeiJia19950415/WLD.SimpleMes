using WLD.SimpleMes.MultiTenancy.Dto;
using WLD.SimpleMes.Users.Dto;
using System.Collections.Generic;
using WLD.SimpleMes.WorkProcess.Dto;

namespace WLD.SimpleMes.Models.TokenAuth
{
    public class AuthenticateResultModel
    {
        /// <summary>
        /// 登录Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 登录后的用户信息
        /// </summary>
        public UserDto UserInfo { get; set; }

        /// <summary>
        /// 如果存在多个TenantList
        /// </summary>
        public List<TenantDto> TenantList { get; set; }

        /// <summary>
        /// 跳转地址
        /// </summary>
        public string ReturnUrl { get; set; }

        public List<WorkProcessInfoDto> WorkProcess { get; set; }
    }
}

