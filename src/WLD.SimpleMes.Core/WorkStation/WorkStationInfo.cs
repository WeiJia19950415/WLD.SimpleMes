using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkStation
{
    /// <summary>
    /// 工位
    /// </summary>
    public class WorkStationInfo : Entity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        /// <summary>
        /// 工作中心名称
        /// </summary>
        public string WorkStationName { get; set; }

        /// <summary>
        /// 工作中心编号
        /// </summary>
        public string WorkStationNumber { get; set; }

        /// <summary>
        /// 归属产线Id
        /// </summary>
        public long? BelongProductLineId { get; set; }

        /// <summary>
        /// 归属的产线
        /// </summary>
        public ProductLine BelongProductLine { get; set; }

        /// <summary>
        /// 所属车间ID
        /// </summary>
        public long? BelongWorkShopId { get; set; }

        /// <summary>
        /// 是否被共享【共享工位不需要判断所属产线】
        /// </summary>
        public bool IsShared { get; set; }

        /// <summary>
        /// 所属车间编号
        /// </summary>
        public string BelongWorkShopNumber { get; set; }

        /// <summary>
        /// 所属车间
        /// </summary>
        public WorkShopInfo BelongWorkShop { get; set; }

        /// <summary>
        /// 操作员工
        /// </summary>
        public List<WorkStationUserRelation> OpeartUsers { get; set; }

    }
}
