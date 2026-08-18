using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Material.Dto
{
    public class ERPInStockInfoDto : EntityDto<long>
    {
        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        public string MaterialNumber { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 物料规格
        /// </summary>
        public string Specification { get; set; }

        public string UnitName { get; set; }

        /// <summary>
        /// 物料批次号【来自ERP的批次号】
        /// </summary>
        public string BatchNo { get; set; }



        public string FromErpBatchNumber
        {
            get
            {
                return this.BatchNo;
            }
        }

        /// <summary>
        /// 使用单位
        /// </summary>
        public string UseUnitName { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime WarehousingTime { get; set; }

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
        /// 物料工单消耗信息
        /// </summary>
        public string MaterialWorkOrderCostInfo { get; set; }
    }
}
