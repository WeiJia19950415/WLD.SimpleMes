using Abp.Application.Services;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.DTO;
using SC.SimpleMes.QualityControl.Dto;
using SC.SimpleMes.Report.Dto;

namespace SC.SimpleMes.QualityControl
{
    public interface IQualityProblemDefineAppService : IAsyncCrudAppService<ProblemDefineDto, long, CommonPageRequestDto, ProblemDefineDto, ProblemDefineDto>, IApplicationService
    {
        List<ProblemDefineDto> GetProblemDefineByCatetoeryCode(string id);

    }
}
