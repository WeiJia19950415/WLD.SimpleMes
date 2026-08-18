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
using SC.SimpleMes.QualityControl;
using SC.SimpleMes.QualityControl.Dto;

namespace SC.SimpleMes.Controllers
{
    [Route("api/[controller]/[action]")]
    [AbpAuthorize]
    [ApiController]
    public class ProblemDefineController : SimpleMesControllerBase
    {
        private readonly IQualityProblemDefineAppService _qualityProblemDefineAppService;
        public ProblemDefineController(IQualityProblemDefineAppService qualityProblemDefineAppService)
        {
            _qualityProblemDefineAppService = qualityProblemDefineAppService;
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<PageData<ProblemDefineDto>>> LoadProblemDefineAsync([FromBody] JHTPageAjaxResquest<DTO.CommonConditionData> where)
        {
            var result = await _qualityProblemDefineAppService.GetAllAsync(new DTO.CommonPageRequestDto()
            {
                QueryConditionObj = where.Condition,
                MaxResultCount = where.PageSize,
                SkipCount = where.SkipCount,
            });

            return new JHTPageAjaxRespone<PageData<ProblemDefineDto>>()
            {
                Data = new PageData<ProblemDefineDto>()
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
        public async Task<JHTAjaxResponse<ProblemDefineDto>> CreatProblemDefineAsync([FromBody] ProblemDefineDto model)
        {
            return new JHTAjaxResponse<ProblemDefineDto>()
            {
                Data = await _qualityProblemDefineAppService.CreateAsync(model)
            };
        }

        /// <summary>
        /// 删除材料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<bool>> DelProblemDefineAsync([FromBody] EntityDto<long> model)
        {
            await _qualityProblemDefineAppService.DeleteAsync(model);
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
        public async Task<JHTAjaxResponse<ProblemDefineDto>> UpdateProblemDefineAsync([FromBody] ProblemDefineDto model)
        {
            return new JHTAjaxResponse<ProblemDefineDto>()
            {
                Data = await _qualityProblemDefineAppService.UpdateAsync(model)
            };
        }



        /// <summary>
        /// 加载关联的问题定义
        /// </summary>
        /// <param name="parentCategoryCode"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<List<ProblemDefineDto>> LoadSubProblemDefineInfos([FromBody] EntityDto<string> parentCategoryCode)
        {
            JHTAjaxResponse<List<ProblemDefineDto>> ajaxResponse = new JHTAjaxResponse<List<ProblemDefineDto>>();
            ajaxResponse.Data = _qualityProblemDefineAppService.GetProblemDefineByCatetoeryCode(parentCategoryCode.Id);
            return ajaxResponse;
        }

    }
}
