using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.BatchNoByInStockInfo.Dto
{
    public class PrintBatchNoDto
    {
        public long Id { get; set; }

        public string BatchNumber { get; set; }

        public int PrintCounts { get; set; }
    }
}
