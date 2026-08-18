using Abp.Domain.Entities;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.WorkStation;

namespace SC.SimpleMes.LineSideWarehouse
{
    /// <summary>
    /// 加工操作记录
    /// </summary>
    public class LineSideMaterialOperatorRecord : Entity<long>, ISoftDelete
    {
        /// <summary>
        /// 操作物料Id
        /// </summary>
        public long LineSideMaterialInfoId { get; set; }

        /// <summary>
        /// 操作车间
        /// </summary>
        public long OperatorWorkShopId { get; set; }

        /// <summary>
        /// 关联的生产任务单
        /// </summary>
        public string WorkOrderNumber { get; set; }

        public string ProjectNumber { get; set; }

        public string ProjectName { get; set; }

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
        public long? HandleUserId { get; set; }

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

        public bool IsDeleted { get; set; }

        public void InStock(decimal OperatorCount, DateTime InStockTime, long LineSideMaterialInfoId, long OperatorWorkShopId, long OpertaorId)
        {
            if (OperatorCount <= 0)
            {
                throw new UserFriendlyException("入库数量不能为空！");
            }
            this.StockOperatoerType = StockOperatoerType.入库;
            this.OperatorStockTime = InStockTime;
            this.OperatorCount = OperatorCount;
            this.OperatorTime = DateTime.Now;
            this.LineSideMaterialInfoId = LineSideMaterialInfoId;
            this.OperatorWorkShopId = OperatorWorkShopId;
            this.OpertaorId = OpertaorId;
        }

        public void OutStock(decimal OperatorCount, DateTime InStockTime, long LineSideMaterialInfoId, long OperatorWorkShopId, long OpertaorId)
        {
            if (OperatorCount > 0)
            {
                this.OperatorCount = 0 - OperatorCount;
            }
            else
            {
                this.OperatorCount = OperatorCount;
            }

            this.StockOperatoerType = StockOperatoerType.出库;
            this.OperatorStockTime = InStockTime;
            this.OperatorTime = DateTime.Now;
            this.LineSideMaterialInfoId = LineSideMaterialInfoId;
            this.OperatorWorkShopId = OperatorWorkShopId;
            this.OpertaorId = OpertaorId;
        }
    }

    public enum StockOperatoerType
    {
        入库 = 1,
        出库 = 2,
    }
}
