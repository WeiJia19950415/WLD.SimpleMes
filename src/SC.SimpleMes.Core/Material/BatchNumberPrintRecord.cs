using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Material
{
    public class BatchNumberPrintRecord : Entity<long>
    {
        /// <summary>
        /// 物料编号
        /// </summary>
        public string MaterialNumber { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 打印批次号
        /// </summary>
        public string BatchNumber { get; set; }

        /// <summary>
        /// 打印数量
        /// </summary>
        public int PrintCounts { get; set; }

        /// <summary>
        /// 打印时间
        /// </summary>
        public DateTime PrintTime { get; set; }

        /// <summary>
        /// 打印机器
        /// </summary>
        public string PrintMachine { get; set; }

        /// <summary>
        /// 打印人员Id
        /// </summary>
        public long OperatorId { get; set; }

        /// <summary>
        /// 操作人员
        /// </summary>
        public string OperatorName { get; set; }
    }
}
