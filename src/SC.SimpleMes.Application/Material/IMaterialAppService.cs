using Abp.Application.Services;
using JHT.Abp.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.DTO;
using SC.SimpleMes.Material.Dto;
using SC.SimpleMes.Report.Dto;

namespace SC.SimpleMes.Material
{
    public interface IMaterialAppService:IAsyncCrudAppService<MaterialInfoDto, long, CommonPageRequestDto, MaterialInfoDto, MaterialInfoDto>, IApplicationService
    {
        List<MaterialInfoDto> LoadFromK3();
        Task<JHTAjaxResponse> MarkBatchNoOverUseInfoAsync(View_BatchMaterialUsedReportDto request);
        Task<JHTAjaxResponse> SetMaterialStatuAsync(MaterialBatchNumberDto request);
    }
}
