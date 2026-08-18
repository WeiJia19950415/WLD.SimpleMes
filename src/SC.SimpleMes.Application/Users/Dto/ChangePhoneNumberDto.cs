using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Users.Dto
{
    public class ChangePhoneNumberDto
    {
        /// <summary>
        /// 当前用户密码
        /// </summary>
        [Required]
        public string PassWord { get; set; }
        /// <summary>
        /// 新电话号码
        /// </summary>
        [Required]
        public string NewPhone { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required]
        public string ValidCode { get; set; }
    }
}

