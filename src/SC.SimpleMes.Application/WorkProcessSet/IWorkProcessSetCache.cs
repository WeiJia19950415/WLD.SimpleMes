using Abp.Domain.Entities.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.WorkProcessSet.Dto;

namespace SC.SimpleMes.WorkProcessSet
{
    public interface IWorkProcessSetCache : IEntityCache<WorkProcessSetInfoCacheDto, long>
    {
    }
}
