using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.WorkProcess;

namespace WLD.SimpleMes.DynamicForms
{
    /// <summary>
    /// 电堆重要信息
    /// </summary>
    public class DDImportantInfos : BaseSaveEntityInfo, IExtendableObject, ICloneable
    {
        public const string MaterialRecordInfos = "MaterialRecordInfos";

        public const string UploadImagUrls = "UploadImagUrls";

        #region 检验操作相关

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
        /// <summary>
        /// 湿度
        /// </summary>
        public decimal Humidity { get; set; }



        /// <summary>
        /// 运行压力
        /// </summary>
        public decimal OperatingPressure { get; set; }

        /// <summary>
        /// 运行温度
        /// </summary>
        public decimal OperatingTemperature { get; set; }

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
        /// 电解液浓度
        /// </summary>
        public decimal Concentration { get; set; }

        /// <summary>
        /// 电解液体积
        /// </summary>
        public decimal UnilateralVolume { get; set; }

        #endregion

        #region 测试结果

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
        public decimal? InternalResistanceAfter { get; set; }

        /// <summary>
        /// 测试前内阻
        /// </summary>
        public decimal? InternalResistanceBefore { get; set; }

        /// <summary>
        /// 开路内阻
        /// </summary>
        public decimal OnInternalResistance { get; set; }

        /// <summary>
        /// 电堆内阻
        /// </summary>
        public decimal InternalResistance { get; set; }

        /// <summary>
        /// 绝缘电阻
        /// </summary>
        public decimal InsulationResistance { get;set; }

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


        public string ExtensionData { get; set; }

        /// <summary>
        /// 设置图片路径
        /// </summary>
        /// <param name="imgUrls"></param>
        public void SetUploadImgUrls(List<UploadUrlInfos> imgUrls)
        {
            this.SetData(UploadImagUrls, imgUrls);
        }

        /// <summary>
        /// 保存相关物料信息
        /// </summary>
        /// <param name="materialRecordSimplyInfos"></param>
        public void SetMaterialRecordInfo(List<MaterialRecordSimplyInfo> materialRecordSimplyInfos)
        {
            var distinctArray = materialRecordSimplyInfos.GroupBy(p => new { p.BatchNo, p.MaterialNumber, p.MatreialName, p.Supplier, p.WarehousingTime })
                 .Select(p => new MaterialRecordSimplyInfo()
                 {
                     BatchNo = p.Key.BatchNo,
                     MaterialNumber = p.Key.MaterialNumber,
                     MatreialName = p.Key.MatreialName,
                     Supplier = p.Key.Supplier,
                     WarehousingTime = p.Key.WarehousingTime,
                 }).ToList();
            this.SetData(MaterialRecordInfos, distinctArray);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public List<UploadUrlInfos> UploadUrls
        {
            get
            {
                return this.GetData<List<UploadUrlInfos>>(UploadImagUrls);
            }
        }

        public List<MaterialRecordSimplyInfo> MaterialRecordSimplyInfos
        {
            get
            {
                return this.GetData<List<MaterialRecordSimplyInfo>>(MaterialRecordInfos);
            }
        }

        public class UploadUrlInfos
        {
            public string Name { get; set; }

            public string Url { get; set; }
        }

        public class MaterialRecordSimplyInfo
        {
            /// <summary>
            /// 物料名称
            /// </summary>
            public string MatreialName { get; set; }

            /// <summary>
            /// 物料编号
            /// </summary>
            public string MaterialNumber { get; set; }

            /// <summary>
            /// 供应商
            /// </summary>
            public string Supplier { get; set; }

            /// <summary>
            /// 入库时间
            /// </summary>
            public DateTime WarehousingTime { get; set; }

            /// <summary>
            /// 入库批次号
            /// </summary>
            public string BatchNo { get; set; }
        }

        /// <summary>
        /// 电堆等级
        /// </summary>
        public enum LevelEnum
        {
            Ⅰ = 1,
            Ⅱ = 2,
            Ⅲ = 3,
            Ⅳ = 4
        }
    }
}
