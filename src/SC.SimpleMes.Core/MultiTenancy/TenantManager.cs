using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using SC.SimpleMes.Authorization.Users;
using SC.SimpleMes.Editions;

namespace SC.SimpleMes.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
        }
    }
}

