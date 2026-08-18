using Abp.Dapper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.DynamicForms.Repository
{
    public interface IFormTemplateInfoDapperRepository : IDapperRepository<FormTemplateInfo, long>
    {
        void SetCurrent( long id,string formsName);
    }
}
