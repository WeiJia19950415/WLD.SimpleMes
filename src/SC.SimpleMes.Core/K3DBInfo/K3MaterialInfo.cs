using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.K3DBInfo
{
    public class K3MaterialInfo : Entity<int>
    {
        public string FNumber { get; set; }

        public string FFullNumber { get; set; }

        public string FShortNumber { get; set; }

        public Int16 FLevel { get; set; }

        public string FName { get; set; }

        public string FFullName { get; set; }
    }
}
