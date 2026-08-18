using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkProcess.Dto
{
    /// <summary>
    /// 工序Dto对象
    /// </summary>
    public class WorkProcessInfoDto : EntityDto<long>
    {
        public WorkProcessInfoDto()
        {
            this.BelongWorkStationsIds = new List<List<long>>();
            this.BelongWorkStationNames = new List<string>();
        }
        public int? TenantId { get; set; }

        /// <summary>
        /// 工序名称
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
        /// 工序权重 
        /// </summary>
        public WorkProcessPowerTypeEnum? WorkProcessPowerType { get; set; }

        /// <summary>
        /// 工序类型
        /// </summary>

        public WorkProcessTypeEnum WorkProcessType { get; set; }

        public string WorkProcessTypeStr
        {
            get
            {
                return this.WorkProcessType.ToString();
            }
        }
        /// <summary>
        /// 标准工时
        /// </summary>
        public decimal? StandWorkTime { get; set; }

        /// <summary>
        /// 归属的工位信息
        /// </summary>
        public List<string> BelongWorkStationNames { get; set; }

        /// <summary>
        /// 归属于哪些工位
        /// </summary>
        public List<List<long>> BelongWorkStationsIds { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsDone { get; set; }

        public long CurrentWorkStationId { get; set; }
    }
}
