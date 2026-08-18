using Abp.Application.Services.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.JHTOrganzations.Dto
{
    public class OrganizationUnitTreeDto : EntityDto<long>
    {
        public OrganizationUnitTreeDto()
        {
            ChildrenDepart = new List<OrganizationUnitTreeDto>();
        }
        /// <summary>
        /// 部门名称
        /// </summary>       
        public string ShortName { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public long SortNumber { get; set; }

        /// <summary>
        /// 下级
        /// </summary>
        [JsonProperty("children")]
        public List<OrganizationUnitTreeDto> ChildrenDepart { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        [JsonProperty("spread")]
        public bool IsSpread { get; set; } = false;
        /// <summary>
        /// 是否选中
        /// </summary>
        [JsonProperty("checked")]
        public bool IsChecked { get; set; } = false;
        /// <summary>
        /// 是否禁用
        /// </summary>
        [JsonProperty("disabled")]
        public bool IsDisabled { get; set; } = false;

        public bool IsLeaf { get; set; } = false;
    }
}

