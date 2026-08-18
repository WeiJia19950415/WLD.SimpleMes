using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.WorkProcessSet.Dto
{
    public class WorkProcessProductConditionDto
    {
        public string KeyWord { get; set; }
        public long? MaterialId { get; set; }
        public long? WorkProcesSetId { get; set; }
    }
}
