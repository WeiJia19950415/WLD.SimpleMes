using JHT.AspNetCore.Quartz;
using JHT.AspNetCore.Quartz.Model;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Report;

namespace WLD.SimpleMes.QuaterJob
{
    [DisallowConcurrentExecution]
    [PersistJobDataAfterExecution]
    public class PrepaireWorkProcessDailyJob : IJob, IParmerinfoProvider
    {
        private readonly IReportAppService _reportAppService;
        public string JobUniqName => "前置工序物料加工统计报表—允许重跑";

        public PrepaireWorkProcessDailyJob()
        {

        }

        public PrepaireWorkProcessDailyJob(IReportAppService reportAppService)
        {
            _reportAppService = reportAppService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var staticTime = DateTime.Parse(context.MergedJobDataMap.Get("staticTime").ToString());
            while (staticTime.Date.AddDays(1).Date < DateTime.Now.Date)
            {
                staticTime = staticTime.AddDays(1).Date;
               await _reportAppService.BuildPrepaireWorkProcessDayReportsAsync(staticTime);
            }

            context.JobDetail.JobDataMap["staticTime"] = staticTime.ToString("yyyy-MM-dd");
        }

        public List<JobDataParmerDescrption> GetJobDataParmerDescrptions()
        {
            return new List<JobDataParmerDescrption>()
            {
                new JobDataParmerDescrption(){Descreption="统计日期",ParamerName="staticTime",ParamterControls="input",ParmaerType="string"},
            };
        }
    }
}
