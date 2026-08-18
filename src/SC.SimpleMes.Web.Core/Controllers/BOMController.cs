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

namespace SC.SimpleMes.Controllers
{

    [Route("api/[controller]/[action]")]
    [AbpAuthorize]
    [AbpMvcAuthorize(PermissionNames.Page_BomManager)]
    public class BOMController : SimpleMesControllerBase
    {
        private readonly IBOMAppService _bOMAppService;

        public BOMController(IBOMAppService bOMAppService)
        {
            _bOMAppService = bOMAppService;
        }

        /// <summary>
        /// 添加BOM
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<long>> AddBom([FromBody] BomAddDto bomAddDto)
        {
            var Id = await _bOMAppService.CreateAsync(bomAddDto);
            return new JHTAjaxResponse<long>
            {
                Data = Id.Id,
            };
        }

        /// <summary>
        /// 修改BOM
        /// </summary>
        /// <param name="bomUpdateDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<long>> UpdateBom([FromBody] BomUpdateDto bomUpdateDto)
        {
            var Id = await _bOMAppService.UpdateAsync(bomUpdateDto);
            return new JHTAjaxResponse<long>
            {
                Data = Id.Id,
            };
        }

        /// <summary>
        /// 删除BOM
        /// </summary>
        /// <param name="bomUpdateDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<bool>> DeleteBom([FromBody] EntityDto<long> entityDto)
        {
            await _bOMAppService.DeleteAsync(entityDto);
            return new JHTAjaxResponse<bool>
            {
                Data = true,
            };
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="bomQueryDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> QueryBom([FromBody] JHTPageAjaxResquest<CommonConditionData> pageAjaxResquest)
        {
            PagedResultDto<BomDto> ret = await _bOMAppService.GetAllAsync(new DTO.CommonPageRequestDto()
            {
                MaxResultCount = pageAjaxResquest.PageSize,
                SkipCount = pageAjaxResquest.SkipCount,
                QueryConditionObj = pageAjaxResquest.Condition
            });
            return new JHTAjaxResponse
            {
                Data = ret
            };
        }

        /// <summary>
        /// 获取物料-版本的BOM结构
        /// </summary>
        /// <param name="bomQueryDto"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<List<UICascaderModel<BomDto, long>>> GetBOMInCascader()
        {

            return new JHTAjaxResponse<List<UICascaderModel<BomDto, long>>>()
            {
                Data = _bOMAppService.GetBOMInCascader()
            };
        }


        [HttpPost]
        public async Task<JHTAjaxResponse>  SetBomIsCurrent([FromBody] EntityDto<long> entityDto)
        {
            await _bOMAppService.SetBomIsCurrentAsync(entityDto);
            return new JHTAjaxResponse
            {
                Data = true,
            };
        }

    }
}
