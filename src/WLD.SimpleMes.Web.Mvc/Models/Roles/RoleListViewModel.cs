using System.Collections.Generic;
using WLD.SimpleMes.Roles.Dto;

namespace WLD.SimpleMes.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}

