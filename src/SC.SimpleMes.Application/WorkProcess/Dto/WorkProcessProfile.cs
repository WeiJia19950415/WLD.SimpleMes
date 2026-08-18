using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Material.Dto;

namespace SC.SimpleMes.WorkProcess.Dto
{
    public class WorkProcessProfile : Profile
    {
        public WorkProcessProfile()
        {
            this.CreateMap<WorkProcessInfo, WorkProcessInfoDto>();
            this.CreateMap<WorkProcessInfoDto, WorkProcessInfo>();
            this.CreateMap<WorkProcessOperatorRecord, WorkProcessOperatorRecordDto>();

            this.CreateMap<WorkProcessFormInfoRelation, WorkProcessFormInfoRelationDto>()
                .ForMember(p => p.FormsName, opt => opt.MapFrom(d => d.BelongFormInfo.FormsName));

            this.CreateMap<WorkProcessMaterialRecord, MaterialBatchNumberDto>()
                .ForMember(p => p.MaterialNumber, opt => opt.MapFrom(d => d.InputMaterialNumber))
                .ForMember(p => p.BatchNumber, opt => opt.MapFrom(d => d.InputMaterialBatchNumber))
                .ForMember(p=>p.FromErpBatchNumber,opt=>opt.MapFrom(d=>d.BatchNo))
                .ForMember(p=>p.MaterialName,opt=>opt.MapFrom(d=>d.InputMaterialName))
                .ForMember(p => p.MaterialId, opt => opt.MapFrom(d => d.InputMaterilId))
                .ForMember(p => p.WrapUniteName, opt => opt.MapFrom(d => d.InputUnitName))
                .ForMember(p => p.MatrialCount, opt => opt.MapFrom(d => d.InputMaterialCount));

            this.CreateMap<WorkProcessMaterialRecord, WorkProcessMaterialRecordDto>();
            this.CreateMap<WorkProcessMaterialRecordHistory, WorkProcessMaterialRecordDto>();
        }
    }
}
