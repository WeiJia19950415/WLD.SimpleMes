using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Json;
using JHT.Abp.CommonModels;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WLD.SimpleMes.AttachFile;
using WLD.SimpleMes.Authorization;
using WLD.SimpleMes.Configuration;
using WLD.SimpleMes.Configuration.Dto;
using WLD.SimpleMes.DynamicForms.DTO;
using WLD.SimpleMes.QualityControl;
using WLD.SimpleMes.QualityControl.Dto;
using WLD.SimpleMes.Report;
using WLD.SimpleMes.Report.Dto;
using WLD.SimpleMes.WorkOrder.DTO;
using WLD.SimpleMes.WorkProcess.Dto;
using Xceed.Document.NET;
using Xceed.Words.NET;
namespace WLD.SimpleMes.Controllers
{

    [Route("api/[controller]/[action]")]
    [AbpAuthorize]

    public class ReportController : SimpleMesControllerBase
    {
        private readonly IReportAppService _reportAppService;

        private readonly IProblemRecordAppService _problemRecordAppService;
        private readonly IocManager _iocManager;
        private readonly IExcelExporter _exporter;
        private readonly IOptionsMonitor<FileSaveOptions> _fileSaveOptions;
        private readonly IConfigurationAppService _configurationAppService;

        public ReportController(
            IReportAppService reportAppService, IProblemRecordAppService problemRecordAppService,
            IOptionsMonitor<FileSaveOptions> fileSaveOptions,
            IConfigurationAppService configurationAppService,
            IocManager iocManager, IExcelExporter exporter)
        {
            _reportAppService = reportAppService;
            _problemRecordAppService = problemRecordAppService;
            _iocManager = iocManager;
            _exporter = exporter;
            _fileSaveOptions = fileSaveOptions;
            _configurationAppService = configurationAppService;
        }


        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_StationCapacity)]
        public async Task<JHTPageAjaxRespone<PageData<WorkProcessCapacityDailyReportRecordDto>>> QueryStationCapacityDailyReportAsync([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            return new JHTPageAjaxRespone<PageData<WorkProcessCapacityDailyReportRecordDto>>()
            {
                Data = await _reportAppService.LoadStationCacityDailyRecordsAsync(where)
            };
        }


        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_StationCapacity)]
        public async Task<JHTPageAjaxRespone<PageData<WorkProcessCapacityStaticReportDto>>> LoadStationCacityStaticRecordsAsync([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            return new JHTPageAjaxRespone<PageData<WorkProcessCapacityStaticReportDto>>()
            {
                Data = await _reportAppService.LoadStationCacityStaticRecordsAsync(where)
            };
        }


        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_ProductLineCapacity)]
        public async Task<JHTPageAjaxRespone<PageData<ProductLineCapacityDailyReportRecordDto>>> LoadProductLineCacityStaticRecordsAsync([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            return new JHTPageAjaxRespone<PageData<ProductLineCapacityDailyReportRecordDto>>()
            {
                Data = await _reportAppService.LoadProductLineCacityStaticRecordsAsync(where)
            };
        }


        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_QualitityReport)]
        public async Task<JHTPageAjaxRespone<PageData<WorkProcessProblemDailyReportRecordDto>>> LoadWorkProcessProblemStaticRecordsAsync([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            return new JHTPageAjaxRespone<PageData<WorkProcessProblemDailyReportRecordDto>>()
            {
                Data = await _reportAppService.LoadWorkProcessProblemStaticRecordsAsync(where)
            };
        }


        /// <summary>
        /// 加载问题处理记录
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_QualitityRecord)]
        public JHTAjaxResponse<ProblemDealRecordDto> LoadProblemDealRecordByProblemId([FromBody] EntityDto<long> id)
        {
            JHTAjaxResponse<ProblemDealRecordDto> ajaxResponse = new JHTAjaxResponse<ProblemDealRecordDto>();
            ajaxResponse.Data = _problemRecordAppService.LoadProblemDealRecordByProblemId(id);

            return ajaxResponse;
        }

        /// <summary>
        /// 加载问题处理记录
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_QualitityRecord)]
        public JHTAjaxResponse<List<ProblemDealRecordDto>> LoadProblemDealRecords([FromBody] EntityDto<string> id)
        {
            JHTAjaxResponse<List<ProblemDealRecordDto>> ajaxResponse = new JHTAjaxResponse<List<ProblemDealRecordDto>>();
            ajaxResponse.Data = _problemRecordAppService.LoadProblemDealRecords(id);

            return ajaxResponse;
        }


        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_QualitityRecord)]
        public async Task<JHTPageAjaxRespone<PageData<View_ProblemRecordDto>>> LoadQualityDetailsRecordsAsync([FromBody] JHTPageAjaxResquest<ProblemRecordQueryCondition> where)
        {
            return new JHTPageAjaxRespone<PageData<View_ProblemRecordDto>>()
            {
                Data = await _problemRecordAppService.LoadQualityDetailsRecordsAsync(where)
            };
        }


        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_QualitityRecord)]
        public async Task<JHTPageAjaxRespone<PageData<View_ProblemRecordDto>>> LoadQualityDetailsRecordsTestsAsync([FromBody] JHTPageAjaxResquest<ProblemRecordQueryCondition> where)
        {


            return new JHTPageAjaxRespone<PageData<View_ProblemRecordDto>>()
            {
                Data = await _problemRecordAppService.LoadQualityDetailsRecordsAsync(where)
            };
        }



        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_DDKeyInfoReport)]
        public async Task<JHTPageAjaxRespone<PageData<DDImportantInfoDto>>> LoadDDImportantInfosAsync([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {

            return new JHTPageAjaxRespone<PageData<DDImportantInfoDto>>()
            {
                Data = await _reportAppService.LoadDDImportantInfosAsync(where)
            };
        }


        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_DDKeyInfoReport)]
        public async Task<FileStreamResult> ExportDDImportantInfos([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            List<DDImportantInfoExportDto> dDImportantInfoExportDtos = _reportAppService.LoadExportDDImportantInfosAsync(where);
            var byteArrary = await _exporter.ExportAsByteArray(dDImportantInfoExportDtos);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            return new FileStreamResult(new MemoryStream(byteArrary), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = $"电堆关键性能信息表{DateTime.Now.ToString("yyyyMMddHHmm")}.xlsx",
            };
        }


        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_DDKeyInfoReport)]
        public async Task<FileStreamResult> ExportStockImportantInfos([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            List<StockDDExportDto> dDImportantInfoExportDtos = await _reportAppService.LoadExportStockDDImportantInfosAsync(where);
            var byteArrary = await _exporter
                .Append(dDImportantInfoExportDtos, "入库报表")
                .SeparateByRow()
                .Append(new List<SignDto>() { new SignDto()
                {
                    Qualitor="品质：",
                    Productor="生产：",
                    Stocker="库房：",
                } }, "填报信息")
                .ExportAppendDataAsByteArray();

            var memoryStream = new MemoryStream(byteArrary);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var exportStream = new MemoryStream();
            using (var p = new ExcelPackage(memoryStream))
            {
                var range = p.Workbook.Worksheets[0].Cells[2, 1, dDImportantInfoExportDtos.Count + 2, 5];
                range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                p.SaveAs(exportStream);
            }

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            exportStream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(exportStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = $"电堆入库报表_{DateTime.Now.ToString("yyyyMMddHHmm")}.xlsx",
            };
        }


        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_BatchNoByInStockInfo_Nameplate)]
        public async Task<JHTAjaxResponse<DDImportantInfoDto>> LoadDDDImportantInfo([FromBody] EntityDto<string> snInfo)
        {
            DDImportantInfoDto data = await _reportAppService.LoadDDImportantInfoAsync(snInfo);

            return new JHTAjaxResponse<DDImportantInfoDto>() { Data = data };
        }

        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_AuditeDDKeyInfoReport)]
        public async Task<JHTAjaxResponse> AuditeDDImportantInfosAsync([FromBody] DDImportantInfoDto dDImportantInfoDto)
        {
            await _reportAppService.AuidtDDImportantInfoAsync(dDImportantInfoDto);
            return new JHTAjaxResponse()
            {
                Msg = "审核成功",
                Code = 200
            };
        }

        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_DDKeyInfoReport)]
        public async Task<FileStreamResult> ExportDDWordInfos([FromBody] EntityDto<long> entity)
        {
            DDImportantInfoWordExportDto data = await _reportAppService.LoadExportDDImportantInfosAsync(entity.Id);
            var ddtestConfig = this.SettingManager.GetSettingValue(AppSettingNames.DDTestMachineConfig).FromJsonString<List<DDTestMachineConfig>>();
            data.TestMachineNumber = ddtestConfig.FirstOrDefault(p => p.ProductLineId == data.BelongProductLineId).TestMachineNumber;
            var reportTemplateInfo = _configurationAppService.GetDDTestReportConfig(data.MaterialNumber);
            MemoryStream memoryStream = new MemoryStream();

            // 2024-09-05  2024-09-05  客制化名称显示
            // data.MatreialName = data.MatreialName.Replace("35KW", "43.5KW");

            CreateExporetDocInfo(reportTemplateInfo.TemplateDocSavePath, data, memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            return new FileStreamResult(memoryStream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                FileDownloadName = $"{data.BelongMaterialBatchNumber}电堆测试报告.docx",
            };
        }

        private void CreateExporetDocInfo(string templatePath, DDImportantInfoWordExportDto data, MemoryStream memoryStream)
        {
            string imagePath = string.Empty;
            using (var document = DocX.Load(templatePath))
            {
                var propertyInfo = typeof(DDImportantInfoWordExportDto).GetProperties();
                foreach (var item in propertyInfo)
                {
                    if (item.Name == "UploadUrls" && document.Bookmarks.Any(p => p.Name == "VImagePath"))
                    {
                        foreach (var imgItem in data.UploadUrls)
                        {
                            imagePath = imgItem.Url.Replace(_fileSaveOptions.CurrentValue.DeafaultSaveDomain, _fileSaveOptions.CurrentValue.DeafaultSavePath);
                            if (System.IO.File.Exists(imagePath))
                            {
                                var image = document.AddImage(imagePath);
                                var picture = image.CreatePicture(200f, 400f);
                                document.Bookmarks["VImagePath"].Paragraph.InsertParagraphAfterSelf(string.Empty).AppendPicture(picture).Alignment = Alignment.center;
                                picture.InsertCaptionAfterSelf(imgItem.Name).FontSize(14).Alignment = Alignment.center;
                            }
                        }
                    }

                    var searchValue = $"${item.Name}$";
                    StringReplaceTextOptions options = new StringReplaceTextOptions()
                    {
                        SearchValue = searchValue,
                    };

                    var dataValue = item.GetValue(data);
                    if (dataValue != null)
                    {
                        if (item.PropertyType == typeof(string) || item.PropertyType == typeof(int) || item.PropertyType == typeof(long))
                        {
                            options.NewValue = dataValue.ToString();
                            document.ReplaceText(options);
                        }
                        else if (item.PropertyType == typeof(decimal) || item.PropertyType == typeof(float) || item.PropertyType == typeof(double))
                        {

                            options.NewValue = Convert.ToDecimal(dataValue).ToString("#0.000");
                            document.ReplaceText(options);
                        }
                        else if (item.PropertyType == typeof(DateTime))
                        {
                            options.NewValue = Convert.ToDateTime(dataValue).ToString("yyyy年MM月dd日");
                            document.ReplaceText(options);
                        }
                    }
                }

                var matreialTable = document.Tables.FirstOrDefault(p => p.TableCaption == "MaterialInfos");
                if (matreialTable != null)
                {
                    if (matreialTable.RowCount > 1)
                    {
                        var rowPattern = matreialTable.Rows[1];
                        for (int i = 0; i < data.MaterialRecordSimplyInfos.Count; i++)
                        {
                            AddItemToTable(matreialTable, rowPattern, data.MaterialRecordSimplyInfos[i], i + 1);
                        }

                        rowPattern.Remove();
                    }
                }

                document.SaveAs(memoryStream);
            }
        }

        private void AddItemToTable(Table table, Row rowPattern, MaterialRecordSimplyInfoDto item, int index)
        {
            // Insert a copy of the rowPattern at the last index in the table.
            var newItem = table.InsertRow(rowPattern, table.RowCount - 1);

            // Replace the default values of the newly inserted row.
            newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "$SortNumber$", NewValue = index.ToString() });
            newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "$InputMatreialName$", NewValue = item.InputMatreialName });
            newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "$Supplier$", NewValue = string.IsNullOrEmpty(item.Supplier) ? "" : item.Supplier });
            newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "$WarehousingTime$", NewValue = item.WarehousingTime.ToString("yyyy-MM-dd") });
            newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "$BatchNo$", NewValue = string.IsNullOrEmpty(item.BatchNo) ? "" : item.BatchNo });
        }

        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_WorkProcessOnePassRate)]
        public async Task<JHTPageAjaxRespone<PageData<WorkProcessOnePassRateReportDto>>> LoadWorkProcessOnePassRateReportRecordsAsync([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            return new JHTPageAjaxRespone<PageData<WorkProcessOnePassRateReportDto>>()
            {
                Data = await _reportAppService.LoadWorkProcessOnePassRateReportRecordsAsync(where)
            };
        }


        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_PrepaireWorkPorcess)]
        public async Task<JHTPageAjaxRespone<PageData<PrepaireWorkProcessDayReportDto>>> LoadPrepaireWorkProcessDayReportAsync([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            return new JHTPageAjaxRespone<PageData<PrepaireWorkProcessDayReportDto>>()
            {
                Data = await _reportAppService.LoadPrepaireWorkProcessDayReportAsync(where)
            };
        }


        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_OrderMaterialProduceStatuse)]
        public async Task<JHTPageAjaxRespone<PageData<OrderMaterialProduceStatuDto>>> LoadOrderMaterialProduceStatuReportAsync([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            return new JHTPageAjaxRespone<PageData<OrderMaterialProduceStatuDto>>()
            {
                Data = await _reportAppService.LoadOrderMaterialProduceStatuReportAsync(where)
            };
        }


        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_OrderMaterialProduceStatuse)]
        public JHTAjaxResponse<SNInStockInfoDto> LoadERPInStockInfo([FromBody] EntityDto<string> snNumber)
        {
            return new JHTAjaxResponse<SNInStockInfoDto>()
            {
                Data = _reportAppService.LoadERPInStockInfo(snNumber.Id)
            };
        }

        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_OrderMaterialProduceStatuse)]
        public async Task<FileStreamResult> ExportProduceStatuReportAsync([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            List<OrderMaterialProduceStatuExportDto> dDImportantInfoExportDtos = await _reportAppService.LoadOrderMaterialProduceStatuExportReportAsync(where);
            var byteArrary = await _exporter.ExportAsByteArray(dDImportantInfoExportDtos);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            return new FileStreamResult(new MemoryStream(byteArrary), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = $"电堆生产情况报表{DateTime.Now.ToString("yyyyMMddHHmm")}.xlsx",
            };
        }

        [HttpPost]
        public async Task<JHTPageAjaxRespone<ProductSummaryDto>> LoadProductSummaryReportAsync([FromBody] ReportQueryConditonDto where)
        {
            return new JHTPageAjaxRespone<ProductSummaryDto>()
            {
                Data = await _reportAppService.LoadProductSummaryReportAsync(where)
            };
        }


        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_BatchMaterialUsedReport)]
        public async Task<JHTPageAjaxRespone<PageData<View_BatchMaterialUsedReportDto>>> LoadBatchMaterialUsedReportAsync([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            var data = await _reportAppService.LoadBatchMaterialUsedReportAsync(where);
            return new JHTPageAjaxRespone<PageData<View_BatchMaterialUsedReportDto>>()
            {
                Data = data
            };
        }

        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_WorkOrderMaterialUsedReport)]
        public async Task<JHTAjaxResponse<List<WorkOrderMaterilCostItem>>> LoadKeyMaterilCostByWorkOrderNumberAsync([FromBody] EntityDto<string> workOrderNumber)
        {
            var data = await _reportAppService.LoadKeyMaterilCostByWorkOrderNumberAsync(workOrderNumber.Id);
            return new JHTPageAjaxRespone<List<WorkOrderMaterilCostItem>>()
            {
                Data = data
            };
        }


        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_ProductConstructMaterialInfos)]
        public async Task<JHTAjaxResponse<PageData<ProductConstructMaterialInfoDto>>> LoadProductConstructMaterialInfosAsync([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            return new JHTAjaxResponse<PageData<ProductConstructMaterialInfoDto>>()
            {
                Data = await _reportAppService.LoadProductConstructMaterialInfos(where)
            };
        }

        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_ProductConstructMaterialInfos)]
        public async Task<FileStreamResult> ExportProductConstructMaterialInfosAsync([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            List<ProductConstructMaterialInfoExportDto> dDImportantInfoExportDtos = await _reportAppService.ExportProductConstructMaterialInfos(where);
            var byteArrary = await _exporter.ExportAsByteArray(dDImportantInfoExportDtos);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            return new FileStreamResult(new MemoryStream(byteArrary), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = $"电堆物料使用情况报表{DateTime.Now.ToString("yyyyMMddHHmm")}.xlsx",
            };
        }


        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_PrepareUserWorkStatic)]
        public async Task<JHTAjaxResponse<PageData<PrepareUserWorkStaticDto>>> LoadPrepareUserWorkStaticAsync([FromBody] JHTPageAjaxResquest<PrepareUserWorkStaticQueryCondtionDto> where)
        {
            return new JHTAjaxResponse<PageData<PrepareUserWorkStaticDto>>()
            {
                Data = await _reportAppService.LoadPrepareUserWorkStaticAsync(where)
            };
        }


        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_DDTestDayKPI)]
        public async Task<JHTAjaxResponse<PageData<DDTestDayKPIDto>>> LoadDDTestDayKPIAsync([FromBody] JHTPageAjaxResquest<PrepareUserWorkStaticQueryCondtionDto> where)
        {
            return new JHTAjaxResponse<PageData<DDTestDayKPIDto>>()
            {
                Data = await _reportAppService.LoadDDTestDayKPIAsync(where)
            };
        }


        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_DDWeekOnePassRate)]
        public JHTAjaxResponse<PageData<DDWeekOnePassRateReportDto>> LoadDDWeekOnePassRateReport([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            return new JHTAjaxResponse<PageData<DDWeekOnePassRateReportDto>>()
            {
                Data = _reportAppService.LoadDDWeekOnePassRateReport(where)
            };
        }


        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_OrgProductProcessWorkLoadReport)]
        public JHTAjaxResponse<PageData<OrgProductProcessWorkLoadReportDto>> LoadOrgProductProcessWorkLoadReport([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            return new JHTAjaxResponse<PageData<OrgProductProcessWorkLoadReportDto>>()
            {
                Data = _reportAppService.LoadOrgProductProcessWorkLoadReport(where)
            };
        }

        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_RepairedMatreialReport)]
        public async Task<JHTAjaxResponse<PageData<WorkProcessMaterialRecordDto>>> LoadRepaierdMaterialRecords([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            return new JHTAjaxResponse<PageData<WorkProcessMaterialRecordDto>>()
            {
                Data = await _reportAppService.LoadRepairedInputMaterial(where)
            };
        }


        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_MatreialDiscardReport)]
        public async Task<JHTAjaxResponse<PageData<MaterialDiscardRecordDTO>>> LoadMaterialDiscardRecordReportAsync([FromBody] JHTPageAjaxResquest<DiscardRecordReportCondtionDto> where)
        {
            return new JHTAjaxResponse<PageData<MaterialDiscardRecordDTO>>()
            {
                Data = await _reportAppService.LoadMaterialDiscardRecordReportAsync(where)
            };
        }



        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Report_MatreialDiscardReport)]
        public async Task<FileStreamResult> ExportMaterialDiscardRecordReportAsync([FromBody] JHTPageAjaxResquest<DiscardRecordReportCondtionDto> where)
        {
            List<MaterialDiscardRecordExportDTO> dDImportantInfoExportDtos = await _reportAppService.ExportMaterialDiscardRecordReportAsync(where);
            var byteArrary = await _exporter.ExportAsByteArray(dDImportantInfoExportDtos);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            return new FileStreamResult(new MemoryStream(byteArrary), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = $"物料报废记录表{DateTime.Now.ToString("yyyyMMddHHmm")}.xlsx",
            };
        }

        [HttpPost]
        public async Task<JHTAjaxResponse<PageData<ERPInStockInfoOperateRecordDTO>>> LoadBatchOperatorRecord([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            return new JHTAjaxResponse<PageData<ERPInStockInfoOperateRecordDTO>>()
            {
                Data = await _reportAppService.LoadBatchOperatorRecord(where)
            };
        }
    }
}
