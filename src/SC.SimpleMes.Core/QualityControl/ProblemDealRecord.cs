using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.QualityControl
{
    /// <summary>
    /// 问题处理记录
    /// </summary>
    public class ProblemDealRecord : Entity<long>
    {
        public long ProblemRecordId { get; set; }
        public long OperatorId { get; set; }
        public string OperatorName { get; set; }

        public DateTime DealTime { get; set; }

        public ProblemDealTypeEnum ProblemDealType { get; set; }

        /// <summary>
        /// 处理意见 操作描述
        /// </summary>
        public string OperatorDescreption { get; set; }
    }


    public enum ProblemDealTypeEnum
    {
        // 针对产品
        正常接收 = 0,
        让步接收=1,
        返修=2,
        部分报废=3,
        复测=4,
        全部报废=5,
        封存=6,
    }
}
