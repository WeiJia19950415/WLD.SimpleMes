using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SC.SimpleMes.DynamicForms.DDImportantInfos;

namespace SC.SimpleMes.DynamicForms.DTO
{
    public class FormTemplateInfoProfile : Profile
    {
        public FormTemplateInfoProfile()
        {
            this.CreateMap<FormTemplateInfo, FormTemplateInfoDto>();
            this.CreateMap<FormTemplateInfoDto, FormTemplateInfo>();
            this.CreateMap<FormTemplateInfo, FormTemplateBasicInfoDto>();
            this.CreateMap<FormInfoRecordDto, FormInfoRecord>();
            this.CreateMap<FormInfoRecord, FormInfoRecordDto>();
            this.CreateMap<DDImportantInfos, DDImportantInfoDto>()
                .ForMember(p => p.LevelString, d => d.MapFrom(opt => opt.Level.ToString()));

            this.CreateMap<DDImportantInfos, DDImportantInfoExportDto>()
                .ForMember(p => p.MaterialRecordInfos, d => d.MapFrom(opt => ConstructMateriralInfos(opt.MaterialRecordSimplyInfos)))
                .ForMember(p => p.LevelString, d => d.MapFrom(opt => opt.Level.ToString()));


            this.CreateMap<View_DDImportantInfos, StockDDExportDto>();

            this.CreateMap<DDImportantInfos, DDImportantInfoWordExportDto>();

            this.CreateMap<View_DDImportantInfos, DDImportantInfoDto>()
                .ForMember(p => p.LevelString, d => d.MapFrom(opt => opt.Level.ToString()));

            this.CreateMap<View_DDImportantInfos, DDImportantInfoExportDto>()
                .ForMember(p => p.MaterialRecordInfos, d => d.MapFrom(opt => ConstructMateriralInfos(opt.MaterialRecordSimplyInfos)))
                .ForMember(p => p.LevelString, d => d.MapFrom(opt => opt.Level.ToString()));

            this.CreateMap<View_DDImportantInfos, DDImportantInfoWordExportDto>();

            this.CreateMap<MaterialRecordSimplyInfo, MaterialRecordSimplyInfoDto>()
                .ForMember(p => p.InputMatreialName, opt => opt.MapFrom(d => d.MatreialName));
        }

        public static string ConstructMateriralInfos(List<MaterialRecordSimplyInfo> materialRecordSimplyInfos)
        {
            if (materialRecordSimplyInfos == null)
            {

                return string.Empty;
            }

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in materialRecordSimplyInfos)
            {
                stringBuilder.Append($"{item.MatreialName}_{item.Supplier}_{item.WarehousingTime.ToString("yyyy-MM-dd")}_{item.BatchNo}\r\n");
            }

            return stringBuilder.ToString();
        }
    }
}
