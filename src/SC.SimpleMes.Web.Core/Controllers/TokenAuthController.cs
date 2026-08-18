using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using Abp.UI;
using SC.SimpleMes.Authentication.External;
using SC.SimpleMes.Authentication.JwtBearer;
using SC.SimpleMes.Authorization;
using SC.SimpleMes.Authorization.Users;
using SC.SimpleMes.Models.TokenAuth;
using SC.SimpleMes.MultiTenancy;
using AutoMapper;
using SC.SimpleMes.Users;
using SC.SimpleMes.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Authorization;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using SC.SimpleMes.Configuration;
using SC.SimpleMes.Authorization.Accounts.Dto;
using SC.SimpleMes.Users.Dto;
using SC.SimpleMes.MultiTenancy.Dto;
using Abp.Extensions;

namespace SC.SimpleMes.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TokenAuthController : SimpleMesControllerBase
    {
        private readonly LogInManager _logInManager;
        private readonly UserManager _userManager;
        private readonly TenantManager _tenantManager;
        private readonly ITenantCache _tenantCache;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly TokenAuthConfiguration _configuration;
        private readonly IdentityOptions _identityOptions;
        private readonly IMapper _mapper;
        private readonly IExternalAuthConfiguration _externalAuthConfiguration;
        private readonly IExternalAuthManager _externalAuthManager;
        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly SignInManager _signInManager;
        private readonly IUserAppService _userAppService;

        public TokenAuthController(
            LogInManager logInManager,
            IMapper mapper,
            ITenantCache tenantCache,
            UserManager userManager,
            TenantManager tenantManager,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            TokenAuthConfiguration configuration,
            IOptionsMonitor<IdentityOptions> identityOptions,
            SignInManager signInManager,
            IExternalAuthConfiguration externalAuthConfiguration,
            IExternalAuthManager externalAuthManager,
            UserRegistrationManager userRegistrationManager,
            IUserAppService userAppService)
        {
            _logInManager = logInManager;
            _tenantCache = tenantCache;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _configuration = configuration;
            _userManager = userManager;
            _identityOptions = identityOptions.CurrentValue;
            _tenantManager = tenantManager;
            _externalAuthConfiguration = externalAuthConfiguration;
            _externalAuthManager = externalAuthManager;
            _userRegistrationManager = userRegistrationManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _userAppService = userAppService;
        }

        /// <summary>
        /// 根据用户名/邮箱/电话号码查找用户所有的公司信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AbpAuthorize]
        public List<Tenant> FindPossibleTenantsOfUser()
        {
            var user = _userManager.GetUserById(AbpSession.UserId.GetValueOrDefault());
            var allUsers = _userManager.FindPossibleTenantsOfUserByConfirmerPhoneNumber(user.PhoneNumber);
            return allUsers
           .Where(u => u.TenantId != null)
           .Select(u => _tenantManager.FindById(u.TenantId.GetValueOrDefault()))
           .ToList();

        }
        /// <summary>
        /// 根据目的发送手机验证码
        /// </summary>
        /// <param name="valid">发送验证码信息</param>
        /// <returns></returns>
        [HttpPost]
        [AbpAllowAnonymous]
        public async Task<JHTAjaxResponse> SendValidCodeForPurposeAsync([FromBody] ValidCodeForPurposeDto valid)
        {
            var userInfo = valid.Purpose == PurposeEnum.ChangePhoneNumber ? await _userManager.FindByIdAsync(AbpSession.UserId.Value) : await _userManager.FindByPhoneNumberAsync(valid.PhoneNumber);
            if (userInfo != null)
            {

                var token = await _userManager.GenerateUserTokenAsync(userInfo, _identityOptions.Tokens.ChangePhoneNumberTokenProvider, valid.Purpose.ToString());
                //TODO 通过短信将Token发送
                return new JHTAjaxResponse(token) { Msg = "测试环境下验证码直接线上：" + token };
            }

            return new JHTAjaxResponse() { Msg = "验证码发送失败，手机号不存在" };
        }


        /// <summary>
        /// 传统帐号方式登录:用户名邮箱密码/电话号码密码
        /// 动态验证码方式登录:
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpAllowAnonymous]
        public async Task<JHTAjaxResponse<AuthenticateResultModel>> Authenticate([FromBody] AuthenticateModel model)
        {
            if (model.LoginWay == 2) // 如果正在切换租户
            {
                return await ChangeTeantAsync(new EntityDto<int>() { Id = model.Id });
            }
            AbpLoginResult<Tenant, User> loginResult = null;
            loginResult = await GetLoginResultAsync(
                       model.Account,
                       model.Password,
                      !string.IsNullOrEmpty(model.TeancyName) ? model.TeancyName : GetTenancyNameOrNull(),
                       model.LoginWay
                   );
            var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));
            var result = new AuthenticateResultModel { Token = accessToken, ReturnUrl = model.ReturnUrl };
            var possbillyUser = _userManager.FindPossibleTenantsOfUserByConfirmerPhoneNumber(loginResult.User.PhoneNumber);
            result.UserInfo = _mapper.Map<User, UserDto>(loginResult.User);
            if (string.IsNullOrEmpty(GetTenancyNameOrNull()) && possbillyUser.Count > 1)
            {
                var teant = possbillyUser.Where(u => u.TenantId != null).Select(u => _tenantManager.FindById(u.TenantId.GetValueOrDefault())).ToList();
                result.TenantList = _mapper.Map<List<TenantDto>>(teant);
            }
            return new JHTAjaxResponse<AuthenticateResultModel>
            {
                Data = result
            };
        }

        /// <summary>
        /// 在PAD端登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpAllowAnonymous]
        public async Task<JHTAjaxResponse<AuthenticateResultModel>> PadAuthenticate([FromBody] AuthenticateModel model)
        {
            if (model.LoginWay == 2) // 如果正在切换租户
            {
                return await ChangeTeantAsync(new EntityDto<int>() { Id = model.Id });
            }

            AbpLoginResult<Tenant, User> loginResult = null;
            loginResult = await GetLoginResultAsync(
                       model.Account,
                       model.Password,
                      !string.IsNullOrEmpty(model.TeancyName) ? model.TeancyName : GetTenancyNameOrNull(),
                       model.LoginWay
                   );
            var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));
            var result = new AuthenticateResultModel { Token = accessToken, ReturnUrl = model.ReturnUrl };
            var possbillyUser = _userManager.FindPossibleTenantsOfUserByConfirmerPhoneNumber(loginResult.User.PhoneNumber);
            result.UserInfo = _mapper.Map<User, UserDto>(loginResult.User);
            if (string.IsNullOrEmpty(GetTenancyNameOrNull()) && possbillyUser.Count > 1)
            {
                var teant = possbillyUser.Where(u => u.TenantId != null).Select(u => _tenantManager.FindById(u.TenantId.GetValueOrDefault())).ToList();
                result.TenantList = _mapper.Map<List<TenantDto>>(teant);
            }

            return new JHTAjaxResponse<AuthenticateResultModel>
            {
                Data = result
            };
        }

        /// <summary>
        /// 切换所在公司
        /// </summary>
        /// <param name="entityDto">所在公司ID</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<JHTAjaxResponse<AuthenticateResultModel>> ChangeTeantAsync([FromBody] EntityDto<int> entityDto)
        {
            var loginResult = await _logInManager.ChangeTeantLoginResultAsync(entityDto.Id);
            if (loginResult.Result != AbpLoginResultType.Success)
            {
                throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, "", "");
            }

            var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));
            var result = new AuthenticateResultModel { Token = accessToken, ReturnUrl = "" };
            var possbillyUser = _userManager.FindPossibleTenantsOfUserByConfirmerPhoneNumber(loginResult.User.PhoneNumber);
            result.UserInfo = _mapper.Map<User, UserDto>(loginResult.User);
            if (string.IsNullOrEmpty(GetTenancyNameOrNull()) && possbillyUser.Count > 1)
            {
                var teant = possbillyUser.Where(u => u.TenantId != null).Select(u => _tenantManager.FindById(u.TenantId.GetValueOrDefault())).ToList();
                result.TenantList = _mapper.Map<List<TenantDto>>(teant);
            }
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(loginResult.Identity, true);
            return new JHTAjaxResponse<AuthenticateResultModel>
            {
                Data = result
            };
        }
        /// <summary>
        /// 登出操作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AbpAuthorize]
        public async Task<JHTAjaxResponse> LogoutAsync()
        {
            JHTAjaxResponse result = new JHTAjaxResponse();
            await _signInManager.SignOutAsync();
            return result;
        }
        /// <summary>
        /// 获取登录前的图像文字信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AbpAllowAnonymous]
        [DisableAuditing]
        public JHTAjaxResponse<SiteInfo> GetBeforeLoginData()
        {

            var info = new SiteInfo()
            {
                Logo = SettingManager.GetSettingValue(AppSettingNames.WebLogoImg),
                Title = SettingManager.GetSettingValue(AppSettingNames.WebTitleName),
                BgImg = SettingManager.GetSettingValue(AppSettingNames.WebBgImg)
            };

            return new JHTAjaxResponse<SiteInfo>()
            {
                Data = info
            };
        }

        /// <summary>
        /// 获取全局信息 -登陆后的
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTAjaxResponse<GlobalDataDto> GetGlobalData()
        {
            var info = new SiteInfo()
            {
                Logo = SettingManager.GetSettingValue(AppSettingNames.WebLogoImg),
                Title = SettingManager.GetSettingValue(AppSettingNames.WebTitleName)
            }; // 平台的Logo地址

            if (AbpSession.TenantId != null)
            {
                var tenand = _tenantManager.GetById(AbpSession.TenantId.GetValueOrDefault());
                if (!tenand.LogoImage.IsNullOrEmpty()) info.Logo = tenand.LogoImage;
            }
            return new JHTAjaxResponse<GlobalDataDto>()
            {
                Data = new GlobalDataDto() { Options = null, SiteInfo = info }
            };

        }

        [HttpGet]
        [DisableAuditing]
        public List<ExternalLoginProviderInfoModel> GetExternalAuthenticationProviders()
        {
            return ObjectMapper.Map<List<ExternalLoginProviderInfoModel>>(_externalAuthConfiguration.Providers);
        }

        [HttpPost]
        public async Task<ExternalAuthenticateResultModel> ExternalAuthenticate([FromBody] ExternalAuthenticateModel model)
        {
            var externalUser = await GetExternalUserInfo(model);

            var loginResult = await _logInManager.LoginAsync(new UserLoginInfo(model.AuthProvider, model.ProviderKey, model.AuthProvider), GetTenancyNameOrNull());

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    {
                        var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));
                        return new ExternalAuthenticateResultModel
                        {
                            AccessToken = accessToken,
                            EncryptedAccessToken = GetEncryptedAccessToken(accessToken),
                            ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds
                        };
                    }
                case AbpLoginResultType.UnknownExternalLogin:
                    {
                        var newUser = await RegisterExternalUserAsync(externalUser);
                        if (!newUser.IsActive)
                        {
                            return new ExternalAuthenticateResultModel
                            {
                                WaitingForActivation = true
                            };
                        }

                        // Try to login again with newly registered user!
                        loginResult = await _logInManager.LoginAsync(new UserLoginInfo(model.AuthProvider, model.ProviderKey, model.AuthProvider), GetTenancyNameOrNull());
                        if (loginResult.Result != AbpLoginResultType.Success)
                        {
                            throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                                loginResult.Result,
                                model.ProviderKey,
                                GetTenancyNameOrNull()
                            );
                        }

                        return new ExternalAuthenticateResultModel
                        {
                            AccessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity)),
                            ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds
                        };
                    }
                default:
                    {
                        throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                            loginResult.Result,
                            model.ProviderKey,
                            GetTenancyNameOrNull()
                        );
                    }
            }
        }

        private async Task<User> RegisterExternalUserAsync(ExternalAuthUserInfo externalUser)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                externalUser.Name,
                externalUser.Surname,
                externalUser.EmailAddress,
                externalUser.EmailAddress,
                Authorization.Users.User.CreateRandomPassword(),
                true
            );

            user.Logins = new List<UserLogin>
            {
                new UserLogin
                {
                    LoginProvider = externalUser.Provider,
                    ProviderKey = externalUser.ProviderKey,
                    TenantId = user.TenantId
                }
            };

            await CurrentUnitOfWork.SaveChangesAsync();

            return user;
        }

        private async Task<ExternalAuthUserInfo> GetExternalUserInfo(ExternalAuthenticateModel model)
        {
            var userInfo = await _externalAuthManager.GetUserInfo(model.AuthProvider, model.ProviderAccessCode);
            if (userInfo.ProviderKey != model.ProviderKey)
            {
                throw new UserFriendlyException(L("CouldNotValidateExternalUser"));
            }

            return userInfo;
        }

        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName, int loginMthod)
        {
            AbpLoginResult<Tenant, User> loginResult = null;
            if (loginMthod == 0)
            {
                loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);
            }
            else
            {
                loginResult = await _logInManager.LoginByPhoneDymaicTokenAsync(usernameOrEmailAddress, password, tenancyName);
            }

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? _configuration.Expiration),
                signingCredentials: _configuration.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });

            return claims;
        }

        private string GetEncryptedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, SimpleMesConsts.DefaultPassPhrase);
        }
    }
}


