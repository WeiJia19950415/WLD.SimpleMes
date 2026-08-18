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
using WLD.SimpleMes.DynamicForms;
using WLD.SimpleMes.QualityControl.Dto;

namespace WLD.SimpleMes.QualityControl
{
    public class ProblemDefineCache : EntityCache<QualityProblemDefine, ProblemDefineDto, long>, IProblemDefineCache, ITransientDependency
    {
        public ProblemDefineCache(ICacheManager cacheManager, IRepository<QualityProblemDefine, long> repository, IUnitOfWorkManager unitOfWorkManager, string cacheName = null) : base(cacheManager, repository, unitOfWorkManager, cacheName)
        {
        }
    }
}
