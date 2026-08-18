using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Material
{
    /// <summary>
    /// 批次物料操作记录
    /// </summary>
    public class ERPInStockInfoOperateRecord : Entity<long>
    {
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialNumber { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 操作人员Id
        /// </summary>
        public long OperatorId { get; set; }

        /// <summary>
        /// 操作人员
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string OperateDesp { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateTime { get; set; }
    }
}
