using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Abp.ObjectMapping;
using Abp.Events.Bus.Handlers;
using Abp.Events.Bus.Entities;

namespace WLD.SimpleMes.Material
{
    public class MaterialCache : IMaterialCache, ITransientDependency, IEventHandler<EntityChangedEventData<MaterialInfo>>
    {
        protected readonly string MaterialCacheKey = typeof(MaterialCache).FullName;
        protected readonly ITypedCache<string, MaterialInfoDto> InternalCache;
        private readonly IRepository<MaterialInfo, long> _repository;
        protected readonly IObjectMapper ObjectMapper;
        public MaterialCache(ICacheManager cacheManager, IRepository<MaterialInfo, long> repository, IObjectMapper objectMapper)
        {
            _repository = repository;
            ObjectMapper = objectMapper;
            InternalCache = cacheManager.GetCache<string, MaterialInfoDto>(this.MaterialCacheKey);
        }

        public async Task<MaterialInfoDto> GetByMaterialNumberAsync(string materialNumber)
        {
            if (InternalCache.TryGetValue(materialNumber, out MaterialInfoDto materialInfo))
            {
                return materialInfo;
            }
            else
            {
                materialInfo = ObjectMapper.Map<MaterialInfoDto>(await _repository.FirstOrDefaultAsync(p => p.MaterialNumber == materialNumber));
                if (materialInfo == null)
                {
                    throw new Exception("该物料编码不存在，请核对料号！");
                }

                await InternalCache.SetAsync(materialNumber, materialInfo);

                return materialInfo;
            }
        }

        public MaterialInfoDto GetByMaterialNumber(string materialNumber)
        {
            return InternalCache.Get(materialNumber, (key) =>
            {
                return ObjectMapper.Map<MaterialInfoDto>(_repository.FirstOrDefault(p => p.MaterialNumber == key));
            });
        }


        /// <summary>
        /// 当实体发生改变时移除缓存
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityChangedEventData<MaterialInfo> eventData)
        {
            InternalCache.Remove(eventData.Entity.MaterialNumber);
        }
    }
}
