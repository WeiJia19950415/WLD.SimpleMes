using JHT.AspNetCore.Quartz;
using JHT.AspNetCore.Quartz.Model;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.QuaterJob
{
    public class SyncMaterialNameJob : IJob, IParmerinfoProvider
    {
        public string JobUniqName => "同步ERP物料名称";

        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }

        public List<JobDataParmerDescrption> GetJobDataParmerDescrptions()
        {
            return new List<JobDataParmerDescrption>()
            {
            };

        }
    }
}
