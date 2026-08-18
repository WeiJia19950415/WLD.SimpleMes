using Abp.Dapper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.QualityControl;
using SC.SimpleMes.WorkProcess;

namespace SC.SimpleMes.WorkProcess.Repository
{
    public interface IWorkProcessMaterialRecordDapperRep: IDapperRepository<WorkProcessMaterialRecord, long>
    {
        void BatchInsertMaterialRecord(List<WorkProcessMaterialRecord> materialRecords);

        void BatchInsertMaterialRecordHistory(List<WorkProcessMaterialRecordHistory> materialRecords);

        void BatchDelMaterialRecord(string batchNumber);
        void BatchInsertMaterialDiscardRecords(List<MaterialDiscardRecord> materialDiscardRecords);
    }
}
