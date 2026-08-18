using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.LineSideWarehouse;

namespace SC.SimpleMes.OnLineStock.Dto
{
    public class OnlineStockProflie : Profile
    {
        public OnlineStockProflie()
        {
            this.CreateMap<LineSideMaterialInfoBomItemDto, LineSideMaterialInfoBomItem>();
            this.CreateMap< LineSideMaterialInfoBomItem, LineSideMaterialInfoBomItemDto>();
        }
    }
}
