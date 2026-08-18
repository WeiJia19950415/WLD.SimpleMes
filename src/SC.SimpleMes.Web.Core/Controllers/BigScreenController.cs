using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.Authorization;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Authorization;
using SC.SimpleMes.Models.BigScreenStaticModel;
using SC.SimpleMes.MultiTenancy;
using SC.SimpleMes.Report;
using SC.SimpleMes.Report.Dto;
using SC.SimpleMes.WorkOrder.DTO;
using static SC.SimpleMes.Report.ReportAppService;

namespace SC.SimpleMes.Controllers
{
    [Route("api/[controller]/[action]")]
    [AbpAllowAnonymous]
    public class BigScreenController : SimpleMesControllerBase
    {
        private readonly IReportAppService _reportAppService;

        private readonly TenantManager _tenantManager;
        public BigScreenController(IReportAppService reportAppService, TenantManager tenantManager)
        {
            _reportAppService = reportAppService;
            _tenantManager = tenantManager;
        }

        /// <summary>
        /// 工序投入产出数量统计
        /// </summary>
        /// <param name="reportQueryConditonDto"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTAjaxResponse<WorkProcessInputOutPutData> LoadWorkProcessDayInputOutPutData([FromBody] ReportQueryConditonDto reportQueryConditonDto)
        {
            WorkProcessInputOutPutData data = new WorkProcessInputOutPutData();
            reportQueryConditonDto.ParseTime();
            var result = _reportAppService.LoadDayWorkProcessReport(reportQueryConditonDto);
            foreach (var item in result)
            {
                data.WorkProcess.Add(item.WorkProcessName);
                data.InputData.Add(item.InputCount);
                data.OutputData.Add(item.FinishedCount);
            }

            return new JHTAjaxResponse<WorkProcessInputOutPutData>(data);
        }

        /// <summary>
        /// 当日工序异常数据统计数量
        /// </summary>
        /// <param name="staticDate"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTAjaxResponse<List<PieStaticsData>> LoadDayWorkProcessProblemStatics([FromBody] ReportQueryConditonDto reportQueryConditon)
        {
            List<PieStaticsData> dayWorkProcessProblemStatics = new List<PieStaticsData>();
            reportQueryConditon.ParseTime();
            List<DayWorkProcessProblemStaticsDto> data = _reportAppService.LoadDayWorkProcessProblemStatics(reportQueryConditon);
            foreach (var item in data)
            {
                dayWorkProcessProblemStatics.Add(new PieStaticsData() { Name = item.WorkProcess, Value = item.ProblemCount });

            }

            return new JHTAjaxResponse<List<PieStaticsData>>(dayWorkProcessProblemStatics);
        }

        /// <summary>
        /// 加载问题分类统计
        /// </summary>
        /// <param name="reportQueryConditon"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTAjaxResponse<List<PieStaticsData>> LoadDayProblemStatics([FromBody] ReportQueryConditonDto reportQueryConditon)
        {
            List<PieStaticsData> dayProblemStatics = new List<PieStaticsData>();
            reportQueryConditon.ParseTime();

            List<DayProblemStaticsDto> data = _reportAppService.LoadDayProblemStatics(reportQueryConditon);
#if DEBUG
            //if (data.Count == 0)
            //{
            //    for (int i = 1; i < 7; i++)
            //    {
            //        dayProblemStatics.Add(new PieStaticsData { Name = "问题名称1" + i, Value = i });
            //    }
            //}
#endif


            foreach (var item in data)
            {
                dayProblemStatics.Add(new PieStaticsData { Name = item.ProblemName, Value = item.ProblemCount });
            }

            return new JHTAjaxResponse<List<PieStaticsData>>(dayProblemStatics);
        }

        /// <summary>
        /// 工序平均加工时长
        /// </summary>
        /// <param name="reportQueryConditon"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTAjaxResponse<List<WorkProcessAvgTimeStaticsDto>> LoadDayWorkProcessAvgTimeStatics([FromBody] ReportQueryConditonDto reportQueryConditon)
        {
            var result = _reportAppService.LoadDayWorkProcessAvgTimeStatics(reportQueryConditon).OrderByDescending(p => p.CostMinutes).ToList();
            return new JHTAjaxResponse<List<WorkProcessAvgTimeStaticsDto>>()
            {
                Data = result.Where(p => p.CostMinutes > 5 && p.CostMinutes < 1440).ToList(), // 屏蔽1异常工序
            };
        }

        /// <summary>
        /// 产线日产能分布图
        /// </summary>
        /// <param name="reportQueryConditon"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTAjaxResponse<TrendStaticModel<decimal>> LoadProductLineTrendData([FromBody] ReportQueryConditonDto reportQueryConditon)
        {
            JHTAjaxResponse<TrendStaticModel<decimal>> ajaxResponse = new JHTAjaxResponse<TrendStaticModel<decimal>>();
            ajaxResponse.Data = new TrendStaticModel<decimal>();
            reportQueryConditon.ParseTime();
            var result = _reportAppService.LoadProductLineCacityStaticData(reportQueryConditon);
            var inputCount = new List<decimal>();
            var outPutCount = new List<decimal>();
            var startDate = reportQueryConditon.StartDate.GetValueOrDefault();
            do
            {
                ajaxResponse.Data.XDataInfo.Add(startDate.ToString("dd"));

                var itemInfo = result.FirstOrDefault(p => p.StaticDate == startDate);
                if (itemInfo != null)
                {
                    inputCount.Add(itemInfo.InputCount);
                    outPutCount.Add(itemInfo.FinishedCount);
                }
                else
                {
                    inputCount.Add(0);
                    outPutCount.Add(0);
                }

                startDate = startDate.AddDays(1);

            } while (reportQueryConditon.EndDate > startDate);

            ajaxResponse.Data.YDataInfo.Add(inputCount);
            ajaxResponse.Data.YDataInfo.Add(outPutCount);

            return ajaxResponse;
        }


        /// <summary>
        /// 产品数量分类产出统计
        /// </summary>
        /// <param name="reportQueryConditon"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTAjaxResponse<TrendStaticModel<int>> LoadPoductCategoryOutputStatics([FromBody] ReportQueryConditonDto reportQueryConditon)
        {
            JHTAjaxResponse<TrendStaticModel<int>> ajaxResponse = new JHTAjaxResponse<TrendStaticModel<int>>();
            ajaxResponse.Data = new TrendStaticModel<int>();
            reportQueryConditon.ParseTime();
            var reult = _reportAppService.LoadProductCategoryOutputConfig(reportQueryConditon);
            var inputCount = new List<int>();
            foreach (var item in reult)
            {
                if (item.FinishedCount > 0)
                {
                    ajaxResponse.Data.XDataInfo.Add(item.MaterialName);
                    inputCount.Add((int)item.FinishedCount);
                }
            }

            ajaxResponse.Data.YDataInfo.Add(inputCount);
            return ajaxResponse;
        }

        /// <summary>
        /// 前置物料数量统计
        /// </summary>
        /// <param name="reportQueryConditon"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTAjaxResponse<List<PieStaticsData>> LoadPadPrepaireWorkProcessReport([FromBody] ReportQueryConditonDto reportQueryConditon)
        {
            reportQueryConditon.ParseTime();
            var result = _reportAppService.LoadPadPrepaireWorkProcessReport(reportQueryConditon.StartDate.GetValueOrDefault());
            List<PieStaticsData> returnValue = new List<PieStaticsData>();
            foreach (var item in result)
            {
                returnValue.Add(new PieStaticsData()
                {
                    Name = item.MaterialNumber,
                    Value = item.FinishedCount
                });
            }
            return new JHTAjaxResponse<List<PieStaticsData>>()
            {
                Data = returnValue
            };
        }

        /// <summary>
        /// 工单完成情况统计
        /// </summary>
        /// <param name="reportQueryConditon"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTAjaxResponse<ProductQuantityDto> LoadWorkOrderCompletionStatus([FromBody] ReportQueryConditonDto reportQueryConditon)
        {
            reportQueryConditon.ParseTime();
            var result = _reportAppService.LoadWorkOrderCompletionStatus(reportQueryConditon);
            return new JHTAjaxResponse<ProductQuantityDto>()
            {
                Data = result
            };
        }

        //缺陷处理进度
        [HttpPost]
        [DisableAuditing]
        public JHTAjaxResponse<List<PieStaticsData>> DefectHandlingProgressStatus([FromBody] ReportQueryConditonDto reportQueryConditon)
        {
            reportQueryConditon.ParseTime();
            var result = _reportAppService.DefectHandlingProgressStatus(reportQueryConditon);
            List<PieStaticsData> returnValue = new List<PieStaticsData>();

            returnValue.Add(new PieStaticsData()
            {
                Name = "未处理",
                Value = result.UnFinishedCount
            });

            returnValue.Add(new PieStaticsData()
            {
                Name = "已处理",
                Value = result.FinishedCount
            });

            return new JHTAjaxResponse<List<PieStaticsData>>()
            {
                Data = returnValue
            };
        }


        /// <summary>
        /// 产线投入产出累计值
        /// </summary>
        /// <param name="reportQueryConditon"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTAjaxResponse<TrendStaticModel<decimal>> LoadInputOutPutAccumulateStatic([FromBody] ReportQueryConditonDto reportQueryConditon)
        {
            JHTAjaxResponse<TrendStaticModel<decimal>> ajaxResponse = new JHTAjaxResponse<TrendStaticModel<decimal>>();
            ajaxResponse.Data = new TrendStaticModel<decimal>();
            reportQueryConditon.ParseTime();
            var result = _reportAppService.LoadProductLineCacityStaticData(reportQueryConditon);
            var inputCount = new List<decimal>();
            var outPutCount = new List<decimal>();
            var startDate = reportQueryConditon.StartDate.GetValueOrDefault();
            do
            {
                ajaxResponse.Data.XDataInfo.Add(startDate.ToString("dd"));
                inputCount.Add(result.Where(p => p.StaticDate <= startDate).Sum(p => p.InputCount));
                outPutCount.Add(result.Where(p => p.StaticDate <= startDate).Sum(p => p.FinishedCount));
                startDate = startDate.AddDays(1);

            } while (reportQueryConditon.EndDate > startDate);

            ajaxResponse.Data.YDataInfo.Add(inputCount);
            ajaxResponse.Data.YDataInfo.Add(outPutCount);
            return ajaxResponse;
        }

        /// <summary>
        /// 工序滞留产品数量统计
        /// </summary>
        /// <param name="reportQueryConditon"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTAjaxResponse<List<PieStaticsData>> LoadWorkProcessStayProductStatic([FromBody] ReportQueryConditonDto reportQueryConditon)
        {
            reportQueryConditon.ParseTime();
            var defaultTeantId = AbpSession.TenantId.GetValueOrDefault();
            if (defaultTeantId == 0)
            {
                defaultTeantId = _tenantManager.FindByTenancyName(Tenant.DefaultTenantName).Id;
            }

            using (UnitOfWorkManager.Current.SetTenantId(defaultTeantId))
            {
                var data = _reportAppService.LoadWorkProcessStayProductStatic(reportQueryConditon);
                List<PieStaticsData> returnData = new List<PieStaticsData>();
                foreach (var item in data)
                {
                    returnData.Add(new PieStaticsData() { Name = item.WorkProcessName, Value = item.FinishedCount });
                }

                return new JHTAjaxResponse<List<PieStaticsData>>()
                {
                    Data = returnData
                };
            }
        }


        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<PageData<OrderMaterialProduceStatuDto>>> LoadOrderMaterialProduceStatuReportAsync([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            return new JHTPageAjaxRespone<PageData<OrderMaterialProduceStatuDto>>()
            {
                Data = await _reportAppService.LoadOrderMaterialProduceStatuReportAsync(where)
            };
        }


        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<ProductSummaryDto>> LoadProductSummaryReportAsync([FromBody] ReportQueryConditonDto where)
        {
            return new JHTPageAjaxRespone<ProductSummaryDto>()
            {
                Data = await _reportAppService.LoadProductSummaryReportAsync(where)
            };
        }

        /// <summary>
        /// 工单完成率统计报表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTAjaxResponse<List<WorkOrderFinishedInfoDto>>> LoadWorkOrderFinishedInfoAsync([FromBody] ReportQueryConditonDto where)
        {
            return new JHTPageAjaxRespone<List<WorkOrderFinishedInfoDto>>()
            {
                Data = await _reportAppService.LoadWorkOrderFinishedInfoAsync(where)
            };
        }

        /// <summary>
        /// 工序产能统计报表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<PageData<WorkProcessCapacityStaticReportDto>>> LoadStationCacityStaticRecordsAsync([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            return new JHTPageAjaxRespone<PageData<WorkProcessCapacityStaticReportDto>>()
            {
                Data = await _reportAppService.LoadStationCacityStaticRecordsAsync(where)
            };
        }

        [HttpPost]
        [DisableAuditing]
        public async Task<JHTAjaxResponse<TrendStaticModel<decimal>>> QueryProductLineYearStaticReportAsync([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            where.Condition.ParseTime();
            if (where.Condition.StartDate == null)
            {
                var startDate = DateTime.Now.AddYears(-1);// 最近一年
                where.Condition.StartDate = new DateTime(startDate.Year, startDate.Month, 1);
            }

            if (where.Condition.EndDate == null)
            {
                var endDate = DateTime.Now.AddMonths(1);// 最近一年
                where.Condition.EndDate = new DateTime(endDate.Year, endDate.Month, 1);
            }

            List<ProductLineCapacityYearReportRecord> result = await _reportAppService.QueryProductLineYearStaticReportAsync(where);
            TrendStaticModel<decimal> trendStaticModel = new TrendStaticModel<decimal>();
            var itemValue = where.Condition.StartDate.GetValueOrDefault();
            List<decimal> finishedCount = new List<decimal>();
            while (itemValue < where.Condition.EndDate)
            {
                trendStaticModel.XDataInfo.Add(itemValue.ToString("yyyyMM"));
                var defatultResult = result.FirstOrDefault(p => p.StaticMonth == itemValue.Month && p.StaticYear == itemValue.Year);
                finishedCount.Add(defatultResult == null ? 0 : defatultResult.FinishedCount);
                itemValue = itemValue.AddMonths(1);
            }

            trendStaticModel.YDataInfo.Add(finishedCount);
            return new JHTAjaxResponse<TrendStaticModel<decimal>>() { Data = trendStaticModel };
        }
    }

    public class DayWorkProcessProblemStatics
    {
        public DayWorkProcessProblemStatics()
        {
            WorkProcess = new List<string>();
            ProblemCount = new List<decimal>();
        }

        public List<string> WorkProcess { get; set; }

        public List<decimal> ProblemCount { get; set; }
    }

    public class WorkProcessInputOutPutData
    {
        public WorkProcessInputOutPutData()
        {
            WorkProcess = new List<string>();
            InputData = new List<decimal>();
            OutputData = new List<decimal>();
        }
        public List<string> WorkProcess { get; set; }
        public List<decimal> InputData { get; set; }

        public List<decimal> OutputData { get; set; }
    }

}
