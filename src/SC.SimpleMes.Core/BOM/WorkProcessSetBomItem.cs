using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Material;
using SC.SimpleMes.WorkProcess;

namespace SC.SimpleMes.BOM
{
    public class WorkProcessSetBomItem : Entity<long>
    {
        /// <summary>
        /// 归属的工艺BOMId
        /// </summary>
        public long BelongWorkProcessSetBomId { get; set; }

        /// <summary>
        /// 所属的工序
        /// </summary>
        public long BelongWorkProcessId { get; set; }

        /// <summary>
        /// 所属工序
        /// </summary>
        public WorkProcessInfo BelongWorkProcess { get; set; }

        /// <summary>
        /// 投入的物料
        /// </summary>
        public long InputMaterialId { get; set; }

        /// <summary>
        /// 投入物料信息
        /// </summary>
        public MaterialInfo InputMaterial { get; set; }

        /// <summary>
        /// 投入数量
        /// </summary>
        public decimal InputMaterialCount { get; set; }
    }
}
