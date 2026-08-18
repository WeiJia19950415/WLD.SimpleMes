using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Log.Dto
{
    public class AuditLogConditionDto
    {
        public string KeyWord { get; set; }
        public DateTime ExecutionTime { get; set; }
        public bool? IsExecution { get; set; }


    }
}

