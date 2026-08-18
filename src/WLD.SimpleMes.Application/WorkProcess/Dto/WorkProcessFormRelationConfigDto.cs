using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkProcess.Dto
{
    public class WorkProcessFormRelationConfigDto
    {
        public long Id { get; set; }

        public List<long> FormTemplateIds { get; set; }

        public FormUseTypeEnum FormUseType { get; set; }
    }
}
