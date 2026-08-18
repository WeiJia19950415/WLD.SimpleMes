using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.WorkOrder;

namespace SC.SimpleMes.Report
{
    public class View_ProductConstructMaterialInfo:Entity<long>
    {
        /// <summary>
        /// 产品编号
        /// </summary>
        public string MaterialBatchNumber { get; set; }

        public string MaterialName { get; set; }

        public string MaterialNumber { get; set; }



        /// <summary>
        /// 生产状态
        /// </summary>
        public ProduceStatusEnum ProduceStatus { get; set; }

        /// <summary>
        /// 产线名称
        /// </summary>
        public string ProductLineName { get; set; }

        /// <summary>
        /// 所在工序名称
        /// </summary>
        public string ProcessName { get; set; }

        public long CurrentProductLineId { get;set; }
        public long CurrentWorkProcessId { get; set; }

        public long CurrentWorkStationId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectNumber { get; set; }

        /// <summary>
        /// ERP批次号
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// 投入物料
        /// </summary>
        public string InputMaterialNumber { get; set; }

        /// <summary>
        /// 投入物料名称
        /// </summary>
        public string InputMaterialName { get; set; }
    }
}
