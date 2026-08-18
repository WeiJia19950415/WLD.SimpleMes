using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.QualityControl.Dto
{
    public class View_ProblemRecordDto : EntityDto<string>
    {

        public long? RecordId { get; set; }
        /// <summary>
        /// 工单编号
        /// </summary>
        public string WorkOrderNumber { get; set; }

        public long MaterialId { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        public string MaterialNumber { get; set; }

        /// <summary>
        /// 产品批次号
        /// </summary>
        public string BatchMaterilaNumber { get; set; }

        public long? ProductLineId { get; set; }

        /// <summary>
        /// 产线名称
        /// </summary>
        public string ProductLineName { get; set; }

        public long WorkProcessId { get; set; }

        /// <summary>
        /// 工序名称
        /// </summary>
        public string WorkProcessName { get; set; }

        /// <summary>
        /// 缺陷分类
        /// </summary>
        public string ProbleCategoryFullName { get; set; }
        /// <summary>
        /// 问题名称
        /// </summary>
        public string ProbleName { get; set; }

        /// <summary>
        /// 是否处理
        /// </summary>
        public bool IsClosed { get; set; }


        /// <summary>
        /// 具体描述
        /// </summary>
        public string DetailDescretion { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Createor { get; set; }

        /// <summary>
        /// 处理方式
        /// </summary>
        public string ProblemDealType { get; set; }

        /// <summary>
        /// 处理人员
        /// </summary>
        public string OperatorName { get; set; }


        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? DealTime { get; set; }
        public long? ProblemDefineId { get; set; }
        public string CategoryCode { get; set; }

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
        /// 问题数量
        /// </summary>
        public decimal? ProblemCount { get; set; } = 1;

        public string UnitName { get; set; }

        public string ResponsibleWorkProcessName { get; set; }

        public string ResponsibleDepartmentName { get; set; }
    }
}
