using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.WorkProcess
{
    /// <summary>
    /// 工序集合详情配置
    /// </summary>
    public class WorkProcessSetDetail
    {
        public long BelongWorkProcessSetId { get; set; }

        public long BelongWorkProcessInfoId { get; set; }
        
        /// <summary>
        /// 图中节点Id
        /// </summary>
        public string NodeId { get; set; }

        /// <summary>
        /// 配置图中的父节点Id
        /// </summary>
        public List<string> ParentNodeId { get; set; }

        /// <summary>
        /// 工序名称
        /// </summary>
        public string WorkProcessName { get; set; }

        /// <summary>
        /// 工序编号
        /// </summary>
        public string WorkProcessNumber { get; set; }

        /// <summary>
        /// 排序编号【全局唯一】
        /// </summary>
        public string SortNumber { get; set; }
    }
}
