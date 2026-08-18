using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.WorkStation.Dto
{
    /// <summary>
    /// Dto对象匹配Profile文件
    /// </summary>
    public class WorkStationProfile : Profile
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkStationProfile()
        {
            this.CreateMap<WorkShopInfo, WorkShopInfoDto>();
            this.CreateMap<WorkShopInfoDto, WorkShopInfo>();

            this.CreateMap<ProductLine, ProductLineDto>()
                .ForMember(p => p.WorkShopNumber, d => d.MapFrom(s => s.BelongWorkShop.WorkShopNumber))
                .ForMember(p => p.WorkShopName, d => d.MapFrom(s => s.BelongWorkShop.WorkShopName));
            this.CreateMap<ProductLineDto, ProductLine>();

            this.CreateMap<WorkStationInfo, WorkStationInfoDto>()
                .ForMember(p => p.BelongWorkShopName, d => d.MapFrom(s => s.BelongWorkShop.WorkShopName))
                .ForMember(p => p.BelongWorkShopNumber, d => d.MapFrom(s => s.BelongWorkShop.WorkShopNumber))
                .ForMember(p => p.ProductLineName, d => d.MapFrom(s => s.BelongProductLine.ProductLineName))
                .ForMember(p => p.ProductLineNumber, d => d.MapFrom(s => s.BelongProductLine.ProductLineNumber));
            this.CreateMap<WorkStationInfoDto, WorkStationInfo>();
            this.CreateMap<CreateUpdateWorkStationInfoDto,WorkStationInfo>();

        }
    }
}
