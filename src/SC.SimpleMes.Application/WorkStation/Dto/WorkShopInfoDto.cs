using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.WorkStation.Dto
{
    public class WorkShopInfoDto : EntityDto<long>
    {
        /// <summary>
        /// 车间编号
        /// </summary>
        public string WorkShopNumber { get; set; }

        /// <summary>
        /// 车间名称 
        /// </summary>
        public string WorkShopName { get; set; }

        /// <summary>
        /// 所在工厂
        /// </summary>
        public int? TenantId { get; set; }
    }
}
