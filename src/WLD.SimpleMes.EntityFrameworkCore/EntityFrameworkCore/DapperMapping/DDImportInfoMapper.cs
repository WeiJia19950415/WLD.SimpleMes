using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.DynamicForms;

namespace WLD.SimpleMes.EntityFrameworkCore.DapperMapping
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
