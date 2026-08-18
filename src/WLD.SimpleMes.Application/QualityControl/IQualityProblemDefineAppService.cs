using Abp.Application.Services;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.QualityControl.Dto;
using WLD.SimpleMes.Report.Dto;

namespace WLD.SimpleMes.QualityControl
{
    public interface IQualityProblemDefineAppService : IAsyncCrudAppService<ProblemDefineDto, long, CommonPageRequestDto, ProblemDefineDto, ProblemDefineDto>, IApplicationService
    {
        List<ProblemDefineDto> GetProblemDefineByCatetoeryCode(string id);

    }
}
