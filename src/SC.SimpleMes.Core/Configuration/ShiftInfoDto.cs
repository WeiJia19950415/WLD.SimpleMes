using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Material;

namespace SC.SimpleMes.Configuration
{
    public class ShiftInfoDto
    {
        public string ShiftName { get; set; }

        public string ShiftCode { get; set; }

        /// <summary>
        /// 是否跨天
        /// </summary>
        public bool IsAcrrossDay { get; set; }

        public TimeSpan StartWorkTime { get; set; }

        public TimeSpan OffWorkTime { get; set; }

        public ShitCodeEnum ShitCodeType
        {
            get
            {
                var shiftCodeType = ShitCodeEnum.D;
                if( Enum.TryParse<ShitCodeEnum>(ShiftCode,out shiftCodeType))
                {
                    return shiftCodeType;
                }

                return ShitCodeEnum.D;

            }
        }
    }
}
