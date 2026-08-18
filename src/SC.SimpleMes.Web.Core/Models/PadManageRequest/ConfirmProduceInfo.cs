using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Material.Dto;
using SC.SimpleMes.QualityControl;
using SC.SimpleMes.QualityControl.Dto;
using SC.SimpleMes.Users.Dto;

namespace SC.SimpleMes.Models.PadManageRequest
{
    public class ConfirmProduceInfo : PadManageRequestModel
    {

        /// <summary>
        /// 投入物料信息
        /// </summary>
        public List<MaterialBatchNumberDto> InputMaterialInfos { get; set; }

        /// <summary>
        /// 操作人员
        /// </summary>
        public List<UserDto> Operator { get; set; }

        /// <summary>
        /// 物料报废记录信息
        /// </summary>
        public List<MaterialDiscardRecordDTO> MaterialDiscardRecords { get; set; }
    }
}
