using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Material.SerialNumberGenerator
{
    public class LineSideMaterialGenerator : CommonBatchNumberGenerator
    {
        public const string WIP = "WIP";
        public override string GenerateMaterialBatchNumber()
        {
            var dayInfo = GetEncryptDayInfo(this.ShiftInfo);
            return $"{BatchNumberPrefix}{WIP}{MaterialInfoId}{FactoryNumber}{ProductLineNumber}{this.ShiftInfo.ShiftCode}{dayInfo}{FlowNumber}";
        }
    }
}
