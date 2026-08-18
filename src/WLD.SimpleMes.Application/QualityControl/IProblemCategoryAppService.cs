using Abp.Application.Services;
using System.Collections.Generic;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.Material.Dto;
using WLD.SimpleMes.QualityControl.Dto;

namespace WLD.SimpleMes.QualityControl
{
    public interface IProblemCategoryAppService : IAsyncCrudAppService<ProblemCategoryDto, long, CommonPageRequestDto, ProblemCategoryDto, ProblemCategoryDto>, IApplicationService
    {
        List<UICascaderModel<string, string>> LoadCascadeProblemCategory(string categGoryCode);
    }
}
