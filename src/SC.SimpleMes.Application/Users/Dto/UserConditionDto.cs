using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Users.Dto
{
    public class UserConditionDto
    {
        /// <summary>
        /// 用户名、姓名、电话号码
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int? RoleId { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public long? OrgId { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// 所属公司
        /// </summary>
        public int TeantId { get; set; }
    }
}

