using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using SC.SimpleMes.Material;
using SC.SimpleMes.WorkProcess;
using SC.SimpleMes.WorkProcess.Dto;

namespace SC.SimpleMes.Controllers
{
    [Route("api/[controller]/[action]")]
    [AbpAuthorize]
    [ApiController]
    public class WorkProcessController : SimpleMesControllerBase
    {
        private readonly IWorkProcessAppService _workProcessAppService;
        public WorkProcessController(IWorkProcessAppService workProcessAppService)
        {
            _workProcessAppService = workProcessAppService;
        }


        /// <summary>
        /// 获取工序列表（带绑定工位）
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<PageData<WorkProcessInfoDto>>> SearchWorkProcessInfo([FromBody] JHTPageAjaxResquest<WorkProcessConditionDto> where)
        {
            var result = _workProcessAppService.SearchWorkProcessInfo(where);

            return new JHTPageAjaxRespone<PageData<WorkProcessInfoDto>>()
            {
                Data = result
            };
        }

        /// <summary>
        /// 编辑工序
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<WorkProcessInfoDto>> UpdateAsync([FromBody] WorkProcessInfoDto up)
        {
            return new JHTAjaxResponse<WorkProcessInfoDto>()
            {
                Data = await _workProcessAppService.UpdateAsync(up)
            };
        }

        /// <summary>
        /// 删除工序
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<bool>> DeleteAsync([FromBody] WorkProcessInfoDto del)
        {
            await _workProcessAppService.DeleteAsync(del);
            return new JHTAjaxResponse<bool>()
            {
                Data = true
            };
        }

        /// <summary>
        /// 禁用/启用
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<bool>> ToggleEnableWorkProcessAsync([FromBody] EntityDto<long> dto)
        {
            await _workProcessAppService.ToggleEnableWorkProcessAsync(dto);
            return new JHTAjaxResponse<bool>()
            {
                Data = true
            };
        }

        /// <summary>
        /// 新增工序
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<WorkProcessInfoDto>> CreatAsync([FromBody] WorkProcessInfoDto add)
        {
            add.TenantId = AbpSession.TenantId.Value;
            return new JHTAjaxResponse<WorkProcessInfoDto>()
            {
                Data = await _workProcessAppService.CreateAsync(add)
            };
        }


        /// <summary>
        /// 保存工序物料配置
        /// </summary>
        /// <param name="configDto"></param>
        /// <returns></returns>
        [HttpPost]

        public async Task<JHTAjaxResponse> SaveWorkProcessMaterialConfigAsync([FromBody] WorkProcessMaterialConfigDto configDto)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            await _workProcessAppService.SetWorkProcessMaterialConfigAsync(configDto);
            ajaxResponse.Msg = "操作成功";
            return ajaxResponse;
        }

        /// <summary>
        /// 加载工序物料配置信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<List<MaterialInfoDto>>> LoadWorkConfigProcessMaterialAsync([FromBody] EntityDto<long> dto)
        {
            JHTAjaxResponse<List<MaterialInfoDto>> response = new JHTAjaxResponse<List<MaterialInfoDto>>();
            response.Data = await _workProcessAppService.LoadConfigdMaterialInfosAsync(dto);
            return response;
        }

        /// <summary>
        /// 加载工序表单配置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<List<WorkProcessFormInfoRelationDto>>> GetWorkProcessFormRelationAsync([FromBody] EntityDto<long> dto)
        {
            JHTAjaxResponse<List<WorkProcessFormInfoRelationDto>> response = new JHTAjaxResponse<List<WorkProcessFormInfoRelationDto>>();
            response.Data = _workProcessAppService.GetWorkProcessFormRelation(dto.Id);
            return response;
        }

        /// <summary>
        /// 设置工序与表单的关系
        /// </summary>
        /// <param name="relationConfigDto"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse SetWorkProcessFormRelation([FromBody] WorkProcessFormRelationConfigDto relationConfigDto)
        {
            JHTAjaxResponse response = new JHTAjaxResponse();
            _workProcessAppService.SetWorkProcessFormRelation(relationConfigDto);
            response.Msg = "操作成功";
            return response;
        }

        /// <summary>
        /// 启用禁用工序表单
        /// </summary>
        /// <param name="realitonId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> ToggleWorkProcessFormEnabledAsync([FromBody] EntityDto<long> realitonId)
        {
            JHTAjaxResponse response = new JHTAjaxResponse();
            await _workProcessAppService.ToggleWorkProcessFormEnabledAsync(realitonId);
            response.Msg = "操作成功";
            return response;
        }

        /// <summary>
        /// 设置工序表单用途
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> SetWorkProcessFormUseTypeAsync([FromBody] WorkProcessFormInfoRelationDto relation)
        {
            JHTAjaxResponse response = new JHTAjaxResponse();
            await _workProcessAppService.SetWorkProcessFormUseTypeAsync(relation);
            response.Msg = "操作成功";
            return response;
        }
    }
}
