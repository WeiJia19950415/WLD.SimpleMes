using Abp.Events.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Material.DomainEvent
{
    public class MaterialBatchNumberDelEventData : EventData
    {
        public MaterialBatchNumber MaterialBatchNumberDeleted { get; set; }
    }
}
