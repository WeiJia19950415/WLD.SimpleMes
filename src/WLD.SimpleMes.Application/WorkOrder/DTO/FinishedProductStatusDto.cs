using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkOrder.DTO
{
    /// <summary>
    /// 制成品（电堆）生产状态
    /// </summary>
    public class FinishedProductStatusDto:EntityDto<long>
    {
        /// <summary>
        /// 工单编号
        /// </summary>
        public string WorkOrderNumber { get; set; }

        /// <summary>
        /// 生产的制成品
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 制成品物料编码
        /// </summary>
        public string MaterialNumber { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNumber { get; set; }

        /// <summary>
        /// 车间名称
        /// </summary>
        public string ProduceWorkShopName { get; set; }

        /// <summary>
        /// 产线名称
        /// </summary>
        public string ProductLineName { get; set; }

        /// <summary>
        /// 生产状态 （未开始、生产中、异常、异常处置、报废、已完成）
        /// </summary>
        public string ProduceStatus { get; set; }

        /// <summary>
        /// 完成工序数量
        /// </summary>
        public int CompleteNumber { get; set; }

        /// <summary>
        /// 应该执行的工序
        /// </summary>
        public List<ProduceStep> ProduceSteps { get; set; }
    }

    public class ProduceStep
    {
        /// <summary>
        /// 生产使用的工序
        /// </summary>
        public long WorkProcessInfoId { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        public string WorkProcessInfoName { get; set; }
        /// <summary>
        /// 工序执行状态：未开始、生产中、已完成
        /// </summary>
        public string ProcessInfoState { get; set; }
        /// <summary>
        /// 执行顺序
        /// </summary>
        public int ImplementOrder { get; set; }
        /// <summary>
        /// 开始生产时间
        /// </summary>
        public DateTime? PlanStartTimeStart { get; set; }
        /// <summary>
        /// 结束生产时间
        /// </summary>
        public DateTime? PlanStartTimeEnd { get; set; }

    }
}
