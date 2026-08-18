using Microsoft.AspNetCore.Identity;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Zero.Configuration;
using SC.SimpleMes.Authorization.Roles;
using SC.SimpleMes.Authorization.Users;
using SC.SimpleMes.MultiTenancy;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Abp.Runtime.Session;
using System.Linq;

namespace SC.SimpleMes.Authorization
{
    public class LogInManager : AbpLogInManager<Tenant, Role, User>
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly UserManager _userManager;
        private readonly IRepository<Tenant> _tenantRepository;
        private readonly IdentityOptions _identityOptions;
        private readonly IAbpSession _abpSession;
        public LogInManager(
            UserManager userManager, 
            IMultiTenancyConfig multiTenancyConfig,
            IRepository<Tenant> tenantRepository,
            IRepository<User, long> userRepository,
            IOptionsMonitor<IdentityOptions> identityOptions,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager, 
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository, 
            IUserManagementConfig userManagementConfig,
            IIocResolver iocResolver,
            IPasswordHasher<User> passwordHasher, 
            RoleManager roleManager,
            IAbpSession abpSession,
            UserClaimsPrincipalFactory claimsPrincipalFactory) 
            : base(
                  userManager, 
                  multiTenancyConfig,
                  tenantRepository, 
                  unitOfWorkManager, 
                  settingManager, 
                  userLoginAttemptRepository, 
                  userManagementConfig, 
                  iocResolver, 
                  passwordHasher, 
                  roleManager, 
                  claimsPrincipalFactory)
        {
            this._userRepository = userRepository;
            this._userManager = userManager;
            this._tenantRepository = tenantRepository;
            this._identityOptions = identityOptions.CurrentValue;
            this._abpSession = abpSession;
        }
        public override async Task<AbpLoginResult<Tenant, User>> LoginAsync(string userNameOrEmailAddress, string plainPassword, string tenancyName = null, bool shouldLockout = true)
        {
            var result = await base.LoginAsync(userNameOrEmailAddress, plainPassword, tenancyName, shouldLockout);
            if (result.Result != AbpLoginResultType.Success)
            {
                var teant = _tenantRepository.FirstOrDefault(p => p.TenancyName == tenancyName);
                User userInfo = null;
                if (teant == null)
                {
                    userInfo = this._userRepository.FirstOrDefault(p => p.PhoneNumber == userNameOrEmailAddress && p.TenantId == null);
                }
                else
                {
                    using (UnitOfWorkManager.Current.SetTenantId(teant.Id))
                    {
                        userInfo = this._userRepository.FirstOrDefault(p => p.PhoneNumber == userNameOrEmailAddress && p.TenantId == teant.Id);
                    }
                }
                if (userInfo != null && _userManager.PasswordHasher.VerifyHashedPassword(userInfo, userInfo.Password, plainPassword) == PasswordVerificationResult.Success)// 并校验密码
                {
                    result = await base.LoginAsync(userInfo.UserName, plainPassword, tenancyName, shouldLockout);
                }
            }

            return result;
        }
        /// <summary>
        /// 使用动态验证码登录
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="token"></param>
        /// <param name="tenancyName"></param>
        /// <returns></returns>
        public async Task<AbpLoginResult<Tenant, User>> LoginByPhoneDymaicTokenAsync(string phoneNumber, string token, string tenancyName = null)
        {
            var userInfo = await _userManager.FindByPhoneNumberAsync(phoneNumber);
            var result = await _userManager.VerifyUserTokenAsync(userInfo, _identityOptions.Tokens.ChangePhoneNumberTokenProvider, "login", token);
            if (result)
            {
                var teant = _tenantRepository.FirstOrDefault(p => p.TenancyName == tenancyName);
                return await CreateLoginResultAsync(userInfo, teant);
            }

            return new AbpLoginResult<Tenant, User>(AbpLoginResultType.InvalidPassword) { };
        }

        /// <summary>
        /// 更改租户
        /// </summary>
        /// <param name="teantId"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task<AbpLoginResult<Tenant, User>> ChangeTeantLoginResultAsync(int? teantId)
        {
            var user = await _userManager.FindByIdAsync(_abpSession.UserId.GetValueOrDefault());
            var allUser = _userManager.FindPossibleTenantsOfUserByConfirmerPhoneNumber(user.PhoneNumber);

            var loginUser = allUser.FirstOrDefault(p => p.TenantId == teantId);
            var tenant = _tenantRepository.FirstOrDefault(p => p.Id == teantId);

            return await CreateLoginResultAsync(loginUser, tenant);
        }
    }
}

