using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.WorkProcess;

namespace SC.SimpleMes.Models.PadManageRequest
{
    public class LoadFormFillRequestModel : PadManageRequestModel
    {
        public FormUseTypeEnum FormUseType { get; set; }
    }
}
