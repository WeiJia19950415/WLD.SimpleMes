using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.QualityControl;

namespace SC.SimpleMes.Report.Dto
{
    [ExcelExporter(Name = "物料报废报表", Author = "四川伟力得", AutoFitAllColumn = false, AutoFitMaxRows = 5000, TableStyle = OfficeOpenXml.Table.TableStyles.Custom, AutoCenter = true)]
    public class MaterialDiscardRecordExportDTO
    {
        public MaterialDiscardRecordExportDTO() { }

        /// <summary>
        /// 报废车间
        /// </summary>
        [ExporterHeader(DisplayName = "报废车间")]
        public string ProductLineName { get; set; }
        /// <summary>
        /// 报废工序
        /// </summary>
        [ExporterHeader(DisplayName = "报废工序")]
        public string WorkProcessName { get; set; }


        /// <summary>
        /// 关联报废产品序列号/批次号
        /// </summary>
        [ExporterHeader(DisplayName = "报废物料序列号/批次号")]
        public string BatchMaterilaNumber { get; set; }

        /// <summary>
        /// 关联产品名称
        /// </summary>
        [ExporterHeader(DisplayName = "关联产品名称")]
        public string ProductMaterialName { get; set; }

        /// <summary>
        /// 关联产品编码
        /// </summary>
        [ExporterHeader(DisplayName = "关联产品编码")]
        public string ProductMaterialNumber { get; set; }

        /// <summary>
        /// 关联工单编码
        /// </summary>
        [ExporterHeader(DisplayName = "关联工单编码")]
        public string WorkOrderNumber { get; set; }

        /// <summary>
        /// 报废物料名称
        /// </summary>
        [ExporterHeader(DisplayName = "报废物料名称")]
        public string MaterialName { get; set; }

        /// <summary>
        /// 报废物料编码
        /// </summary>
        [ExporterHeader(DisplayName = "报废物料编码")]
        public string MaterialNumber { get; set; }

        /// <summary>
        /// 报废物料数量
        /// </summary>
        [ExporterHeader(DisplayName = "报废物料数量")]
        public decimal DiccardCount { get; set; }

        /// <summary>
        /// 物料计量单位
        /// </summary>
        [ExporterHeader(DisplayName = "物料计量单位")]
        public string UnitName { get; set; }

        /// <summary>
        /// 记录日期
        /// </summary>
        [ExporterHeader(DisplayName = "记录日期")]
        public DateTime RecordDate { get; set; }

        /// <summary>
        /// 报废原因
        /// </summary>
        [ExporterHeader(DisplayName = "报废原因")]
        public DiscardTypeEnum DiscardType { get; set; }

        /// <summary>
        /// 关联问题
        /// </summary>
        [ExporterHeader(DisplayName = "关联问题")]
        public string ProbleName { get; set; }

        /// <summary>
        /// 关联问题编号
        /// </summary>
        [ExporterHeader(DisplayName = "物料关联问题编号")]
        public string QualityProblemNumber { get; set; }

        /// <summary>
        /// 物料供应商
        /// </summary>
        [ExporterHeader(DisplayName = "物料供应商")]
        public string Supplier { get; set; }
    }
}
