using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkOrder
{
    public class View_OrderMaterialProduceStatuses : Entity<long>
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


        public string MaterialName { get; set; }


        public string MaterialNumber { get; set; }

        /// <summary>
        /// 生产状态
        /// </summary>
        public ProduceStatusEnum ProduceStatus { get; set; }

        /// <summary>
        /// 当前生产产线Id
        /// </summary>
        public long CurrentProductLineId { get; set; }

        public string ProductLineName { get; set; }

        /// <summary>
        /// 当前生产工位
        /// </summary>
        public long CurrentWorkStationId { get; set; }
        public string WorkStationName { get; set; }

        /// <summary>
        /// 当前工序Id
        /// </summary>
        public long CurrentWorkProcessId { get; set; }

        public string ProcessName { get; set; }

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
        /// 确认当前工序是否已完成
        /// </summary>
        public bool IsCurrentWorkProcessDone { get; set; } = false;

        /// <summary>
        /// 正常进度的工序Id
        /// </summary>
        public long NormalWorkProcessId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectNumber { get; set; }

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
        /// 滞留天数
        /// </summary>
        public int StayTime { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal CurrentMatrialCount { get; set; }
    }
}
