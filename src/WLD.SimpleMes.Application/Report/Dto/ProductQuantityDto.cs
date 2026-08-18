using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Report.Dto
{
    public class ProductQuantityDto
    {
        public int CancalNum { get; set; }
        public int ClosedNum { get; set; }
        public int IssuedNumb { get; set; }
        public int ProduceNum { get; set; }
        public int PausedNumb { get; set; }
        public int NotStartedNum { get; set; }
    }
}
