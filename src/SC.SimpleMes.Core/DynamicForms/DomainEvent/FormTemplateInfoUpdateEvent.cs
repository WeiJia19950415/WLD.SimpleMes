using Abp.Events.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.DynamicForms.DomainEvent
{
    public class FormTemplateInfoUpdateEvent: EventData
    {
        public string FormsName { get; set; }
        public long OldFormTemplateId { get; set; }

        public long NewFormTemplateId { get; set; }
    }
}
