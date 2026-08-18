using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Material;
using WLD.SimpleMes.Material.Dto;

namespace WLD.SimpleMes.Controllers
{
    [Route("api/[controller]/[action]")]
    [AbpAuthorize]
    [ApiController]
    public class BatchNumberRulerController : SimpleMesControllerBase
    {
        private readonly IMaterialBatchNumberRulerAppService _materialBatchNumberRulerAppService;
        public BatchNumberRulerController(IMaterialBatchNumberRulerAppService materialBatchNumberRulerAppService)
        {
            _materialBatchNumberRulerAppService = materialBatchNumberRulerAppService;
        }

        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<PageData<MaterialBatchNumberRulerDto>>> SearchRulerAsync([FromBody] JHTPageAjaxResquest<DTO.CommonConditionData> where)
        {
            var result = await _materialBatchNumberRulerAppService.GetAllAsync(new DTO.CommonPageRequestDto()
            {
                QueryConditionObj = where.Condition,
                MaxResultCount = where.PageSize,
                SkipCount = where.SkipCount,
            });

            return new JHTPageAjaxRespone<PageData<MaterialBatchNumberRulerDto>>()
            {
                Data = new PageData<MaterialBatchNumberRulerDto>()
                {
                    List = result.Items.ToList(),
                    Total = result.TotalCount,
                }
            };
        }

        /// <summary>
        /// 创建规则
        /// </summary>
        /// <param name="rulerDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<MaterialBatchNumberRulerDto>> CreatMaterialRulerAsync([FromBody] MaterialBatchNumberRulerDto rulerDto)
        {
            JHTAjaxResponse<MaterialBatchNumberRulerDto> ajaxResponse = new JHTAjaxResponse<MaterialBatchNumberRulerDto>();
            ajaxResponse.Data = await _materialBatchNumberRulerAppService.CreateAsync(rulerDto);
            return ajaxResponse;
        }

        /// <summary>
        /// 更新规则
        /// </summary>
        /// <param name="rulerDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<MaterialBatchNumberRulerDto>> UpdateMaterialRulerAsync([FromBody] MaterialBatchNumberRulerDto rulerDto)
        {
            JHTAjaxResponse<MaterialBatchNumberRulerDto> ajaxResponse = new JHTAjaxResponse<MaterialBatchNumberRulerDto>();
            ajaxResponse.Data = await _materialBatchNumberRulerAppService.UpdateAsync(rulerDto);
            return ajaxResponse;
        }

        /// <summary>
        /// 删除规则
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> DelRulerAsync(EntityDto<long> entityDto)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            await _materialBatchNumberRulerAppService.DeleteAsync(entityDto);
            return ajaxResponse;
        }
    }
}
