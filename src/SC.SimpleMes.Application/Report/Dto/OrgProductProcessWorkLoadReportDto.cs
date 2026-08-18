using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Report.Dto
{
    public class OrgProductProcessWorkLoadReportDto:Entity<long>
    {
        /// <summary>
        /// 产线ID
        /// </summary>
        public long ProductLineId { get; set; }

        /// <summary>
        /// 产线名称
        /// </summary>
        public string ProductLineName { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long DepartmentId { get; set; }


        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 工位Id
        /// </summary>
        public long WorkStationId { get; set; }

        /// <summary>
        /// 工位名称
        /// </summary>
        public string WorkStationName { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        public long WorkProcessId { get; set; }

        /// <summary>
        /// 工序名称
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// 物料ID
        /// </summary>
        public long MaterialInfoId { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialNumber { get; set; }

        /// <summary>
        /// 接收量
        /// </summary>
        public decimal ReceivedCount { get; set; }

        /// <summary>
        /// 首次完成量
        /// </summary>
        public decimal FirstFinishedCount { get; set; }

        /// <summary>
        /// 完成产品的数量
        /// </summary>
        public decimal FinishedProductCount { get; set; }

        /// <summary>
        /// 完成返修的产品数量
        /// </summary>
        public decimal FinishedRepairProductCount { get; set; }

        /// <summary>
        /// 统计时间
        /// </summary>
        public DateTime StaticDate { get; set; }
    }
}
