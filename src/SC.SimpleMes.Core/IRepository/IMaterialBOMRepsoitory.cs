using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.BOM;
using SC.SimpleMes.LineSideWarehouse;

namespace SC.SimpleMes.IRepository
{
    public interface IMaterialBOMRepsoitory
    {
        Task BatchInsertBomItemAsync(List<LineSideMaterialInfoBomItem> bomItemInfos);

        Task BatchDeleteBomItemAsync(long materialId);
    }
}
