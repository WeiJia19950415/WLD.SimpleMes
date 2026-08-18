using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Models.PadManageRequest
{
    public class LoadErpInstocsRequestModel : PadManageRequestModel
    {
        public string ErpInstockBatchNumber { get; set; }

        public string WorkOrderNumber { get; set; }

        /// <summary>
        /// 加工产品Id【在制品】
        /// </summary>
        public long? OnlineMaterialInfoId { get; set; }

        public bool NeedCheck { get; set; } = true;
    }
}
