using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.DynamicForms;
using SC.SimpleMes.DynamicForms.DTO;
using SC.SimpleMes.Material.Dto;
using SC.SimpleMes.Models;
using SC.SimpleMes.Models.PadManageRequest;
using SC.SimpleMes.QualityControl.Dto;
using SC.SimpleMes.Startup;
using SC.SimpleMes.Users.Dto;
using SC.SimpleMes.WorkOrder;
using SC.SimpleMes.WorkOrder.DTO;
using SC.SimpleMes.WorkProcess;
using SC.SimpleMes.WorkProcess.Dto;
using SC.SimpleMes.WorkStation;

namespace SC.SimpleMes.Controllers.PadManage
{
    [Route("api/[controller]/[action]")]
    [AbpAuthorize]
    [ApiController]
    public class PadManageWorkProcessController : SimpleMesControllerBase
    {
        private readonly IWorkProcessAppService _workProcessAppService;
        private readonly IFormTemplateInfoAppService _formTemplateInfoAppService;
        private readonly IWorkOrderAppService _workOrderAppService;
        public PadManageWorkProcessController(
            IWorkProcessAppService workProcessAppService,
            IFormTemplateInfoAppService formTemplateInfoAppService,
            IWorkOrderAppService workOrderAppService)
        {
            _workProcessAppService = workProcessAppService;
            _formTemplateInfoAppService = formTemplateInfoAppService;
            _workOrderAppService = workOrderAppService;
        }


        /// <summary>
        /// 获取投放物料批信息
        /// 判断该物料能否在该工序中使用
        /// 1、物料允许使用
        /// 2、物料批次号未被记录
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(PadManageActionFilter))]
        public async Task<JHTAjaxResponse<MaterialBatchNumberDto>> CheckInputMaterialBatchNumberAsync([FromBody] ScanMaterialBatchNumberRequest entityDto)
        {
            JHTAjaxResponse<MaterialBatchNumberDto> ajaxResponse = new JHTAjaxResponse<MaterialBatchNumberDto>();
            return await _workProcessAppService.CheckInputMaterialBatchNumberAsync(entityDto.ProductMaterialBatchNumber, entityDto.MaterialBatchNumber, entityDto.CurrentWorkProcessId);
        }


        /// <summary>
        /// 投入物料和人员生产信息
        /// 1、检查物料是否合规，合量
        /// 2、插入投料记录，人员操作记录
        /// 3、开始生产
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(PadManageActionFilter))]
        public async Task<JHTAjaxResponse> InputMaterialAndOperatorAsync([FromBody] ConfirmProduceInfo confirmProduceInfo)
        {
            var inputInfo = new InputOperatorRecordInfo()
            {
                OperatroMaterilBatchNumber = confirmProduceInfo.ProductMaterialBatchNumber,
                OperatroMaterilBatchType = WorkProcessOperateTypeEnum.开始生产,
                Users = confirmProduceInfo.Operator,
                WorkProcessId = confirmProduceInfo.CurrentWorkProcessId,
                WorkStationId = confirmProduceInfo.CurrentWorkStaionId,
                InputMaterialInfos = confirmProduceInfo.InputMaterialInfos,
            };

            return await _workProcessAppService.InputMaterialAndOperatorAsync(inputInfo);
        }

        /// <summary>
        /// 生成产品批次号信息
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(PadManageActionFilter))]
        public async Task<JHTAjaxResponse<MaterialBatchNumberDto>> BuildWorkOrderBatchNumberAsync([FromBody] BuildMaterialBatchNumberRequest entityDto)
        {
            return await _workProcessAppService.BuildWorkOrderBatchNumberAsync(new BuildSubMaterialBatchNumberDto()
            {
                CurrentWorkProcessId = entityDto.CurrentWorkProcessId,
                CurrentWorkStationId = entityDto.CurrentWorkStaionId,
                WorkOrderNumber = entityDto.WorkOrderNumber,
                MatrialCount = entityDto.MaterialCount,
                Creator = entityDto.Creator,
                OperateRecordId = entityDto.OperateRecordId,
            });

        }

        /// <summary>
        /// 生成前置物料批次信息
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(PadManageActionFilter))]
        public async Task<JHTAjaxResponse<MaterialBatchNumberDto>> BuildPrepareWorkProcessBatchNumberAsync([FromBody] BuildSubMaterialBatchNumberRequest entityDto)
        {
            return await _workProcessAppService.BuildPrepareWorkProcessBatchNumberAsync(new BuildSubMaterialBatchNumberDto()
            {
                CurrentWorkProcessId = entityDto.CurrentWorkProcessId,
                CurrentWorkStationId = entityDto.CurrentWorkStaionId,
                InputMatreilInfos = entityDto.InputMatreilInfos,
                WorkOrderNumber = entityDto.WorkOrderNumber,
                MatrialCount = entityDto.MatrialCount,
                Creator = entityDto.Creator,
                OnlineMaterialInfoId = entityDto.OnlineMaterialInfoId,
                OperateRecordId = entityDto.OperateRecordId,
                IsRepairedInput = entityDto.IsRepairedInput,
            });
        }

        [HttpPost]
        [TypeFilter(typeof(PadManageActionFilter))]
        public async Task<JHTAjaxResponse<MaterialBatchNumberDto>> CheckPrepareWorkProcessMaterialInfoAsync([FromBody] BuildSubMaterialBatchNumberRequest entityDto)
        {
            return await _workProcessAppService.CheckLineMaterialInfoBOMAsync(new BuildSubMaterialBatchNumberDto()
            {
                CurrentWorkProcessId = entityDto.CurrentWorkProcessId,
                CurrentWorkStationId = entityDto.CurrentWorkStaionId,
                InputMatreilInfos = entityDto.InputMatreilInfos,
                WorkOrderNumber = entityDto.WorkOrderNumber,
                MatrialCount = entityDto.MatrialCount,
                Creator = entityDto.Creator,
                OnlineMaterialInfoId = entityDto.OnlineMaterialInfoId,
            });
        }

        /// <summary>
        /// 开始生产
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(PadManageActionFilter))]
        public async Task<JHTAjaxResponse> StartProduce([FromBody] StartProduceRequestModel entityDto)
        {
            return await _workProcessAppService.StartProduce(new InputOperatorRecordInfo()
            {
                OperatroMaterilBatchNumber = entityDto.OperatroMaterilBatchNumber,
                OperatroMaterilBatchType = entityDto.OperatroMaterilBatchType,
                Users = entityDto.Users,
                WorkProcessId = entityDto.CurrentWorkProcessId,
                WorkStationId = entityDto.CurrentWorkStaionId,
                WorkOrderNumber = entityDto.WorkOrderNumber,
            });
        }

        /// <summary>
        /// 检查当前产品,当前工序的工序信息
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<WorkProcessOperatorRecordDto> LoadCurrentWorkProcessOperatorInfo([FromBody] PadManageRequestModel entityDto)
        {
            return _workProcessAppService.LoadCurrentWorkProcessOperatorInfo(new InputOperatorRecordInfo()
            {
                WorkProcessId = entityDto.CurrentWorkProcessId,
                WorkStationId = entityDto.CurrentWorkStaionId,
                WorkOrderNumber = entityDto.WorkOrderNumber,
                OperatroMaterilBatchNumber = entityDto.ProductMaterialBatchNumber,
                OperatroMaterilBatchType = WorkProcessOperateTypeEnum.开始生产
            });
        }

        /// <summary>
        /// 获取当前工序的填报表单数据
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<FormInfoRecordDto> LoadCurrentWorkProcessFormFillInfo([FromBody] LoadFormFillRequestModel entityDto)
        {
            return _formTemplateInfoAppService.LoadFormInfoRecordInfo(new InputOperatorRecordInfo()
            {
                WorkProcessId = entityDto.CurrentWorkProcessId,
                WorkStationId = entityDto.CurrentWorkStaionId,
                OperatroMaterilBatchNumber = entityDto.ProductMaterialBatchNumber,
            }, entityDto.FormUseType);
        }


        [HttpPost]
        [TypeFilter(typeof(PadManageActionFilter))]
        public JHTAjaxResponse<EntityDto<long>> SaveFormDraft([FromBody] CompletWorkProcessRequest entityDto)
        {
            return _workProcessAppService.SaveFormDraft(new CompleteWorkProcessRecordDto()
            {
                WorkProcessId = entityDto.CurrentWorkProcessId,
                WorkStationId = entityDto.CurrentWorkStaionId,
                OperatroMaterilBatchNumber = entityDto.ProductMaterialBatchNumber,
                IsNormalFinish = entityDto.IsNormalFinish,
                FormTemlpateId = entityDto.FormTemlpateId,
                FormRecordInfo = entityDto.FormRecordInfo,
                FormRecordInfoId = entityDto.FormRecordId
            });
        }

        [HttpPost]
        [TypeFilter(typeof(PadManageActionFilter))]
        public JHTAjaxResponse<EntityDto<long>> UpdateFormFillInfo([FromBody] CompletWorkProcessRequest entityDto)
        {
            return _workProcessAppService.SaveFormDraft(new CompleteWorkProcessRecordDto()
            {
                WorkProcessId = entityDto.CurrentWorkProcessId,
                WorkStationId = entityDto.CurrentWorkStaionId,
                OperatroMaterilBatchNumber = entityDto.ProductMaterialBatchNumber,
                IsNormalFinish = entityDto.IsNormalFinish,
                FormTemlpateId = entityDto.FormTemlpateId,
                FormRecordInfo = entityDto.FormRecordInfo,
                FormRecordInfoId = entityDto.FormRecordId
            }, false);
        }

        /// <summary>
        ///  完成工序
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(PadManageActionFilter))]
        public JHTAjaxResponse CompleteCurrentWorkProcess([FromBody] CompletWorkProcessRequest entityDto)
        {
            return _workProcessAppService.NormalCompleteCurrentWorkProcess(new CompleteWorkProcessRecordDto()
            {
                WorkProcessId = entityDto.CurrentWorkProcessId,
                WorkStationId = entityDto.CurrentWorkStaionId,
                OperatroMaterilBatchNumber = entityDto.ProductMaterialBatchNumber,
                IsNormalFinish = entityDto.IsNormalFinish,
                FormTemlpateId = entityDto.FormTemlpateId,
                FormRecordInfoId = entityDto.FormRecordId,
                FormRecordInfo = entityDto.FormRecordInfo,
            });
        }

        [HttpPost]
        [TypeFilter(typeof(PadManageActionFilter))]
        public JHTAjaxResponse CompleteIPQCWorkProcess([FromBody] CompletWorkProcessRequest entityDto)
        {
            return _workProcessAppService.CompleteIPQCWorkProcess(new CompleteWorkProcessRecordDto()
            {
                WorkProcessId = entityDto.CurrentWorkProcessId,
                WorkStationId = entityDto.CurrentWorkStaionId,
                OperatroMaterilBatchNumber = entityDto.ProductMaterialBatchNumber,
                FormTemlpateId = entityDto.FormTemlpateId,
                FormRecordInfo = entityDto.FormRecordInfo,
            });
        }

        #region 异常处理


        /// <summary>
        /// 上报问题
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(PadManageActionFilter))]
        public JHTAjaxResponse ReportProblem([FromBody] ProblemReportRequest problemReportRequest)
        {
            return _workProcessAppService.ReportProblem(new ProblemRecordDto()
            {
                WorkStationId = problemReportRequest.CurrentWorkStaionId,
                BatchMaterilaNumber = problemReportRequest.ProductMaterialBatchNumber,
                BelongProblemDefineId = problemReportRequest.BelongProblemDefineId,
                BelongWorkProcessId = problemReportRequest.CurrentWorkProcessId,
                WorkOrderNumber = problemReportRequest.WorkOrderNumber,
                CreationTime = DateTime.Now,
                CreatorUserId = AbpSession.UserId.GetValueOrDefault(),
                DetailDescretion = problemReportRequest.DetailDescretion,
                QualityProblemDefineNumber = problemReportRequest.QualityProblemDefineNumber,
                RelationImgs = problemReportRequest.RelationImgs,
                Supplier = problemReportRequest.Supplier,
                UnitName = problemReportRequest.UnitName,
                CheckCount = problemReportRequest.CheckCount,
                ProblemCount = problemReportRequest.ProblemCount,
                MaterialNumber = problemReportRequest.MaterialNumber,
                MaterialName = problemReportRequest.MaterialName,
            });
        }

        /// <summary>
        /// 上报普通问题
        /// </summary>
        /// <param name="problemReportRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse ReportCommonProblem([FromBody] ProblemReportRequest problemReportRequest)
        {
            return _workProcessAppService.ReportCommonProblem(new ProblemRecordDto()
            {
                WorkStationId = problemReportRequest.WorkStationId,
                BatchMaterilaNumber = problemReportRequest.ProductMaterialBatchNumber,
                BelongProblemDefineId = problemReportRequest.BelongProblemDefineId,
                BelongWorkProcessId = problemReportRequest.BelongWorkProcessId,
                WorkOrderNumber = problemReportRequest.WorkOrderNumber,
                ResponsibleDepartmentId = problemReportRequest.ResponsibleDepartmentId,
                CreationTime = DateTime.Now,
                CheckWarpCount = problemReportRequest.CheckWarpCount,
                ProblemWarpCount = problemReportRequest.ProblemWarpCount,
                WrapUnitName = problemReportRequest.WrapUnitName,
                CreatorUserId = AbpSession.UserId.GetValueOrDefault(),
                DetailDescretion = problemReportRequest.DetailDescretion,
                QualityProblemDefineNumber = problemReportRequest.QualityProblemDefineNumber,
                DiscardType = problemReportRequest.DiscardType,
                RelationImgs = problemReportRequest.RelationImgs,
                Supplier = problemReportRequest.Supplier,
                UnitName = problemReportRequest.UnitName,
                CheckCount = problemReportRequest.CheckCount,
                ProblemCount = problemReportRequest.ProblemCount,
                MaterialNumber = problemReportRequest.MaterialNumber,
                MaterialName = problemReportRequest.MaterialName,
            }, problemReportRequest.DealRecordDto);
        }

        /// <summary>
        /// 上报产品问题
        /// </summary>
        /// <param name="problemReportRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse ReportProductProblem([FromBody] ProblemReportRequest problemReportRequest)
        {
            return _workProcessAppService.ReportProblem(new ProblemRecordDto()
            {
                WorkStationId = problemReportRequest.CurrentWorkStaionId,
                BatchMaterilaNumber = problemReportRequest.ProductMaterialBatchNumber,
                BelongProblemDefineId = problemReportRequest.BelongProblemDefineId,
                BelongWorkProcessId = problemReportRequest.CurrentWorkProcessId,
                WorkOrderNumber = problemReportRequest.WorkOrderNumber,
                CreationTime = DateTime.Now,
                CreatorUserId = AbpSession.UserId.GetValueOrDefault(),
                DetailDescretion = problemReportRequest.DetailDescretion,
                QualityProblemDefineNumber = problemReportRequest.QualityProblemDefineNumber,
                RelationImgs = problemReportRequest.RelationImgs,
                Supplier = problemReportRequest.Supplier,
                UnitName = problemReportRequest.UnitName,
                CheckCount = problemReportRequest.CheckCount,
                ProblemCount = problemReportRequest.ProblemCount,
                MaterialNumber = problemReportRequest.MaterialNumber,
                MaterialName = problemReportRequest.MaterialName,
            });
        }

        /// <summary>
        /// 上传问题文件
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<string>> UploadExceptionFileAsync([FromForm] IFormFile files)
        {
            if (files == null && Request.Form.Files.Count > 0)
            {
                files = Request.Form.Files[0];
            }

            var result = await _workProcessAppService.SaveExceptionImgs(files);
            return new JHTAjaxResponse<string>(result) { Msg = "图片上传成功" };
        }

        /// <summary>
        /// 上传问题文件
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<string>> SaveDymaicFormImgsAsync([FromForm] IFormFile files)
        {
            if (files == null && Request.Form.Files.Count > 0)
            {
                files = Request.Form.Files[0];
            }

            var result = await _workProcessAppService.SaveDymaicFormImgs(files);
            return new JHTAjaxResponse<string>(result) { Msg = "图片上传成功" };
        }


        /// <summary>
        /// 加载当前产品已经完成的工序
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<List<WorkProcessInfoDto>> LoadFinishWorkPorcess([FromBody] PadManageRequestModel requestModel)
        {
            return _workProcessAppService.LoadFinishWorkPorcess(new InputOperatorRecordInfo()
            {
                OperatroMaterilBatchNumber = requestModel.ProductMaterialBatchNumber,
                WorkProcessId = requestModel.CurrentWorkProcessId,
                WorkStationId = requestModel.CurrentWorkStaionId
            });
        }


        /// <summary>
        /// 加载当前产品的首工序
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<List<WorkProcessInfoDto>> LoadStartWorkProcess([FromBody] PadManageRequestModel requestModel)
        {
            return _workProcessAppService.LoadStartWorkProcess(new InputOperatorRecordInfo()
            {
                OperatroMaterilBatchNumber = requestModel.ProductMaterialBatchNumber,
                WorkProcessId = requestModel.CurrentWorkProcessId,
                WorkStationId = requestModel.CurrentWorkStaionId
            });
        }


        [HttpPost]
        public JHTAjaxResponse<List<WorkProcessInfoDto>> LoadProductWorkPorcess([FromBody] PadManageRequestModel requestModel)
        {
            JHTAjaxResponse<List<WorkProcessInfoDto>> ajaxResponse = new JHTAjaxResponse<List<WorkProcessInfoDto>>();
            var finishWorkProcess = _workProcessAppService.LoadFinishWorkPorcess(new InputOperatorRecordInfo()
            {
                OperatroMaterilBatchNumber = requestModel.ProductMaterialBatchNumber,
                WorkProcessId = requestModel.CurrentWorkProcessId,
                WorkStationId = requestModel.CurrentWorkStaionId
            });

            List<WorkProcessInfoDto> workProcessInfoDtos = _workProcessAppService.LoadProductSortedWorkProcess(requestModel.ProductMaterialBatchNumber);

            workProcessInfoDtos.ForEach(p =>
            {
                if (finishWorkProcess.Data.Count(d => d.Id == p.Id) > 0)
                {
                    p.IsDone = true;
                }
            });

            ajaxResponse.Data = workProcessInfoDtos.OrderByDescending(p => p.IsDone).ToList();
            return ajaxResponse;

        }

        /// <summary>
        /// 开始异常处置
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> StartExceptionDeal([FromBody] PadManageRequestModel requestModel)
        {
            return await _workProcessAppService.StartExceptionDealAsync(new InputOperatorRecordInfo()
            {
                OperatroMaterilBatchNumber = requestModel.ProductMaterialBatchNumber,
                WorkProcessId = requestModel.CurrentWorkProcessId,
                WorkStationId = requestModel.CurrentWorkStaionId,
                OperatroMaterilBatchType = WorkProcessOperateTypeEnum.异常处置,

            });
        }


        /// <summary>
        /// 完成异常处置
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse FinishExceptionDeal([FromBody] FinishExceptionDealRequest requestModel)
        {
            return _workProcessAppService.FinishExceptionDeal(requestModel.ProblemDealRecord, new InputOperatorRecordInfo()
            {
                WorkStationId = requestModel.CurrentWorkStaionId,
                WorkProcessId = requestModel.CurrentWorkProcessId,
                OperatroMaterilBatchNumber = requestModel.ProductMaterialBatchNumber,
            });
        }

        /// <summary>
        /// 保存处置方案
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> SaveProblemDealRecord([FromBody] FinishExceptionDealRequest requestModel)
        {
            return _workProcessAppService.SaveProblemDealRecord(requestModel.ProblemDealRecord, requestModel.ProblemRecord);
        }

        /// <summary>
        /// 保存判定信息
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse UpdateProblemJudgeInfo([FromBody] ProblemRecordDto requestModel)
        {
            return _workProcessAppService.UpdateProblemJudgeInfo(requestModel);
        }

        /// <summary>
        /// 保存处置方案
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse SaveTeclogoySolution([FromBody] ProblemRecordDto requestModel)
        {
            return _workProcessAppService.SaveProblemDealRecord(requestModel.DealRecordDto);
        }


        /// <summary>
        /// 加载产品当前的工序
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<WorkProcessInfoDto> LoadProductCurrentWorkProcess([FromBody] PadManageRequestModel requestModel)
        {
            return _workProcessAppService.LoadProductCurrentWorkProcess(new InputOperatorRecordInfo()
            {
                WorkStationId = requestModel.CurrentWorkStaionId,
                WorkProcessId = requestModel.CurrentWorkProcessId,
                OperatroMaterilBatchNumber = requestModel.ProductMaterialBatchNumber
            });
        }


        /// <summary>
        /// 异常处理，更新工序物料信息
        /// </summary>
        /// <param name="produceInfo"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        public async Task<JHTAjaxResponse> UpdateWorkProcessMaterialInfoAsync([FromBody] ConfirmProduceInfo confirmProduceInfo)
        {
            return await _workProcessAppService.UpdateWorkProcessMaterialInfoAsync(new InputOperatorRecordInfo()
            {
                OperatroMaterilBatchNumber = confirmProduceInfo.ProductMaterialBatchNumber,
                OperatroMaterilBatchType = WorkProcessOperateTypeEnum.开始生产,
                Users = confirmProduceInfo.Operator,
                WorkProcessId = confirmProduceInfo.CurrentWorkProcessId,
                WorkStationId = confirmProduceInfo.CurrentWorkStaionId,
                InputMaterialInfos = confirmProduceInfo.InputMaterialInfos,
                MaterialDiscardRecords = confirmProduceInfo.MaterialDiscardRecords,
            });
        }

        /// <summary>
        /// 异常处理更新工序填报数据
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        public JHTAjaxResponse UpdateWorkProcessFillInfo([FromBody] CompletWorkProcessRequest entityDto)
        {
            return _workProcessAppService.UpdateWorkProcessFillInfo(new CompleteWorkProcessRecordDto()
            {
                WorkProcessId = entityDto.CurrentWorkProcessId,
                WorkStationId = entityDto.CurrentWorkStaionId,
                OperatroMaterilBatchNumber = entityDto.ProductMaterialBatchNumber,
                FormTemlpateId = entityDto.FormTemlpateId,
                FormRecordInfo = entityDto.FormRecordInfo,
            });
        }

        #endregion
    }
}
