using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Authorization.Users;

namespace SC.SimpleMes.WorkStation
{
    public class ProductLineUserRelation : Entity<long>
    {
        public long UserInfoId { get; set; }

        public User UserInfo { get; set; }

        public long ProductLineId { get; set; }

        public ProductLine ProductLine;
    }
}
