using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Material.Dto;

namespace WLD.SimpleMes.Material
{
    public interface IMaterialBatchNumberCache
    {
        Task<MaterialBatchNumberDto> GetByMaterialBatchNumberAsync(string materialBatchNumber);
        MaterialBatchNumberDto GetByMaterialBatchNumber(string materialBatchNumber);

    }
}
