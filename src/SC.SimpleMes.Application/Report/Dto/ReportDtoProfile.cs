using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.K3DBInfo;
using SC.SimpleMes.Material;
using SC.SimpleMes.WorkOrder;

namespace SC.SimpleMes.Report.Dto
{
    public class ReportDtoProfile : Profile
    {
        public ReportDtoProfile()
        {
            this.CreateMap<WorkProcessCapacityDailyReportRecord, WorkProcessCapacityDailyReportRecordDto>();
            this.CreateMap<ProductLineCapacityDailyReportRecord, ProductLineCapacityDailyReportRecordDto>();
            this.CreateMap<WorkProcessProblemDailyReportRecord, WorkProcessProblemDailyReportRecordDto>();
            this.CreateMap<WorkProcessOnePassRateReport, WorkProcessOnePassRateReportDto>();
            this.CreateMap<PrepaireWorkProcessDayReport, PrepaireWorkProcessDayReportDto>();
            this.CreateMap<View_OrderMaterialProduceStatuses, OrderMaterialProduceStatuExportDto>();
            this.CreateMap<View_ProductConstructMaterialInfo, ProductConstructMaterialInfoDto>();
            this.CreateMap<View_ProductConstructMaterialInfo, ProductConstructMaterialInfoExportDto>();
            this.CreateMap<View_PrepareUserWorkStatic, PrepareUserWorkStaticDto>();
            this.CreateMap<View_DDTestDayKPI, DDTestDayKPIDto>();
            this.CreateMap<SNInStockInfo, SNInStockInfoDto>();
            this.CreateMap<DDWeekOnePassRateReport, DDWeekOnePassRateReportDto>();
            this.CreateMap<OrgProductProcessWorkLoadReport, OrgProductProcessWorkLoadReportDto>();
            this.CreateMap<ERPInStockInfoOperateRecord, ERPInStockInfoOperateRecordDTO>();
            this.CreateMap<View_BatchMaterialUsedReport, View_BatchMaterialUsedReportDto>()
                .ForMember(p => p.IsOverUsed, d => d.MapFrom(opt => opt.IsOverUsed > 0));
        }
    }
}
