using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Material;

namespace WLD.SimpleMes.BatchNoByInStockInfo.Dto
{
    public class BatchNoByInStockInfoProfile : Profile
    {
        public BatchNoByInStockInfoProfile()
        {
            this.CreateMap<ERPInStockInfo, BatchNoByInStockInfoDto>()
                .ForMember(p => p.SourceType, opt => opt.MapFrom( x=> x.SourceType.ToString()));
        }
    }
}
