using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Material
{
    /// <summary>
    /// ERP同步过来的入库信息
    /// </summary>
    public class ERPInStockInfo : Entity<long>, ISoftDelete
    {
        /// <summary>
        /// 入库类型
        /// </summary>
        public ERPInStockSourceType SourceType { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime WarehousingTime { get; set; }

        /// <summary>
        /// 入库单号
        /// </summary>
        public string WarehousingNumber { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        public string MaterialNumber { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 物料规格
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 物料单位
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 物料批次号
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// 接收数量
        /// 【询问下帆总】
        /// </summary>
        public decimal ReceiptQuantity { get; set; }

        /// <summary>
        /// 销售订单号
        /// </summary>
        public string FSourceBillNo { get; set; }

        /// <summary>
        /// 是否打印过
        /// </summary>
        public bool WhetherPrint { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 物料是否用尽
        /// </summary>
        public bool IsUsedOut { get; set; }
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 物料可用状态
        /// </summary>
        public MaterialStatuEnum MaterialStatu { get; set; } = MaterialStatuEnum.可用;
    }

    /// <summary>
    /// 物料可用状态
    /// </summary>
    public enum MaterialStatuEnum
    {
        可用 = 1,
        全部报废 = 2,
        封存 = 3,
    }

    public enum ERPInStockSourceType
    {
        外购入库 = 1,
        成品入库 = 2,
        委外入库 = 5,
        其它入库 = 10
    }
}
