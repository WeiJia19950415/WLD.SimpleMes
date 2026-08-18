using JHT.AspNetCore.Quartz;
using JHT.AspNetCore.Quartz.Model;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Report;

namespace SC.SimpleMes.QuaterJob
{
    [DisallowConcurrentExecution]
    [PersistJobDataAfterExecution]
    public class WorkProcessProblemDailyJob : IJob, IParmerinfoProvider
    {
        private readonly IReportAppService _reportAppService;
        public string JobUniqName => "工序问题日统计—支持重跑";

        public WorkProcessProblemDailyJob()
        {

        }

        public WorkProcessProblemDailyJob(IReportAppService reportAppService)
        {
            _reportAppService = reportAppService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var staticTime = DateTime.Parse(context.MergedJobDataMap.Get("staticTime").ToString());
            while (staticTime.AddDays(1).Date < DateTime.Now.Date)
            {
                staticTime = staticTime.AddDays(1).Date;
               await _reportAppService.BuildWorkProcessProblemDailyReportAsync(staticTime);
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
