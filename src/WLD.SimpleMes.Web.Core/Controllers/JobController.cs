using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.Configuration.Startup;
using Abp.Dependency;
using WLD.SimpleMes.Authorization;
using JHT.Abp.CommonModels;
using JHT.AspNetCore.Quartz;
using JHT.AspNetCore.Quartz.Jobs;
using JHT.AspNetCore.Quartz.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Controllers
{
    /// <summary>
    ///  任务管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [AbpMvcAuthorize(PermissionNames.Pages_Job)]
    public class JobController : SimpleMesControllerBase
    {

        private readonly IQuartzScheduleJobManager _scheduler;
        private readonly QuartzModlueConfig _quartzModlueConfig;
        private readonly IAbpStartupConfiguration _abpStartupConfiguration;
        public JobController(IQuartzScheduleJobManager scheduler, QuartzModlueConfig quartzModlueConfig, IAbpStartupConfiguration abpStartupConfiguration)
        {
            _scheduler = scheduler;
            _quartzModlueConfig = quartzModlueConfig;
            _abpStartupConfiguration = abpStartupConfiguration;
        }



        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<bool>> AddJob([FromBody] JobDto entity)
        {
            entity.TriggerInfo.Frequence = TimeSpan.FromMinutes(entity.TriggerInfo.Frequencemin);
            entity.JobType = _quartzModlueConfig.GetJobType(entity.JobTypeName);
            var e = JsonConvert.SerializeObject(entity);
            var result = false;

            if (entity.JobType != null)
            {
                switch (entity.JobTypeName)
                {
                    case "Http任务":
                        CreateJobDto<HttpJob> Hjob = JsonConvert.DeserializeObject<CreateJobDto<HttpJob>>(e);
                        result = await _scheduler.AddJobAsync(Hjob);
                        break;
                    default:
                        result = await _scheduler.AddJobAsync(entity);
                        break;
                }
            }
            else
            {
                result = await _scheduler.AddJobAsync(entity);
            }

            return new JHTAjaxResponse<bool>()
            {
                Data = result
            };
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<bool>> ModifyJob([FromBody] JobDto entity)
        {
            await _scheduler.PauseJobAsync(new JobKey(entity.Name, entity.GroupName));
            await _scheduler.RemoveJobAsync(new JobKey(entity.Name, entity.GroupName));

            entity.TriggerInfo.Frequence = TimeSpan.FromMinutes(entity.TriggerInfo.Frequencemin);
            entity.JobType = _quartzModlueConfig.GetJobType(entity.JobTypeName);
            var e = JsonConvert.SerializeObject(entity);
            var result = false;
            if (entity.JobType != null)
            {
                switch (entity.JobTypeName)
                {
                    case "Http任务":
                        CreateJobDto<HttpJob> Hjob = JsonConvert.DeserializeObject<CreateJobDto<HttpJob>>(e);
                        result = await _scheduler.AddJobAsync(Hjob);
                        break;
                    default:
                        result = await _scheduler.AddJobAsync(entity);
                        break;
                }
            }
            else
            {
                result = await _scheduler.AddJobAsync(entity);
            }

            return new JHTAjaxResponse<bool>()
            {
                Data = result
            };

        }


        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<bool>> StopJob([FromBody] JobKey job)
        {
            await _scheduler.PauseJobAsync(job);
            return new JHTAjaxResponse<bool>()
            {
                Data = true
            };

        }

        /// <summary>
        /// 删除任务
        /// </summary> 
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<bool>> RemoveJob([FromBody] JobKey job)
        {

            return new JHTAjaxResponse<bool>()
            {
                Data = await _scheduler.RemoveJobAsync(job)
            };
        }

        /// <summary>
        /// 恢复运行暂停的任务
        /// </summary> 
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<bool>> ResumeJob([FromBody] JobKey job)
        {
            return new JHTAjaxResponse<bool>()
            {
                Data = await _scheduler.ResumeJobAsync(job)
            };

        }

        /// <summary>
        /// 查询任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTAjaxResponse<List<JobItemDto>>> QueryJob([FromBody] JobKey job)
        {

            return new JHTAjaxResponse<List<JobItemDto>>()
            {
                Data = await _scheduler.QueryAllJobsAsync(job)
            };
        }
        /// <summary>
        /// 查询详情任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTAjaxResponse<JobDto>> QueryDetailJob([FromBody] JobKey job)
        {
            var data = await _scheduler.QueryDetailJobAsync(job);
            data.JobTypeName = _quartzModlueConfig.JobTypeName(data.JobType);
            return new JHTAjaxResponse<JobDto>()
            {
                Data = data
            };
        }

        /// <summary>
        /// 立即执行
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<bool>> TriggerJob([FromBody] JobKey job)
        {
            await _scheduler.FireJobAsync(job);
            return new JHTAjaxResponse<bool>()
            {
                Data = true
            };
        }


        /// <summary>
        /// 获取所有任务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<JHTAjaxResponse<List<JobItemDto>>> GetAllJob()
        {
            return new JHTAjaxResponse<List<JobItemDto>>()
            {
                Data = await _scheduler.QueryAllJobsAsync(new JobKey(null, null))
            };
        }
        /// <summary>
        /// 获取任务参数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Dictionary<string, List<JobDataParmerDescrption>> GetParmer()
        {
            var querayerManger = IocManager.Instance.Resolve<IQuartzScheduleJobManager>();
            if (querayerManger == null)
            {

                return null;
            }

            Dictionary<string, List<JobDataParmerDescrption>> rslt = new Dictionary<string, List<JobDataParmerDescrption>>();
            foreach (var item in _abpStartupConfiguration.Modules.QuartzModlue().ParmerInfos)
            {
                rslt.Add(item.Key.JobDescreption, item.Value);
            }
            return rslt;
        }
    }
}

