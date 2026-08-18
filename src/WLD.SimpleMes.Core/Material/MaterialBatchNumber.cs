using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Material.SerialNumberGenerator;

namespace WLD.SimpleMes.Material
{
    /// <summary>
    /// 物料序列号/批次号信息
    /// </summary>
    public class MaterialBatchNumber : Entity<long>, ICreationAudited, IHasCreationTime
    {
        /// <summary>
        /// 归属物料
        /// </summary>
        public long MaterialId { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        public string MaterialNumber { get; set; }

        /// <summary>
        /// 批次编码：每种类型的批次编码生成规则不一致
        /// </summary>
        public string BatchNumber { get; set; }

        /// <summary>
        /// 流水号
        /// </summary>
        public int FlowNumber { get; set; }

        /// <summary>
        /// 是否为序列号，如果为序列号则全局唯一
        /// </summary>
        public bool IsSerialsNumber { get; set; }

        /// <summary>
        /// 打印次数
        /// </summary>
        public int PrintTimes { get; set; }

        /// <summary>
        /// 相关物料数量 默认为1
        /// </summary>
        public decimal MatrialCount { get; set; } = 1;

        /// <summary>
        /// 订单来源
        /// 生产订单：则需要进行关联检查 WO
        /// 入库单据：PO-
        /// </summary>
        public string FromOrderNumber { get; set; }

        /// <summary>
        /// ERP中的原材料批次号
        /// </summary>
        public string FromErpBatchNumber { get; set; }

        /// <summary>
        /// 供应商信息【如果为自制件，则显示自制】
        /// </summary>
        public string Suppiler { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public long? CreatorUserId { get; set; }

        /// <summary>
        /// 创建人【可以是多个，存储姓名】
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建人【可以是多个，存储ID值】
        /// </summary>
        public string CreatorIds { get; set; }

        /// <summary>
        /// 创建工位
        /// </summary>
        public long? CreateWorkStationId { get; set; }

        /// <summary>
        /// 创建工位名称
        /// </summary>
        public string CreateWorkStationName { get; set; }

        /// <summary>
        /// 创建产线
        /// </summary>
        public long? CreateProductLineId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }


        /// <summary>
        /// 包装单位
        /// </summary>
        public string WrapUniteName { get; set; }

        /// <summary>
        /// 投入物料数量
        /// </summary>
        public decimal BOMMaterialCount { get; set; }

        /// <summary>
        /// 投入数量单位
        /// </summary>
        public string BOMMaterialUnitName { get; set; }

        /// <summary>
        /// 最后一次打印时间
        /// </summary>
        public DateTime? LastPrintTime { get; set; }

        /// <summary>
        /// 物料状态
        /// </summary>
        public MaterialStatuEnum? MaterialStatu { get; set; } = Material.MaterialStatuEnum.可用;

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

    }
}
