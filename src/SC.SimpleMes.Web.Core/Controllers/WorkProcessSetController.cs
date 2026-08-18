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
using SC.SimpleMes.DTO;
using SC.SimpleMes.WorkProcess;
using SC.SimpleMes.WorkProcessSet;
using SC.SimpleMes.WorkProcessSet.Dto;

namespace SC.SimpleMes.Controllers
{
    [Route("api/[controller]/[action]")]
    [AbpAuthorize]
    [ApiController]
    public class WorkProcessSetController : SimpleMesControllerBase
    {
        private readonly IWorkProcessSetAppService _workProcessSetAppService;
        public WorkProcessSetController(IWorkProcessSetAppService workProcessSetAppService)
        {
            _workProcessSetAppService = workProcessSetAppService;
        }

        [HttpPost]
        [DisableAuditing]
        public JHTPageAjaxRespone<PageData<WorkProcessSetInfoDto>> GetWorkProcessSets([FromBody] JHTPageAjaxResquest<DTO.CommonConditionData> where)
        {
            JHTPageAjaxRespone<PageData<WorkProcessSetInfoDto>> pageAjaxRespone = new JHTPageAjaxRespone<PageData<WorkProcessSetInfoDto>>();

            pageAjaxRespone.Data = _workProcessSetAppService.GetWorkProcessSetPageData(where);
            return pageAjaxRespone;
        }

        /// <summary>
        /// 添加工艺信息
        /// </summary>
        /// <param name="productWorkProcessConfig"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<WorkProcessSetInfoDto>> AddWorkProcessSetAsync([FromBody] WorkProcessSetInfoDto workProcessSetInfoDto)
        {
            var result = new JHTAjaxResponse<WorkProcessSetInfoDto>();
            result.Data = await _workProcessSetAppService.CreateAsync(workProcessSetInfoDto);
            return result;
        }

        /// <summary>
        /// 加载工艺配置数据
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<WorkProcessSetInfoDto>> LoadWorkProcessConfigDetailAsync([FromBody] EntityDto<long> entityDto)
        {
            var result = new JHTAjaxResponse<WorkProcessSetInfoDto>();
            result.Data = await _workProcessSetAppService.GetAsync(entityDto);
            return result;
        }

        /// <summary>
        /// 更新工艺基础西悉尼
        /// </summary>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> UpdateWorkProcessSetAsync([FromBody] WorkProcessSetInfoDto updateDto)
        {
            var result = new JHTAjaxResponse();
            await _workProcessSetAppService.UpdateAsync(updateDto);
            return result;
        }

        /// <summary>
        /// 更新配置详情
        /// </summary>
        /// <param name="productWorkProcessConfigDetailDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> UpdateWorkProcessSetConfigDataAsync([FromBody] WorkProcessSetInfoDto productWorkProcessConfigDetailDto)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            await _workProcessSetAppService.UpdateWorkProcessConfigDataAsync(productWorkProcessConfigDetailDto);
            ajaxResponse.Msg = "操作成功";
            return ajaxResponse;
        }


        /// <summary>
        /// 复制工艺信息
        /// </summary>
        /// <param name="copyProductConditionModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> CopyWorkProcessSetAsync([FromBody] EntityDto<long> copySetConfigId)
        {
            JHTAjaxResponse jHTAjaxResponse = new JHTAjaxResponse();
            await _workProcessSetAppService.CopyWorkProcessSetAsync(copySetConfigId);
            return jHTAjaxResponse;
        }


        /// <summary>
        /// 删除工艺
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> DeleteWorkProcessSetAsync([FromBody] EntityDto<long> id)
        {
            await _workProcessSetAppService.DeleteAsync(id);
            return new JHTAjaxResponse() { Msg = "操作成功" };
        }

        /// <summary>
        /// 获取工艺名称-版本，联级选择器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<List<UICascaderModel<WorkProcessSetInfoDto, long>>> GetProcessSetInCascader()
        {
            return new JHTAjaxResponse<List<UICascaderModel<WorkProcessSetInfoDto, long>>>()
            {
                Data = _workProcessSetAppService.GetProcessSetInCascader()
            };
        }

        #region 产品工艺配置

        /// <summary>
        /// 加载产品工艺配置
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPost]
        public PageData<ProductWorkProcessSetDto> LoadProductWorkProcessConfig([FromBody] JHTPageAjaxResquest<WorkProcessProductConditionDto> org)
        {
            return _workProcessSetAppService.LoadProductWorkProcessConfig(org);
        }

        /// <summary>
        /// 获取工艺名称
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<List<KeyValuePair<string, string>>>> GetWorkProcessSetNames()
        {
            List<KeyValuePair<string, string>> configNames = new List<KeyValuePair<string, string>>();
            var result = await _workProcessSetAppService.GetAllAsync(new DTO.CommonPageRequestDto()
            {
                QueryConditionObj = new DTO.CommonConditionData(),
                MaxResultCount = int.MaxValue,
                SkipCount = 0,
            });

            result.Items.GroupBy(p => p.SetName).ToList().ForEach(p =>
            {
                configNames.Add(new KeyValuePair<string, string>(p.Key, p.Key));
            });

            return new JHTAjaxResponse<List<KeyValuePair<string, string>>>()
            {
                Data = configNames
            };
        }

        /// <summary>
        /// 更新产品工艺信息
        /// </summary>
        /// <param name="creatModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> UpdateProdutctWorkProcessSetAsync([FromBody] SetProductProcessSetDto creatModel)
        {
            return await _workProcessSetAppService.UpdateProdutctWorkProcessSetAsync(creatModel);
        }

        /// <summary>
        /// 当前产品的工艺信息设置为当前工艺
        /// </summary>
        /// <param name="setProductCurrentWorkProcessConfigDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> SetProductCurrentWorkProcessSetAsync([FromBody] SetProductProcessSetDto dataModel)
        {
            return await _workProcessSetAppService.SetProductCurrentWorkProcessSetAsync(dataModel);
        }

        [HttpPost]
        public async Task<JHTAjaxResponse> SetProductProcessSetAsync([FromBody] SetProductProcessSetDto dataModel)
        {
            return await _workProcessSetAppService.SetProductProcessSetAsync(dataModel);
        }


        /// <summary>
        /// 删除当前产品工艺
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> DelProductWorkProcessSetRelation(EntityDto entityDto)
        {
            return await _workProcessSetAppService.DelProductCurrentWorkProcessSetAsync(entityDto);
        }


        /// <summary>
        /// 根据产品信息获取最新工序数据
        /// </summary>
        /// <param name="materilId"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<List<WorkProcessSetDetail>> LoadMaterialCurrentWorkProcess(EntityDto materilId)
        {
            JHTAjaxResponse<List<WorkProcessSetDetail>> ajaxResponse = new JHTAjaxResponse<List<WorkProcessSetDetail>>();
            ajaxResponse.Data = _workProcessSetAppService.LoadProductWorkProcessConfigByMaterialId(materilId);
            return ajaxResponse;
        }
        #endregion
    }
}
