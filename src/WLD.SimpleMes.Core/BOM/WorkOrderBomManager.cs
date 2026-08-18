using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.WorkOrder;

namespace WLD.SimpleMes.BOM
{
    public class WorkOrderBomManager : ITransientDependency
    {
        private readonly IRepository<WorkOrderBomItem, long> _itemRep;
        private readonly IRepository<WorkOrderBom, long> _workOrderBomRep;
        public WorkOrderBomManager(IRepository<WorkOrderBomItem, long> itemRep,
            IRepository<OrderMaterialProduceStatu, long> orderMaterialStatuRep,
            IRepository<WorkOrderBom, long> workOrderBomRep)
        {
            _itemRep = itemRep;
            _workOrderBomRep = workOrderBomRep;
        }

        /// <summary>
        /// 该物料能否在工序中使用
        /// </summary>
        /// <param name="materialId"></param>
        /// <param name="workProcessId"></param>
        /// <param name="workOrderId"></param>
        /// <returns></returns>
        public bool CheckWorkBomMaterail(long materialId, long workProcessId, long workOrderId)
        {
            var workOrderBom = _workOrderBomRep.FirstOrDefault(p => p.WorkOrderId == workOrderId);
            return _itemRep.GetAll().Any(p => p.InputMaterialId == materialId && p.BelongWorkProcessId == workProcessId && p.BelongWorkOrderBomId == workOrderBom.Id);
        }

        /// <summary>
        /// 该物料能否在工序中使用
        /// </summary>
        /// <param name="materialId"></param>
        /// <param name="workProcessId"></param>
        /// <param name="workOrderId"></param>
        /// <returns></returns>
        public bool CheckWorkBomMaterail(long materialId, long workProcessId, string workOrderNumber)
        {
            var workOrderBom = _workOrderBomRep.FirstOrDefault(p => p.WorkOrderNumber == workOrderNumber);
            return _itemRep.GetAll().Any(p => p.InputMaterialId == materialId && p.BelongWorkProcessId == workProcessId && p.BelongWorkOrderBomId == workOrderBom.Id);
        }

        /// <summary>
        /// 获取工单BOM项中的物料信息
        /// </summary>
        /// <param name="materialId"></param>
        /// <param name="workProcessId"></param>
        /// <param name="workOrderNumber"></param>
        /// <returns></returns>
        public List<WorkOrderBomItem> GetWorkOrderBomItems(long workProcessId, string workOrderNumber)
        {
            var workOrderBom = _workOrderBomRep.FirstOrDefault(p => p.WorkOrderNumber == workOrderNumber);
            if (workOrderBom == null)
            {
                throw new UserFriendlyException("未配置工单BOM，请联系管理员配置工单BOM！");
            }

            return _itemRep.GetAllIncluding(p => p.InputMaterial).Where(p => p.BelongWorkProcessId == workProcessId && p.BelongWorkOrderBomId == workOrderBom.Id).ToList();
        }


    }
}
