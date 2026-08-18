using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.BatchNoByInStockInfo.Dto
{
    public class QueryDto
    {
        public bool? WhetherPrint { get; set; }

        /// <summary>
        /// 批次号、物料信息、入库单号
        /// </summary>
        public string KeyWord { get; set; }

        public DateTime? StartTime{get; set; }

        public DateTime? EndTime { get; set; }


    }
}
