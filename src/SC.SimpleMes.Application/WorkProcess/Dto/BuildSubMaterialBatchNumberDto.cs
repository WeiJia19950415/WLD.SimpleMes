using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Material.Dto;
using SC.SimpleMes.Users.Dto;

namespace SC.SimpleMes.WorkProcess.Dto
{
    public class BuildSubMaterialBatchNumberDto
    {
        /// <summary>
        /// 工单编号
        /// </summary>
        public string WorkOrderNumber { get; set; }
        public long CurrentWorkProcessId { get; set; }

        public List<MaterialBatchNumberDto> InputMatreilInfos { get; set; }

        /// <summary>
        /// 加工单位
        /// </summary>
        public string WrapUnitNmae { get; set; }

        /// <summary>
        /// 加工数量
        /// </summary>
        public decimal MatrialCount { get; set; }

        public long CurrentWorkStationId { get; set; }

        public List<UserDto> Creator { get; set; }

        public long? OnlineMaterialInfoId { get; set; }

        public long? OperateRecordId { get; set; }

        /// <summary>
        /// 是否为返修投入
        /// </summary>
        public bool IsRepairedInput { get; set; }
    }

}
