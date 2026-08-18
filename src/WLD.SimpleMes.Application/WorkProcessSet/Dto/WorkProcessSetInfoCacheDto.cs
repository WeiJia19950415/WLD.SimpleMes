using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.WorkProcess;

namespace WLD.SimpleMes.WorkProcessSet.Dto
{
    public class WorkProcessSetInfoCacheDto : EntityDto<long>
    {
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
        /// 归属租户ID
        /// </summary>
        public int? TenantId { get; set; }

        public List<WorkProcessSetDetail> WorkProcessSetDetails { get; set; }

        /// <summary>
        /// 计算剩余的工序数量
        /// </summary>
        /// <param name="workProcessId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int ComputeLeftProcessCount(long workProcessId)
        {
            int leftCount = 0;
            do
            {
                var curentWorkProcess = this.WorkProcessSetDetails.FirstOrDefault(p => p.BelongWorkProcessInfoId == workProcessId);
                var nextWorkProcss = this.WorkProcessSetDetails.FirstOrDefault(p => p.ParentNodeId.Contains(curentWorkProcess.NodeId));
                if (nextWorkProcss != null)
                {
                    workProcessId = nextWorkProcss.BelongWorkProcessInfoId;
                    leftCount++;
                }
                else
                {
                    workProcessId = 0;
                }
            }
            while (workProcessId > 0);

            return leftCount;
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
            var allProcess = this.WorkProcessSetDetails;
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
            var allProcess = this.WorkProcessSetDetails;
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

        public List<long> GetFinishedWorkProcess(long workProcessId)
        {
            List<long> finishedIds = new List<long>();
          
            do
            {
                var curentWorkProcess = this.WorkProcessSetDetails.FirstOrDefault(p => p.BelongWorkProcessInfoId == workProcessId);
                if (curentWorkProcess == null)
                {
                    workProcessId=0;
                    break;
                }

                finishedIds.Add(workProcessId);
                var nextWorkProcss = this.WorkProcessSetDetails.FirstOrDefault(p => curentWorkProcess.ParentNodeId.Contains(p.NodeId));
                if (nextWorkProcss != null)
                {
                    workProcessId = nextWorkProcss.BelongWorkProcessInfoId;
                    if (!finishedIds.Contains(workProcessId))
                    {
                        finishedIds.Add(workProcessId);
                    }
                }
                else
                {
                    workProcessId = 0;
                }
            }
            while (workProcessId > 0);

            return finishedIds;
        }
    }
}
