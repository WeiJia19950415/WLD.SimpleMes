using Abp.Dapper.Repositories;
using Abp.Data;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.WorkProcess;
using WLD.SimpleMes.WorkProcess.Repository;
using Dapper;
using DapperExtensions;

namespace WLD.SimpleMes.EntityFrameworkCore.Repositories
{
    public class WorkProcessDapperRepository : DapperEfRepositoryBase<SimpleMesDbContext, WorkProcessInfo, long>, IWorkProcessDapperRepository
    {
        public WorkProcessDapperRepository(IActiveTransactionProvider activeTransactionProvider, ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(activeTransactionProvider, currentUnitOfWorkProvider)
        {
        }

        public void BatchInsert(List<WorkProcessStationRelation> workProcessStationRelations)
        {
            var insertData = workProcessStationRelations.Select(p =>
              new
              {
                  BelongWorkStationId = p.BelongWorkStationId,
                  BelongWorkProcessId = p.BelongWorkProcessId,
                  CreatTime = p.CreatTime,
              }).ToArray();
            this.GetConnection().Execute(@"Insert WorkProcessStationRelations(BelongWorkStationId,BelongWorkProcessId,CreatTime) values (@BelongWorkStationId,@BelongWorkProcessId,@CreatTime)", insertData, this.GetActiveTransaction());
        }

        public void BatchInsertFormTemplate(List<WorkProcessFormInfoRelation> workProcessFormInfoRelations)
        {
            var insertData = workProcessFormInfoRelations.Select(p => new
            {
                BelongWorkProcessId = p.BelongWorkProcessId,
                BelongFormInfoId = p.BelongFormInfoId,
                FormUseType = p.FormUseType,
                IsEnabled = p.IsEnabled,
            });

            this.GetConnection()
                .Execute("Insert WorkProcessFormInfoRelations(BelongWorkProcessId,BelongFormInfoId,FormUseType,IsEnabled) " +
                " values(@BelongWorkProcessId,@BelongFormInfoId,@FormUseType,@IsEnabled)"
                , insertData,this.GetActiveTransaction());
        }

        public void DeleteWorkProcessStationByProcessId(long belongWorkProcessId)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("BelongWorkProcessId", belongWorkProcessId);
            this.GetConnection().Execute($"delete from WorkProcessStationRelations where BelongWorkProcessId=@BelongWorkProcessId", dynamicParameters, this.GetActiveTransaction());
        }

        public void UpdateFormRelation(long oldFormTemplateId, long newFormTemplateId)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("newFormTemplateId", newFormTemplateId);
            dynamicParameters.Add("oldFormTemplateId", oldFormTemplateId);

            this.GetConnection().Execute($"update WorkProcessFormInfoRelations set BelongFormInfoId=@newFormTemplateId where BelongFormInfoId=@oldFormTemplateId", dynamicParameters, this.GetActiveTransaction());
        }

        public void UpdateWorkProcessSetProuductRelationUnCurrentExcept(long BelongWorkProcessSetId, long MaterialId)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("BelongWorkProcessSetId", BelongWorkProcessSetId);
            dynamicParameters.Add("MaterialInfoId", MaterialId);
            this.GetConnection().Execute($"Update WorkProcessSetProductRelations Set IsCurrent=0 where MaterialInfoId=@MaterialInfoId and BelongWorkProcessSetId!=@BelongWorkProcessSetId", dynamicParameters, this.GetActiveTransaction());
        }
    }
}
