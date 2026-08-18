using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Report.Dto;

namespace SC.SimpleMes.QualityControl.Dto
{
    public class QutlityProfiler : Profile
    {
        public QutlityProfiler()
        {
            this.CreateMap<ProblemCategory, ProblemCategoryDto>()
                .ForMember(p => p.ParentCategoryCode, opt => opt.MapFrom(d => ProblemCategory.GetParentCode(d.CategoryCode)));

            this.CreateMap<ProblemCategoryDto, ProblemCategory>();

            this.CreateMap<ProblemRecord, ProblemRecordDto>()
                .ForMember(p => p.RelationImgs, opt => opt.MapFrom(d => d.GetImgs()))
                .ForMember(p => p.BelongProblemCategoryCode, opt => opt.MapFrom(d => d.GetBelongCategorCode()));

            this.CreateMap<ProblemRecordDto, ProblemRecord>();

            this.CreateMap<ProblemDealRecord, ProblemDealRecordDto>();
            this.CreateMap<ProblemDealRecordDto, ProblemDealRecord>();

            this.CreateMap<ProblemDefineDto, QualityProblemDefine>();
            this.CreateMap<QualityProblemDefine, ProblemDefineDto>()
                .ForMember(p => p.ShowCategoryCode, opt => opt.MapFrom(d => d.GetOwnCode()))
                .ForMember(p => p.FullCategoryName, opt => opt.MapFrom(d => d.ProblemCategory != null ? d.ProblemCategory.FullCategoryName : ""))
                .ForMember(p => p.CategoryCode, opt => opt.MapFrom(d => d.ProblemCategory != null ? d.ProblemCategory.CategoryCode : ""))
                .ForMember(p => p.QualityProblemNumber, opt => opt.MapFrom(d => d.QualityProblemNumber));

            this.CreateMap<View_ProblemRecord, View_ProblemRecordDto>()
                .ForMember(p => p.ProblemDealType, opt => opt.MapFrom(d => d.ProblemDealType == null ? "" : d.ProblemDealType.ToString()))
                .ForMember(p => p.Id, opt => opt.MapFrom(d => $"{d.Id}-{d.RecordId}"))
                ;
            this.CreateMap<View_ProblemRecordDto, ProblemRecordDto>()
                .ForMember(p=>p.BelongProblemDefineName,opt=>opt.MapFrom(d=>d.ProbleName))
                .ForMember(p=>p.BelongProblemCategoryFullName,opt=>opt.MapFrom(d=>d.ProbleCategoryFullName))
                .ForMember(p=>p.BelongProblemCategoryCode,opt=>opt.MapFrom(d=>d.CategoryCode))
                .ForMember(p=>p.Id,opt=>opt.Ignore())
                ;
            this.CreateMap<MaterialDiscardRecord, MaterialDiscardRecordDTO>();
            this.CreateMap<View_MaterialDiscardRecord, MaterialDiscardRecordDTO>();
            this.CreateMap<View_MaterialDiscardRecord, MaterialDiscardRecordExportDTO>();
            this.CreateMap<MaterialDiscardRecordDTO, MaterialDiscardRecord>();

        }
    }
}

