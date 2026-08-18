using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SC.SimpleMes.JHTLog;

namespace SC.SimpleMes.QuaterJob
{
    public class JobListener : IJobListener
    {
        private readonly NquartzJobLogStore _jobLogStore;
        private readonly ILogger _logger;

        public JobListener(NquartzJobLogStore nquartzJobLogStore, ILogger<JobListener> logger)
        {
            _jobLogStore = nquartzJobLogStore;
            _logger = logger;
        }

        public string Name => "数据库任务执行监视器";

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug($"Job {context.JobDetail.JobType.Name} executing...");
            return Task.FromResult(0);
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Job {context.JobDetail.JobType.Name} executing operation vetoed...");
            return Task.FromResult(0);
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            var log = new NquartzJobLog()
            {
                BeginExcuteTime = DateTime.Now,
                Duration = (int)context.JobRunTime.TotalMilliseconds,
                JobGroup = context.JobDetail.Key.Group,
                JobName = context.JobDetail.Key.Name
            };
            if (jobException == null)
            {
                log.JobResult = JobResultEnum.Sucess;
                _logger.LogDebug($"Job {context.JobDetail.JobType.Name} successfully executed.");
                await _jobLogStore.SaveAsync(log);
            }
            else
            {
                log.JobResult = JobResultEnum.Fail;
                log.ExcpetionMessage = string.Format("错误消息：{0},错误堆栈：{1}", jobException.Message, jobException.StackTrace);
                _logger.LogWarning($"Job {context.JobDetail.JobType.Name} failed with exception: {jobException}");
                await _jobLogStore.SaveAsync(log);
            }
        }
    }
}
