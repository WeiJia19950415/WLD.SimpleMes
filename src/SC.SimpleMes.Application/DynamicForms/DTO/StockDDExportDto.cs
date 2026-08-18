using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.DynamicForms.DTO
{
    [ExcelExporter(Author = "四川伟力得", ExcelOutputType = ExcelOutputTypes.None, AutoFitAllColumn = false, AutoFitMaxRows = 5000, AutoCenter = true)]
    public class StockDDExportDto
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        [ExporterHeader(DisplayName = "检验日期", Format = "yyyy-MM-dd",Width =20)]
        public DateTime? RecordDate { get; set; }

        /// <summary>
        /// 归属物料序列号
        /// </summary>
        [ExporterHeader(DisplayName = "电堆序列号", IsAutoFit = true, Width = 20)]
        public string BelongMaterialBatchNumber { get; set; }

        [ExportImageField(width: 10, height: 80,XOffset =5,YOffset =5)]
        [ExporterHeader(DisplayName = "二维码", IsAutoFit = true, Width = 20)]
        public string QrCodeImg { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        [ExporterHeader(DisplayName = "项目编号", IsAutoFit = true, Width = 20)]
        public string ProjectNumber { get; set; }


        [ExporterHeader(DisplayName = "工单编号", IsAutoFit = true, Width = 20)]
        public string BelongOrderNumber { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [ExporterHeader(DisplayName = "产品名称", IsAutoFit = true)]
        public string MatreialName { get; set; }
    }

    public class SignDto
    {
        [ExporterHeader(DisplayName = "质检", AutoCenterColumn =true)]
        public string Qualitor { get; set; }

        [ExporterHeader(DisplayName = "生产", Width = 120, AutoCenterColumn = true)]
        public string Productor { get; set; }

        [ExporterHeader(DisplayName = "库房", Width = 120, AutoCenterColumn = true)]
        public string Stocker { get; set; }
    }
}
