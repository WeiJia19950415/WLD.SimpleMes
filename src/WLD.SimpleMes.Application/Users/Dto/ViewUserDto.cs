using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WLD.SimpleMes.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Users.Dto
{
    [AutoMapFrom(typeof(ViewUser))]
    public class ViewUserDto : EntityDto<long>
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// 是否活动
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// 租户Id
        /// </summary>
        public int? TenantId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? BirthDay { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Byte Gender { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImage { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNumber { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string WorkNumber { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 部门Id
        /// </summary>
        public long? OrganizationUnitId { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string OrganizationUnitCode { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string OrganizationUnitName { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int? RoleId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
    }
}

