using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using SC.SimpleMes.Authorization.Roles;

namespace SC.SimpleMes.Authorization.Users
{
    public class UserStore : AbpUserStore<Role, User>
    {
        IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
        public UserStore(
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<User, long> userRepository,
            IRepository<Role> roleRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<UserLogin, long> userLoginRepository,
            IRepository<UserClaim, long> userClaimRepository,
            IRepository<UserPermissionSetting, long> userPermissionSettingRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IRepository<OrganizationUnitRole, long> organizationUnitRoleRepository)
            : base(unitOfWorkManager,
                  userRepository,
                  roleRepository,
                  userRoleRepository,
                  userLoginRepository,
                  userClaimRepository,
                  userPermissionSettingRepository,
                  userOrganizationUnitRepository,
                  organizationUnitRoleRepository
                  )
        {
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
        }

        internal List<UserOrganizationUnit> GetUserOrganizations(long userId)
        {
            return this._userOrganizationUnitRepository.GetAll().Where(p => p.UserId == userId).ToList();
        }
    }
}

