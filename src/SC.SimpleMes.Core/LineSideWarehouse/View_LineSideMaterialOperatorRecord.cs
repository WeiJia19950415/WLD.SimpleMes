using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.LineSideWarehouse
{
    public class View_LineSideMaterialOperatorRecord : Entity<long>
    {
        /// <summary>
        /// 操作物料Id
        /// </summary>
        public long LineSideMaterialInfoId { get; set; }

        public string MaterialName { get; set; }
        public string HandleUserName { get; set; }
        public string UnitName { get; set; }

        public string Specification { get; set; }

        public string ProjectNumber { get; set; }

        public string ProjectName { get; set; }

        /// <summary>
        /// 操作车间
        /// </summary>
        public long OperatorWorkShopId { get; set; }

        public string WorkShopName { get; set; }

        /// <summary>
        /// 关联的生产任务单
        /// </summary>
        public string WorkOrderNumber { get; set; }

        /// <summary>
        /// 操作人员
        /// </summary>
        public string OpertaorName { get; set; }

        /// <summary>
        /// 操作人员
        /// </summary>
        public long OpertaorId { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperatorTime { get; set; }

        /// <summary>
        /// 库存时间
        /// </summary>
        public DateTime OperatorStockTime { get; set; }

        /// <summary>
        /// 操作数量 允许为负值
        /// </summary>
        public decimal OperatorCount { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public StockOperatoerType StockOperatoerType { get; set; }
    }
}
