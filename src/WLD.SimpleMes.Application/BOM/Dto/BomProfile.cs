using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.WorkOrder;

namespace WLD.SimpleMes.BOM.Dto
{
    public class BomProfile : Profile
    {
        public BomProfile()
        {
            this.CreateMap<BomAddDto, BomInfo>();
            this.CreateMap<BomItemDto, BomItemInfo>();

            this.CreateMap<BomItemInfo, BomItemDto>();
            this.CreateMap<BomDto, BomInfo>();
            this.CreateMap<BomInfo, BomDto>()
            .ForMember(p => p.BomItemDtos, opt => opt.MapFrom(d => d.BomItems));
            this.CreateMap<BomUpdateDto, BomInfo>();

            this.CreateMap<WorkOrderBomItem, BomItemDto>()
                .ForMember(p=>p.FormMaterialId,opt=>opt.MapFrom(d=>d.InputMaterialId))
                .ForMember(p => p.FormMaterialName, opt => opt.MapFrom(d => d.InputMaterial.MaterialName))
                .ForMember(p => p.FormMaterialNumber, opt => opt.MapFrom(d => d.InputMaterial.MaterialNumber))
                .ForMember(p=>p.FormCount,opt=> opt.MapFrom(d => d.InputMaterialCount))
                .ForMember(p => p.UnitName, opt => opt.MapFrom(d => d.InputMaterial.UnitName))
                .ForMember(p => p.Specification, opt => opt.MapFrom(d => d.InputMaterial.Specification))
                ;
        }
    }
}
