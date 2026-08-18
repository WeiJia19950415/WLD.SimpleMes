using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkProcessSetBom.Dto
{
    public class WorkProcessSetBomCacheDto : EntityDto<long>
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
        /// 所属物料
        /// </summary>
        public long MaterialId { get; set; }

        /// <summary>
        /// 所属物料编号
        /// </summary>
        public string MaterialNumber { get; set; }

        /// <summary>
        /// 工艺BOM下挂着的BOM信息
        /// </summary>
        public List<WorkProcessSetBomItemByShowDto> Item { get; set; }

    }
}
