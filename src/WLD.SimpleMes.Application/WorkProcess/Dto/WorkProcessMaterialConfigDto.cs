using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkProcess.Dto
{
    public class WorkProcessMaterialConfigDto
    {
        public long WorkProcessId { get; set; }

        public List<long> MaterialIds { get; set; }
    }
}
