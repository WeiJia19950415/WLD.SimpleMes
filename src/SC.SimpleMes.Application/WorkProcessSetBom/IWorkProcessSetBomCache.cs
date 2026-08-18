using Abp.Domain.Entities.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.WorkProcessSetBom.Dto;

namespace SC.SimpleMes.WorkProcessSetBom
{
    public interface IWorkProcessSetBomCache:IEntityCache<WorkProcessSetBomCacheDto,long>
    {

    }
}
