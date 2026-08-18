using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Features;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using Abp.UI;
using Abp.UI.Inputs;
using JHT.Abp.CommonModels;
using SC.SimpleMes.AttachFile;
using SC.SimpleMes.Authorization;
using SC.SimpleMes.Authorization.Roles;
using SC.SimpleMes.Authorization.Users;
using SC.SimpleMes.Editions;
using SC.SimpleMes.MultiTenancy.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using JHT.CommonUtity;

namespace SC.SimpleMes.MultiTenancy
{
    [AbpAuthorize(PermissionNames.Pages_Tenants)]
    public class TenantAppService : AsyncCrudAppService<Tenant, TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>, ITenantAppService
    {
        private readonly TenantManager _tenantManager;
        private readonly EditionManager _editionManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly FileSaveOptions _fileSaveOptions;
        private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;
        private readonly IRepository<Tenant, int> _repository;
        private readonly IFeatureManager _featureManager;
        private readonly ILocalizationManager _localizationManager;

        public TenantAppService(
            IRepository<Tenant, int> repository,
            TenantManager tenantManager,
            EditionManager editionManager,
            IOptionsMonitor<FileSaveOptions> fileSaveOptions,
            UserManager userManager,
            RoleManager roleManager,
            IAbpZeroDbMigrator abpZeroDbMigrator,
            ILocalizationManager localizationManager,
            IFeatureManager featureManager)
            : base(repository)
        {
            _repository = repository;
            _tenantManager = tenantManager;
            _editionManager = editionManager;
            _fileSaveOptions = fileSaveOptions.CurrentValue;
            _userManager = userManager;
            _roleManager = roleManager;
            _abpZeroDbMigrator = abpZeroDbMigrator;
            _featureManager = featureManager;
            _localizationManager = localizationManager;

        }

        public override async Task<TenantDto> CreateAsync(CreateTenantDto input)
        {
            // CheckCreatePermission();

            // Create tenant
            var tenant = ObjectMapper.Map<Tenant>(input);
            //tenant.ConnectionString = input.ConnectionString.IsNullOrEmpty()
            //    ? null
            //    : SimpleStringCipher.Instance.Encrypt(input.ConnectionString);

            var defaultEdition = await _editionManager.FindByNameAsync(EditionManager.DefaultEditionName);
            if (defaultEdition != null)
            {
                tenant.EditionId = defaultEdition.Id;
            }

            await _tenantManager.CreateAsync(tenant);
            await CurrentUnitOfWork.SaveChangesAsync(); // To get new tenant's id.

            // Create tenant database
            _abpZeroDbMigrator.CreateOrMigrateForTenant(tenant);

            // We are working entities of new tenant, so changing tenant filter
            using (CurrentUnitOfWork.SetTenantId(tenant.Id))
            {
                // Create static roles for new tenant
                CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));

                await CurrentUnitOfWork.SaveChangesAsync(); // To get static role ids

                // Grant all permissions to admin role
                var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                await _roleManager.GrantAllPermissionsAsync(adminRole);

                // Create admin user for the tenant
                var adminUser = User.CreateTenantAdminUser(tenant.Id, input.ContactEmail, input.ContactPhone, input.ContactName);
                await _userManager.InitializeOptionsAsync(tenant.Id);
                CheckErrors(await _userManager.CreateAsync(adminUser, User.DefaultPassword));
                await CurrentUnitOfWork.SaveChangesAsync(); // To get admin user's id

                // Assign admin user to role!
                CheckErrors(await _userManager.AddToRoleAsync(adminUser, adminRole.Name));
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            return MapToEntityDto(tenant);
        }

        protected override IQueryable<Tenant> CreateFilteredQuery(PagedTenantResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.TenancyName.Contains(input.Keyword) || x.Name.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

        protected override void MapToEntity(TenantDto updateInput, Tenant entity)
        {
            // Manually mapped since TenantDto contains non-editable properties too.
            entity.Name = updateInput.Name;
            entity.TenancyName = updateInput.TenancyName;
            entity.IsActive = updateInput.IsActive;
            entity.Address = updateInput.Address;
            entity.AreaCode = updateInput.AreaCode;
            entity.BriefIntroduction = updateInput.BriefIntroduction;
            entity.ContactEmail = updateInput.ContactEmail;
            entity.ContactName = updateInput.ContactName;
            entity.ContactPhone = updateInput.ContactPhone;
            entity.TenantScale = updateInput.TenantScale;
            entity.LogoImage = updateInput.LogoImage;
        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();

            var tenant = await _tenantManager.GetByIdAsync(input.Id);
            await _tenantManager.DeleteAsync(tenant);
        }

        private void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public PageData<TenantDto> SearchTenant(JHTPageAjaxResquest<TenantConditionDto> condition)
        {
            var input = condition.Condition;
            var req = Repository.GetAll()
              .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.TenancyName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.UniformSocialCreditCode.Contains(input.Keyword))
              .WhereIf(!input.AreaCode.IsNullOrEmpty(), p => p.AreaCode.Contains(input.AreaCode))
              .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
            var list = ObjectMapper.ProjectTo<TenantDto>(req.OrderBy(t => t.Name).PageBy(condition.SkipCount, condition.PageSize)).ToList();
            var result = new PageData<TenantDto>()
            {
                List = list,
                Total = req.Count()
            };
            return result;
        }
        [AbpAllowAnonymous]
        public async Task<string> SaveHeadImageAsync(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            var fileRenameName = Guid.NewGuid().ToString(); //Path.GetRandomFileName();
            var path = string.Format("{0}/{1}", _fileSaveOptions.TenantImageSavePath, fileRenameName + fileExtension);
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
        [AbpAllowAnonymous]
        public TenantDto GetOwnTenant(int teantId)
        {
            teantId = AbpSession.TenantId != null ? AbpSession.TenantId.GetValueOrDefault() : teantId;
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            {
                return ObjectMapper.Map<TenantDto>(_repository.FirstOrDefault(p => p.Id == teantId));
            }

        }
        [AbpAllowAnonymous]
        public async Task<TenantDto> UpdateOwnTenant(TenantDto dto)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            {
                var update = ObjectMapper.Map<Tenant>(dto);
                update.Id = AbpSession.TenantId.GetValueOrDefault();
                return ObjectMapper.Map<TenantDto>(await _repository.UpdateAsync(update));
            }
        }

        public async Task UpdateActive(EntityDto<int> dto)
        {
            var data = await _repository.GetAsync(dto.Id);
            data.IsActive = !data.IsActive;
            await _repository.UpdateAsync(data);
        }

        public List<FlatFeatureDto> GetFlatFeatureDtos(int tenandId)
        {
            var featureList = _featureManager.GetAll();
            List<FlatFeatureDto> ret = new List<FlatFeatureDto>();
            var tent = _tenantManager.GetFeatureValues(tenandId);
            foreach (var item in featureList)
            {
                // string item.DisplayName.Localize();
                var m = tent.FirstOrDefault(t => t.Name == item.Name);
                if (m != null)
                {
                    item.DefaultValue = m.Value;
                    var data = ObjectMapper.Map<FlatFeatureDto>(item);
                    data.DisplayName = _localizationManager.GetString(item.DisplayName as LocalizableString);
                    data.Vlaue = item.DefaultValue;
                    data.TenandId = tenandId;
                    ret.Add(data);
                }
            }
            return ret;
        }

        public bool SaveFeature(SaveFlatFeatureDto dto)
        {
            _tenantManager.SetFeatureValue(dto.TenandId, dto.Name, dto.Value);
            return true;
        }


        private bool VerifyFeature(Feature feature, NameValue name)
        {


            if (feature.InputType is ComboboxInputType)
            {
                return true;
            }
            if (feature.InputType is CheckboxInputType)
            {
                bool isbool;


                if (bool.TryParse(name.Value, out isbool))
                {
                    if (isbool)
                        return true;
                }
                return false;
            }
            if (feature.InputType is SingleLineStringInputType)
            {
                DateTime date;


                if (DateTime.TryParse(name.Value, out date))
                {
                    if (date > DateTime.Now)
                        return true;
                }
                int i;
                if (int.TryParse(name.Value, out i))
                {
                    if (i > 0)
                        return true;
                }
                return false;
            }
            return false;
        }

    }
}


