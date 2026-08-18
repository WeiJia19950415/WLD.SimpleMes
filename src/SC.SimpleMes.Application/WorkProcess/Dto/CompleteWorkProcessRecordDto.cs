using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.WorkProcess.Dto
{
    public class CompleteWorkProcessRecordDto : InputOperatorRecordInfo
    {
        public long FormTemlpateId { get; set; }

        public long FormRecordInfoId { get; set; }
        public string FormRecordInfo { get; set; }
        public bool IsNormalFinish { get; set; }



    }
}
