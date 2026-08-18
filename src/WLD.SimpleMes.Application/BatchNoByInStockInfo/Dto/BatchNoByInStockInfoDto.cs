using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.BatchNoByInStockInfo.Dto
{
    public class BatchNoByInStockInfoDto : EntityDto<long>
    {
        /// <summary>
        /// 入库类型
        /// </summary>
        public string SourceType { get; set; }

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
        /// 是否打印过
        /// </summary>
        public bool WhetherPrint { get; set; }

        /// <summary>
        /// 销售订单号
        /// </summary>
        public string FSourceBillNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
