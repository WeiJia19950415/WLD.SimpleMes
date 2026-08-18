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
    public interface IMaterialBatchNumberRulerAppService : IAsyncCrudAppService<MaterialBatchNumberRulerDto, long, CommonPageRequestDto, MaterialBatchNumberRulerDto, MaterialBatchNumberRulerDto>, IApplicationService
    {
    }
}
