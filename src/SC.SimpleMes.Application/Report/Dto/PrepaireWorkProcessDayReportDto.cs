using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Report.Dto
{
    public class PrepaireWorkProcessDayReportDto : EntityDto<long>
    {
        public string WorkOrderNumber { get; set; }

        public long MaterialId { get; set; }

        public string MaterialName { get; set; }

        public string MaterialNumber { get; set; }

        public long ProductLineId { get; set; }

        public string ProductLineName { get; set; }

        public long WorkStationId { get; set; }

        public string WorkStationName { get; set; }

        /// <summary>
        /// 完成数量
        /// </summary>
        public decimal FinishedCount { get; set; }

        /// <summary>
        /// 裁切物料单位【张】
        /// </summary>
        public string CutMaterialUnitName { get; set; }


        /// <summary>
        /// 耗费BOM单中的数量
        /// </summary>
        public decimal BomUniteCount { get; set; }

        /// <summary>
        /// BOM单中的单位
        /// </summary>
        public string BomUnitName { get; set; }

        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime StaticDate { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatTime { get; set; }
    }
}
