using Abp.Dapper.Repositories;
using Abp.Data;
using Abp.Domain.Uow;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.DynamicForms;
using SC.SimpleMes.DynamicForms.Repository;

namespace SC.SimpleMes.EntityFrameworkCore.Repositories
{
    public class FormRecordDapperRepository : DapperEfRepositoryBase<LogReportDbContext, FormInfoRecord, long>, IFormRecordDapperRepository
    {
        public FormRecordDapperRepository(IActiveTransactionProvider activeTransactionProvider, ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(activeTransactionProvider, currentUnitOfWorkProvider)
        {
        }

        public void SaveFormRecordToDataBase(DDImportantInfos ojb)
        {
            this.GetConnection().Insert(ojb, this.GetActiveTransaction());
        }
    }
}
