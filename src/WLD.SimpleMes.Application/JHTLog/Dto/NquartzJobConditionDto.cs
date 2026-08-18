using WLD.SimpleMes.JHTLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Log.Dto
{
    public class NquartzJobConditionDto
    {
        public string KeyWord { get; set; }
        /// <summary>
        /// 执行事件
        /// </summary>
        public DateTime ExcuteDate { get; set; } = DateTime.Now.Date;

        /// <summary>
        /// Job的执行结果
        /// </summary>
        public JobResultEnum? JobResult { get; set; }
    }
}

