using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkProcess.Dto
{
    public class WorkProcessConditionDto
    {
        public string KeyWord { get; set; }
        public WorkProcessPowerTypeEnum? WorkProcessPowerType { get; set; }

        public WorkProcessTypeEnum? WorkProcessType { get; set; }

        /// <summary>
        /// 后期考虑
        /// </summary>
        public long? BelongWorkStaionId { get; set; }
    }
}
