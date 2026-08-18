using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.BOM;
using SC.SimpleMes.Material;
using SC.SimpleMes.WorkStation;

namespace SC.SimpleMes.WorkOrder
{
    public class View_OverUseWorkOrderInfo:Entity<long>
    {

        /// <summary>
        /// 工单编号：前缀+生产时间+流水号4位【自动补齐】
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 来源工单：销售订单
        /// </summary>
        public string FromOrderNumber { get; set; }

        /// <summary>
        /// 使用的标准BOMId
        /// </summary>
        public long? BOMId { get; set; }

        /// <summary>
        /// 生产产品Id
        /// </summary>
        public long MaterialInfoId { get; set; }

        /// <summary>
        /// 材料名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 材料编号
        /// </summary>
        public string MaterialNumber { get; private set; }

        /// <summary>
        /// 工单BomId
        /// </summary>
        public long? WorkOrderBomId { get; set; }



        /// <summary>
        /// 生产车间
        /// </summary>
        public long? ProduceWorkShopId { get; set; }


        /// <summary>
        /// 车间名称 
        /// </summary>
        public string WorkShopName { get; set; }

        /// <summary>
        /// 车间编号
        /// </summary>
        public string WorkShopNumber { get; set; }

        /// <summary>
        /// 生产产线
        /// </summary>
        public long? ProduceLineId { get; set; }


        /// <summary>
        /// 产线名称
        /// </summary>
        public string ProductLineName { get; set; }

        /// <summary>
        /// 产线编号
        /// </summary>
        public string ProductLineNumber { get; set; }

        /// <summary>
        /// 生产数量
        /// </summary>
        public decimal ProduceCount { get; set; }

        /// <summary>
        /// 已投产数量
        /// </summary>
        public decimal ProdcuingCount { get; set; }

        /// <summary>
        /// 已完工数量
        /// </summary>
        public decimal FinishedCount { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime PlanStartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime PlanEndTime { get; set; }

        /// <summary>
        /// 实际开始时间
        /// </summary>
        public DateTime? ActrualStartTime { get; set; }

        /// <summary>
        /// 实际结束时间
        /// </summary>
        public DateTime? ActuralEndTime { get; set; }

        /// <summary>
        /// 交货时间
        /// </summary>
        public DateTime? DeliveryTime { get; set; }

        /// <summary>
        /// 工单状态
        /// </summary>
        public WorkOrderStatuEnum WorkOrderStatu { get; private set; } = WorkOrderStatuEnum.未开始;

        /// <summary>
        /// 归属租户
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 工艺ID
        /// </summary>
        public long? WorkProcessSetId { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public long? ProjectId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectNumber { get; set; }

        /// <summary>
        /// 首次预警时间
        /// </summary>
        public DateTime? FirstWarningTime { get; set; }

        /// <summary>
        /// 预警最后更新时间
        /// </summary>
        public DateTime? LastWarningTime { get; set; }
    }
}
