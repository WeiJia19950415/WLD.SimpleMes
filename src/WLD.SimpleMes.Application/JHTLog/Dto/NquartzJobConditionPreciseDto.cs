using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Log.Dto
{
   public class NquartzJobConditionPreciseDto
    {
        public string JobGroup { get; set; }

        public string JobName { get; set; }

        public bool IsExcpetion { get; set; }
    }
}

