using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkProcessSet.Dto
{
    public class WorkProcessSetInfoDto : EntityDto<long>
    {
        /// <summary>
        /// 工艺名称
        /// </summary>
        public string SetName { get; set; }

        /// <summary>
        /// 工艺版本
        /// </summary>
        public string SetVersion { get; set; }

        /// <summary>
        /// 工艺数据
        /// </summary>
        public string GraphData { get; set; }

        /// <summary>
        /// 版本描述
        /// </summary>
        public string Descreption { get; set; }

        public int? TenantId { get; set; }
    }
}
