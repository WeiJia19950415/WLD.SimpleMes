using Abp.Dapper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.WorkProcess;

namespace WLD.SimpleMes.K3DBInfo
{
    public interface IK3ErpRepostiory : IDapperRepository<K3MaterialInfo, int>
    {
        SNInStockInfo GetSNInStockInfo(string snInfo);
        WorkOrderPickingMaterilInfo GetWorkOrderPickingMaterilInfo(string workOrderNumber, string materilNumber);
    }
}
