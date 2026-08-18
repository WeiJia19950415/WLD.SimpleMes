using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.WorkStation.Dto;

namespace WLD.SimpleMes.Models
{
    public class ManagerWorkStationModel
    {
        public List<string>  ManageProductLineNames { get; set; }

        public List<WorkStationInfoDto> ManagedWorkStations { get; set; }
    }
}
