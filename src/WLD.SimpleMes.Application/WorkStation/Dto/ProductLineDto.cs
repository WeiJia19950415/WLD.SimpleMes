using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkStation.Dto
{
    /// <summary>
    /// 产线Dto对象
    /// </summary>
    public class ProductLineDto : EntityDto<long>
    {
        /// <summary>
        /// 产线名称
        /// </summary>
        public string ProductLineName { get; set; }

        /// <summary>
        /// 产线编号
        /// </summary>
        public string ProductLineNumber { get; set; }

        /// <summary>
        /// 产线状态
        /// </summary>
        public ProductLineStateEnum ProductLineState { get; set; }

        /// <summary>
        /// 所属车间ID
        /// </summary>
        public long? BelongWorkShopId { get; set; }

        /// <summary>
        /// 所属车间名称
        /// </summary>
        public string WorkShopName { get; set; }

        /// <summary>
        /// 所属车间编号
        /// </summary>
        public string WorkShopNumber { get; set; }

        public int? TenantId { get; set; }

        /// <summary>
        /// 产品初始序号
        /// </summary>
        public int InitStartNumber { get; set; }
    }
}
