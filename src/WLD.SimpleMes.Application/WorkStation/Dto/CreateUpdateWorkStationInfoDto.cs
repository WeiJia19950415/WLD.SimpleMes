using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkStation.Dto
{
    public class CreateUpdateWorkStationInfoDto : EntityDto<long>
    {

        public int? TenantId { get; set; }

        /// <summary>
        /// 工作中心名称
        /// </summary>
        [Required(ErrorMessage = "请输入工位名称")]
        [MaxLength(50, ErrorMessage = "工位名称最多不超过25个汉字")]
        public string WorkStationName { get; set; }

        /// <summary>
        /// 工作中心编号
        /// </summary>
        [Required(ErrorMessage = "请输入工位编号")]
        [MaxLength(50, ErrorMessage = "工位编号最多不超过25个汉字")]
        public string WorkStationNumber { get; set; }

        /// <summary>
        /// 所属车间ID
        /// </summary>
        public long BelongWorkShopId { get; set; }


        /// <summary>
        /// 所属产线id
        /// </summary>
        public long BelongProductLineId { get; set; }

        public bool IsShared { get; set; }
    }
}
