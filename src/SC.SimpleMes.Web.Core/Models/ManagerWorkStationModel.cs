using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.WorkStation.Dto;

namespace SC.SimpleMes.Models
{
    public class ManagerWorkStationModel
    {
        public List<string>  ManageProductLineNames { get; set; }

        public List<WorkStationInfoDto> ManagedWorkStations { get; set; }
    }
}
