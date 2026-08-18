using Abp.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.BOM;
using WLD.SimpleMes.IRepository;
using Microsoft.EntityFrameworkCore;

namespace WLD.SimpleMes.EntityFrameworkCore.Repositories
{
    public class BomItemRepsoitory: SimpleMesRepositoryBase<BomItemInfo,long>, IBomItemRepsoitory
    {
        public BomItemRepsoitory(IDbContextProvider<SimpleMesDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task BatchDeleteBomItemAsync(long BomInfoId)
        {
            var deleteOldData = $"Delete From BomItemInfos Where BelongBomId = {BomInfoId}";
            await this.GetDbContext().Database.ExecuteSqlRawAsync(deleteOldData);
        }

        public async Task BatchDelWorkProcessSetBomItemByIdAsync(long WorkProcessSetBomId)
        {
            var deleteOldData = $"Delete From WorkProcessSetBomItems Where BelongWorkProcessSetBomId = {WorkProcessSetBomId}";
            await this.GetDbContext().Database.ExecuteSqlRawAsync(deleteOldData);
        }

        public async Task BatchInsertBomItemAsync(List<BomItemInfo> bomItemInfos)
        {
            foreach (var item in bomItemInfos)
            {
                await this.GetContext().BomItemInfos.AddAsync(item);
            }
            this.GetContext().SaveChanges();
        }
    }
}
