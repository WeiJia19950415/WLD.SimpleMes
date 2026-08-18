using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Material.Dto
{
    public class MaterialBatchNumberRulerDto : EntityDto<long>
    {
        public long MaterialCategoryInfoId { get; set; }
        public string MateriaCategoryCode { get; set; }

        public string GenerateType { get; set; }

        public string MateriaCategoryName { get; set; }

        /// <summary>
        /// 是否为序列号
        /// </summary>
        public bool IsSerailNumber { get; set; }

        /// <summary>
        /// 流水号规则
        /// </summary>
        public FlowNumberRulerEnum FlowNumberRuler { get; set; }

        /// <summary>
        /// 流水号长度
        /// </summary>
        public int FlowNumberRulerLength { get; set; }

        /// <summary>
        /// 按产线计算流水数
        /// </summary>
        public bool ComputePerProductLine { get; set; }
    }
}
