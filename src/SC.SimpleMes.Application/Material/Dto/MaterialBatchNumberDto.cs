using Abp.Application.Services.Dto;
using Magicodes.ExporterAndImporter.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Material.SerialNumberGenerator;
using SC.SimpleMes.WorkOrder;
using SC.SimpleMes.WorkProcess.Dto;

namespace SC.SimpleMes.Material.Dto
{

    public class MaterialBatchNumberDto : EntityDto<long>
    {
        public long MaterialId { get; set; }

        public bool IsSerialsNumber { get; set; }

        public long? CreatorUserId { get; set; }


        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }

        public string MaterialNumber { get; set; }

        public string MaterialName { get; set; }

        /// <summary>
        /// 批次编码：每种类型的批次编码生成规则不一致
        /// </summary>
        public string BatchNumber { get; set; }


        /// <summary>
        /// ERP中的原材料批次号
        /// </summary>
        public string FromErpBatchNumber { get; set; }

        /// <summary>
        /// 打印次数
        /// </summary>
        public int PrintTimes { get; set; }

        /// <summary>
        /// 相关物料数量 默认为1
        /// </summary>
        public decimal? MatrialCount { get; set; } = 1;

        /// <summary>
        /// 包装单位
        /// </summary>
        public string WrapUniteName { get; set; } = "";


        /// <summary>
        /// 创建工位
        /// </summary>
        public long? WorkStationId { get; set; }

        /// <summary>
        /// 创建工位名称
        /// </summary>
        public string CreateWorkStationName { get; set; }

        /// <summary>
        /// 创建产线
        /// </summary>
        public long? CreateProductLineId { get; set; }

        /// <summary>
        /// 订单来源
        /// 生产订单：则需要进行关联检查
        /// 采购入库订单
        /// </summary>
        public string FromOrderNumber { get; set; }

        /// <summary>
        /// 最后一次打印时间
        /// </summary>
        public DateTime? LastPrintTime { get; set; }

        /// <summary>
        /// 投入物料数量(BOM单中的数量)
        /// </summary>
        public decimal? BOMMaterialCount { get; set; }

        /// <summary>
        /// 投入数量单位(BOM单中的单位)
        /// </summary>
        public string BOMUnitName { get; set; }

        /// <summary>
        /// 产品生产状态
        /// </summary>
        public ProduceStatusEnum? ProduceStatus { get; set; }

        public DateTime? CreationTime { get; set; }

        public WorkProcessInfoDto CurrentWorkProcess { get; set; }

        /// <summary>
        /// 已使用量
        /// </summary>
        public decimal InputMaterialCount { get; set; }

        /// <summary>
        /// 投入单位
        /// </summary>
        public string InputUnitName { get; }

        /// <summary>
        /// 该二维码是否被使用
        /// </summary>
        public int IsUsed { get; set; }

        public string IsUsedString
        {
            get
            {
                if (this.IsUsed == 1)
                {
                    return "已使用";
                }

                return "未使用";
            }
        }


        public long CurrentWorkStationId { get; set; }
        public bool IsLineMaterialInfo
        {
            get
            {
                if (!string.IsNullOrEmpty(BatchNumber))
                {
                    return this.BatchNumber.IndexOf(LineSideMaterialGenerator.WIP) >= 0;
                }

                return false;
            }
        }

        /// <summary>
        /// 是否返修
        /// </summary>
        public bool IsRepired { get; set; }

        public MaterialStatuEnum? MaterialStatu { get; set; }

        public MaterialBatchNumberDto Clone()
        {
            return (MaterialBatchNumberDto)this.MemberwiseClone();
        }
    }
}
