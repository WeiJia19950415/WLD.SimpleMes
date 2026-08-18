using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.WorkOrder.DTO
{
    public class OrderMaterialProduceStatuDto : EntityDto<long>
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

        public string ProduceStatusString
        {
            get
            {
                return this.ProduceStatus.ToString();
            }
        }

        public string ProcessName { get; set; }

        public string WorkStationName { get; set; }

        public string ProductLineName { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 剩余工序数量
        /// </summary>
        public int LeftWorkProcessCount { get; set; }

        /// <summary>
        /// 完成进度
        /// </summary>
        public int FinishPercentage { get; set; }

        /// <summary>
        /// 预计结束时间
        /// 每次更新工序时计算
        /// </summary>
        public DateTime? PredictEndTime { get; set; }

        public string ProjectNumber { get; set; }

        public string ProjectName { get; set; }


        /// <summary>
        /// 是否维修过
        /// </summary>
        public bool HaveRepaired { get; set; }

        /// <summary>
        /// 是否超期
        /// </summary>
        public bool IsOverDay { get; set; }

        /// <summary>
        /// 最后一道工序测试维修
        /// </summary>
        public bool IsLastFqcRepaired { get; set; }

        /// <summary>
        /// 测试通过次数
        /// </summary>
        public int PassCounts { get; set; }

        /// <summary>
        /// 测试失败次数
        /// </summary>
        public int FailCounts { get; set; }

        /// <summary>
        /// 滞留时间
        /// </summary>
        public int StayTime { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal CurrentMatrialCount { get; set; }
    }
}
