using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.Material.Dto;

namespace WLD.SimpleMes.Material
{
    public interface IMaterialCategoryAppService : IAsyncCrudAppService<MaterialCategoryDto, long, CommonPageRequestDto, MaterialCategoryDto, MaterialCategoryDto>, IApplicationService
    {
        List<UICascaderModel<string, string>> LoadCascadeMaterialCategory(string categGoryCode);
        Task<List<MaterialCategoryDto>> LoadAllProductCategoryAsync();
    }
}
