using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.DTO;
using SC.SimpleMes.Material.Dto;

namespace SC.SimpleMes.Material
{
    public interface IMaterialBatchNumberRulerAppService : IAsyncCrudAppService<MaterialBatchNumberRulerDto, long, CommonPageRequestDto, MaterialBatchNumberRulerDto, MaterialBatchNumberRulerDto>, IApplicationService
    {
    }
}
