using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.LineSideWarehouse;

namespace SC.SimpleMes.OnLineStock.Dto
{
    [AutoMap(typeof(View_LineSideMaterialOperatorRecord))]
    public class View_LineSideMaterialOperatorRecordDto
    {
        /// <summary>
        /// 操作物料Id
        /// </summary>
        public long LineSideMaterialInfoId { get; set; }

        public string MaterialName { get; set; }

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
        /// 办理人员
        /// </summary>
        public string HandleUserName { get; set; }

        /// <summary>
        /// 办理人员
        /// </summary>
        public long HandleUserId { get; set; }

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

        /// <summary>
        /// 操作类型
        /// </summary>
        public string StockOperatoerTypeStr { get { 
            return this.StockOperatoerType.ToString();
            } }
    }
}
