using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Common
{
    public class WLDDbFunctionsExtension
    {
        [DbFunction("JSON_QUERY",IsBuiltIn =true)]
        public static string JsonQuery(string column, [NotParameterized] string path)
        {
            throw new NotImplementedException();
        }
    }
}
