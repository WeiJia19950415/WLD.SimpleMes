using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.BOM;

namespace SC.SimpleMes.WorkProcess
{
    /// <summary>
    ///  工艺集信息
    ///  工艺与产品绑定后，不允许进行修改，只允更改工艺版本
    /// </summary>
    public class WorkProcessSet : FullAuditedEntity<long>, IMayHaveTenant, IExtendableObject
    {
        public const string WorkProcessConfigs = "WorkProcessConfigs";
        /// <summary>
        /// 工艺名称
        /// </summary>
        public string SetName { get; set; }

        /// <summary>
        /// 工艺版本
        /// </summary>
        public string SetVersion { get; set; }

        /// <summary>
        /// 版本描述
        /// </summary>
        public string Descreption { get; set; }


        /// <summary>
        /// 图数据保存
        /// </summary>
        public string GraphData { get; set; }
        /// <summary>
        /// 工艺数据【保存数据库】
        /// </summary>
        public string ExtensionData { get; set; }

        public int? TenantId { get; set; }

        public List<WorkProcessSetBom> WorkProcessSetBoms { get; set; }
        /// <summary>
        /// 添加工艺工序
        /// </summary>
        public void SetWorkProcessSetConfigs(List<WorkProcessSetDetail> workProcessSetDetail)
        {
            this.SetWorkProcessSortNumber(workProcessSetDetail);
            this.SetData(WorkProcessConfigs, workProcessSetDetail.OrderBy(p => p.SortNumber).ToList());
        }

        /// <summary>
        /// 设置工艺排序顺序
        /// </summary>
        /// <param name="workProcessSetDetail"></param>
        public void SetWorkProcessSortNumber(List<WorkProcessSetDetail> workProcessSetDetail)
        {
            var firstNode = workProcessSetDetail.Where(p => p.ParentNodeId == null || p.ParentNodeId.Count == 0).ToList();
            // 仅适用于单流程的生产，不适合混合式的工艺路线
            int level = 1;
            for (int i = 0; i < firstNode.Count(); i++)
            {
                firstNode[i].SortNumber = $"{level}_{i}";

                setChildSortNumber(firstNode[i].NodeId, workProcessSetDetail, level + 1);
            }

        }

        private void setChildSortNumber(string nodeId, List<WorkProcessSetDetail> workProcessSetDetail, int level)
        {
            var nextNode = workProcessSetDetail.Where(p => p.ParentNodeId.Contains(nodeId)).ToList();
            if (nextNode != null)
            {
                for (int i = 0; i < nextNode.Count(); i++)
                {
                    nextNode[i].SortNumber = $"{level}_{i}";
                    setChildSortNumber(nextNode[i].NodeId, workProcessSetDetail, level + 1);
                }
            }
        }



        /// <summary>
        /// 获取工艺开始的首节点
        /// </summary>
        /// <returns></returns>
        public List<WorkProcessSetDetail> GetFirstWorkProcessId()
        {
            List<WorkProcessSetDetail> firstNodes = new List<WorkProcessSetDetail>();
            var allProcess = this.GetWorkProcessSetDetails();
            if (allProcess != null)
            {
                firstNodes = allProcess.Where(p => p.ParentNodeId == null || p.ParentNodeId.Count == 0).ToList();
            }

            return firstNodes;
        }

        /// <summary>
        /// 获取工艺的结束节点
        /// </summary>
        /// <returns></returns>
        public List<WorkProcessSetDetail> GetLastWorkProcessId()
        {
            List<WorkProcessSetDetail> lastNode = new List<WorkProcessSetDetail>();
            var allProcess = this.GetWorkProcessSetDetails();
            if (allProcess != null)
            {
                var allNode = allProcess.Select(p => p.NodeId).ToList();
                foreach (var item in allNode)
                {
                    if (!allProcess.Any(p => p.ParentNodeId.Contains(item)))
                    {
                        lastNode.Add(allProcess.FirstOrDefault(p => p.NodeId == item));
                    }
                }
            }

            return lastNode;
        }

        public List<WorkProcessSetDetail> GetWorkProcessSetDetails()
        {
            return this.GetData<List<WorkProcessSetDetail>>(WorkProcessConfigs);
        }
    }
}
