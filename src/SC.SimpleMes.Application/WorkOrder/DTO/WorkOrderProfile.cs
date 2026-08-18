using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.BOM;
using SC.SimpleMes.K3DBInfo;
using SC.SimpleMes.WorkProcess;
using SC.SimpleMes.WorkProcess.Dto;

namespace SC.SimpleMes.WorkOrder.DTO
{
    public class WorkOrderProfile : Profile
    {
        public WorkOrderProfile()
        {
            this.CreateMap<WorkOrderInfo, WorkOrderInfoDto>()
                .ForMember(p => p.MaterialName, opt => opt.MapFrom(d => d.MaterialInfo.MaterialName))
                .ForMember(p => p.MaterialNumber, opt => opt.MapFrom(d => d.MaterialInfo.MaterialNumber))
                .ForMember(p => p.ProduceWorkShopName, opt => opt.MapFrom(d => d.ProduceLine == null ? "" : d.ProduceWorkShop.WorkShopName))
                .ForMember(p => p.ProductLineName, opt => opt.MapFrom(d => d.ProduceLine == null ? "" : d.ProduceLine.ProductLineName))
                .ForMember(p => p.CustomerProductInfo, opt => opt.MapFrom(d => d.CustomerProductInfo))
                ;

            this.CreateMap<CustomerProductInfo, CustomerProductInfoDto>();
            this.CreateMap<CustomerProductInfoDto, CustomerProductInfo>();

            this.CreateMap<CreateUpdateWorkOrderInfoDto, WorkOrderInfo>();

            this.CreateMap<OrderMaterialProduceStatu, OrderMaterialProduceStatuDto>();

            this.CreateMap<View_OrderMaterialProduceStatuses, OrderMaterialProduceStatuDto>();
            this.CreateMap<View_OverUseWorkOrderInfo, WorkOrderInfoDto>()
                              .ForMember(p => p.ProduceWorkShopName, opt => opt.MapFrom(d => d.WorkShopName));

            this.CreateMap<WorkOrderBom, WorkOrderBomDto>();
            this.CreateMap<WorkOrderPickingMaterilInfo, WorkOrderPickingMaterilInfoDto>();
            this.CreateMap<WorkOrderBomItem, WorkOrderBomItemDto>()
                .ForMember(p => p.InputMaterialNumber, opt => opt.MapFrom(d => d.InputMaterial.MaterialNumber))
                .ForMember(p => p.InputMaterialName, opt => opt.MapFrom(d => d.InputMaterial.MaterialName))
                .ForMember(p => p.InputMaterialUnitName, opt => opt.MapFrom(d => d.InputMaterial.UnitName))
                .ForMember(p => p.Specification, opt => opt.MapFrom(d => d.InputMaterial.Specification))
                .ForMember(p => p.BelongWorkProcessNumber, opt => opt.MapFrom(d => d.BelongWorkProcess.ProcessNumber))
                ;
        }
    }
}

