using Abp;
using Abp.Application.Features;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.JHTFeature
{
    /// <summary>
    /// 功能检查增加有效截至时间的查询
    /// </summary>
    public class JHTFeatureChecker : IFeatureChecker, ITransientDependency, IIocManagerAccessor
    {
        /// <summary>
        /// Reference to the current session.
        /// </summary>
        public IAbpSession AbpSession { get; set; }

        /// <summary>
        /// Reference to the store used to get feature values.
        /// </summary>
        public IFeatureValueStore FeatureValueStore { get; set; }

        public IIocManager IocManager { get; set; }

        private readonly IFeatureManager _featureManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;

        /// <summary>
        /// Creates a new <see cref="FeatureChecker"/> object.
        /// </summary>
        public JHTFeatureChecker(IFeatureManager featureManager, IMultiTenancyConfig multiTenancyConfig)
        {
            _featureManager = featureManager;
            _multiTenancyConfig = multiTenancyConfig;

            FeatureValueStore = NullFeatureValueStore.Instance;
            AbpSession = NullAbpSession.Instance;
        }

        /// <inheritdoc/>
        public Task<string> GetValueAsync(string name)
        {
            if (AbpSession.TenantId == null)
            {
                throw new AbpException("FeatureChecker can not get a feature value by name. TenantId is not set in the IAbpSession!");
            }

            return GetValueAsync(AbpSession.TenantId.Value, name);
        }

        /// <inheritdoc/>
        public async Task<string> GetValueAsync(int tenantId, string name)
        {
            var feature = _featureManager.Get(name);
            var value = await FeatureValueStore.GetValueOrNullAsync(tenantId, feature);

            return value ?? feature.DefaultValue;
        }

        /// <inheritdoc/>
        public async Task<bool> IsEnabledAsync(string featureName)
        {
            if (AbpSession.TenantId == null && _multiTenancyConfig.IgnoreFeatureCheckForHostUsers)
            {
                return true;
            }


            var featureValue = await GetValueAsync(featureName);
            var validEndDateTime = DateTime.Now;
            if (DateTime.TryParse(featureValue, out validEndDateTime))
            {
                return validEndDateTime.Date > DateTime.Now.Date;
            }

            return string.Equals(featureValue, "true", StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc/>
        public async Task<bool> IsEnabledAsync(int tenantId, string featureName)
        {

            var featureValue = await GetValueAsync(tenantId, featureName);
            var validEndDateTime = DateTime.Now;
            if (DateTime.TryParse(featureValue, out validEndDateTime))
            {
                return validEndDateTime.Date > DateTime.Now.Date;
            }

            return string.Equals(featureValue, "true", StringComparison.OrdinalIgnoreCase);
        }

        public string GetValue(string name)
        {
            if (AbpSession.TenantId == null)
            {
                throw new AbpException("未登陆到企业中！");
            }

            return GetValue(AbpSession.TenantId.Value, name);
        }

        public string GetValue(int tenantId, string name)
        {
            var feature = _featureManager.Get(name);
            var value = FeatureValueStore.GetValueOrNull(tenantId, feature);

            return value ?? feature.DefaultValue;
        }

        public bool IsEnabled(string featureName)
        {
            if (AbpSession.TenantId == null && _multiTenancyConfig.IgnoreFeatureCheckForHostUsers)
            {
                return true;
            }


            var featureValue = GetValue(featureName);
            var validEndDateTime = DateTime.Now;
            if (DateTime.TryParse(featureValue, out validEndDateTime))
            {
                return validEndDateTime.Date > DateTime.Now.Date;
            }

            return string.Equals(featureValue, "true", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsEnabled(int tenantId, string featureName)
        {
            var featureValue = GetValue(tenantId, featureName);
            var validEndDateTime = DateTime.Now;
            if (DateTime.TryParse(featureValue, out validEndDateTime))
            {
                return validEndDateTime.Date > DateTime.Now.Date;
            }

            return string.Equals(featureValue, "true", StringComparison.OrdinalIgnoreCase);
        }
    }
}

