using Abp.Dapper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.WorkProcess.Repository
{
    public interface IWorkProcessDapperRepository : IDapperRepository<WorkProcessInfo, long>
    {
        void BatchInsert(List<WorkProcessStationRelation> workProcessStationRelations);

        void DeleteWorkProcessStationByProcessId(long workProcessId);

        void UpdateWorkProcessSetProuductRelationUnCurrentExcept(long BelongWorkProcessSetId, long MaterialId);
        void BatchInsertFormTemplate(List<WorkProcessFormInfoRelation> workProcessFormInfoRelations);
        void UpdateFormRelation(long oldFormTemplateId, long newFormTemplateId);
    }
}
