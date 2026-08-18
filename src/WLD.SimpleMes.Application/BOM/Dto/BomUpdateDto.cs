using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace WLD.SimpleMes.BOM.Dto
{
    public class BomUpdateDto : EntityDto<long>
    {
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
        /// 版本编号
        /// </summary>
        public string Version { get; set; }


        public int? TenantId { get; set; }

        /// <summary>
        /// BOM内容
        /// </summary>
        public List<BomItemDto> BomItemDtos { get; set; }
    }
}
