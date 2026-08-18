using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Models.PadManageRequest
{
    public class ScanMaterialBatchNumberRequest : PadManageRequestModel
    {
        /// <summary>
        /// 投入物料批次号
        /// </summary>
        public string MaterialBatchNumber { get; set; }

        /// <summary>
        /// 操作工单
        /// </summary>
        public string WorkOrderNumber { get; set; }
    }
}
