using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.BOM;

namespace SC.SimpleMes.IRepository
{
    public interface IBomItemRepsoitory
    {
        Task BatchInsertBomItemAsync(List<BomItemInfo> bomItemInfos);

        Task BatchDeleteBomItemAsync(long BomInfoId);

        /// <summary>
        /// 清空工艺BOM对应的所有详情
        /// </summary>
        /// <param name="WorkProcessSetBomId"></param>
        /// <returns></returns>
        Task BatchDelWorkProcessSetBomItemByIdAsync(long WorkProcessSetBomId);
    }
}
