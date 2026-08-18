using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.WorkOrder;

namespace SC.SimpleMes.WorkProcess
{
    /// <summary>
    /// 更新时间
    /// </summary>
    public class WorkProcessOperatorRecordEventHandler : ITransientDependency, IEventHandler<EntityChangedEventData<WorkProcessOperatorRecord>>, IEventHandler<EntityCreatingEventData<WorkProcessOperatorRecord>>
    {
        private readonly IRepository<OrderMaterialProduceStatu, long> _orderMaterialProStatuRep;

        public WorkProcessOperatorRecordEventHandler(IRepository<OrderMaterialProduceStatu, long> orderMaterialProStatuRep)
        {
            _orderMaterialProStatuRep = orderMaterialProStatuRep;
        }

        public void HandleEvent(EntityChangedEventData<WorkProcessOperatorRecord> eventData)
        {
            UpdateWorkOrderStatus(eventData.Entity.BatchNumber);
        }

        public void HandleEvent(EntityCreatingEventData<WorkProcessOperatorRecord> eventData)
        {
            UpdateWorkOrderStatus(eventData.Entity.BatchNumber);
        }

        [UnitOfWork]
        public virtual void UpdateWorkOrderStatus(string batchNumber)
        {
            if (!string.IsNullOrEmpty(batchNumber))
            {
                var status = _orderMaterialProStatuRep.FirstOrDefault(p => p.MaterialBatchNumber == batchNumber);

                if (status != null)
                {
                    status.LastUpdateTime = DateTime.Now;
                }
            }
        }
    }
}
