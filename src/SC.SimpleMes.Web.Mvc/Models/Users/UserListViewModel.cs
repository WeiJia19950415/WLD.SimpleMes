using System.Collections.Generic;
using WLD.SimpleMes.Roles.Dto;

namespace WLD.SimpleMes.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}

