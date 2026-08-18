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
    public class WorkProcessOnePassRateDailyJob : IJob, IParmerinfoProvider
    {
        private readonly IReportAppService _reportAppService;
        public string JobUniqName => "工序一次性通过率日统计任务—允许重跑";

        public WorkProcessOnePassRateDailyJob()
        {

        }

        public WorkProcessOnePassRateDailyJob(IReportAppService reportAppService)
        {
            _reportAppService = reportAppService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var staticTime = DateTime.Parse(context.MergedJobDataMap.Get("staticTime").ToString());
            var materialNumber = context.MergedJobDataMap.Get("materialNumber").ToString();
            while (staticTime.Date.AddDays(1).Date < DateTime.Now.Date)
            {
                staticTime = staticTime.AddDays(1).Date;
               await _reportAppService.BuildWorkProcessOnePassRateReportAsync(staticTime, materialNumber);
            }

            context.JobDetail.JobDataMap["staticTime"] = staticTime.ToString("yyyy-MM-dd");
        }

        public List<JobDataParmerDescrption> GetJobDataParmerDescrptions()
        {
            return new List<JobDataParmerDescrption>()
            {
                new JobDataParmerDescrption(){Descreption="统计日期",ParamerName="staticTime",ParamterControls="input",ParmaerType="string"},
                new JobDataParmerDescrption(){Descreption="统计产品分类",ParamerName="materialNumber",ParamterControls="input",ParmaerType="string"},
            };
        }
    }
}
