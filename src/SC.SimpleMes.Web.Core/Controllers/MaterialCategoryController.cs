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
using SC.SimpleMes.DTO;
using SC.SimpleMes.Material;
using SC.SimpleMes.Material.Dto;

namespace SC.SimpleMes.Controllers
{
    [Route("api/[controller]/[action]")]
    [AbpAuthorize]
    [ApiController]
    public class MaterialCategoryController : SimpleMesControllerBase
    {
        private readonly IMaterialCategoryAppService _materialCategoryAppService;
        public MaterialCategoryController(IMaterialCategoryAppService materialCategoryAppService)
        {
            _materialCategoryAppService = materialCategoryAppService;
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<PageData<MaterialCategoryDto>>> LoadMaterialCateogryAsync([FromBody] JHTPageAjaxResquest<DTO.CommonConditionData> where)
        {
            var result = await _materialCategoryAppService.GetAllAsync(new DTO.CommonPageRequestDto()
            {
                QueryConditionObj = where.Condition,
                MaxResultCount = where.PageSize,
                SkipCount = where.SkipCount,
            });

            return new JHTPageAjaxRespone<PageData<MaterialCategoryDto>>()
            {
                Data = new PageData<MaterialCategoryDto>()
                {
                    List = result.Items.ToList(),
                    Total = result.TotalCount,
                }
            };
        }


        /// <summary>
        /// 加载所有的成品，半成品的分组信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTAjaxResponse<List<MaterialCategoryDto>>> LoadAllProductCategoryAsync()
        {
            JHTAjaxResponse<List<MaterialCategoryDto>> ajaxResponse = new JHTAjaxResponse<List<MaterialCategoryDto>>();
            ajaxResponse.Data = await _materialCategoryAppService.LoadAllProductCategoryAsync();
            return ajaxResponse;
        }

        /// <summary>
        /// 新增材料
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_Material_Category)]
        public async Task<JHTAjaxResponse<MaterialCategoryDto>> CreatMaterialCateogryAsync([FromBody] MaterialCategoryDto model)
        {
            model.TenantId = AbpSession.TenantId.Value;
            return new JHTAjaxResponse<MaterialCategoryDto>()
            {
                Data = await _materialCategoryAppService.CreateAsync(model)
            };
        }

        /// <summary>
        /// 删除材料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_Material_Category)]
        public async Task<JHTAjaxResponse<bool>> DelMaterialCateogryAsync([FromBody] EntityDto<long> model)
        {
            await _materialCategoryAppService.DeleteAsync(model);
            return new JHTAjaxResponse<bool>()
            {
                Data = true
            };
        }


        /// <summary>
        /// 编辑材料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_Material_Category)]
        public async Task<JHTAjaxResponse<MaterialCategoryDto>> UpdateMaterialCateogryAsync([FromBody] MaterialCategoryDto model)
        {
            return new JHTAjaxResponse<MaterialCategoryDto>()
            {
                Data = await _materialCategoryAppService.UpdateAsync(model)
            };
        }

        /// <summary>
        /// 懒加载级联数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<List<UICascaderModel<string, string>>>> LoadCascadeMaterialCategory([FromBody] EntityDto<string> model)
        {
            return new JHTAjaxResponse<List<UICascaderModel<string, string>>>()
            {
                Data = _materialCategoryAppService.LoadCascadeMaterialCategory(model.Id)
            };
        }
    }
}
