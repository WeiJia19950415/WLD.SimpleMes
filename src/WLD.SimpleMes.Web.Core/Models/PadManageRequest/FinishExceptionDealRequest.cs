using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.QualityControl;
using WLD.SimpleMes.QualityControl.Dto;

namespace WLD.SimpleMes.Models.PadManageRequest
{
    public class FinishExceptionDealRequest : PadManageRequestModel
    {
       public ProblemDealRecordDto ProblemDealRecord { get; set; }

        public ProblemRecordDto ProblemRecord { get; set; }
    }
}
