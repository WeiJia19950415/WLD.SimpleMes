using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.WorkOrder.DTO
{
    public class WorkOrderBomItemDto : EntityDto<long>
    {
        /// <summary>
        /// 归属的工单BOM
        /// </summary>
        public long BelongWorkOrderBomId { get; set; }

        /// <summary>
        /// 工序编号
        /// </summary>
        public string BelongWorkProcessNumber { get; set; }

        /// <summary>
        /// 所属的工序
        /// </summary>
        public long BelongWorkProcessId { get; set; }

        public string InputMaterialName { get; set; }
        
        /// <summary>
        /// 投入的物料
        /// </summary>
        public long InputMaterialId { get; set; }

        /// <summary>
        /// 投入的物料编号
        /// </summary>
        public string InputMaterialNumber { get; set; }

        /// <summary>
        /// 投入数量
        /// </summary>
        public decimal InputMaterialCount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public long? CreatorUserId { get; set; }

        public string InputMaterialUnitName { get; set; }

        public string Specification { get; set; }
    }
}
