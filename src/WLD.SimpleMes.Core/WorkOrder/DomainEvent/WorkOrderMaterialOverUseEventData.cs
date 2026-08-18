using Abp.Events.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkOrder.DomainEvent
{
    public class WorkOrderMaterialOverUseEventData: EventData
    {
        public string WorkOrderNumber { get; set; }
    }
}
