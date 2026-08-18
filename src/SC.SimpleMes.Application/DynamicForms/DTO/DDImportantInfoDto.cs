using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SC.SimpleMes.DynamicForms.DDImportantInfos;

namespace SC.SimpleMes.DynamicForms.DTO
{
    /// <summary>
    /// 电堆关键性能报表
    /// </summary>
    public class DDImportantInfoDto : EntityDto<long>
    {
        /// <summary>
        /// 归属工单
        /// </summary>
        public string BelongOrderNumber { get; set; }

        /// <summary>
        /// 物料Id
        /// </summary>
        public long MaterialId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string MatreialName { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        public string MaterialNumber { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectNumber { get; set; }

        /// <summary>
        /// 归属物料序列号
        /// </summary>
        public string BelongMaterialBatchNumber { get; set; }

        /// <summary>
        /// 归属产线Id
        /// </summary>
        public long BelongProductLineId { get; set; }

        /// <summary>
        /// 归属产线名称
        /// </summary>
        public string BelongProductLineName { get; set; }

        #region 检验操作相关

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime ProduceDateTime { get; set; }
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
        public DateTime? AuditeDate { get; set; }

        /// <summary>
        /// 检查人员
        /// </summary>
        public string Checkor { get; set; }

        /// <summary>
        /// 检验人员ID
        /// </summary>
        public long CheckorId { get; set; }

        /// <summary>
        /// 审核人员
        /// </summary>
        public string Auditor { get; set; }

        /// <summary>
        /// 审核人员ID
        /// </summary>
        public long? AuditorId { get; set; }

        /// <summary>
        /// 是否审核
        /// </summary>
        public bool IsAudited { get; set; }

        #endregion


        #region 电堆参数

        /// <summary>
        /// 压堆厚度
        /// </summary>
        public decimal PressThickness { get; set; }

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
        /// 电堆登级【】
        /// </summary>
        public string LevelString { get; set; }

        /// <summary>
        /// 库伦效率【】
        /// </summary>
        public decimal CoulombEfficiency { get; set; }

        /// <summary>
        /// 电压效率
        /// </summary>
        public decimal VoltageEfficiency { get; set; }

        /// <summary>
        /// 电流密度
        /// </summary>
        public decimal CurrentDensity { get; set; }

        /// <summary>
        /// 能量效率
        /// </summary>
        public decimal EnergyEfficiency { get; set; }

        /// <summary>
        /// 电解液利用率
        /// </summary>
        public decimal ElectrolyteUtilization { get; set; }

        /// <summary>
        /// 测试后内阻
        /// </summary>
        public decimal InternalResistanceAfter { get; set; }

        /// <summary>
        /// 测试前内阻
        /// </summary>
        public decimal InternalResistanceBefore { get; set; }

        /// <summary>
        /// 绝缘内阻
        /// </summary>
        public decimal InsulationResistance { get; set; }


        /// <summary>
        /// 开路内阻
        /// </summary>
        public decimal OnInternalResistance { get; set; }

        /// <summary>
        /// 电堆内阻
        /// </summary>
        public decimal InternalResistance { get; set; }


        /// <summary>
        /// 电堆评级分数
        /// </summary>
        public decimal Scores { get; set; }

        /// <summary>
        /// 电堆登级【】
        /// </summary>
        public LevelEnum? Level { get; set; }


        public string Remark { get; set; }

        #endregion

        public List<string> UploadUrls { get; set; }

        /// <summary>
        /// 使用到的物料信息
        /// </summary>
        public List<MaterialRecordSimplyInfo> MaterialRecords { get; set; }

        /// <summary>
        /// 是否入库
        /// </summary>
        public int IsInStock { get; set; }
    }
}
