using Abp.Authorization;
using WLD.SimpleMes.Authorization.Roles;
using WLD.SimpleMes.Authorization.Users;

namespace WLD.SimpleMes.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}

