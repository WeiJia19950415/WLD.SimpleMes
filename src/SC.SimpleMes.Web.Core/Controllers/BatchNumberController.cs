using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.Authorization;
using JHT.Abp.CommonModels;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Authorization;
using SC.SimpleMes.Material;
using SC.SimpleMes.Material.Dto;
using SC.SimpleMes.QualityControl;
using SC.SimpleMes.QualityControl.Dto;
using SC.SimpleMes.Report.Dto;

namespace SC.SimpleMes.Controllers
{
    /// <summary>
    /// 批次号管理接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [AbpAuthorize]
    [ApiController]
    public class BatchNumberController : SimpleMesControllerBase
    {
        private readonly IMaterialBatchNumberAppService _materialBatchNumberAppService;
        private readonly IExcelExporter _exporter;
        private readonly IProblemRecordAppService _problemRecordAppService;
        public BatchNumberController(IMaterialBatchNumberAppService materialBatchNumberAppService, IExcelExporter exporter, IProblemRecordAppService problemRecordAppService)
        {
            _materialBatchNumberAppService = materialBatchNumberAppService;
            _problemRecordAppService = problemRecordAppService;
            _exporter=exporter;
        }


        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<PageData<MaterialBatchNumberDto>>> SearchBatchNumberAsync([FromBody] JHTPageAjaxResquest<MaterialBatchNumberConditionDto> where)
        {
            var result = await _materialBatchNumberAppService.GetAllAsync(new DTO.CommonPageRequestDto()
            {
                QueryConditionObj = where.Condition,
                MaxResultCount = where.PageSize,
                SkipCount = where.SkipCount,
            });

            return new JHTPageAjaxRespone<PageData<MaterialBatchNumberDto>>()
            {
                Data = new PageData<MaterialBatchNumberDto>()
                {
                    List = result.Items.ToList(),
                    Total = result.TotalCount
                }
            };
        }

        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<PageData<MaterialBatchNumberDto>>> LoadCreatedBatchNumberAsync([FromBody] JHTPageAjaxResquest<MaterialBatchNumberConditionDto> where)
        {
            var result = await _materialBatchNumberAppService.LoadCreatedBatchNumberAsync(where);

            return new JHTPageAjaxRespone<PageData<MaterialBatchNumberDto>>()
            {
                Data = result
            };
        }

        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_OrderMaterialProduceStatuse)]
        public async Task<FileStreamResult> ExportProduceStatuReportAsync([FromBody] JHTPageAjaxResquest<MaterialBatchNumberConditionDto> where)
        {
            List<MaterialBatchNumberExportDto> dDImportantInfoExportDtos = await _materialBatchNumberAppService.LoadCreatedBatchNumberExportDtoAsync(where);
            var byteArrary = await _exporter.ExportAsByteArray(dDImportantInfoExportDtos);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            return new FileStreamResult(new MemoryStream(byteArrary), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = $"批次物料报表{DateTime.Now.ToString("yyyyMMddHHmm")}.xlsx",
            };
        }


        [HttpPost]
        public async Task<JHTAjaxResponse> DelBatchNumber(EntityDto<long> entityDto)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            ajaxResponse.Msg = "操作成功";
            await _materialBatchNumberAppService.DeleteAsync(entityDto);
            return ajaxResponse;
        }



        [HttpPost]
        public JHTAjaxResponse<List<ProblemRecordDto>> LoadProblemRecord([FromBody] EntityDto<string> inputModel)
        {
            JHTAjaxResponse<List<ProblemRecordDto>> ajaxResponse = new JHTAjaxResponse<List<ProblemRecordDto>>();
            ajaxResponse.Data = _problemRecordAppService.LoadCurrentWorkProcessProblemRecord(inputModel.Id);

            return ajaxResponse;
        }

    }   
}
