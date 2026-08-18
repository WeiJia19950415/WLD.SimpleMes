using Abp.Application.Services;
using System.Collections.Generic;
using SC.SimpleMes.DTO;
using SC.SimpleMes.Material.Dto;
using SC.SimpleMes.QualityControl.Dto;

namespace SC.SimpleMes.QualityControl
{
    public interface IProblemCategoryAppService : IAsyncCrudAppService<ProblemCategoryDto, long, CommonPageRequestDto, ProblemCategoryDto, ProblemCategoryDto>, IApplicationService
    {
        List<UICascaderModel<string, string>> LoadCascadeProblemCategory(string categGoryCode);
    }
}
