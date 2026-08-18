using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.BOM.Dto
{
    public class BomQueryDto
    {
        /// <summary>
        /// 归属的物料名称/物料编码
        /// 包含的物料名称/物料编码
        /// </summary>
        public string KeyWord { get; set; }
    }
}
