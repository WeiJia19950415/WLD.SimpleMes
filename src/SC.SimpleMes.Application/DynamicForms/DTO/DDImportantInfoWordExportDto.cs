using Magicodes.ExporterAndImporter.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;
using static SC.SimpleMes.DynamicForms.DDImportantInfos;

namespace SC.SimpleMes.DynamicForms.DTO
{
    public class DDImportantInfoWordExportDto
    {
        #region 检验相关
        /// <summary>
        /// 检验仪器
        /// </summary>
        public string TestMachineNumber { get; set; }
        /// <summary>
        /// 记录日期
        /// </summary>
        public DateTime RecordDate { get; set; }

        /// <summary>
        /// 送样时间
        /// </summary>
        public DateTime SendDate { get; set; }

        /// <summary>
        /// 检验日期
        /// </summary>
        public DateTime CheckDate { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime AuditeDate { get; set; }

        /// <summary>
        /// 检查人员
        /// </summary>
        public string Checkor { get; set; }

        /// <summary>
        /// 审核人员
        /// </summary>
        public string Auditor { get; set; }

        #endregion


        /// <summary>
        /// 产品编号
        /// </summary>

        public string BelongMaterialBatchNumber { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string MatreialName { get; set; }


        /// <summary>
        /// 所属产线
        /// </summary>
        public string BelongProductLineName { get; set; }

        public long BelongProductLineId { get; set; }


        /// <summary>
        /// 产品编码
        /// </summary>
        public string MaterialNumber { get; set; }

        #region 电堆参数

        /// <summary>
        /// 装堆厚度
        /// </summary>
        public decimal PileThickness { get; set; }

        /// <summary>
        /// 干堆重量
        /// </summary>
        public decimal DryWeight { get; set; }

        /// <summary>
        /// 湿堆重量
        /// </summary>
        public decimal WetWeight { get; set; }


        #endregion

        #region 测试条件

        public decimal Humidity { get; set; }
        /// <summary>
        /// 循环次数
        /// </summary>
        public int LoopCount { get; set; }

        /// <summary>
        /// 正罐平均温度
        /// </summary>
        public decimal PostiveCanAvgTempeature { get; set; }

        /// <summary>
        /// 付罐平均温度
        /// </summary>
        public decimal NegativeCanAvgTempeature { get; set; }

        /// <summary>
        /// 正极平均流量
        /// </summary>
        public decimal PostiveAvgFlowRate { get; set; }

        /// <summary>
        /// 负极平均流量
        /// </summary>
        public decimal NegativeAvgFlowRate { get; set; }

        /// <summary>
        /// 平均充电内阻
        /// </summary>
        public decimal ChargeAvgInternalResistance { get; set; }

        /// <summary>
        /// 平均放电内阻
        /// </summary>
        public decimal DischargeAvgInternalResistance { get; set; }

        /// <summary>
        /// 平均充电时间
        /// </summary>
        public decimal AvgChargeTime { get; set; }

        /// <summary>
        /// 平均放电时间
        /// </summary>
        public decimal AvgDischargeTime { get; set; }

        /// <summary>
        /// 平均充电截止OCV
        /// </summary>
        public decimal AvgChargeOCV { get; set; }

        /// <summary>
        /// 平均放电截止OCV
        /// </summary>
        public decimal AvgDischargeOCV { get; set; }

        /// <summary>
        /// 运行压力
        /// </summary>
        public decimal OperatingPressure { get; set; }

        /// <summary>
        /// 运行温度
        /// </summary>
        public decimal OperatingTemperature { get; set; }

        #endregion

        #region 测试结果

        /// <summary>
        /// 开路内阻
        /// </summary>
        public decimal OnInternalResistance { get; set; }

        /// <summary>
        /// 电堆内阻
        /// </summary>
        public decimal InternalResistance { get; set; }

        /// <summary>
        /// 电流密度及功率
        /// </summary>

        public decimal CurrentDensity { get; set; }

        /// <summary>
        /// 测试前内阻
        /// </summary>
        public decimal? InternalResistanceBefore { get; set; }

        /// <summary>
        /// 测后内阻
        /// </summary>
        public decimal? InternalResistanceAfter { get; set; }

        /// <summary>
        /// 库伦效率
        /// </summary>
        public decimal CoulombEfficiency { get; set; }

        /// <summary>
        /// 能量效率
        /// </summary>
        public decimal EnergyEfficiency { get; set; }

        /// <summary>
        /// 电压效率
        /// </summary>
        public decimal VoltageEfficiency { get; set; }

        /// <summary>
        /// 电解液使用率
        /// </summary>
        public decimal ElectrolyteUtilization { get; set; }

        /// <summary>
        /// 绝缘电阻
        /// </summary>
        public decimal InsulationResistance { get; set; }


        /// <summary>
        /// 内阻变化率
        /// </summary>
        public decimal InternalResistanceChangeRate
        {
            get
            {
                if (this.InternalResistanceBefore == 0 || this.InternalResistanceBefore == null)
                {
                    return 0;
                }

                return (this.InternalResistanceAfter - this.InternalResistanceBefore).GetValueOrDefault() * 100 / this.InternalResistanceAfter.GetValueOrDefault();
            }
        }

        /// <summary>
        /// 电堆评级分数
        /// </summary>
        public decimal Scores { get; set; }

        public string Remark { get; set; }

        public LevelEnum? Level { get; set; }

        public string LevelString
        {
            get
            {
                return this.Level?.ToString();
            }
        }

        #endregion


        public List<MaterialRecordSimplyInfoDto> MaterialRecordSimplyInfos
        {
            get;
            set;
        }

        public List<UploadUrlInfos> UploadUrls { get; set; }
    }
}
