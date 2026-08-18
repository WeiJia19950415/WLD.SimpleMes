using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.WorkProcess;

namespace WLD.SimpleMes.Models.PadManageRequest
{
    public class LoadFormFillRequestModel : PadManageRequestModel
    {
        public FormUseTypeEnum FormUseType { get; set; }
    }
}
