using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.Authorization;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WLD.SimpleMes.Authorization;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.Material;
using WLD.SimpleMes.Material.Dto;
using WLD.SimpleMes.QualityControl;
using WLD.SimpleMes.QualityControl.Dto;

namespace WLD.SimpleMes.Controllers
{
    [Route("api/[controller]/[action]")]
    [AbpAuthorize]
    [ApiController]
    public class ProblemCategoryController : SimpleMesControllerBase
    {
        private readonly IProblemCategoryAppService _problemCategoryAppService;
        private readonly IProblemCategoryCache _problemCategoryCache;
        public ProblemCategoryController(IProblemCategoryAppService problemCategoryAppService, IProblemCategoryCache problemCategoryCache)
        {
            _problemCategoryAppService = problemCategoryAppService;
            _problemCategoryCache = problemCategoryCache;
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<PageData<ProblemCategoryDto>>> LoadProblemCategoryAsync([FromBody] JHTPageAjaxResquest<DTO.CommonConditionData> where)
        {
            var result = await _problemCategoryAppService.GetAllAsync(new DTO.CommonPageRequestDto()
            {
                QueryConditionObj = where.Condition,
                MaxResultCount = where.PageSize,
                SkipCount = where.SkipCount,
            });

            return new JHTPageAjaxRespone<PageData<ProblemCategoryDto>>()
            {
                Data = new PageData<ProblemCategoryDto>()
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
        public async Task<JHTAjaxResponse<ProblemCategoryDto>> CreatProblemCategoryAsync([FromBody] ProblemCategoryDto model)
        {
            model.TenantId = AbpSession.TenantId.Value;
            return new JHTAjaxResponse<ProblemCategoryDto>()
            {
                Data = await _problemCategoryAppService.CreateAsync(model)
            };
        }

        /// <summary>
        /// 删除材料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<bool>> DelProblemCategoryAsync([FromBody] EntityDto<long> model)
        {
            await _problemCategoryAppService.DeleteAsync(model);
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
        public async Task<JHTAjaxResponse<ProblemCategoryDto>> UpdateProblemCategoryAsync([FromBody] ProblemCategoryDto model)
        {
            return new JHTAjaxResponse<ProblemCategoryDto>()
            {
                Data = await _problemCategoryAppService.UpdateAsync(model)
            };
        }

        /// <summary>
        /// 懒加载级联数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<List<UICascaderModel<string, string>>>> LoadCascadeProblemCategory([FromBody] EntityDto<string> model)
        {
            return new JHTAjaxResponse<List<UICascaderModel<string, string>>>()
            {
                Data = _problemCategoryAppService.LoadCascadeProblemCategory(model.Id)
            };
        }



        /// <summary>
        /// 加载所有缓存的级联质量分类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<List<UICascaderModel<string, string>>> LoadAllProbleCasclaeInfo()
        {
            JHTAjaxResponse<List<UICascaderModel<string, string>>> ajaxResponse = new JHTAjaxResponse<List<UICascaderModel<string, string>>>();
            ajaxResponse.Data = _problemCategoryCache.LoadAllProbleCasclaeInfo();
            return ajaxResponse;
        }
    }
}
