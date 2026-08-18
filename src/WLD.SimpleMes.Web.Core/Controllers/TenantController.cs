using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.Authorization;
using WLD.SimpleMes.Authorization;
using WLD.SimpleMes.MultiTenancy;
using WLD.SimpleMes.MultiTenancy.Dto;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Controllers
{
    /// <summary>
    /// 租户管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class TenantController : SimpleMesControllerBase
    {
        private readonly ITenantAppService _tenantAppService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tenantAppService"></param>
        /// <param name="tenantAuthAppService"></param>
        public TenantController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }
        /// <summary>
        /// 企业注册
        /// </summary>
        /// <returns></returns>
        [AbpAllowAnonymous]
        [HttpPost]
        public async Task<JHTAjaxResponse<TenantDto>> Register([FromBody] CreateTenantDto create)
        {
            return new JHTAjaxResponse<TenantDto>()
            {
                Data = await _tenantAppService.CreateAsync(create)
            };
        }

        /// <summary>
        /// 获取租户信息
        /// </summary>
        /// <param name="id">租户Id</param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Pages_Tenants)]
        public async Task<JHTAjaxResponse<TenantDto>> GetTenant([FromBody] EntityDto<int> id)
        {
            return new JHTAjaxResponse<TenantDto>()
            {
                Data = await _tenantAppService.GetAsync(id)

            };
        }
        /// <summary>
        /// 企业修改
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Pages_Tenants)]
        public async Task<JHTAjaxResponse<TenantDto>> UpadateTenant([FromBody] TenantDto tenantDto)
        {
            return new JHTAjaxResponse<TenantDto>()
            {
                Data = await _tenantAppService.UpdateAsync(tenantDto)
            };
        }
        /// <summary>
        /// 企业列表查询
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Pages_Tenants)]
        [DisableAuditing]
        public JHTPageAjaxRespone<PageData<TenantDto>> SearchTenant([FromBody] JHTPageAjaxResquest<TenantConditionDto> condition)
        {
            return new JHTPageAjaxRespone<PageData<TenantDto>>()
            {
                Data = _tenantAppService.SearchTenant(condition)
            };
        }
        /// <summary>
        /// 修改企业启用禁用状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Pages_Tenants)]
        public async Task<JHTAjaxResponse> UpdateActive([FromBody] EntityDto<int> id)
        {
            await _tenantAppService.UpdateActive(id);
            return new JHTAjaxResponse();
        }

        /// <summary>
        /// 图像上传
        /// </summary>
        /// <param name="files">文件</param>
        /// <returns></returns>
        [HttpPost]
        [AbpAllowAnonymous]
        public async Task<JHTAjaxResponse<string>> UploadHeadImgAsync([FromForm] IFormFile files)
        {
            if (files == null && Request.Form.Files.Count > 0)
            {
                files = Request.Form.Files[0];
            }

            var result = await _tenantAppService.SaveHeadImageAsync(files);

            return new JHTAjaxResponse<string>(result) { Msg = "图片上传成功" };
        }
        /// <summary>
        /// 获取企业的应用授权状态
        /// </summary>
        /// <param name="files">文件</param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<List<FlatFeatureDto>> GetFlatFeatureDtos([FromBody] EntityDto<int> tenandId)
        {
            var data = _tenantAppService.GetFlatFeatureDtos(tenandId.Id);
            return new JHTAjaxResponse<List<FlatFeatureDto>>()
            {
                Data = data
            };
        }
        /// <summary>
        /// 设置企业的应用授权状态
        /// </summary>
        /// <param name="files">文件</param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<bool> SaveFeature([FromBody] List<FlatFeatureDto> dto)
        {
            foreach (var item in dto)
            {
                _tenantAppService.SaveFeature(new SaveFlatFeatureDto() { Name = item.Name, Value = item.Vlaue, TenandId = item.TenandId });
            }

            return new JHTAjaxResponse<bool>() { Data = true };
        }
    }
}

