using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.WorkProcess;

namespace WLD.SimpleMes.DynamicForms.DTO
{
    public class FormInfoRecordDto : EntityDto<long>
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
        /// 归属工序Id
        /// </summary>
        public long BelongWorkProcessId { get; set; }

        /// <summary>
        /// 归属工序号
        /// </summary>
        public string BelongWorkProcessNumber { get; set; }

        /// <summary>
        /// 所属工序名称
        /// </summary>
        public string WorkProcessName { get; set; }

        /// <summary>
        /// 归属表单
        /// </summary>
        public long BelongFormId { get; set; }

        /// <summary>
        /// 表单记录
        /// </summary>
        public string FormRecordData { get; set; }

        /// <summary>
        /// 操作人员
        /// </summary>
        public long OperatorUserId { get; set; }

        public string Operator { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperatorTime { get; set; }

        /// <summary>
        /// 关联的表单设计信息
        /// </summary>
        public FormTemplateInfoDto  FormTemplateInfo { get; set; }

        /// <summary>
        /// 表单用途
        /// </summary>
        public FormUseTypeEnum FormUseType { get; set; }

        /// <summary>
        /// 是否为草稿
        /// </summary>
        public bool IsDraft { get; set; } = false;
    }
}
