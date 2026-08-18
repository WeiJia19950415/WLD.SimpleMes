using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Users.Dto
{
    public class FindBackPassWordDto
    {
        /// <summary>
        /// 新密码
        /// </summary>
        [Required]
        public string NewPassWord { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required]
        public string Code { get; set; }
    }
}

