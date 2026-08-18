using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.ERPSyncTask.SyncBusiness
{
    public abstract class IErpSyncBusiness
    {
        protected ERPSyncTask ERPSyncData { get; set; }
        public IErpSyncBusiness()
        {
        }
        protected abstract void DoBusinessCore();

        public void DoBusiness()
        {
            try
            {
                DoBusinessCore();
                ERPSyncData.SyncState = SyncState.Complete;
            }
            catch (Exception)
            {
                ERPSyncData.SyncState = SyncState.fail;
                throw;
            }
            finally
            {

            }

        }
    }
}
