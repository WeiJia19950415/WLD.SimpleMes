using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.WorkOrder;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;

namespace SC.SimpleMes.Report.Dto
{
    [ExcelExporter(Name = "电堆生产状态报表", Author = "四川伟力得", AutoFitAllColumn = false, AutoFitMaxRows = 5000, TableStyle = OfficeOpenXml.Table.TableStyles.Custom, AutoCenter = true)]
    public class OrderMaterialProduceStatuExportDto
    {
        [ExporterHeader(DisplayName = "项目编号")]
        public string ProjectNumber { get; set; }

        [ExporterHeader(DisplayName = "项目名称")]
        public string ProjectName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ExporterHeader(DisplayName = "生产任务单号")]
        /// <summary>
        /// 工单编号
        /// </summary>
        public string WorkOrderNumber { get; set; }

        /// <summary>
        /// 生产物料批次号
        /// </summary>
        [ExporterHeader(DisplayName = "产品序列号")]
        public string MaterialBatchNumber { get; set; }

        /// <summary>
        /// 生产产品
        /// </summary>
        public long MaterialInfoId { get; set; }

        [ExporterHeader(DisplayName = "产品名称")]
        public string MaterialName { get; set; }

        [ExporterHeader(DisplayName = "物料编码")]
        public string MaterialNumber { get; set; }
        /// <summary>
        /// 生产状态
        /// </summary>
        [ExporterHeader(DisplayName = "生产状态")]
        public ProduceStatusEnum ProduceStatus { get; set; }

        [ExporterHeader(DisplayName = "当前工序")]
        public string ProcessName { get; set; }

        [ExporterHeader(DisplayName = "当前工位")]
        public string WorkStationName { get; set; }

        [ExporterHeader(DisplayName = "当前产线")]
        public string ProductLineName { get; set; }

        [ExporterHeader(DisplayName = "投产时间", Format = "yyyy-MM-dd HH:mm:ss")]
        public DateTime? StartTime { get; set; }

        [ExporterHeader(DisplayName = "更新时间", Format = "yyyy-MM-dd HH:mm:ss")]
        public DateTime? LastUpdateTime { get; set; }

        [ExporterHeader(DisplayName = "结束时间", Format = "yyyy-MM-dd HH:mm:ss")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 剩余工序数量
        /// </summary>
        [ExporterHeader(DisplayName = "剩余工序数量")]
        public int LeftWorkProcessCount { get; set; }

        /// <summary>
        /// 是否维修过
        /// </summary>
        [ExporterHeader(DisplayName = "呆滞天数")]
        public int StayTime { get; set; }

        /// <summary>
        /// 是否维修过
        /// </summary>
        [ExporterHeader(DisplayName = "是否返修")]
        public bool HaveRepaired { get; set; }
    }
}
