using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.DynamicForms
{
    public abstract class BaseSaveEntityInfo : Entity<long>
    {
        /// <summary>
        /// 归属工单
        /// </summary>
        public string BelongOrderNumber { get; set; }

        /// <summary>
        /// 物料Id
        /// </summary>
        public long MaterialId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectNumber { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string MatreialName { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        public string MaterialNumber { get; set; }

        /// <summary>
        /// 归属物料序列号
        /// </summary>
        public string BelongMaterialBatchNumber { get; set; }

        /// <summary>
        /// 归属产线Id
        /// </summary>
        public long BelongProductLineId { get; set; }

        /// <summary>
        /// 归属产线名称
        /// </summary>
        public string BelongProductLineName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime RecordDate { get; set; }
    }
}
