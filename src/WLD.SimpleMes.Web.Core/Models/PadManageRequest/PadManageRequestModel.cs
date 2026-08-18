using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Models
{
    public class PadManageRequestModel
    {
        /// <summary>
        /// 当前加工工序
        /// </summary>
        // [Required(ErrorMessage = "请选择操作工序")]
        public long CurrentWorkProcessId { get; set; }

        /// <summary>
        /// 加工产品
        /// </summary>
        public string ProductMaterialBatchNumber { get; set; }

        /// <summary>
        /// 加工工位
        /// </summary>
        [Required(ErrorMessage = "请选择操作工位")]
        public long CurrentWorkStaionId { get; set; }

        /// <summary>
        /// 关联工单
        /// </summary>
        public string WorkOrderNumber { get; set; }
    }
}
