using Abp.Configuration;
using Abp.Dependency;
using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.SAPSDK.Configuation;

namespace WLD.SimpleMes.SAPSDK
{
    public class SAPClientSDK : ITransientDependency,ISAPClientSDK
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly HttpClient httpClient;

        /// <summary>
        /// SAPClient 接口
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="httpClientFactory"></param>
        /// <param name="settingManager"></param>
        /// <param name="configuration"></param>
        public SAPClientSDK(ILoggerFactory loggerFactory, IHttpClientFactory httpClientFactory, ISettingManager settingManager, IConfiguration configuration)
        {
            this.httpClient = httpClientFactory.CreateClient("SAPClient");
            this.httpClient.BaseAddress = new Uri(settingManager.GetSettingValue(AppSettingNames.SAPInterfaceUrl));
        }
    }
}
