using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.WorkProcess;

namespace SC.SimpleMes.WorkStation
{
    /// <summary>
    /// 产线
    /// </summary>
    public class ProductLine : Entity<long>, IMayHaveTenant
    {

        public int? TenantId { get; set; }

        /// <summary>
        /// 产线名称
        /// </summary>
        public string ProductLineName { get; set; }

        /// <summary>
        /// 产线编号
        /// </summary>
        public string ProductLineNumber { get; set; }

        /// <summary>
        /// 归属车间
        /// </summary>
        public long? BelongWorkShopId { get; set; }

        /// <summary>
        /// 产品初始序号
        /// </summary>
        public int InitStartNumber { get; set; }

        public WorkShopInfo BelongWorkShop { get; set; }

        /// <summary>
        /// 产线状态
        /// </summary>
        public ProductLineStateEnum ProductLineState { get; set; }

        /// <summary>
        /// 关联的工位
        /// </summary>
        public List<WorkStationInfo> ManageWorkStations { get; set; }

        /// <summary>
        /// 产线负责人
        /// </summary>
        public List<ProductLineUserRelation> OpeartUsers { get; set; }

    }

    /// <summary>
    /// 产线状态
    /// </summary>
    public enum ProductLineStateEnum
    {
        生产中 = 1,
        检修中 = 2,
        已停用 = 3
    }
}
