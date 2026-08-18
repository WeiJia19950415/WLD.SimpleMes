using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.QualityControl;
using SC.SimpleMes.QualityControl.Dto;

namespace SC.SimpleMes.Models.PadManageRequest
{
    public class FinishExceptionDealRequest : PadManageRequestModel
    {
       public ProblemDealRecordDto ProblemDealRecord { get; set; }

        public ProblemRecordDto ProblemRecord { get; set; }
    }
}
