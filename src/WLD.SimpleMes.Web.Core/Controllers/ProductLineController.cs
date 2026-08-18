using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.Authorization;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Authorization;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.MultiTenancy;
using WLD.SimpleMes.WorkStation;
using WLD.SimpleMes.WorkStation.Dto;

namespace WLD.SimpleMes.Controllers
{

    /// <summary>
    /// 产线信息
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class ProductLineController : SimpleMesControllerBase
    {
        private readonly IProductLineAppService _productLineAppService;

        private readonly TenantManager _tenantManager;

        public ProductLineController(IProductLineAppService productLineAppService, TenantManager tenantManager)
        {
            _productLineAppService = productLineAppService;
            _tenantManager = tenantManager;
        }

        /// <summary>
        /// 获取产线信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        [AbpAllowAnonymous]
        public async Task<JHTPageAjaxRespone<PageData<ProductLineDto>>> GetProductLines([FromBody] JHTPageAjaxResquest<ProductLineConditionDto> where)
        {
            var defaultTeantInfo = this._tenantManager.FindByTenancyName(Tenant.DefaultTenantName);
            var defaultTeantId = AbpSession.TenantId > 0 ? AbpSession.TenantId : defaultTeantInfo.Id;
            using (UnitOfWorkManager.Current.SetTenantId(defaultTeantId))
            {
                var result = await _productLineAppService.GetAllAsync(new DTO.CommonPageRequestDto()
                {
                    QueryConditionObj = where.Condition,
                    MaxResultCount = where.PageSize,
                    SkipCount = where.SkipCount,
                });


                return new JHTPageAjaxRespone<PageData<ProductLineDto>>()
                {
                    Data = new PageData<ProductLineDto>()
                    {
                        List = result.Items.ToList(),
                        Total = result.TotalCount,
                    }
                };
            }
        }

        /// <summary>
        /// 创建产线
        /// </summary>
        /// <param name="addWorkshop"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_EditProductLine)]
        public async Task<JHTAjaxResponse> CreateProductLineInfo([FromBody] ProductLineDto createModel)
        {
            await _productLineAppService.CreateAsync(createModel);
            return new JHTAjaxResponse()
            {
                Msg = "添加成功",
            };
        }

        /// <summary>
        /// 修改产线信息
        /// </summary>
        /// <param name="updateWorkshopCategory"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_EditProductLine)]
        public async Task<JHTAjaxResponse> UpdateProductLineInfo([FromBody] ProductLineDto updateModel)
        {
            await _productLineAppService.UpdateAsync(updateModel);
            return new JHTAjaxResponse()
            {
                Msg = "修改成功",
            };
        }

        /// <summary>
        /// 查询工位员工关系
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpAuthorize]
        public JHTAjaxResponse<TransferDto> GetUserListAndBingUser([FromBody] EntityDto dto)
        {
            return new JHTAjaxResponse<TransferDto>()
            {
                Data = _productLineAppService.GetUserListAndBingUser(dto)
            };
        }

        /// <summary>
        /// 绑定员工-工位关系
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpAuthorize]
        public async Task<JHTAjaxResponse<bool>> BingUserAndWorkProcess([FromBody] TransferDto dto)
        {
            return new JHTAjaxResponse<bool>()
            {
                Data = await _productLineAppService.BingUserAndWorkProcessAsync(dto)
            };
        }

        /// <summary>
        /// 获取用户管理的产线信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AbpAuthorize]
        public async Task<JHTAjaxResponse<List<ProductLineDto>>> GetMangedProductLines()
        {
            return new JHTAjaxResponse<List<ProductLineDto>>()
            {
                Data = await _productLineAppService.GetMangedProductLinesAsync()
            };
        }
    }
}
