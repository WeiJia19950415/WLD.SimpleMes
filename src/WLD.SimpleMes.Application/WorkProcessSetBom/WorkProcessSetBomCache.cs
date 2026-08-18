using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.BOM;
using WLD.SimpleMes.WorkProcessSetBom.Dto;

namespace WLD.SimpleMes.WorkProcessSetBom
{
    public class WorkProcessSetBomCache : EntityCache<BOM.WorkProcessSetBom, WorkProcessSetBomCacheDto, long>, IWorkProcessSetBomCache
    {
        private readonly BomUnitManager _bomUnitManager;
        public WorkProcessSetBomCache(ICacheManager cacheManager, BomUnitManager bomUnitManager, IRepository<BOM.WorkProcessSetBom, long> repository, IUnitOfWorkManager unitOfWorkManager, string cacheName = null) : base(cacheManager, repository, unitOfWorkManager, cacheName)
        {
            _bomUnitManager = bomUnitManager;
        }


        protected override WorkProcessSetBomCacheDto MapToCacheItem(BOM.WorkProcessSetBom entity)
        {
            var result = base.MapToCacheItem(entity);
            List<WorkProcessSetBomItemByShowDto> ret = new List<WorkProcessSetBomItemByShowDto>();
            var WorkProcessInfos = _bomUnitManager.GetWorkProcessSetBomBySetDetail(entity.Id);
            var setBOMItem = _bomUnitManager.GetWorkProcessSetBomItems(entity.Id, WorkProcessInfos);
            foreach (var item in WorkProcessInfos)
            {
                var retAdd = ObjectMapper.Map<WorkProcessSetBomItemByShowDto>(item);
                var addSetBomItem = setBOMItem.Where(p => p.BelongWorkProcessSetBomId == entity.Id && p.BelongWorkProcessId == item.Id).ToList();
                retAdd.BomItem = ObjectMapper.Map<List<ProcessBomItem>>(addSetBomItem);
                ret.Add(retAdd);
            }

            result.Item = ret;
            return result;
        }
    }
}
