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
    [DisallowConcurrentExecution]
    [PersistJobDataAfterExecution]
    public class K3ERPBasicInfoSyncJob : IJob, IParmerinfoProvider
    {
        public string JobUniqName => "K3基础信息同步";

        public Task Execute(IJobExecutionContext context)
        {

            throw new NotImplementedException();
        }

        public List<JobDataParmerDescrption> GetJobDataParmerDescrptions()
        {
            return new List<JobDataParmerDescrption>()
            {
                new JobDataParmerDescrption(){Descreption="统计租户",ParamerName="teantId",ParamterControls="input",ParmaerType="number"}
            };
        }
    }
}
