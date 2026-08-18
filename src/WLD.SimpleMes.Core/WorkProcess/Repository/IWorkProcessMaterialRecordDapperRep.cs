using Abp.Dapper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.QualityControl;
using WLD.SimpleMes.WorkProcess;

namespace WLD.SimpleMes.WorkProcess.Repository
{
    public interface IWorkProcessMaterialRecordDapperRep: IDapperRepository<WorkProcessMaterialRecord, long>
    {
        void BatchInsertMaterialRecord(List<WorkProcessMaterialRecord> materialRecords);

        void BatchInsertMaterialRecordHistory(List<WorkProcessMaterialRecordHistory> materialRecords);

        void BatchDelMaterialRecord(string batchNumber);
        void BatchInsertMaterialDiscardRecords(List<MaterialDiscardRecord> materialDiscardRecords);
    }
}
