using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Material.Dto;

namespace SC.SimpleMes.Material
{
    public interface IMaterialBatchNumberCache
    {
        Task<MaterialBatchNumberDto> GetByMaterialBatchNumberAsync(string materialBatchNumber);
        MaterialBatchNumberDto GetByMaterialBatchNumber(string materialBatchNumber);

    }
}
