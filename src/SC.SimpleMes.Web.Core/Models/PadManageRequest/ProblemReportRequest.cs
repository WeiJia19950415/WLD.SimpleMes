using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.QualityControl;
using SC.SimpleMes.QualityControl.Dto;

namespace SC.SimpleMes.Models.PadManageRequest
{
    public class ProblemReportRequest : PadManageRequestModel
    {
        /// <summary>
        /// 相关图片
        /// </summary>
        public List<string> RelationImgs { get; set; }

        /// <summary>
        /// 问题具体描述
        /// </summary>
        public string DetailDescretion { get; set; }

        /// <summary>
        /// 归属问题id
        /// </summary>
        public long BelongProblemDefineId { get; set; }

        /// <summary>
        /// 责任部门
        /// </summary>
        public long? ResponsibleDepartmentId { get; set; }

        public long? BelongWorkProcessId { get; set; }


        public long? WorkStationId { get; set; }

        /// <summary>
        /// 问题定义编号
        /// </summary>
        public string QualityProblemDefineNumber { get; set; }

        /// <summary>
        /// 检查数量
        /// </summary>
        public decimal? CheckCount { get; set; } = 1;

        /// <summary>
        /// 问题数量
        /// </summary>
        public decimal? ProblemCount { get; set; } = 1;

        /// <summary>
        /// 包装单位
        /// </summary>

        public string WrapUnitName { get; set; }

        /// <summary>
        /// 检查数量
        /// </summary>
        public decimal? ProblemWarpCount { get; set; }
        /// <summary>
        /// 检查的组装数量
        /// </summary>
        public decimal? CheckWarpCount { get; set; }

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

        /// <summary>
        /// 处理记录
        /// </summary>
        public ProblemDealRecordDto DealRecordDto { get; set; }

        /// <summary>
        /// 报废原因分类
        /// </summary>
        public DiscardTypeEnum? DiscardType { get; set; }

    }
}
