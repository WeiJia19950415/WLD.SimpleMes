using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.WorkProcessSetBom.Dto
{
    /// <summary>
    /// 前端配置工艺BOM模型，此处ID为工序ID
    /// </summary>
    public class WorkProcessSetBomItemByShowDto : EntityDto<long>
    {

        public long ProcessId { get; set; }
        /// <summary>
        /// 工序Name
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// 工序编号
        /// </summary>
        public string ProcessNumber { get; set; }

        /// <summary>
        /// 能否跳过
        /// </summary>
        public bool CanJump { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 工序下的工艺BOM配置
        /// </summary>
        public List<ProcessBomItem> BomItem { get; set; }
    }

    public class ProcessBomItem
    {
        public long WorkProcessId { get; set; }
        public long FormMaterialId { get; set; }
        public string FormMaterialNumber { get; set; }
        public string FormMaterialName { get; set; }
        public string FormCount { get; set; }
        public string FormUnitName { get; set; }
        public string Specification { get; set; }
    }
}
