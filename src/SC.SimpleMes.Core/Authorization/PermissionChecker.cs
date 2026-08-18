using Abp.Authorization;
using SC.SimpleMes.Authorization.Roles;
using SC.SimpleMes.Authorization.Users;

namespace SC.SimpleMes.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}

