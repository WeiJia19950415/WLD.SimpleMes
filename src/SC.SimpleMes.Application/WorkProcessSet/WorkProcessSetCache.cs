using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.WorkProcessSet.Dto;

namespace SC.SimpleMes.WorkProcessSet
{
    /// <summary>
    /// WorkProcess缓存 可以从中取出相流程流转数据
    /// </summary>
    public class WorkProcessSetCache : EntityCache<WorkProcess.WorkProcessSet, WorkProcessSetInfoCacheDto, long>, IWorkProcessSetCache, ITransientDependency
    {
        public WorkProcessSetCache(ICacheManager cacheManager, IRepository<WorkProcess.WorkProcessSet, long> repository, IUnitOfWorkManager unitOfWorkManager, string cacheName = null) : base(cacheManager, repository, unitOfWorkManager, cacheName)
        {
        }
    }
}
