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
using SC.SimpleMes.Authorization;
using SC.SimpleMes.Material;
using SC.SimpleMes.Material.Dto;

namespace SC.SimpleMes.Controllers
{
    /// <summary>
    /// ERP入库单批次号打印
    /// </summary>
    [Route("api/[controller]/[action]")]
    [AbpMvcAuthorize]
    public class CutMaterialConfigController : SimpleMesControllerBase
    {
        private readonly ICutMaterialConfigAppService _cutMaterialConfigAppService;
        public CutMaterialConfigController(ICutMaterialConfigAppService cutMaterialConfigAppService)
        {
            this._cutMaterialConfigAppService = cutMaterialConfigAppService;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<PageData<CutMaterialConfigDto>>> SearchCutMaterialConfigAsync([FromBody] JHTPageAjaxResquest<CutMaterialConfigConditionDto> where)
        {
            var result = await _cutMaterialConfigAppService.GetAllAsync(new DTO.CommonPageRequestDto()
            {
                QueryConditionObj = where.Condition,
                MaxResultCount = where.PageSize,
                SkipCount = where.SkipCount,
            });

            return new JHTPageAjaxRespone<PageData<CutMaterialConfigDto>>()
            {
                Data = new PageData<CutMaterialConfigDto>()
                {
                    List = result.Items.ToList(),
                    Total = result.TotalCount,
                }
            };
        }


        /// <summary>
        /// 新增材料
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_CutMaterialConfig)]
        public async Task<JHTAjaxResponse<CutMaterialConfigDto>> CreatCutMaterialConfigAsync([FromBody] CutMaterialConfigDto model)
        {
            return new JHTAjaxResponse<CutMaterialConfigDto>()
            {
                Data = await _cutMaterialConfigAppService.CreateAsync(model)
            };
        }

        /// <summary>
        /// 编辑材料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_CutMaterialConfig)]
        public async Task<JHTAjaxResponse<CutMaterialConfigDto>> UpdateCutMaterialConfigAsync([FromBody] CutMaterialConfigDto model)
        {
            return new JHTAjaxResponse<CutMaterialConfigDto>()
            {
                Data = await _cutMaterialConfigAppService.UpdateAsync(model)
            };
        }

        /// <summary>
        /// 编辑材料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_CutMaterialConfig)]
        public async Task<JHTAjaxResponse<CutMaterialConfigDto>> DelCutMaterialConfigAsync([FromBody] EntityDto<long> model)
        {
            await _cutMaterialConfigAppService.DeleteAsync(model);
            return new JHTAjaxResponse<CutMaterialConfigDto>()
            {
                Msg = "已删除该配置"
            };
        }

        [HttpPost]
        public async Task<JHTAjaxResponse<CutMaterialConfigDto>> LoadCutMaterialConfig([FromBody] CutMaterialConfigDto materialConfigDto )
        {
            JHTAjaxResponse<CutMaterialConfigDto> ajaxResponse = new JHTAjaxResponse<CutMaterialConfigDto>();
            ajaxResponse.Data= _cutMaterialConfigAppService.LoadCutMaterialConfig(materialConfigDto);
            return ajaxResponse;
        }
    }
}
