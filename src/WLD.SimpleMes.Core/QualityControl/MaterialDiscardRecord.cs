using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.QualityControl
{
    /// <summary>
    /// 异常报废记录表
    /// </summary>
    public class MaterialDiscardRecord : Entity<long>
    {

        /// <summary>
        /// 工单编号
        /// </summary>
        public string WorkOrderNumber { get; set; }
        public long? ProblemRecordId { get; set; }
        /// <summary>
        /// 报废物料【包括在制品，成品，半成品】
        /// </summary>
        public string MaterialNumber { get; set; }

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
        /// 报废数量【BOM单位】
        /// </summary>
        public decimal DiccardCount { get; set; }

        /// <summary>
        /// 单位【BOM单位】
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 报废数量【按组装单位】
        /// </summary>
        public decimal DiccardWarpCount { get; set; }

        /// <summary>
        /// 组装单位
        /// </summary>
        public string WrapUnitName { get; set; }

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

        /// <summary>
        /// 记录日期
        /// </summary>
        public DateTime RecordDate { get; set; }
    }

    public enum DiscardTypeEnum
    {
        /// <summary>
        /// 由设计原因造成的物料报废
        /// </summary>
        设计异常 = 1,
        /// <summary>
        /// 材料异常
        /// </summary>
        来料异常 = 2,
        /// <summary>
        /// 设备异常
        /// </summary>
        设备异常 = 3,
        /// <summary>
        /// 人员操作异常造成
        /// </summary>
        操作异常 = 4,

        /// <summary>
        /// 售后更换
        /// </summary>
        售后更换 = 5,// 返修电堆
    }
}
