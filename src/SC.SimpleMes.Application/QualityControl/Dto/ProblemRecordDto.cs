using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.QualityControl.Dto
{
    public class ProblemRecordDto : EntityDto<long>
    {
        /// <summary>
        /// 责任判定人员
        /// </summary>
        public long? AuditorId { get; set; }

        /// <summary>
        /// 责任判定人员姓名
        /// </summary>
        public string AuditorName { get; set; }

        /// <summary>
        /// 判定时间
        /// </summary>
        public DateTime? AuditTime { get; set; }

        /// <summary>
        /// 原因分析
        /// </summary>
        public string ReasonAnlysis { get; set; }
        /// <summary>
        /// 检查数量
        /// </summary>
        public decimal? CheckCount { get; set; } = 1;

        /// <summary>
        /// 检查的组装数量
        /// </summary>
        public decimal? CheckWarpCount { get; set; }

        /// <summary>
        /// 问题数量
        /// </summary>
        public decimal? ProblemCount { get; set; } = 1;

        /// <summary>
        /// 问题数量【按包装单位】
        /// </summary>
        public decimal? ProblemWarpCount { get; set; }

        /// <summary>
        /// 包装单位
        /// </summary>

        public string WrapUnitName { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        public string MaterialNumber { get; set; }

        /// <summary>
        /// 加工单位
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 供应人员
        /// </summary>
        public string Supplier { get; set; }


        public long? WorkStationId { get; set; }
        /// <summary>
        /// 问题定义编号
        /// </summary>
        public string QualityProblemDefineNumber { get; set; }

        /// <summary>
        /// 父级代码
        /// </summary>
        public string BelongProblemCategoryCode { get; set; }

        public string BelongProblemCategoryFullName { get; set; }

        /// <summary>
        /// 归属问题id
        /// </summary>
        public long BelongProblemDefineId { get; set; }

        public string BelongProblemDefineName { get; set; }

        /// <summary>
        /// 问题具体描述
        /// </summary>
        public string DetailDescretion { get; set; }

        /// <summary>
        /// 所属产线
        /// </summary>
        public long? BelongProductLineId { get; set; }

        /// <summary>
        /// 所属工位
        /// </summary>
        public long? BelongWorkStaionId { get; set; }

        /// <summary>
        /// 发生工序Id
        /// </summary>
        public long? BelongWorkProcessId { get; set; }

        /// <summary>
        /// 发生工序的部门
        /// </summary>
        public long? ResponsibleWorkProcessId { get; set; }


        public long? ResponsibleDepartmentId { get; set; }
        /// <summary>
        /// 发送工序编号
        /// </summary>
        public string OnWorkProcessNumber { get; set; }

        /// <summary>
        /// 工序名称
        /// </summary>
        public string WorkProcessName { get; set; }

        /// <summary>
        /// 关联工单
        /// </summary>
        public string WorkOrderNumber { get; set; }

        /// <summary>
        /// 关联产品序列号
        /// </summary>
        public string BatchMaterilaNumber { get; set; }

        /// <summary>
        /// 相关图片
        /// </summary>
        public List<string> RelationImgs { get; set; }

        /// <summary>
        /// 是否生效
        /// </summary>
        public bool IsEffect { get; set; }

        /// <summary>
        /// 该问题是否关闭
        /// </summary>
        public bool IsClosed { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long? CreatorUserId { get; set; }

        /// <summary>
        /// 上报人
        /// </summary>
        public string Createor { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }


        public string ResponsibleWorkProcessName { get; set; }

        public string ResponsibleDepartmentName { get; set; }

        /// <summary>
        /// 处理记录
        /// </summary>
        public ProblemDealRecordDto DealRecordDto { get; set; }

        /// <summary>
        /// 报废原因
        /// </summary>
        public DiscardTypeEnum? DiscardType { get; set; }
    }
}
