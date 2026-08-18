using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.WorkStation
{
    public class WorkShopInfo : Entity<long>, IMayHaveTenant
    {
        /// <summary>
        /// 所在工厂
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 车间名称 
        /// </summary>
        public string WorkShopName { get; set; }

        /// <summary>
        /// 车间编号
        /// </summary>
        public string WorkShopNumber { get; set; }

        /// <summary>
        /// 下属工作中心
        /// </summary>
        public List<WorkStationInfo> WorkStationInfos { get; set; }

        /// <summary>
        /// 关联的产线
        /// </summary>
        public List<ProductLine> ProductLines { get; set; }
    }
}
