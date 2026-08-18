using Abp.Application.Features;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JHT.Abp.CommonModels;
using SC.SimpleMes.MultiTenancy.Dto;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SC.SimpleMes.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
        /// <summary>
        /// 查询租户
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        PageData<TenantDto> SearchTenant(JHTPageAjaxResquest<TenantConditionDto> condition);

        /// <summary>
        /// 应用图片
        /// </summary>
        /// <param name="file"></param>
        /// <param name="platformId"></param>
        /// <returns></returns>
        Task<string> SaveHeadImageAsync(IFormFile file);
        /// <summary>
        /// 获取本企业的信息
        /// </summary>
        /// <returns></returns>
        TenantDto GetOwnTenant(int teantId = 0);

        Task<TenantDto> UpdateOwnTenant(TenantDto dto);


        Task UpdateActive(EntityDto<int> dto);
        /// <summary>
        /// 获取企业的应用授权状态
        /// </summary>
        /// <returns></returns>
        List<FlatFeatureDto> GetFlatFeatureDtos(int tenandId);
        /// <summary>
        /// 保存企业的应用授权
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool SaveFeature(SaveFlatFeatureDto dto);
    }
}


