using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkProcess
{
    /// <summary>
    /// 工序历史操作记录
    /// </summary>
    public class WorkProcessMaterialRecordHistory : Entity<long>
    {
        /// <summary>
        /// 车间Id
        /// </summary>
        public long WrokShopId { get; set; }

        /// <summary>
        /// 产线Id
        /// </summary>
        public long ProductLineId { get; set; }


        /// <summary>
        /// 关联工单编号
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 归属产品的序列号
        /// </summary>
        public string ProductBatchNumber { get; set; }

        /// <summary>
        /// 工序Id
        /// </summary>
        public long WorkProcessId { get; set; }


        public string WorkProcessName { get; set; }

        /// <summary>
        /// 所属工位
        /// </summary>
        public long WorkStationId { get; set; }

        /// <summary>
        /// 所属工位名称
        /// </summary>
        public string WorkStationName { get; set; }


        /// <summary>
        /// 投入物料
        /// </summary>
        public long InputMaterilId { get; set; }

        /// <summary>
        /// 投入物料编码
        /// </summary>
        public string InputMaterialNumber { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime WarehousingTime { get; set; }

        /// <summary>
        /// 库存物料批次号
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// 投入物料名称
        /// </summary>
        public string InputMaterialName { get; set; }

        /// <summary>
        /// 投入物料批次号
        /// </summary>
        public string InputMaterialBatchNumber { get; set; }

        /// <summary>
        /// 投入物料数量
        /// </summary>
        public decimal? InputMaterialCount { get; set; }

        /// <summary>
        /// 投放单位
        /// </summary>
        public string InputUnitName { get; set; }

        /// <summary>
        /// 基于BOM单位的计算量
        /// </summary>
        public decimal? BOMMaterialCount { get; set; }

        /// <summary>
        /// 基于BOM的单位
        /// </summary>
        public string BOMUnitName { get; set; }

        /// <summary>
        /// 超放投入量
        /// </summary>
        public decimal? OutRangeCount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 变化原因【新增、修改数量、移除】
        /// </summary>
        public string  ChangeReason { get; set; }

    }
}
