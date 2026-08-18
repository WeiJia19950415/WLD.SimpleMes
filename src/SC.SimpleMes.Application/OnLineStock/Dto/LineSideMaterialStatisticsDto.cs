using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.OnLineStock.Dto
{
    public class LineSideMaterialStatisticsDto
    {
        public string MaterialName { get; set; }

        public string UnitName { get; set; }
        public decimal OutputQuantity { get; set; }

        public decimal ConsumptionQuantity { get; set; }

        public string Specification { get; set; }

        public decimal LeftQuantity
        {
            get
            {
                return OutputQuantity - ConsumptionQuantity;
            }
        }
    }
}
