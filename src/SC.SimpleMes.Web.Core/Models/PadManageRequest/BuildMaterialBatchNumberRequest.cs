using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Users.Dto;

namespace SC.SimpleMes.Models.PadManageRequest
{
    public class BuildMaterialBatchNumberRequest : PadManageRequestModel
    {
        public long WorkOrderId { get; set; }
        public long OperateRecordId { get; set; }
        public long? CurrentProductLineId { get;  set; }
        public long MaterialCount { get;  set; }
        public List<UserDto> Creator { get; set; }
    }
}
