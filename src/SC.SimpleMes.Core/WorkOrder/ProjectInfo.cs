using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.WorkOrder
{
    public class ProjectInfo : Entity<long>
    {
        public string ProjectName { get; set; }

        public string ProjectNumber { get; set; }

        public string ProjectFullName { get; set; }
    }
}
