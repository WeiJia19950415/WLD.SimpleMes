using Abp.Domain.Entities;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.WorkOrder;

namespace SC.SimpleMes.Report.Dto
{
    [ExcelExporter(Name = "电堆构建物料信息", Author = "四川伟力得", AutoFitAllColumn = false, AutoFitMaxRows = 5000, TableStyle = OfficeOpenXml.Table.TableStyles.Custom, AutoCenter = true)]
    public class ProductConstructMaterialInfoExportDto 
    {
        /// <summary>
        /// ERP批次号
        /// </summary>
        [ExporterHeader(DisplayName = "物料批次号")]
        public string BatchNo { get; set; }

        /// <summary>
        /// 投入物料
        /// </summary>
        [ExporterHeader(DisplayName = "物料编号")]
        public string InputMaterialNumber { get; set; }

        /// <summary>
        /// 投入物料名称
        /// </summary>
        [ExporterHeader(DisplayName = "物料名称")]
        public string InputMaterialName { get; set; }

        [ExporterHeader(DisplayName = "产品名称")]
        public string MaterialName { get; set; }

        [IEIgnore]
        public string MaterialNumber { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        [ExporterHeader(DisplayName = "序列号")]
        public string MaterialBatchNumber { get; set; }

        [IEIgnore]
        public ProduceStatusEnum ProduceStatus { get; set; }

        /// <summary>
        /// 生产状态
        /// </summary>
        [ExporterHeader(DisplayName = "生产状态")]
        public string ProduceStatusString
        {
            get
            {
                return this.ProduceStatus.ToString();
            }
        }

        /// <summary>
        /// 产线名称
        /// </summary>
        [ExporterHeader(DisplayName = "产线名称")]
        public string ProductLineName { get; set; }

        /// <summary>
        /// 所在工序名称
        /// </summary>
        [ExporterHeader(DisplayName = "当前工序")]
        public string ProcessName { get; set; }

        ///// <summary>
        ///// 项目名称
        ///// </summary>
        //[ExporterHeader(DisplayName = "项目名称")]
        //public string ProjectName { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        [ExporterHeader(DisplayName = "项目编号")]
        public string ProjectNumber { get; set; }

    }
}
