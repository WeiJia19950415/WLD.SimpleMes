using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.MultiTenancy.Dto
{
    public class SaveFlatFeatureDto
    {
        public int TenandId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}

