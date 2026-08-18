using Abp.Application.Services.Dto;
using Abp.Authorization;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using SC.SimpleMes.Authorization;
using SC.SimpleMes.DTO;
using SC.SimpleMes.Material;
using SC.SimpleMes.Material.Dto;
using SC.SimpleMes.Models;
using SC.SimpleMes.QualityControl;
using SC.SimpleMes.QualityControl.Dto;
using SC.SimpleMes.Report.Dto;

namespace SC.SimpleMes.Controllers.PadManage
{
    [Route("api/[controller]/[action]")]
    [AbpAuthorize]
    [ApiController]
    public class PadManageQualtityController : SimpleMesControllerBase
    {
        private readonly IQualityProblemDefineAppService _qualityProblemDefineAppService;
        private readonly IProblemCategoryCache _problemCategoryCache;
        private readonly IProblemRecordAppService _problemRecordAppService;
        private readonly IMaterialAppService _materialAppService;

        public PadManageQualtityController(IQualityProblemDefineAppService qualityProblemDefineAppService, 
            IProblemCategoryCache problemCategory, 
            IMaterialAppService materialAppService,
            IProblemRecordAppService problemRecordAppService)
        {
            this._qualityProblemDefineAppService = qualityProblemDefineAppService;
            _problemCategoryCache = problemCategory;
            _problemRecordAppService = problemRecordAppService;
            _materialAppService = materialAppService;
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

        /// <summary>
        /// 加载相关的反馈记录
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>

        [HttpPost]
        public JHTAjaxResponse<List<ProblemRecordDto>> LoadProblemRecord([FromBody] PadManageRequestModel inputModel)
        {
            JHTAjaxResponse<List<ProblemRecordDto>> ajaxResponse = new JHTAjaxResponse<List<ProblemRecordDto>>();
            ajaxResponse.Data = _problemRecordAppService.LoadCurrentWorkProcessProblemRecord(inputModel.ProductMaterialBatchNumber);

            return ajaxResponse;
        }

        /// <summary>
        /// 加载问题处理记录
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<ProblemDealRecordDto> LoadProblemDealRecord([FromBody] PadManageRequestModel inputModel)
        {
            JHTAjaxResponse<ProblemDealRecordDto> ajaxResponse = new JHTAjaxResponse<ProblemDealRecordDto>();
            ajaxResponse.Data = _problemRecordAppService.LoadCurrentWorkProcessProblemDealRecord(new WorkProcess.Dto.InputOperatorRecordInfo()
            {
                OperatroMaterilBatchNumber = inputModel.ProductMaterialBatchNumber,
                WorkProcessId = inputModel.CurrentWorkProcessId,
                WorkStationId = inputModel.CurrentWorkStaionId
            });

            return ajaxResponse;
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.Page_QualityManager_QC)]
        public async Task<JHTAjaxResponse> MarkBatchNoOverUseInfoAsync([FromBody] View_BatchMaterialUsedReportDto request)
        {
           return await _materialAppService.MarkBatchNoOverUseInfoAsync(request);
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.Page_QualityManager_QC)]

        public async Task<JHTAjaxResponse> SetMaterialStatuAsync([FromBody] MaterialBatchNumberDto request)
        {
            return await _materialAppService.SetMaterialStatuAsync(request);
        }
    }
}
