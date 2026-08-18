using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Report.Dto
{
    public class DDWeekOnePassRateReportDto : EntityDto<long>
    {
        public long MaterialId { get; set; }

        public string MaterialName { get; set; }

        public string MaterialNumber { get; set; }

        public long ProductLineId { get; set; }
        public string ProductLineName { get; set; }

        /// <summary>
        /// 测试电堆总数
        /// </summary>
        public decimal TotalTestCount { get; set; }

        /// <summary>
        /// 合格数量
        /// </summary>
        public decimal PassCount { get; set; }

        /// <summary>
        /// 返修电堆数量
        /// </summary>
        public decimal RepairedTestCount { get; set; }


        /// <summary>
        /// 第一次测试的电堆
        /// </summary>
        public decimal NormalTestCount { get; set; }

        /// <summary>
        /// 一次测试通过的电堆
        /// </summary>
        public decimal NoramlPassCount { get; set; }

        /// <summary>
        /// 返修测试通过电堆数量
        /// </summary>
        public decimal RepairedPassDDCount { get; set; }

        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime DataDate { get; set; }

        /// <summary>
        /// 数据产生日期
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 所属周
        /// </summary>
        public int BelongWeek { get; set; }

        /// <summary>
        /// 所属年
        /// </summary>
        public int BelongYear { get; set; }

        /// <summary>
        /// 所属月
        /// </summary>
        public int BelongMonth { get; set; }

        public string OnePassRate
        {
            get
            {
                if (NormalTestCount == 0)
                {
                    return "0.00 %";
                }

                return (NoramlPassCount / NormalTestCount * 100).ToString("0.00") + "%";
            }
        }

        public string PassRate
        {
            get
            {
                if (TotalTestCount > 0)
                {
                    return (PassCount / TotalTestCount * 100).ToString("0.00") + "%";
                }
                else
                {
                    return "0.00 %";
                }
            }
        }
    }
}
