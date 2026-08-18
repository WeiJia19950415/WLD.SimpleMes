using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkProcessSetBom.Dto
{
    public class WorkProcessSetBomDto : EntityDto<long>
    {
        /// <summary>
        /// 所属工艺Id
        /// </summary>
        public long BelongWorkProcessSetId { get; set; }

        /// <summary>
        /// 所属工艺名称
        /// </summary>
        public string BelongWorkProcessSetName { get; set; }

        /// <summary>
        /// 所属工艺版本
        /// </summary>
        public string BelongWorkProcessVersion { get; set; }

        /// <summary>
        /// 引用的标准BOMId
        /// </summary>
        public long ReferenceBomId { get; set; }

        /// <summary>
        /// 引用的标准BOM版本
        /// </summary>
        public string ReferenceBomVersion { get; set; }

        /// <summary>
        /// 所属物料
        /// </summary>
        public long MaterialId { get; set; }

        /// <summary>
        /// 所属物料编号
        /// </summary>
        public string MaterialNumber { get; set; } 
        
        /// <summary>
        /// 所属物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 工艺版本
        /// </summary>
        public string Version { get; set; }

  
    }
}
