using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.ObjectMapping;
using Abp.Runtime.Caching;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.K3DBInfo;
using SC.SimpleMes.Material.Dto;

namespace SC.SimpleMes.Material
{
    public class MaterialBatchNumberCache : IMaterialBatchNumberCache, ITransientDependency, IEventHandler<EntityChangedEventData<MaterialBatchNumber>>
    {
        protected readonly string MaterialCacheKey = typeof(MaterialBatchNumberCache).FullName;
        protected readonly ITypedCache<string, MaterialBatchNumberDto> InternalCache;
        private readonly IRepository<MaterialBatchNumber, long> _repository;
       
        protected readonly IObjectMapper ObjectMapper;

        public MaterialBatchNumberCache(ICacheManager cacheManager,
            IRepository<MaterialBatchNumber, long> repository,
        IObjectMapper objectMapper)
        {
            _repository = repository;
          
            ObjectMapper = objectMapper;
            InternalCache = cacheManager.GetCache<string, MaterialBatchNumberDto>(this.MaterialCacheKey);
        }

        public MaterialBatchNumberDto GetByMaterialBatchNumber(string materialBatchNumber)
        {
            return InternalCache.Get(materialBatchNumber, (key) =>
             {
                 var batchNumber = _repository.FirstOrDefault(p => p.BatchNumber == materialBatchNumber);
                 return ObjectMapper.Map<MaterialBatchNumberDto>(batchNumber);
             });
        }

        public Task<MaterialBatchNumberDto> GetByMaterialBatchNumberAsync(string materialBatchNumber)
        {
            return InternalCache.GetAsync(materialBatchNumber, async (key) =>
            {
                var batchNumber = await _repository.FirstOrDefaultAsync(p => p.BatchNumber == materialBatchNumber);
                return ObjectMapper.Map<MaterialBatchNumberDto>(batchNumber);
            });
        }

        public void HandleEvent(EntityChangedEventData<MaterialBatchNumber> eventData)
        {
            InternalCache.Remove(eventData.Entity.BatchNumber);
        }
    }
}
