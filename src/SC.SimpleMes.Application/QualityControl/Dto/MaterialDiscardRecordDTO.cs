using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.QualityControl.Dto
{
    public class MaterialDiscardRecordDTO : EntityDto<long>
    {
        public long? ProblemRecordId { get; set; }

        public string WorkOrderNumber { get; set; }
        /// <summary>
        /// 报废物料【包括在制品，成品，半成品】
        /// </summary>
        public string MaterialNumber { get; set; }

        public string WorkProcessName { get; set; }

        /// <summary>
        /// 报废物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 加工批次号【非必填】
        /// </summary>
        public string BatchNumber { get; set; }

        /// <summary>
        /// 源批次号【非必填】
        /// </summary>
        public string ErpBatchNumber { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 报废数量
        /// </summary>
        public decimal DiccardCount { get; set; }

        /// <summary>
        /// 报废数量【按组装单位】
        /// </summary>
        public decimal DiccardWarpCount { get; set; }

        /// <summary>
        /// 组装单位
        /// </summary>
        public string WrapUnitName { get; set; }

        /// <summary>
        /// 单位【BOM单位】
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 记录人员
        /// </summary>
        public long RecordUserId { get; set; }

        /// <summary>
        /// 记录人员姓名
        /// </summary>
        public string RecordUserName { get; set; }

        /// <summary>
        /// 报废原因分类
        /// </summary>
        public DiscardTypeEnum DiscardType { get; set; }

        public string DiscardTypeString
        {
            get
            {
                return this.DiscardType.ToString();
            }
        }

        /// <summary>
        /// 报废原因描述
        /// </summary>
        public string DeiscardReasonDescreption { get; set; }

        /// <summary>
        /// 缺陷问题分类
        /// </summary>
        public long? ProblemDefineId { get; set; }

        /// <summary>
        /// 缺陷编码
        /// </summary>
        public string ProblemDefineNumber { get; set; }

        public string ProbleName { get; set; }

        /// <summary>
        /// 记录日期
        /// </summary>
        public DateTime RecordDate { get; set; }
        /// <summary>
        /// 报废车间
        /// </summary>
        public string ProductLineName { get; set; }

        /// <summary>
        /// 关联报废产品序列号/批次号
        /// </summary>
        public string BatchMaterilaNumber { get; set; }

        public long? BelongProductLineId { get; set; }

        /// <summary>
        /// 关联产品名称
        /// </summary>
        public string ProductMaterialName { get; set; }

        /// <summary>
        /// 关联产品编码
        /// </summary>
        public string ProductMaterialNumber { get; set; }
    }
}
