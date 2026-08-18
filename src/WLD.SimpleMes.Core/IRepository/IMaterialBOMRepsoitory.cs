using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.BOM;
using WLD.SimpleMes.LineSideWarehouse;

namespace WLD.SimpleMes.IRepository
{
    public interface IMaterialBOMRepsoitory
    {
        Task BatchInsertBomItemAsync(List<LineSideMaterialInfoBomItem> bomItemInfos);

        Task BatchDeleteBomItemAsync(long materialId);
    }
}
