using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.WorkProcess;
namespace WLD.SimpleMes.WorkProcessSet.Dto
{
    public class WorkProcessSetProfile : Profile
    {
        public WorkProcessSetProfile()
        {
            this.CreateMap<WorkProcess.WorkProcessSet, WorkProcessSetInfoDto>();
            this.CreateMap<WorkProcessSetInfoDto, WorkProcess.WorkProcessSet>();
            this.CreateMap<WorkProcess.WorkProcessSet, WorkProcessSetInfoCacheDto>()
                .ForMember(p => p.WorkProcessSetDetails, opt => opt.MapFrom(d => d.GetWorkProcessSetDetails()));

            this.CreateMap<WorkProcessSetProductRelation, ProductWorkProcessSetDto>()
                .ForMember(p => p.MaterialNumber, opt => opt.MapFrom(d => d.MaterialInfo.MaterialNumber))
                .ForMember(p => p.MaterialName, opt => opt.MapFrom(d => d.MaterialInfo.MaterialName))
                .ForMember(p => p.SetName, opt => opt.MapFrom(d => d.BelongWorkProcessSet.SetName))
                .ForMember(p => p.SetVersion, opt => opt.MapFrom(d => d.BelongWorkProcessSet.SetVersion))
                ;
        }
    }
}
