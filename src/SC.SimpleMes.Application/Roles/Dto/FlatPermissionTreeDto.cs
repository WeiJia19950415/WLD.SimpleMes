using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Roles.Dto
{
    public class FlatPermissionTreeDto : FlatPermissionDto
    {
        public FlatPermissionDto Parent { get; set; }
        public List<FlatPermissionDto> Children { get; set; }
    }
}

