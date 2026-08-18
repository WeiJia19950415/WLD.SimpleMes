using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkOrder.DTO
{
    public class WorkOrderInfoDto : EntityDto<long>
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
        /// 生产产品名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 产品编号
        /// </summary>
        public string MaterialNumber { get; set; }

        /// <summary>
        /// 车间id
        /// </summary>
        public long? ProduceWorkShopId { get; set; }

        /// <summary>
        /// 车间名称
        /// </summary>
        public string ProduceWorkShopName { get; set; }


        /// <summary>
        /// 生产产线
        /// </summary>
        public long? ProduceLineId { get; set; }

        /// <summary>
        /// 产线名称
        /// </summary>
        public string ProductLineName { get; set; }

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
        /// 工单BomId
        /// </summary>
        public long? WorkOrderBomId { get; set; }

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
        public DateTime DeliveryTime { get; set; }

        public WorkOrderStatuEnum   WorkOrderStatu { get; set; }

        public long? WorkProcessSetId { get; set; }

        public string ProjectName { get; set; }

        public string ProjectNumber { get; set; }


        /// <summary>
        /// 首次预警时间
        /// </summary>
        public DateTime? FirstWarningTime { get; set; }

        /// <summary>
        /// 预警最后更新时间
        /// </summary>
        public DateTime? LastWarningTime { get; set; }

        /// <summary>
        /// 客户信息
        /// </summary>
        public  CustomerProductInfo CustomerProductInfo { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark { get; set; }
    }


    /// <summary>
    /// 客户产品信息
    /// </summary>
    public class CustomerProductInfoDto
    {
        /// <summary>
        /// 对应订单编号
        /// </summary>
        public string WorkOrderNumber { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialNumber { get; set; }
    }

}
