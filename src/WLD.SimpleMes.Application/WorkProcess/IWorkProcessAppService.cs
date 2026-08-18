using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Material;
using WLD.SimpleMes.Material.Dto;
using WLD.SimpleMes.QualityControl.Dto;
using WLD.SimpleMes.Users.Dto;
using WLD.SimpleMes.WorkProcess.Dto;

namespace WLD.SimpleMes.WorkProcess
{
    public interface IWorkProcessAppService : IAsyncCrudAppService<WorkProcessInfoDto, long, DTO.CommonPageRequestDto, WorkProcessInfoDto, WorkProcessInfoDto>, IApplicationService
    {
        /// <summary>
        /// 查询工序列表
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        PageData<WorkProcessInfoDto> SearchWorkProcessInfo(JHTPageAjaxResquest<WorkProcessConditionDto> org);
        Task ToggleEnableWorkProcessAsync(EntityDto<long> dto);

        Task<List<MaterialInfoDto>> LoadConfigdMaterialInfosAsync(EntityDto<long> id);

        Task<JHTAjaxResponse> SetWorkProcessMaterialConfigAsync(WorkProcessMaterialConfigDto configDto);


        /// <summary>
        /// 获取工序配置的填报表单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<WorkProcessFormInfoRelationDto> GetWorkProcessFormRelation(long id);

        /// <summary>
        /// 添加工序表单配置
        /// </summary>
        /// <param name="relationConfigDto"></param>
        void SetWorkProcessFormRelation(WorkProcessFormRelationConfigDto relationConfigDto);

        Task<JHTAjaxResponse<MaterialBatchNumberDto>> CheckInputMaterialBatchNumberAsync(string materialBatchNumber, string inputMaterialBatchNumber, long currentWorkProcessId);

        /// <summary>
        /// 启用禁用工序表单
        /// </summary>
        /// <param name="realitonId"></param>
        /// <returns></returns>
        Task ToggleWorkProcessFormEnabledAsync(EntityDto<long> realitonId);

        /// <summary>
        /// 设置工序表单用途
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task SetWorkProcessFormUseTypeAsync(WorkProcessFormInfoRelationDto relation);
        Task<JHTAjaxResponse<MaterialBatchNumberDto>> BuildPrepareWorkProcessBatchNumberAsync(BuildSubMaterialBatchNumberDto buildSubMaterialBatchNumberDto);
        Task<JHTAjaxResponse> StartProduce(InputOperatorRecordInfo entityDto);
        Task<JHTAjaxResponse> InputMaterialAndOperatorAsync(InputOperatorRecordInfo inputInfo);
        JHTAjaxResponse<WorkProcessOperatorRecordDto> LoadCurrentWorkProcessOperatorInfo(InputOperatorRecordInfo workProcessOperatorRecordDto);
        JHTAjaxResponse NormalCompleteCurrentWorkProcess(CompleteWorkProcessRecordDto completeWorkProcessRecordDto);
        JHTAjaxResponse ReportProblem(ProblemRecordDto problemRecordDto, bool needChagneProductState = true);

        Task<string> SaveExceptionImgs(IFormFile files);

        Task<string> SaveDymaicFormImgs(IFormFile file);
        JHTAjaxResponse<List<WorkProcessInfoDto>> LoadFinishWorkPorcess(InputOperatorRecordInfo inputOperatorRecordInfo);
        Task<JHTAjaxResponse> StartExceptionDealAsync(InputOperatorRecordInfo inputOperatorRecordInfo);
        JHTAjaxResponse FinishExceptionDeal(ProblemDealRecordDto problemDealRecord, InputOperatorRecordInfo inputOperatorRecordInfo);
        List<WorkProcessInfoDto> LoadProductSortedWorkProcess(string productMaterialBatchNumber);
        JHTAjaxResponse CompleteIPQCWorkProcess(CompleteWorkProcessRecordDto completeWorkProcessRecordDto);
        JHTAjaxResponse<WorkProcessInfoDto> LoadProductCurrentWorkProcess(InputOperatorRecordInfo inputOperatorRecordInfo);
        WorkProcessInfoDto GetProductCurrentWorkProcessInfo(string productMaterialBatchNumber);

        Task<JHTAjaxResponse> UpdateWorkProcessMaterialInfoAsync(InputOperatorRecordInfo inputOperatorRecordInfo);
        JHTAjaxResponse UpdateWorkProcessFillInfo(CompleteWorkProcessRecordDto completeWorkProcessRecordDto);

        JHTAjaxResponse SaveProblemDealRecord(ProblemDealRecordDto problemDealRecord, ProblemRecordDto problemRecord);
        JHTAjaxResponse<List<WorkProcessInfoDto>> LoadStartWorkProcess(InputOperatorRecordInfo inputOperatorRecordInfo);
        JHTAjaxResponse<EntityDto<long>> SaveFormDraft(CompleteWorkProcessRecordDto completWorkProcessRequest, bool isDraft = true);
        Task<JHTAjaxResponse<MaterialBatchNumberDto>> CheckLineMaterialInfoBOMAsync(BuildSubMaterialBatchNumberDto buildSubMaterialBatchNumberDto);
        JHTAjaxResponse SaveProblemDealRecord(ProblemDealRecordDto problemDealRecord);
        JHTAjaxResponse UpdateProblemJudgeInfo(ProblemRecordDto problemRecord);
        JHTAjaxResponse ReportCommonProblem(ProblemRecordDto inputInfo, ProblemDealRecordDto problemDealRecord);
        Task<JHTAjaxResponse<MaterialBatchNumberDto>> BuildWorkOrderBatchNumberAsync(BuildSubMaterialBatchNumberDto buildSubMaterialBatchNumberDto);
    }
}
