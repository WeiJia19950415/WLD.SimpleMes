using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Material;
using WLD.SimpleMes.WorkProcess;

namespace WLD.SimpleMes.WorkOrder
{
    /// <summary>
    /// 工单商品生产状态
    /// 【工单产生时自动生成，工序变更时更新】
    /// </summary>
    public class OrderMaterialProduceStatu : Entity<long>
    {
        /// <summary>
        /// 工单编号
        /// </summary>
        public string WorkOrderNumber { get; set; }

        /// <summary>
        /// 生产物料批次号
        /// </summary>
        public string MaterialBatchNumber { get; set; }

        /// <summary>
        /// 生产产品
        /// </summary>
        public long MaterialInfoId { get; set; }


        /// <summary>
        /// 生产状态
        /// </summary>
        public ProduceStatusEnum ProduceStatus { get; set; }

        /// <summary>
        /// 当前生产产线Id
        /// </summary>
        public long CurrentProductLineId { get; set; }


        /// <summary>
        /// 当前生产工位
        /// </summary>
        public long CurrentWorkStationId { get; set; }

        /// <summary>
        /// 当前工序Id
        /// </summary>
        public long CurrentWorkProcessId { get; set; }

        public WorkProcessInfo CurrentWorkProcess { get; set; }

        /// <summary>
        /// 剩余工序数量
        /// </summary>
        public int LeftWorkProcessCount { get; set; }

        /// <summary>
        /// 预计结束时间
        /// 每次更新工序时计算
        /// </summary>
        public DateTime? PredictEndTime { get; set; }


        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 最近更新时间
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 是否维修过
        /// </summary>
        public bool HaveRepaired { get; set; }

        /// <summary>
        /// 测试次数
        /// </summary>
        public int TestCounts { get; set; }

        /// <summary>
        /// 测试通过次数
        /// </summary>
        public int PassCounts { get; set; }

        /// <summary>
        /// 测试失败次数
        /// </summary>
        public int FailCounts { get; set; }

        /// <summary>
        /// 确认当前工序是否已完成
        /// </summary>
        public bool IsCurrentWorkProcessDone { get; set; } = false;

        /// <summary>
        /// 正常进度的工序Id
        /// </summary>
        public long NormalWorkProcessId { get; set; }

        /// <summary>
        /// 是否正在维修
        /// </summary>
        /// <returns></returns>
        public bool IsRepairing
        {
            get
            {
                return NormalWorkProcessId != CurrentWorkProcessId;
            }
        }

        /// <summary>
        /// 最后一道工序测试维修
        /// </summary>
        public bool IsLastFqcRepaired { get; set; }
        //CurrentMatrialCount特殊情况下存在多个20250108应对双极板产线，一次加工多个双极板
        /// <summary>
        /// 数量,默认:1
        /// </summary>
        public decimal CurrentMatrialCount { get; set; } = 1; 
    }

    /// <summary>
    /// 生产状态枚举
    /// </summary>
    public enum ProduceStatusEnum
    {
        未开始 = 0,
        生产中,
        异常,
        异常处置,
        报废,
        已完成,
        返修中
    }
}
