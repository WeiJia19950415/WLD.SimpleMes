using Abp.Application.Services.Dto;
using Abp.Authorization;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Report;
using WLD.SimpleMes.Report.Dto;
using WLD.SimpleMes.WorkOrder.DTO;

namespace WLD.SimpleMes.Controllers.PadManage
{
    [Route("api/[controller]/[action]")]
    [AbpAuthorize]
    [ApiController]
    public class PadManageReportController : SimpleMesControllerBase
    {
        private readonly IReportAppService _reportAppService;
        public PadManageReportController(IReportAppService reportAppService)
        {
            _reportAppService = reportAppService;
        }


        [HttpPost]
        public JHTAjaxResponse<List<PrepaireWorkProcessDayReportDto>> LoadPadPrepaireWorkProcessReport(EntityDto<string> staticDate)
        {
            var staticDateInfo = DateTime.Now.Date;

            if (!string.IsNullOrEmpty(staticDate.Id))
            {
                staticDateInfo = DateTime.Parse(staticDate.Id);
            }

            JHTAjaxResponse<List<PrepaireWorkProcessDayReportDto>> ajaxResponse = new JHTAjaxResponse<List<PrepaireWorkProcessDayReportDto>>();
            ajaxResponse.Data = _reportAppService.LoadPadPrepaireWorkProcessReport(staticDateInfo);
            return ajaxResponse;

        }


        [HttpPost]
        public JHTAjaxResponse<List<WorkProcessCapacityDailyReportRecordDto>> LoadDayWorkProcessReport(EntityDto<string> staticDate)
        {
            var staticDateInfo = DateTime.Now.Date;

            if (!string.IsNullOrEmpty(staticDate.Id))
            {
                staticDateInfo = DateTime.Parse(staticDate.Id);
            }

            JHTAjaxResponse<List<WorkProcessCapacityDailyReportRecordDto>> ajaxResponse = new JHTAjaxResponse<List<WorkProcessCapacityDailyReportRecordDto>>();
            ajaxResponse.Data = _reportAppService.LoadDayWorkProcessReport(new ReportQueryConditonDto() { StartDate = staticDateInfo });
            return ajaxResponse;
        }
    }
}
