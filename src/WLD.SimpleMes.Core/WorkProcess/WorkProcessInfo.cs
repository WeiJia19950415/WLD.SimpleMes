using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Material;

namespace WLD.SimpleMes.WorkProcess
{
    /// <summary>
    /// 工序信息
    /// </summary>
    public class WorkProcessInfo : FullAuditedEntity<long>, IMayHaveTenant, IExtendableObject
    {
        protected const string ConfigMaterialInfos = "ConfigMaterialInfos";
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

        /// <summary>
        /// 标准工时
        /// </summary>
        public decimal? StandWorkTime { get; set; }

        /// <summary>
        /// 工序-工位外键关系表
        /// </summary>
        public List<WorkProcessStationRelation> WorkProcessStationRelations { get; set; }

        public string ExtensionData { get; set; }

        public void SetConfigMaterials(List<long> workProcessMaterialConfigs)
        {
            this.SetData(ConfigMaterialInfos, workProcessMaterialConfigs.Distinct().ToList());
        }

        public List<long> GetConfigMaterials()
        {
            return this.GetData<List<long>>(ConfigMaterialInfos);
        }
    }

    public class WorkProcessMaterialConfig
    {
        public long MaterialId { get; set; }
        public string MaterialNumber { get; set; }
        public string MaterialName { get; set; }
    }

    /// <summary>
    /// 工序权重
    /// </summary>
    [Flags]
    public enum WorkProcessPowerTypeEnum
    {
        /// <summary>
        /// 普通工序
        /// </summary>
        Common = 1,

        /// <summary>
        /// 重要工序
        /// </summary>
        Important = 2,

        /// <summary>
        /// 关键工序
        /// </summary>
        KeyPoint = 4,
    }

    /// <summary>
    /// 工序类型
    /// </summary>
    public enum WorkProcessTypeEnum
    {
        前置物料准备工序 = 0,
        标准工序 = 1,
        IPQC = 2,
        FQC = 3,
        /// <summary>
        /// 维修
        /// </summary>
        返修工序 = 4
    }
}
