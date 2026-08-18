using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Configuration.Dto
{
    public class DDPrintFixedInfoDto
    {
        public string MaterialNumberCategory { get; set; }

        /// <summary>
        /// 重量 KG
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// 长宽高 mm
        /// </summary>
        public string SizeInfo { get; set; }
    }
}
