using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.DTO
{
    /// <summary>
    /// 通用分页请求处理
    /// </summary>
    public class CommonPageRequestDto : PagedResultRequestDto
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 真实的查询条件信息
        /// </summary>
        public Object QueryConditionObj { get; set; }
    }

    /// <summary>
    /// 通用条件数据
    /// </summary>
    public class CommonConditionData
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }
    }
}
