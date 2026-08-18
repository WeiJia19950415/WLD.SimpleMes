using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.DynamicForms;

namespace SC.SimpleMes.EntityFrameworkCore.DapperMapping
{
    public class DDImportInfoMapper : ClassMapper<DDImportantInfos>
    {
        public DDImportInfoMapper()
        {
            Table("DDImportantInfos");
            AutoMap();
        }
    }
}
