using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.BOM.Dto;
using WLD.SimpleMes.Material.Dto;

namespace WLD.SimpleMes.WorkProcess.Dto
{
    public class WorkProcessOperatorRecordDto : EntityDto<long>
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

        public long? WorkOrderId { get; set; }

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
        /// 所属工序名称
        /// </summary>
        public string WorkProcessName { get; set; }

        /// <summary>
        /// 工序编号
        /// </summary>

        public string WorkProcessNumber { get; set; }

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
        /// 花费时间
        /// </summary>
        public long? CostTimeSeconds { get; set; }

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

        public List<MaterialBatchNumberDto> InputMaterilaInfo { get; set; }

        public bool CanModifyMaterial { get; set; }

        /// <summary>
        /// 是否正常结束
        /// </summary>
        public bool IsNormalFinish { get; set; }

        /// <summary>
        /// 是否需要投入物料
        /// </summary>
        public List<BomItemDto> ShouldInputMaterial { get; set; }

        /// <summary>
        /// 是否返修操作
        /// </summary>
        public bool IsRepaired { get; set; }

        /// <summary>
        /// 返修操作
        /// </summary>
        public bool IsLastFqcRepaired { set; get; }
    }
}
