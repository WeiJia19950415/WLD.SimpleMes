using Abp.Dapper.Repositories;
using Abp.Data;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.DynamicForms;
using SC.SimpleMes.DynamicForms.Repository;
using Dapper;
using DapperExtensions;

namespace SC.SimpleMes.EntityFrameworkCore.Repositories
{
    internal class FormTemplateInfoDapperRepository : DapperEfRepositoryBase<SimpleMesDbContext, FormTemplateInfo, long>, IFormTemplateInfoDapperRepository
    {
        public FormTemplateInfoDapperRepository(IActiveTransactionProvider activeTransactionProvider, ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(activeTransactionProvider, currentUnitOfWorkProvider)
        {
        }

      

        public void SetCurrent(long id, string formsName)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("id", id);
            dynamicParameters.Add("formsName", formsName);
            this.GetConnection().Execute("Update [FormTemplateInfos] Set IsCurrent=0 Where Id<>@id and FormsName=@formsName", dynamicParameters, this.GetActiveTransaction());
        }


    }
}
