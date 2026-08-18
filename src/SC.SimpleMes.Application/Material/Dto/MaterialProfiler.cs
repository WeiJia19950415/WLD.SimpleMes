using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Material.Dto
{
    public class MaterialProfiler : Profile
    {
        public MaterialProfiler()
        {
            this.CreateMap<MaterialBatchNumber, MaterialBatchNumberDto>();
            this.CreateMap<View_MaterialBatchNumbers, MaterialBatchNumberDto>();
            this.CreateMap<View_MaterialBatchNumbers, MaterialBatchNumberExportDto>();
            this.CreateMap<MaterialBatchNumberDto, MaterialBatchNumber>();



            this.CreateMap<MaterialBatchNumberRuler, MaterialBatchNumberRulerDto>()
                .ForMember(p => p.MateriaCategoryName, d => d.MapFrom(p => p.MaterialCategoryInfo.CategoryName))
                .ForMember(p => p.MateriaCategoryCode, d => d.MapFrom(p => p.MaterialCategoryInfo.CategoryCode));

            this.CreateMap<MaterialBatchNumberRulerDto, MaterialBatchNumberRuler>();
            this.CreateMap<MaterialInfoDto, MaterialInfo>();
            this.CreateMap<MaterialInfo, MaterialInfoDto>()
                .ForMember(p => p.CategoryCode, opt => opt.MapFrom(d => d.BelongCategory.CategoryCode))
                .ForMember(p => p.CategoryName, opt => opt.MapFrom(d => d.BelongCategory.FullCategoryName));

            this.CreateMap<MaterialCategoryDto, MaterialCategory>();
            this.CreateMap<MaterialCategory, MaterialCategoryDto>()
                .ForMember(p => p.ParentCategoryCode, opt => opt.MapFrom(d => MaterialCategory.GetParentCode(d.CategoryCode)));

            this.CreateMap<K3DBInfo.K3MaterialInfo, MaterialInfoDto>()
                .ForMember(p => p.MaterialName, d => d.MapFrom(opt => opt.FName))
                .ForMember(p => p.MaterialNumber, d => d.MapFrom(opt => opt.FFullNumber))
                .ForMember(p => p.CategoryCode, d => d.MapFrom(opt => MaterialCategory.GetParentCode(opt.FFullNumber)))
                ;

            this.CreateMap<ERPInStockInfo, ERPInStockInfoDto>();

            this.CreateMap<CutMaterialConfig, CutMaterialConfigDto>()
                .ForMember(p => p.ProductMaterialNumber, opt => opt.MapFrom(d => d.UsedProduct.MaterialNumber))
                .ForMember(p => p.ProductName, opt => opt.MapFrom(d => d.UsedProduct.MaterialName));

            this.CreateMap<CutMaterialConfigDto, CutMaterialConfig>();
        }
    }
}

