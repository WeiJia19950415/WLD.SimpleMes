using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.WorkProcess.Dto;

namespace SC.SimpleMes.Models
{
    public class WorkProcessMaterialHistoryGroupModel
    {
        public string Key { get; set; }
        public string WorkStationName { get; set; }
        public List<WorkProcessMaterialRecordDto> Value { get; set; }
    }
}
