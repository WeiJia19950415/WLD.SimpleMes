using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WLD.SimpleMes.JHTLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Log.Dto
{
    /// <summary>
    /// 定时任务
    /// </summary>
    [AutoMap(typeof(NquartzJobLog))]
    public class NquartzJobLogDto : EntityDto<long>
    {
        /// <summary>
        /// job的分组
        /// </summary>
        public string JobGroup { get; set; }
        /// <summary>
        /// job的名称
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 开始执行的时间
        /// </summary>
        public DateTime BeginExcuteTime { get; set; }

        /// <summary>
        /// 执行时长
        /// </summary>
        public long Duration { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public string JobTypeName { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string ExcpetionMessage { get; set; }

        /// <summary>
        /// Job的执行结果
        /// </summary>
        public JobResultEnum JobResult { get; set; }
    }
}

