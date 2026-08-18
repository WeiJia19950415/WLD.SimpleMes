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
using WLD.SimpleMes.Material;
using WLD.SimpleMes.Material.Dto;

namespace WLD.SimpleMes.Controllers
{
    [Route("api/[controller]/[action]")]
    [AbpAuthorize]
    [ApiController]
    public class MaterialInfoController : SimpleMesControllerBase
    {
        private readonly IMaterialAppService _materialAppService;
        public MaterialInfoController(IMaterialAppService materialAppService)
        {
            _materialAppService = materialAppService;
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<PageData<MaterialInfoDto>>> SearchMaterialAsync([FromBody] JHTPageAjaxResquest<MaterialConditionDto> where)
        {
            var result = await _materialAppService.GetAllAsync(new DTO.CommonPageRequestDto()
            {
                QueryConditionObj = where.Condition,
                MaxResultCount = where.PageSize,
                SkipCount = where.SkipCount,
            });

            return new JHTPageAjaxRespone<PageData<MaterialInfoDto>>()
            {
                Data = new PageData<MaterialInfoDto>()
                {
                    List = result.Items.ToList(),
                    Total = result.TotalCount,
                }
            };
        }

        [HttpPost]
        public JHTAjaxResponse<List<MaterialInfoDto>> LoadFromK3()
        {
            JHTAjaxResponse<List<MaterialInfoDto>> result = new JHTAjaxResponse<List<MaterialInfoDto>>();
            result.Data = _materialAppService.LoadFromK3();
            return result;
        }
        /// <summary>
        /// 获取所有的物料信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<PageData<MaterialInfoDto>>> GetAllMaterialInfoAsync()
        {
            var result = await _materialAppService.GetAllAsync(new DTO.CommonPageRequestDto()
            {
                QueryConditionObj = null,
                MaxResultCount = 10000,
                SkipCount = 0,
            });
            return new JHTPageAjaxRespone<PageData<MaterialInfoDto>>()
            {
                Data = new PageData<MaterialInfoDto>()
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
        [AbpMvcAuthorize(PermissionNames.Page_MaterialInfo)]
        public async Task<JHTAjaxResponse<MaterialInfoDto>> CreatMaterialAsync([FromBody] MaterialInfoDto model)
        {
            model.TenantId = AbpSession.TenantId.Value;
            return new JHTAjaxResponse<MaterialInfoDto>()
            {
                Data = await _materialAppService.CreateAsync(model)
            };
        }

        /// <summary>
        /// 删除材料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_MaterialInfo)]
        public async Task<JHTAjaxResponse<bool>> DeleteaMaterialAsync([FromBody] MaterialInfoDto model)
        {
            await _materialAppService.DeleteAsync(model);
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
        [AbpMvcAuthorize(PermissionNames.Page_MaterialInfo)]
        public async Task<JHTAjaxResponse<MaterialInfoDto>> UpdateMaterialAsync([FromBody] MaterialInfoDto model)
        {
            return new JHTAjaxResponse<MaterialInfoDto>()
            {
                Data = await _materialAppService.UpdateAsync(model)
            };
        }

        /// <summary>
        /// 获取材料信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<MaterialInfoDto>> GetMaterialAsync([FromBody] EntityDto<long> id)
        {
            return new JHTAjaxResponse<MaterialInfoDto>()
            {
                Data = await _materialAppService.GetAsync(id)
            };
        }
    }
}
