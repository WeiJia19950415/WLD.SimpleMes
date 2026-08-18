using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.LineSideWarehouse;

namespace WLD.SimpleMes.OnLineStock.Dto
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
