using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.Extensions;
using JHT.CommonUtity;
using WLD.SimpleMes.WorkStation;

namespace WLD.SimpleMes.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public const string DefaultPassword = "123qwe";

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                PhoneNumber = "13800138002",
                Roles = new List<UserRole>(),

            };

            user.SetNormalizedNames();

            return user;
        }
        public static User CreateTenantAdminUser(int tenantId, string emailAddress, string phonenumber, string userName)
        {
            var namepinyin = PinYinConverterHelp.GetPingYin(userName);
            var user = new User
            {
                TenantId = tenantId,
                UserName = namepinyin,
                Name = userName,
                Surname = namepinyin,
                EmailAddress = emailAddress,
                PhoneNumber = phonenumber,
                IsPhoneNumberConfirmed = true,//默认创建的用户电话号码为认证
                Roles = new List<UserRole>(),
            };

            user.SetNormalizedNames();

            return user;
        }
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
        /// 微信OpenId
        /// </summary>
        public string WeChatOpenId { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImage { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public string Postion { get; set; }
        /// <summary>
        /// 工作地点
        /// </summary>
        public string WorkAddress { get; set; }

        /// <summary>
        /// 所属工位
        /// </summary>
        public List<WorkStationUserRelation> WorkStationUserRelations { get; set; }


        public GenderEnum Gender { get; set; }
        /// <summary>
        /// 是否信息完善
        /// </summary>
        public bool IsComplete { get; set; }
    }
    public enum GenderEnum
    {
        保密 = 0,
        男 = 1,
        女 = 2
    }
}

