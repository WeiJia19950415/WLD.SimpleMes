using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SC.SimpleMes.DynamicForms.DDImportantInfos;

namespace SC.SimpleMes.DynamicForms.DTO
{
    [ExcelExporter(Name = "电堆关键性能信息", Author = "四川伟力得", ExcelOutputType = ExcelOutputTypes.None, AutoFitAllColumn = false, AutoFitMaxRows = 5000, AutoCenter = true)]
    public class DDImportantInfoExportDto
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        [ExporterHeader(DisplayName = "检验日期", Format = "yyyy-MM-dd")]

        public DateTime RecordDate { get; set; }

        /// <summary>
        /// 归属物料序列号
        /// </summary>
        [ExporterHeader(DisplayName = "电堆编号号", IsAutoFit = true)]
        public string BelongMaterialBatchNumber { get; set; }

        //[ExportImageField(height: 80, width: 10, XOffset = 5, YOffset = 5)]
        //[ExporterHeader(DisplayName = "二维码", IsAutoFit = false, Width = 20)]
        //public string QrCodeImg { get; set; }

        ///// <summary>
        ///// 项目名称
        ///// </summary>
        //[ExporterHeader(DisplayName = "项目名称", IsAutoFit = true)]
        //public string ProjectName { get; set; }

        ///// <summary>
        ///// 项目编号
        ///// </summary>
        //[ExporterHeader(DisplayName = "项目编号", IsAutoFit = true)]
        //public string ProjectNumber { get; set; }

        /// <summary>
        /// 电流密度
        /// </summary>
        [ExporterHeader(DisplayName = "电堆功率 /kw", Format = "0.00", IsAutoFit = true)]
        public decimal CurrentDensity { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [ExporterHeader(DisplayName = "产品名称", IsAutoFit = true)]
        public string MatreialName { get; set; }

        /// <summary>
        /// 归属产线名称
        /// </summary>
        [ExporterHeader(DisplayName = "生产产线", IsAutoFit = true)]
        public string BelongProductLineName { get; set; }

        [ExporterHeader(DisplayName = "室温", IsAutoFit = true, Format = "0.00")]
        public decimal OperatingTemperature { get; set; }


        /// <summary>
        /// 正罐平均温度
        /// </summary>
        [ExporterHeader(DisplayName = "平均正罐温度℃", IsAutoFit = true, Format = "0.00")]
        public decimal PostiveCanAvgTempeature { get; set; }

        /// <summary>
        /// 付罐平均温度
        /// </summary>
        [ExporterHeader(DisplayName = "平均负罐温度℃", IsAutoFit = true, Format = "0.00")]
        public decimal NegativeCanAvgTempeature { get; set; }

        /// <summary>
        /// 装堆厚度
        /// </summary>
        //[ExporterHeader(DisplayName = "装堆厚度 mm", Format = "0.00", IsAutoFit = true)]
        //public decimal PileThickness { get; set; }


        [ExporterHeader(DisplayName = "压堆厚度 mm", Format = "0.00", IsAutoFit = true)]
        public decimal PressThickness { get; set; }


        /// <summary>
        /// 电堆内阻
        /// </summary>
        [ExporterHeader(DisplayName = "电堆内阻\r\nΩ·cm²", Format = "0.00", IsAutoFit = true)]
        public decimal InternalResistance { get; set; }

        [ExporterHeader(DisplayName = "平均充电内阻Ω·cm²", Format = "0.00", IsAutoFit = true)]
        public decimal ChargeAvgInternalResistance { get; set; }



        [ExporterHeader(DisplayName = "平均放电内阻Ω·cm²", Format = "0.00", IsAutoFit = true)]
        public decimal DischargeAvgInternalResistance { get; set; }
        ///// <summary>
        ///// 测试前内阻
        ///// </summary>
        //[ExporterHeader(DisplayName = "测前内阻 mΩ", Format = "0.00", IsAutoFit = true)]
        //public decimal? InternalResistanceBefore { get; set; }

        ///// <summary>
        ///// 测试后内阻
        ///// </summary>
        //[ExporterHeader(DisplayName = "测后内阻 mΩ", Format = "0.00", IsAutoFit = true)]
        //public decimal? InternalResistanceAfter { get; set; }

        /// <summary>
        /// 库伦效率
        /// </summary>
        [ExporterHeader(DisplayName = "库伦效率%", Format = "0.00", IsAutoFit = true)]
        public decimal CoulombEfficiency { get; set; }

        /// <summary>
        /// 能量效率
        /// </summary>
        [ExporterHeader(DisplayName = "能量效率%", Format = "0.00", IsAutoFit = true)]
        public decimal EnergyEfficiency { get; set; }

        /// <summary>
        /// 电压效率
        /// </summary>
        [ExporterHeader(DisplayName = "电压效率%", Format = "0.00", IsAutoFit = true)]
        public decimal VoltageEfficiency { get; set; }

        /// <summary>
        /// 电解液利用率
        /// </summary>
        [ExporterHeader(DisplayName = "利用率%", Format = "0.00", IsAutoFit = true)]
        public decimal ElectrolyteUtilization { get; set; }

        ///// <summary>
        ///// 内阻变化率
        ///// </summary>
        //[ExporterHeader(DisplayName = "内阻变化率%", Format = "0.00", IsAutoFit = true)]
        //public decimal InternalResistanceChangeRate
        //{
        //    get
        //    {
        //        if (this.InternalResistanceBefore == 0 || this.InternalResistanceBefore == null)
        //        {
        //            return 0;
        //        }

        //        return (this.InternalResistanceAfter - this.InternalResistanceBefore).GetValueOrDefault() * 100 / this.InternalResistanceAfter.GetValueOrDefault();
        //    }
        //}

        [ExporterHeader(DisplayName = "电堆等级")]
        public string LevelString { get; set; }

        /// <summary>
        /// 使用到的物料信息
        /// </summary>
        [ExporterHeader(DisplayName = "物料信息\r\n 物料_厂商_入库时间_批次号", IsAutoFit = false, Width = 100, WrapText = true)]
        public string MaterialRecordInfos { get; set; }

        [ExporterHeader(DisplayName = "备注", IsAutoFit = false, Width = 100, WrapText = true)]
        public string Remark { get; set; }
    }
}
