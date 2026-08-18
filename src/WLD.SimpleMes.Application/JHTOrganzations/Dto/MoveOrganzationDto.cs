using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.JHTOrganzations.Dto
{
    public class MoveOrganzationDto : EntityDto<long>
    {
        /// <summary>
        /// 新的父级部门ID
        /// </summary>
        public long? NewParentId { get; set; }

    }
}

