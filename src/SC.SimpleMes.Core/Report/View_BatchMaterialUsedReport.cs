using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Material;

namespace SC.SimpleMes.Report
{
    /// <summary>
    /// 批次材料使用情况
    /// </summary>
    public class View_BatchMaterialUsedReport : Entity<string>
    {
        public string BatchNo { get; set; }
        public string MaterialNumber { get; set; }
        public string MaterialName { get; set; }
        public decimal ReceiptQuantity { get; set; }

        /// <summary>
        /// 成堆中使用情况
        /// </summary>
        public decimal DDUsedCount { get; set; }

        /// <summary>
        /// 前置准备工序使用情况
        /// </summary>
        public decimal PrepaireMaterialCount { get; set; }
        public int IsOverUsed { get; set; }
        public string UnitName { get; set; }

        public DateTime? FirstWarningTime { get; set; }
        public DateTime? LastWarningTime { get; set; }

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


        /// <summary>
        /// 物料可以状态
        /// </summary>
        public MaterialStatuEnum? MaterialStatu { get; set; }
    }
}
