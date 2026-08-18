using Abp.Domain.Entities.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Material
{
    public interface IMaterialCache
    {
        Task<MaterialInfoDto> GetByMaterialNumberAsync(string materialNumber);
        MaterialInfoDto GetByMaterialNumber(string materialNumber);
    }
}
