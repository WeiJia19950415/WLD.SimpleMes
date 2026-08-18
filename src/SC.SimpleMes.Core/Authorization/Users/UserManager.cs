using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Abp.Runtime.Caching;
using SC.SimpleMes.Authorization.Roles;
using System.Threading.Tasks;
using System.Linq;

namespace SC.SimpleMes.Authorization.Users
{
    public class UserManager : AbpUserManager<Role, User>
    {
        /// <summary>
        /// 手机号确认
        /// </summary>
        public const string PhoneConfirmPurpose = "PhoneConfirmation";
        private readonly IRepository<User, long> _userRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IdentityOptions _identityOptions;
        private readonly UserStore _userStore;
        public UserManager(
            RoleManager roleManager,
            UserStore store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<User>> logger,
            IPermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<User, long> userRepository,
            ICacheManager cacheManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IOrganizationUnitSettings organizationUnitSettings,
            ISettingManager settingManager,
            IRepository<UserLogin, long> userLoginRepository)
            : base(
                roleManager,
                store,
                optionsAccessor,
                passwordHasher,
                userValidators,
                passwordValidators,
                keyNormalizer,
                errors,
                services,
                logger,
                permissionManager,
                unitOfWorkManager,
                cacheManager,
                organizationUnitRepository,
                userOrganizationUnitRepository,
                organizationUnitSettings,
                settingManager,
                userLoginRepository)
        {
            _userRepository = userRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _identityOptions = optionsAccessor.Value;
            _userStore = store;
        }
        public async Task<User> FindByPhoneNumberAsync(string phoneNumber)
        {
            using (var uow = _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                return await _userRepository.FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
            }
        }

        public async Task<User> FindByPhoneNumberByTeantIdAsync(string phoneNumber, int? teantId)
        {
            using (var uow = _unitOfWorkManager.Current.SetTenantId(teantId))
            {
                return await _userRepository.FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
            }
        }

        public async Task<User> FindByIdAsync(long id)
        {
            return await _userRepository.FirstOrDefaultAsync(p => p.Id == id);
        }
        public List<User> FindPossibleTenantsOfUserByConfirmerPhoneNumber(string userNameOrEmailAddressOrPhoneNumber)
        {
            using (var uow = _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                return _userRepository.GetAll().Where(p => p.PhoneNumber == userNameOrEmailAddressOrPhoneNumber && p.IsPhoneNumberConfirmed == true).ToList();
            }
        }
        public override async Task<IdentityResult> ChangePhoneNumberAsync(User userInfo, string phoneNumber, string token)
        {

            var result = await this.VerifyUserTokenAsync(userInfo, _identityOptions.Tokens.ChangePhoneNumberTokenProvider, UserManager.ChangePhoneNumberTokenPurpose, token);
            if (result)
            {
                userInfo.PhoneNumber = phoneNumber;
                userInfo.IsPhoneNumberConfirmed = true;
                await this.UpdateSecurityStampAsync(userInfo);
                return await this.UpdateAsync(userInfo);
            }

            return IdentityResult.Failed(ErrorDescriber.InvalidToken());
        }
        /// <summary>
        /// 重置电话号码
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public override async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
        {
            if (await this.VerifyUserTokenAsync(user, Options.Tokens.ChangePhoneNumberTokenProvider, ResetPasswordTokenPurpose, token) == false)
            {
                return IdentityResult.Failed(ErrorDescriber.InvalidToken());
            }

            var result = await UpdatePasswordHash(user, newPassword, validatePassword: true);
            if (!result.Succeeded)
            {
                return result;
            }

            return await UpdateUserAsync(user);
        }
        /// <summary>
        /// 根据电话号码获取当前登录租户内对应的用户
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public User FindByPhoneNumberIsTenant(string phone)
        {
            return _userRepository.GetAll().FirstOrDefault(p => p.PhoneNumber == phone);
        }
        /// <summary>
        /// 跨公司获取该电话对应的所有用户
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public List<User> FindByPhoneNumberList(string phone)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                var data = _userRepository.GetAll().Where(p => p.PhoneNumber == phone).ToList();
                return data;
            }
        }

        public List<long> GetUserOrganizations(long userId)
        {
            return _userStore.GetUserOrganizations(userId).Select(p => p.OrganizationUnitId).ToList();
        }
    }
}

