using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.WorkStation.Dto
{
    public class ProductLineConditionDto
    {
        public string KeyWord { get; set; }
        public long? BelongWorkShopId { get; set; }

        public ProductLineStateEnum? ProductLineState { get; set; }
    }
}
