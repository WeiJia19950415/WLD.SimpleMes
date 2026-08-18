using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Authorization.Accounts.Dto
{
    /// <summary>
    /// 发送验证码请求
    /// </summary>
    public class ValidCodeForPurposeDto
    {
        /// <summary>
        /// 电话号码
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 验证码使用的目的
        /// </summary>
        [Required]
        public PurposeEnum Purpose { get; set; }

    }
    public enum PurposeEnum
    {
        ChangePhoneNumber = 1,
        ResetPassword = 2,
        PhoneConfirmation = 3
    }
}

