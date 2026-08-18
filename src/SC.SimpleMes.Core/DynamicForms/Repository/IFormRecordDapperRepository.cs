using Abp.Dapper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.DynamicForms.Repository
{
    public interface IFormRecordDapperRepository : IDapperRepository<FormInfoRecord, long>
    {
        void SaveFormRecordToDataBase(DDImportantInfos ojb);
    }
}
