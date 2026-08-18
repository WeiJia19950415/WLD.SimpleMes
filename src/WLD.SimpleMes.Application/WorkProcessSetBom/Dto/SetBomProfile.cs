using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.BOM;
using WLD.SimpleMes.WorkProcess;

namespace WLD.SimpleMes.WorkProcessSetBom.Dto
{
    public class SetBomProfile : Profile
    {
        public SetBomProfile()
        {
            this.CreateMap<WorkProcessSetBomDto, BOM.WorkProcessSetBom>();

            this.CreateMap<BOM.WorkProcessSetBom, WorkProcessSetBomDto>()
                .ForMember(p => p.BelongWorkProcessSetName, opt => opt.MapFrom(d => d.BelongWorkProcessSet.SetName))
                .ForMember(p => p.MaterialName, opt => opt.MapFrom(d => d.ReferenceBom.MaterialName))
                .ForMember(p => p.ReferenceBomVersion, opt => opt.MapFrom(d => d.ReferenceBom.Version))
                .ForMember(p => p.BelongWorkProcessVersion, opt => opt.MapFrom(d => d.BelongWorkProcessSet.SetVersion))
                .ForMember(p => p.MaterialNumber, opt => opt.MapFrom(d => d.ReferenceBom.MaterialNumber));

            this.CreateMap<BOM.WorkProcessSetBom, WorkProcessSetBomCacheDto>()
                .ForMember(p => p.BelongWorkProcessSetName, opt => opt.MapFrom(d => d.BelongWorkProcessSet.SetName))
                .ForMember(p => p.MaterialNumber, opt => opt.MapFrom(d => d.ReferenceBom.MaterialNumber));

            this.CreateMap<WorkProcessInfo, WorkProcessSetBomItemByShowDto>()
                .ForMember(p => p.BomItem, opt => opt.Ignore());

            this.CreateMap<WorkProcessSetBomItem, ProcessBomItem>()
                .ForMember(p => p.WorkProcessId, opt => opt.MapFrom(d => d.BelongWorkProcessId))
                .ForMember(p => p.FormMaterialId, opt => opt.MapFrom(d => d.InputMaterialId))
                .ForMember(p => p.FormMaterialNumber, opt => opt.MapFrom(d => d.InputMaterial.MaterialNumber))
                .ForMember(p => p.FormMaterialName, opt => opt.MapFrom(d => d.InputMaterial.MaterialName))
                .ForMember(p => p.FormCount, opt => opt.MapFrom(d => d.InputMaterialCount))
                .ForMember(p => p.Specification, opt => opt.MapFrom(d => d.InputMaterial.Specification))
                .ForMember(p => p.FormUnitName, opt => opt.MapFrom(d => d.InputMaterial.UnitName));
        }
    }
}

