using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using SC.SimpleMes.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UpdateUserDto : EntityDto<long>
    {
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [MaxLength(18)]
        public string IdCard { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNumber { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string WorkNumber { get; set; }
        /// <summary>
        /// 工作地点
        /// </summary>
        public string WorkAddress { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImage { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string Postion { get; set; }

        /// <summary>
        /// 相关角色
        /// </summary>
        public string[] RoleNames { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public long[] OrgIds { get; set; }
        /// <summary>
        /// 海康威视UserCode
        /// </summary>
        public string HKUserCode { get; set; }
    }
}

