using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.BOM;
using SC.SimpleMes.IRepository;
using SC.SimpleMes.LineSideWarehouse;

namespace SC.SimpleMes.EntityFrameworkCore.Repositories
{
    public class MaterialBOMRepsoitory : SimpleMesRepositoryBase<LineSideMaterialInfoBomItem, long>, IMaterialBOMRepsoitory
    {
        public MaterialBOMRepsoitory(IDbContextProvider<SimpleMesDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task BatchDeleteBomItemAsync(long materialId)
        {
            var deleteOldData = $"Delete From LineSideMaterialInfoBomItems Where LineSideMaterialInfoId = {materialId}";
            await this.GetDbContext().Database.ExecuteSqlRawAsync(deleteOldData);
        }

        public async Task BatchInsertBomItemAsync(List<LineSideMaterialInfoBomItem> bomItemInfos)
        {
            foreach (var item in bomItemInfos)
            {
                await this.GetContext().LineSideMaterialInfoBomItems.AddAsync(item);
            }
            this.GetContext().SaveChanges();
        }
    }
}
