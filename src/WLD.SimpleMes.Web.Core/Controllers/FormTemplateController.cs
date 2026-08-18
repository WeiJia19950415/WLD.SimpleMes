using Abp.Application.Services.Dto;
using Abp.Authorization;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.DynamicForms;
using WLD.SimpleMes.DynamicForms.DTO;

namespace WLD.SimpleMes.Controllers
{
    [Route("api/[controller]/[action]")]
    [AbpAuthorize]
    [ApiController]
    public class FormTemplateController : SimpleMesControllerBase
    {
        private readonly IFormTemplateInfoAppService _formTemplateInfoAppService;
        public FormTemplateController(IFormTemplateInfoAppService formTemplateInfoAppService)
        {
            _formTemplateInfoAppService = formTemplateInfoAppService;
        }


        /// <summary>
        /// 添加表单模版
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> AddFormTemplate([FromBody] FormTemplateInfoDto infoDto)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            await _formTemplateInfoAppService.UpdateAsync(infoDto);
            ajaxResponse.Msg = "操作成功";
            return ajaxResponse;
        }

        /// <summary>
        /// 加载表单详情
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<FormTemplateInfoDto>> LoadFormDetail([FromBody] EntityDto<long> entityDto)
        {
            JHTAjaxResponse<FormTemplateInfoDto> ajaxResponse = new JHTAjaxResponse<FormTemplateInfoDto>();
            ajaxResponse.Data = await _formTemplateInfoAppService.GetAsync(entityDto);
            return ajaxResponse;
        }

        /// <summary>
        /// 加载表单修改历史记录
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<List<FormTemplateBasicInfoDto>> LoadFormTemplateHistroy([FromBody] EntityDto<string> entityDto)
        {
            JHTAjaxResponse<List<FormTemplateBasicInfoDto>> ajaxResponse = new JHTAjaxResponse<List<FormTemplateBasicInfoDto>>();
            ajaxResponse.Data = _formTemplateInfoAppService.SearchFromtelateInfoHistory(entityDto);
            return ajaxResponse;
        }

        [HttpPost]
        public async Task<JHTAjaxResponse<PageData<FormTemplateInfoDto>>> SearchFormTemplateInfo([FromBody] JHTPageAjaxResquest<DTO.CommonConditionData> pageAjaxResquest)
        {
            var dataInfo = await _formTemplateInfoAppService.GetAllAsync(new DTO.CommonPageRequestDto()
            {
                QueryConditionObj = pageAjaxResquest.Condition,
                MaxResultCount = pageAjaxResquest.PageSize,
                SkipCount = pageAjaxResquest.SkipCount,
            });


            return new JHTAjaxResponse<PageData<FormTemplateInfoDto>>()
            {
                Data = new PageData<FormTemplateInfoDto>()
                {
                    List = dataInfo.Items.ToList(),
                    Total = dataInfo.TotalCount,
                }
            };
        }
    }
}
