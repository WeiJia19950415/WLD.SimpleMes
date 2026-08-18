using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Models.PadManageRequest
{
    public class CompletWorkProcessRequest : PadManageRequestModel
    {
        public long FormTemlpateId { get; set; }
        public string FormRecordInfo { get; set; }

        public long FormRecordId { get; set; } = 0;

        /// <summary>
        /// 是否正常结束
        /// </summary>
        public bool IsNormalFinish { get; set; }
    }
}
