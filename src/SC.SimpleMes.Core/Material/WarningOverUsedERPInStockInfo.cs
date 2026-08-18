using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Material
{
    /// <summary>
    /// 批次物料超用预警报表
    /// </summary>
    public class WarningOverUsedERPInStockInfo : Entity<long>
    {
        /// <summary>
        /// 入库批次号
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// 首次预警时间
        /// </summary>
        public DateTime FirstWarningTime { get; set; }

        /// <summary>
        /// 预警最后更新时间
        /// </summary>
        public DateTime LastWarningTime { get; set; }

        /// <summary>
        /// 实际使用量
        /// </summary>
        public decimal ActualUseAmount { get; set; }

        /// <summary>
        /// 第一次关注的人员
        /// </summary>
        public string FirstNoticeUser { get; set; }

        public long? FirstNoticeUserId { get; set; }

        /// <summary>
        /// 所属班组
        /// </summary>
        public string BelongDepartmentName { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 备注人员姓名
        /// </summary>
        public string RemarkUserName { get; set; }

        /// <summary>
        /// 备注人员
        /// </summary>
        public long? RemarkUserId { get; set; }

        /// <summary>
        /// 备注时间
        /// </summary>
        public DateTime? RemarkDateTime { get; set; }


    }
}
