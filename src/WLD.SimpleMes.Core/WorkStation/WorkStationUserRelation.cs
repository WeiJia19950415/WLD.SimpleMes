using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Authorization.Users;

namespace WLD.SimpleMes.WorkStation
{
    public class WorkStationUserRelation : Entity<long>
    {
        public long UserInfoId { get; set; }

        public User UserInfo { get; set; }

        public long WorkStationInfoId { get; set; }

        public WorkStationInfo WorkStationInfo { get; set; }
    }
}
