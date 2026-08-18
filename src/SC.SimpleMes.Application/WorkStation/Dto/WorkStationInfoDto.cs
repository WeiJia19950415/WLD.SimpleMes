using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.WorkStation.Dto
{
    public class WorkStationInfoDto : EntityDto<long>
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
        /// 产线名称
        /// </summary>
        public string ProductLineName { get; set; }

        /// <summary>
        /// 产线编号
        /// </summary>
        public string ProductLineNumber { get; set; }

        /// <summary>
        /// 所属车间ID
        /// </summary>
        public long? BelongWorkShopId { get; set; }

        /// <summary>
        /// 所属车间编号
        /// </summary>
        public string BelongWorkShopNumber { get; set; }

        /// <summary>
        /// 所属车间编号
        /// </summary>
        public string BelongWorkShopName { get; set; }

        /// <summary>
        /// 是否被共享
        /// </summary>
        public bool IsShared { get; set; }

    }
}
