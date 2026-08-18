using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Report.Dto
{
    public class SNInStockInfoDto
    {
        public string SNumber { get; set; }
        /// <summary>
        /// 入库单编号
        /// </summary>
        public string InStockBillNo { get; set; }

        /// <summary>
        /// 入库关联的生产任务单号
        /// </summary>
        public string InStockWorkOrderNumber { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectNumber { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
    }
}
