using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using JHT.Abp.CommonModels;
using JHT.CommonUtity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WLD.SimpleMes.AttachFile;
using WLD.SimpleMes.Authorization;
using WLD.SimpleMes.Authorization.Roles;
using WLD.SimpleMes.Authorization.Users;
using WLD.SimpleMes.Roles.Dto;
using WLD.SimpleMes.Users.Dto;
using WLD.SimpleMes.WorkStation;

namespace WLD.SimpleMes.Users
{

    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly IRepository<User, long> _repository;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;
        private readonly FileSaveOptions _fileSaveOptions;
        private readonly IRepository<ViewUser, long> _viewUser;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<UserLogin, long> _userLogin;
        private readonly IRepository<WorkStationUserRelation, long> _workStationUserRelation;
        private readonly IRepository<WorkStationInfo, long> _workStationInfo;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            LogInManager logInManager,
            IRepository<ViewUser, long> viewUser,
            IUnitOfWorkManager unitOfWorkManager,
            IOptionsMonitor<FileSaveOptions> fileSaveOptions,
            IRepository<WorkStationUserRelation, long> workStationUserRelation,
            IRepository<WorkStationInfo, long> workStationInfo,
            IRepository<UserLogin, long> userLogin)
            : base(repository)
        {
            _repository = repository;
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;
            _fileSaveOptions = fileSaveOptions.CurrentValue;
            _viewUser = viewUser;
            _unitOfWorkManager = unitOfWorkManager;
            _userLogin = userLogin;
            _workStationInfo = workStationInfo;
            _workStationUserRelation = workStationUserRelation;
        }

        public override async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            CheckErrors(await _userManager.CreateAsync(user, input.Password));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        public override async Task<UserDto> UpdateAsync(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            if (input.WorkStationUserRelationIds != null && input.WorkStationUserRelationIds.Count > 0)
            {
                var dataBaseUsers = _workStationUserRelation.GetAll().Where(p => p.UserInfoId == input.Id).ToList();
                foreach (var item in dataBaseUsers)
                {
                    await _workStationUserRelation.DeleteAsync(item);
                }
                List<WorkStationUserRelation> newdata = new List<WorkStationUserRelation>();
                foreach (var item in input.WorkStationUserRelationIds)
                {
                    newdata.Add(new WorkStationUserRelation()
                    {
                        UserInfoId = input.Id,
                        WorkStationInfoId = item[2],
                    });
                }
                user.WorkStationUserRelations = newdata;
                CurrentUnitOfWork.SaveChanges();
            }

            return await GetAsync(input);
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        [AbpAuthorize(PermissionNames.Pages_Users_Activation)]
        public void Activate(EntityDto<long> user)
        {
            Repository.Update(user.Id, (entity) =>
           {
               entity.IsActive = true;
           });
        }

        [AbpAuthorize(PermissionNames.Pages_Users_Activation)]
        public void DeActivate(EntityDto<long> user)
        {
            Repository.Update(user.Id, (entity) =>
            {
                entity.IsActive = false;
            });
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roleIds = user.Roles.Select(x => x.RoleId).ToArray();

            var roles = _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.NormalizedName);

            var orgIds = _userManager.GetOrganizationUnits(user).Select(p => p.Id);

            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();
            userDto.OrgId = orgIds.ToArray();

            return userDto;
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedUserResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.EmailAddress.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<bool> ChangePassword(ChangePasswordDto input)
        {
            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            if (await _userManager.CheckPasswordAsync(user, input.CurrentPassword))
            {
                CheckErrors(await _userManager.ChangePasswordAsync(user, input.NewPassword));
            }
            else
            {
                CheckErrors(IdentityResult.Failed(new IdentityError
                {
                    Description = "Incorrect password."
                }));
            }

            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attempting to reset password.");
            }

            var currentUser = await _userManager.GetUserByIdAsync(_abpSession.GetUserId());
            var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
            }

            if (currentUser.IsDeleted || !currentUser.IsActive)
            {
                return false;
            }

            var roles = await _userManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                throw new UserFriendlyException("Only administrators may reset passwords.");
            }

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            return true;
        }

        public async Task<JHTAjaxResponse> ResetPassWord(ResetPasswordDto input)
        {
            JHTAjaxResponse resultDto = new JHTAjaxResponse();
            if (_abpSession.UserId == null)
            {
                resultDto.Code = 500;
                resultDto.Data = false;
                resultDto.Msg = "请登录后再尝试修改密码！";
                return resultDto;
            }
            long currentUserId = _abpSession.UserId.Value;
            var currentUser = await _userManager.GetUserByIdAsync(currentUserId);
            var result = await _userManager.CheckPasswordAsync(currentUser, input.AdminPassword);

            if (result == false)
            {
                resultDto.Code = 500;
                resultDto.Data = false;
                resultDto.Msg = "管理员密码错误！";
                return resultDto;
            }

            var roles = await _userManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                resultDto.Code = 500;
                resultDto.Data = false;
                resultDto.Msg = "只有管理员可以重置用户密码";
                return resultDto;
            }

            if (currentUser.TenantId == null)
            {
                using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
                {
                    var user = await _userManager.GetUserByIdAsync(input.UserId);
                    var b = await _userManager.CheckPasswordAsync(user, input.NewPassword);
                    resultDto.Code = b ? 200 : 500;
                    resultDto.Msg = b ? "重置密码成功!" : "重置密码失败!";
                    return resultDto; ;
                }
            }
            else
            {
                var user = await _userManager.GetUserByIdAsync(input.UserId);
                var changeResult = await _userManager.ChangePasswordAsync(user, input.NewPassword);
                resultDto.Code = changeResult.Succeeded ? 200 : 500;
                resultDto.Msg = changeResult.Succeeded ? "重置密码成功!" : "重置密码失败!";
                return resultDto; ;
            }
        }

        public void BatchToggleUserActiveState(long[] userIds)
        {

            //Repository.GetAll().Where(p => userIds.Contains(p.Id)).UpdateFromQuery((item) => new User
            //{
            //    IsActive = !item.IsActive
            //});
        }

        public async Task<IdentityResult> UpdateUserBasicInfoAsync(UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId());
            user.Birthday = updateUserDto.Birthday;
            user.Name = updateUserDto.Name;
            user.IdCard = updateUserDto.IdCard;
            user.SortNumber = updateUserDto.SortNumber;
            user.WorkAddress = updateUserDto.WorkAddress;
            user.WorkNumber = updateUserDto.WorkNumber;
            user.Postion = updateUserDto.Postion;
            user.HeadImage = updateUserDto.HeadImage;

            IdentityResult result = await _userManager.UpdateAsync(user);
            if (updateUserDto.RoleNames != null && updateUserDto.RoleNames.Count() > 0)
            {
                result = await _userManager.SetRolesAsync(user, updateUserDto.RoleNames);
            }
            if (updateUserDto.OrgIds != null && updateUserDto.OrgIds.Count() > 0)
            {
                await _userManager.SetOrganizationUnitsAsync(user, updateUserDto.OrgIds);
            }

            return result;
        }

        public async Task<string> SaveHeadImageAsync(IFormFile file, long userId = 0)
        {
            userId = userId == 0 ? AbpSession.GetUserId() : userId;
            if (userId == 0)
            {
                return string.Empty;
            }

            var fileExtension = Path.GetExtension(file.FileName);
            var fileRenameName = Guid.NewGuid().ToString(); //Path.GetRandomFileName();
            var path = string.Format("{0}/{1}/{2}", _fileSaveOptions.UserHeadImageSavePath, AbpSession.TenantId.GetValueOrDefault(), fileRenameName + fileExtension);
            List<string> errors = new List<string>();
            var stemaContent = await FileHelpers.ProcessFormFile(file, errors, _fileSaveOptions.AllowedExtensions, _fileSaveOptions.AllowedFileSzie);
            if (errors.Count > 0)
            {
                throw new UserFriendlyException(string.Join(",", errors));
            }
            else
            {
                var savaPath = _fileSaveOptions.DeafaultSavePath + path;
                var dic = Path.GetDirectoryName(savaPath);
                if (!Directory.Exists(dic))
                {
                    Directory.CreateDirectory(dic);
                }
                if (file.Length > _fileSaveOptions.MaxImageSize * 1024)
                {
                    //压缩
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream, CancellationToken.None);
                        using (var img = Image.FromStream(memoryStream))
                        {
                            ImageCropHelper.CompressImageWithProportional(savaPath, img, 500, 500);
                        }
                    }
                }
                else
                {
                    using var stream = File.Create(savaPath);
                    await stream.WriteAsync(stemaContent);
                }
            }
            var imagepath = string.Format("{0}{1}", _fileSaveOptions.DeafaultSaveDomain, path);
            return imagepath;
        }

        public async Task<PageData<ViewUserDto>> SearchUserIncludeRole(JHTPageAjaxResquest<UserConditionDto> whereDto)
        {
            var where = whereDto.Condition;
            var req = _viewUser.GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(where.KeyWord), p => p.Name.Contains(where.KeyWord) || p.UserName.Contains(where.KeyWord) || p.PhoneNumber.Contains(where.KeyWord))
                .WhereIf(where.IsActive.HasValue, p => p.IsActive == where.IsActive)
                .WhereIf(where.OrgId != null, p => p.OrganizationUnitId == where.OrgId.Value)
                .WhereIf(where.RoleId != null, p => p.RoleId == where.RoleId);
            var result = new PageData<ViewUserDto>()
            {
                Total = req.Count(),
                List = await ObjectMapper.ProjectTo<ViewUserDto>(req.PageBy(whereDto.SkipCount, whereDto.PageSize)).ToListAsync()
            };
            return result;
        }

        public async Task<PageData<UserDto>> SearchUser(JHTPageAjaxResquest<UserConditionDto> whereDto)
        {
            var where = whereDto.Condition;
            var req = Repository.GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(where.KeyWord), p => p.Name.Contains(where.KeyWord) || p.UserName.Contains(where.KeyWord) || p.PhoneNumber.Contains(where.KeyWord));
            List<UserDto> lists = ObjectMapper.Map<List<UserDto>>(await req.PageBy(whereDto.SkipCount, whereDto.PageSize).ToListAsync());
            foreach (var item in lists)
            {
                if (!string.IsNullOrEmpty(item.PhoneNumber) && item.PhoneNumber.Length > 10)
                {
                    item.PhoneNumber = GetxxxString(item.PhoneNumber);
                }
            }
            return new PageData<UserDto>()
            {
                Total = req.Count(),
                List = lists
            };
        }
        private static string GetxxxString(string Input)
        {
            string Output = "";
            switch (Input.Length)
            {
                case 1:
                    Output = "*";
                    break;
                case 2:
                    Output = Input[0] + "*";
                    break;
                case 0:
                    Output = "";
                    break;
                default:
                    Output = Input.Substring(0, 3);
                    for (int i = 0; i < Input.Length - 7; i++)
                    {
                        Output += "*";
                    }
                    Output += Input.Substring(Input.Length - 4, 4);
                    break;
            }
            return Output;
        }
        public async Task<UserDto> FindPhoneNumberAsync(int? tenantId, string phoneNumber)
        {
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
                {
                    var userId = _userManager.Users.Where(q => q.PhoneNumber == phoneNumber).Select(q => q.Id).FirstOrDefault();
                    if (userId == 0) return null;
                    var user = await GetAsync(new EntityDto<long>() { Id = userId });
                    if (user == null) return null;
                    return ObjectMapper.Map<UserDto>(user);
                }
            }
        }

        public async Task<UserDto> UpdateAndResumeAsync(UserDto input)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                input.IsDeleted = false;
                return await UpdateAsync(input);
            }
        }

        public async Task<UserDto> FindNameOrEmailAddressAsync(int? tenantId, string userName, string emailAddress)
        {
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
                {
                    var user = (await _userManager.FindByNameAsync(userName));
                    if (user == null && !string.IsNullOrEmpty(emailAddress))
                    {
                        user = (await _userManager.FindByEmailAsync(emailAddress));
                    }
                    return ObjectMapper.Map<UserDto>(user);
                }
            }
        }

        public async Task<ResultDto<bool>> AddUserAsync(UserDto userDto)
        {

            if (!userDto.EmailAddress.IsNullOrEmpty())
            {
                await _userManager.CheckDuplicateUsernameOrEmailAddressAsync(userDto.Id, userDto.UserName, userDto.EmailAddress);
            }

            if (userDto.HeadImage.IsNullOrEmpty()) userDto.HeadImage = "/Images/DefaultHeadImg.png";

            ResultDto<bool> resultDto = new ResultDto<bool>();
            if (!userDto.PhoneNumber.IsNullOrEmpty())
            {
                if (!IsPhoneVail(userDto.PhoneNumber))
                {
                    resultDto.Data = false;
                    resultDto.Message = "手机号码格式不对！";
                    resultDto.Success = false;
                    return resultDto;
                }
                if (_userManager.FindByPhoneNumberIsTenant(userDto.PhoneNumber) != null)
                {
                    resultDto.Data = false;
                    resultDto.Message = "手机号码已存在！";
                    resultDto.Success = false;
                    return resultDto;
                }

            }

            User Insert = ObjectMapper.Map<User>(userDto);
            Insert.IsActive = true;
            Insert.Surname = Insert.UserName;
            Insert.NormalizedUserName = Insert.Name;
            Insert.Password = _userManager.PasswordHasher.HashPassword(Insert, User.DefaultPassword);
            if (Insert.EmailAddress.IsNullOrEmpty())
            {
                Insert.EmailAddress = userDto.UserName + "@jhtwl.com";
                Insert.IsEmailConfirmed = false;
                Insert.NormalizedEmailAddress = Insert.EmailAddress.ToUpper();
            }
            if (Insert.PhoneNumber.IsNullOrEmpty())
            {
                Insert.IsPhoneNumberConfirmed = false;
            }
            Insert.Roles = new List<UserRole>();
            //设置角色
            if (userDto.RoleNames != null)
            {
                foreach (var item in userDto.RoleNames)
                {
                    var Role = _roleManager.GetRoleByName(item);
                    Insert.Roles.Add(new UserRole()
                    {
                        RoleId = Role.Id,
                        TenantId = AbpSession.TenantId,
                        CreationTime = DateTime.Now
                    });
                }
            }

            Insert.TenantId = AbpSession.TenantId;
            var ret = await _userManager.CreateAsync(Insert);

            if (userDto.OrgId != null && userDto.OrgId.Length > 0)
            {
                foreach (var item in userDto.OrgId)
                {
                    await _userManager.AddToOrganizationUnitAsync(Insert.Id, item);
                }
            }

            resultDto.Success = ret.Succeeded;
            resultDto.Message = ret.Errors.Count() > 0 ? string.Join(',', ret.Errors.Select(t => L(t.Description))) : string.Empty;
            resultDto.Data = ret.Succeeded;
            CurrentUnitOfWork.SaveChanges();

            return resultDto;
        }
        private bool IsPhoneVail(string phoneNumber)
        {
            Regex phoneReg = new Regex(@"^1\d{10}$");
            return phoneReg.IsMatch(phoneNumber);
        }

        [AbpAllowAnonymous]
        public UserDto GetUserDto(long Id)
        {
            var user = _repository.Get(Id);
            UserDto userDto = ObjectMapper.Map<UserDto>(user);
            var orgId = _userManager.GetOrganizationUnits(user);
            userDto.OrgId = orgId != null ? orgId.Select(p => p.Id).ToList().ToArray() : new List<long>().ToArray();
            return userDto;
        }

        public async Task<ResultDto<bool>> UpdateUser(UserDto userDto)
        {
            if (!userDto.EmailAddress.IsNullOrEmpty()) await _userManager.CheckDuplicateUsernameOrEmailAddressAsync(userDto.Id, userDto.UserName, userDto.EmailAddress);

            ResultDto<bool> resultDto = new ResultDto<bool>();
            resultDto.Success = true;
            if (!userDto.PhoneNumber.IsNullOrEmpty())
            {

                if (!IsPhoneVail(userDto.PhoneNumber))
                {
                    resultDto.Data = false;
                    resultDto.Message = "手机号码格式不对！";
                    resultDto.Success = false;
                    return resultDto;
                }
                var userList = _userManager.FindByPhoneNumberList(userDto.PhoneNumber);
                if (userList.Exists(m => m.Id != userDto.Id && m.PhoneNumber == userDto.PhoneNumber))
                {
                    resultDto.Data = false;
                    resultDto.Message = "手机号码已经存在！";
                    resultDto.Success = false;
                    return resultDto;
                }
            }

            User user = await _userManager.FindByIdAsync(userDto.Id);

            this._userManager.SetOrganizationUnits(user, userDto.OrgId);

            await this.UpdateAsync(userDto);

            return resultDto;
        }

        public List<List<long>> GetWorkStationUserRelationIds(EntityDto<long> dto)
        {
            var userRelationIds = new List<List<long>>();
            var data = _workStationUserRelation.GetAll().Where(p => p.UserInfoId == dto.Id).Select(p => p.WorkStationInfoId).ToList();
            var stationInfo = _workStationInfo.GetAll().Where(p => data.Contains(p.Id)).ToList();
            foreach (var item in data)
            {
                List<long> additem = new List<long>();
                var itemData = stationInfo.Where(p => p.Id == item).FirstOrDefault();
                additem.Add(itemData.BelongWorkShopId.GetValueOrDefault());
                additem.Add(itemData.BelongProductLineId.GetValueOrDefault());
                additem.Add(item);
                userRelationIds.Add(additem);
            }
            return userRelationIds;
        }
    }
}


