using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Users.Dto
{
    public class UserImportResult
    {

        /// <summary>
        /// 导入成功数量
        /// </summary>
        public int SuccessNumber { get; set; }
        /// <summary>
        /// 导入失败数量
        /// </summary>
        public int FailNumber { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Hint { get; set; }
    }
}

