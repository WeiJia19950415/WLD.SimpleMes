using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus.Entities;
using Abp.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.DynamicForms.DTO;

namespace SC.SimpleMes.DynamicForms
{
    /// <summary>
    /// 数据缓存
    /// </summary>
    public class DynamicFormCache : EntityCache<FormTemplateInfo, FormTemplateInfoDto, long>, IDynamicFormCache, ITransientDependency
    {
        public DynamicFormCache(ICacheManager cacheManager, IRepository<FormTemplateInfo, long> repository, IUnitOfWorkManager unitOfWorkManager, string cacheName = null) : base(cacheManager, repository, unitOfWorkManager, cacheName)
        {

        }

        public override void HandleEvent(EntityChangedEventData<FormTemplateInfo> eventData)
        {
            base.HandleEvent(eventData);
        }
    }
}
