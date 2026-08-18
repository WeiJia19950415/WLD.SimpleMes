using Abp.Domain.Entities.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.WorkProcessSetBom.Dto;

namespace WLD.SimpleMes.WorkProcessSetBom
{
    public interface IWorkProcessSetBomCache:IEntityCache<WorkProcessSetBomCacheDto,long>
    {

    }
}
