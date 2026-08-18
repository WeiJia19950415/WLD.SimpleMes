using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.ERPSyncTask.SyncBusiness;

namespace WLD.SimpleMes.ERPSyncTask
{
    public class ERPSyncTaskAppService : SimpleMesAppServiceBase, IERPSyncTaskAppService
    {
        private readonly IRepository<ERPSyncTask, long> repository;
        public void TriggerErpSyncBusiness()
        {
            var unDoData = repository.GetAll().AsNoTracking().Where(p => p.SyncState == SyncState.StayComplete).ToList();
            foreach (var data in unDoData)
            {
                IErpSyncBusiness coreBusines = null;
                switch (data.SyncType)
                {
                    case SyncType.MaterialCategory:
                        break;
                    case SyncType.Material:
                        break;
                    case SyncType.Warehousing:
                        break;
                    default:
                        break;
                }

                coreBusines.DoBusiness();
            }
        }
    }
}
