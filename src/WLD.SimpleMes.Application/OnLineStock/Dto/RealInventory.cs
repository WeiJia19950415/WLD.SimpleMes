using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.OnLineStock.Dto
{
    public class RealInventory
    {
        public long LineSideMaterialInfoId { get; set; }

        public string MaterialName { get; set; }

        public string UnitName { get; set; }


        public string Specification { get; set; }

        public decimal OperatorCount { get; set; }
    }
}
