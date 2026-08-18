using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using WLD.SimpleMes.Authorization.Users;
using WLD.SimpleMes.Editions;

namespace WLD.SimpleMes.MultiTenancy
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

