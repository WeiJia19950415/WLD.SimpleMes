using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.K3DBInfo
{
    public class SNInStockInfo 
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
        /// 入库时间
        /// </summary>
        public DateTime WarehousingTime { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectNumber { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        public string MaterialName { get; set; }

        public string MaterialNumber { get; set; }

        public string UnitName { get; set; }

        public string UseUnitName { get; set; }
    }
}
