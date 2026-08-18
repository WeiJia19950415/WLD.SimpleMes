using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Authorization;
using SC.SimpleMes.BOM;
using SC.SimpleMes.BOM.Dto;
using SC.SimpleMes.DTO;
using SC.SimpleMes.WorkProcessSetBom;
using SC.SimpleMes.WorkProcessSetBom.Dto;

namespace SC.SimpleMes.Controllers
{
    [Route("api/[controller]/[action]")]
    [AbpAuthorize]
    [AbpMvcAuthorize(PermissionNames.Page_SetBomManager)]
    public class WorkProcessSetBomController : SimpleMesControllerBase
    {
        private readonly IWorkProcessSetBomAppService _workProcessSetBomAppService;
        private readonly IBOMAppService _bOMAppService;
        public WorkProcessSetBomController(IWorkProcessSetBomAppService workProcessSetBomAppService
            , IBOMAppService bOMAppService)
        {
            _workProcessSetBomAppService = workProcessSetBomAppService;
            _bOMAppService = bOMAppService;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="bomQueryDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> QueryWorkProcessBom([FromBody] JHTPageAjaxResquest<DTO.CommonConditionData> bomQueryDto)
        {
            PagedResultDto<WorkProcessSetBomDto> ret = await _workProcessSetBomAppService.GetAllAsync(new CommonPageRequestDto()
            {
                QueryConditionObj=bomQueryDto.Condition,
                MaxResultCount=bomQueryDto.PageSize,
                SkipCount=bomQueryDto.SkipCount,
            });
            return new JHTAjaxResponse
            {
                Data = ret
            };
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="bomQueryDto"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse GetWorkProcessSetBomDtosByMaterial([FromBody] EntityDto<long> MaterialIds)
        {
            return new JHTAjaxResponse
            {
                Data = _workProcessSetBomAppService.GetWorkProcessSetBomDtosByMaterial(MaterialIds.Id)
            };
        }

        /// <summary>
        /// 获取未配置工艺的BOM的标准BOm
        /// </summary>
        /// <param name="materialNumber"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<List<BomDto>> GetUnConfigWorkProcessSetBom([FromBody] EntityDto<string> materialNumber)
        {
            JHTAjaxResponse<List<BomDto>> ajaxResponse = new JHTAjaxResponse<List<BomDto>>();
            ajaxResponse.Data = _bOMAppService.GetUnConfigWorkProcessSetBom(materialNumber.Id);
            return ajaxResponse;
        }

        /// <summary>
        /// 获取配置详情
        /// </summary>
        /// <param name="bomQueryDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> GetConfigWorkProcessBomAsync([FromBody] EntityDto<long> SetBomId)
        {
            var ret = new
            {
                WorkProcessInfo = _workProcessSetBomAppService.GetWorkProcessSetBomItemByShowDtos(SetBomId),
                BOMData = await _bOMAppService.GetBySetBomToImportantAsync(SetBomId)
            };
            return new JHTAjaxResponse
            {
                Data = ret
            };
        }


        /// <summary>
        /// 配置工艺BOM
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> ConfigWorkProcessBom([FromBody] ConfigWorkProcessBomDto dto)
        {
            await _workProcessSetBomAppService.ConfigWorkProcessBomAsync(dto);
            return new JHTAjaxResponse
            {

                Data = true
            };
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> AddWorkProcessBom([FromBody] WorkProcessSetBomDto dto)
        {
            return new JHTAjaxResponse
            {

                Data = await _workProcessSetBomAppService.CreateAsync(dto)
            };
        }
    }
}
