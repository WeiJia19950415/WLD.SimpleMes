using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.QualityControl
{
    /// <summary>
    /// 报废报表视图
    /// </summary>
    public class View_MaterialDiscardRecord:Entity<long>
    {
        public long? BelongProductLineId { get; set; }

        /// <summary>
        /// 报废车间
        /// </summary>
        public string ProductLineName { get; set; }
        /// <summary>
        /// 报废工序
        /// </summary>
        public string WorkProcessName { get;set; }

        /// <summary>
        /// 报废工序Id
        /// </summary>
        public long? BelongWorkProcessId { get; set; }

        /// <summary>
        /// 关联报废产品序列号/批次号
        /// </summary>
        public string BatchMaterilaNumber { get; set; }

        /// <summary>
        /// 关联产品名称
        /// </summary>
        public string ProductMaterialName { get; set; }

        /// <summary>
        /// 关联产品编码
        /// </summary>
        public string ProductMaterialNumber { get; set; }

        /// <summary>
        /// 关联工单编码
        /// </summary>
        public string WorkOrderNumber { get; set; }

        /// <summary>
        /// 报废的产品序列号
        /// </summary>
        public string BatchNumber { get; set; }

        /// <summary>
        /// 报废物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 报废物料编码
        /// </summary>
        public string MaterialNumber { get; set; }

        /// <summary>
        /// 报废物料数量
        /// </summary>
        public decimal DiccardCount { get; set; }

        /// <summary>
        /// 物料计量单位
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 报废数量【按组装单位】
        /// </summary>
        public decimal DiccardWarpCount { get; set; }

        /// <summary>
        /// 组装单位
        /// </summary>
        public string WrapUnitName { get; set; }

        /// <summary>
        /// 记录日期
        /// </summary>
        public DateTime RecordDate { get; set; }

        /// <summary>
        /// 报废原因
        /// </summary>
        public DiscardTypeEnum DiscardType { get; set; }

        public string DiscardTypeString
        {
            get
            {
                return this.DiscardType.ToString();
            }
        }

        /// <summary>
        /// 关联问题
        /// </summary>
        public string ProbleName { get; set; }

        /// <summary>
        /// 关联问题编号
        /// </summary>
        public string QualityProblemNumber { get; set;}

        /// <summary>
        /// 物料供应商
        /// </summary>
        public string Supplier { get; set; }
    }
}
