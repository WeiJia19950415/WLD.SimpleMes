using Abp.Domain.Entities;
using SC.SimpleMes.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.JHTOrganzation
{
    /// <summary>
    /// 组织关系管理员
    /// </summary>
    public class OrganzationManager : Entity<long>
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        [Required]
        public long OrganzationID { get; set; }
        public JHTOrganzation Organzation { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public long UserId { get; set; }
        public User User { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
    }
}

