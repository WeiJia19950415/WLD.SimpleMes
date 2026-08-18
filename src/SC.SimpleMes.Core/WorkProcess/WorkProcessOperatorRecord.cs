using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.WorkStation;

namespace SC.SimpleMes.WorkProcess
{
    /// <summary>
    /// 工序人员操作记录【后续考虑按月建表】
    /// </summary>
    public class WorkProcessOperatorRecord : Entity<long>
    {

        /// <summary>
        /// 操作人员所属部门
        /// </summary>
        public long? DepartmentId { get; set; }

        /// <summary>
        /// 车间Id
        /// </summary>
        public long WrokShopId { get; set; }

        /// <summary>
        /// 产线Id
        /// </summary>
        public long ProductLineId { get; set; }


        /// <summary>
        /// 所属工艺ID
        /// </summary>
        public long? WorkProcessSetId { get; set; }

        /// <summary>
        /// 关联工单编号
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 工序Id
        /// </summary>
        public long WorkProcessId { get; set; }

        /// <summary>
        /// 所属工位
        /// </summary>
        public long WorkStationId { get; set; }

        /// <summary>
        /// 所属工位名称
        /// </summary>
        public string WorkStationName { get; set; }

        /// <summary>
        /// 工序编号
        /// </summary>

        public string WorkProcessNumber { get; set; }

        /// <summary>
        /// 工序名称
        /// </summary>
        public string WorkProcessName { get; set; }

        /// <summary>
        /// 物料批次号
        /// </summary>
        public string BatchNumber { get; set; }

        /// <summary>
        /// 操作开始时间
        /// </summary>
        public DateTime StartTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 操作结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 耗费时间
        /// </summary>
        public long CostTimeSeconds { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public WorkProcessOperateTypeEnum WorkProcessOperateType { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string OperatorDescreption { get; set; }

        /// <summary>
        /// 操作人员
        /// </summary>
        public string OperatroName { get; set; }

        /// <summary>
        /// 操作人员Id
        /// </summary>
        public string OpertaorId { get; set; }

        /// <summary>
        /// 当前操作帐号
        /// </summary>
        public long CurrentOperatroAccountId { get; set; }

        /// <summary>
        /// 是否正常结束
        /// </summary>
        public bool IsNormalFinish { get; set; }

        /// <summary>
        /// 是否返修操作
        /// </summary>
        public bool IsRepaired { get; set; }

        /// <summary>
        /// 返修操作
        /// </summary>
        public bool IsLastFqcRepaired {  set; get; }

        public static WorkProcessOperatorRecord BuildEndWorkProcessRecord(long userId,bool isNormalFinish, string operatroMaterilBatchNumber, WorkStationInfo workStaion, WorkProcessInfo workProcess)
        {
            return new WorkProcessOperatorRecord()
            {
                CurrentOperatroAccountId = userId,
                ProductLineId = workStaion.BelongProductLineId.GetValueOrDefault(),
                WorkProcessId = workProcess.Id,
                WorkProcessNumber = workProcess.ProcessNumber,
                WorkProcessName = workProcess.ProcessName,
                WorkStationId = workStaion.Id,
                OperatorDescreption = isNormalFinish ? "正常完工" : "异常反馈",
                WorkStationName = workStaion.WorkStationName,
                WrokShopId = workStaion.BelongWorkShopId.GetValueOrDefault(),
                WorkProcessOperateType = WorkProcessOperateTypeEnum.开始生产,
                BatchNumber = operatroMaterilBatchNumber,
                IsNormalFinish = isNormalFinish,
            };
        }

    }

    public enum WorkProcessOperateTypeEnum
    {
        物料确认 = 1,
        开始生产 = 2,
        信息填报 = 3,
        异常反馈 = 4,
        异常处置 = 5,
    }
}
