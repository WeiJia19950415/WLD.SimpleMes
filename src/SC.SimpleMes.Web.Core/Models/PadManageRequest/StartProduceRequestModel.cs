using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Material.Dto;
using SC.SimpleMes.Users.Dto;
using SC.SimpleMes.WorkProcess;

namespace SC.SimpleMes.Models.PadManageRequest
{
    public class StartProduceRequestModel : PadManageRequestModel
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        public WorkProcessOperateTypeEnum OperatroMaterilBatchType { get; set; }

        /// <summary>
        /// 操作人员
        /// </summary>
        public List<UserDto> Users { get; set; }

        public List<MaterialBatchNumberDto> InputMaterialInfos { get; set; }
        public string OperatroMaterilBatchNumber { get; set; }

        /// <summary>
        /// 前置物料准备时使用
        /// </summary>
        public string WorkOrderNumber { get; set; }
    }
}
