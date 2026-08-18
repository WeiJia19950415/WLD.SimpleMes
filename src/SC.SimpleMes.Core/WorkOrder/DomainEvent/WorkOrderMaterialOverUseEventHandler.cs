using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Material;
using SC.SimpleMes.Material.DomainEvent;

namespace SC.SimpleMes.WorkOrder.DomainEvent
{
    /// <summary>
    /// 工单物料超用事务处理
    /// </summary>
    public class WorkOrderMaterialOverUseEventHandler :
        ITransientDependency,
        IEventHandler<WorkOrderMaterialOverUseEventData>,
        IEventHandler<MaterialBatchNumberDelEventData>
    {
        private readonly IRepository<WarningOverUsedWorkOrderRecord, long> _workOrderOverUserRecordRep;
        private readonly IRepository<WorkOrderInfo, long> _workOrderInfoRep;
        private readonly IRepository<OrderMaterialProduceStatu, long> _orderMaterialProStatuRep;
        private readonly IRepository<MaterialBatchNumber, long> _materialBatchNumberRep;

        public WorkOrderMaterialOverUseEventHandler(
            IRepository<WarningOverUsedWorkOrderRecord, long> workOrderOverUserRecordRep,
            IRepository<WorkOrderInfo, long> workOrderInfoRep,
            IRepository<MaterialBatchNumber, long> materialBatchNumberRep,
            IRepository<OrderMaterialProduceStatu, long> orderMaterialProStatuRep
            )
        {
            _workOrderOverUserRecordRep = workOrderOverUserRecordRep;
            _workOrderInfoRep = workOrderInfoRep;
            _orderMaterialProStatuRep = orderMaterialProStatuRep;
            _materialBatchNumberRep = materialBatchNumberRep;
        }


        [UnitOfWork]
        public virtual void HandleEvent(WorkOrderMaterialOverUseEventData eventData)
        {
            var record = _workOrderOverUserRecordRep.FirstOrDefault(p => p.WorkOrderNumber == eventData.WorkOrderNumber);
            if (record != null)
            {
                record.LastWarningTime = DateTime.Now;
            }
            else
            {
                _workOrderOverUserRecordRep.Insert(new WarningOverUsedWorkOrderRecord()
                {
                    WorkOrderNumber = eventData.WorkOrderNumber,
                    FirstWarningTime = DateTime.Now,
                    LastWarningTime = DateTime.Now
                });
            }
        }

        [UnitOfWork]
        public virtual void HandleEvent(MaterialBatchNumberDelEventData eventData)
        {
            if (eventData != null && !string.IsNullOrEmpty(eventData.MaterialBatchNumberDeleted.FromOrderNumber))
            {
                var workOrderInfo = _workOrderInfoRep.FirstOrDefault(p => p.OrderNumber == eventData.MaterialBatchNumberDeleted.FromOrderNumber);
                if (workOrderInfo != null && eventData.MaterialBatchNumberDeleted.IsLineMaterialInfo == false)
                {
                    _orderMaterialProStatuRep.Delete(p => p.MaterialBatchNumber == eventData.MaterialBatchNumberDeleted.BatchNumber);

                    workOrderInfo.ProdcuingCount = _orderMaterialProStatuRep.GetAll()
                        .Where(p => p.WorkOrderNumber == workOrderInfo.OrderNumber && p.MaterialBatchNumber != eventData.MaterialBatchNumberDeleted.BatchNumber)
                        .Sum(p => p.CurrentMatrialCount);
                    workOrderInfo.FinishedCount =
                        _orderMaterialProStatuRep.GetAll()
                        .Where(p => p.WorkOrderNumber == workOrderInfo.OrderNumber && p.MaterialBatchNumber != eventData.MaterialBatchNumberDeleted.BatchNumber && p.ProduceStatus == ProduceStatusEnum.已完成)
                        .Sum(p => p.CurrentMatrialCount);
                }
            }
        }
    }
}
