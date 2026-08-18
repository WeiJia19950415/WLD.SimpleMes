using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.JHTLog
{
    /// <summary>
    /// 重要模块操作日志
    /// </summary>
    public class KeyOperatorLog : Entity<long>
    {
        /// <summary>
        /// 操作模块
        /// </summary>
        public ModuleEnum OperatorModule { get; set; }

        /// <summary>
        /// 操作人员
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string OperatorDescrip { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperatorTime { get; set; }
    }

    public enum ModuleEnum
    {
        物料批次号模块,
        工单模块,
    }
}
