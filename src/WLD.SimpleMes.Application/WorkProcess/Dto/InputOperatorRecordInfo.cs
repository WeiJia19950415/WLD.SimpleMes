using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Material.Dto;
using WLD.SimpleMes.QualityControl.Dto;
using WLD.SimpleMes.Users.Dto;

namespace WLD.SimpleMes.WorkProcess.Dto
{
    public class InputOperatorRecordInfo
    {
        /// <summary>
        /// 当前操作的工序
        /// </summary>
        public long WorkProcessId { get; set; }

        /// <summary>
        /// 当前操作的工位
        /// </summary>
        public long WorkStationId { get; set; }

        /// <summary>
        /// 操控的物料批次号
        /// </summary>
        public string OperatroMaterilBatchNumber { get; set; }

        /// <summary>
        /// 工单编号
        /// </summary>
        public string WorkOrderNumber { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public WorkProcessOperateTypeEnum OperatroMaterilBatchType { get; set; }

        /// <summary>
        /// 操作人员
        /// </summary>
        public List<UserDto> Users { get; set; }

        /// 投入物料信息
        /// </summary>
        public List<MaterialBatchNumberDto> InputMaterialInfos { get; set; }

        /// <summary>
        /// 是否为返修物料投入
        /// </summary>
        public bool IsRepiredInput { get; set; }

        /// <summary>
        /// 报废物料信息
        /// </summary>
        public List<MaterialDiscardRecordDTO> MaterialDiscardRecords { get; set; }

    }
}
