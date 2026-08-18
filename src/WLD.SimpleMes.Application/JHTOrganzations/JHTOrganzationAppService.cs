using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Organizations;
using JHT.Abp.CommonModels;
using WLD.SimpleMes.JHTOrganzation;
using WLD.SimpleMes.JHTOrganzations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WLD.SimpleMes.Authorization.Users;
using Abp.Authorization.Users;

namespace WLD.SimpleMes.JHTOrganzations
{
    public class JHTOrganzationAppService : AsyncCrudAppService<JHTOrganzation.JHTOrganzation, JHTOrganzationDto, long, PagedResultRequestDto, JHTOrganzationDto, JHTOrganzationDto>, IJHTOrganzationAppService
    {
        private readonly UserManager _userManager;
        private readonly IRepository<JHTOrganzation.JHTOrganzation, long> _repository;
        private readonly AppOrganizationUnitManager _organizationUnitManager;
        private readonly IRepository<UserOrganizationUnit, long> _userOrgRepostitory;

        public JHTOrganzationAppService(IRepository<JHTOrganzation.JHTOrganzation, long> repository
             , IRepository<UserOrganizationUnit, long> userOrgRepostitory
             , UserManager userManager
             , AppOrganizationUnitManager organizationUnitManager) : base(repository)
        {
            _repository = repository;
            _userOrgRepostitory = userOrgRepostitory;
            _userManager = userManager;
            _organizationUnitManager = organizationUnitManager;

        }

        public override async Task<JHTOrganzationDto> CreateAsync(JHTOrganzationDto input)
        {
            input.Code = _organizationUnitManager.GenerateOrgCode(input.ParentId);
            var model = ObjectMapper.Map<JHTOrganzation.JHTOrganzation>(input);
            model.TenantId = AbpSession.TenantId;
            var modeldto = await _repository.InsertAsync(model);
            return ObjectMapper.Map<JHTOrganzationDto>(modeldto);
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            //await base.DeleteAsync(input);
            var model = _repository.GetAllIncluding(p => p.Children).FirstOrDefault(p => p.Id == input.Id);

            if (model.Children != null && model.Children.Count > 0)
            {
                throw new System.Exception("请先删除该组织的下级组织！");
            }

            var existOrgUser = await _userOrgRepostitory.FirstOrDefaultAsync(p => p.OrganizationUnitId == input.Id);
            if (existOrgUser != null)
            {
                var userName = _userManager.GetUserById(existOrgUser.UserId);
                throw new Exception($"请组织下仍有用户，请先将用户{userName.UserName}移除出组织！");
            }

            if (model.Children != null && model.Children.Count > 0)
            {
                foreach (var item in model.Children)
                {
                    var i = _repository.GetAllIncluding(p => p.Children).FirstOrDefault(p => p.Id == item.Id);
                    if (i.Children != null && i.Children.Count > 0)
                    {
                        await DeleltDepartAsync(i.Children);
                    }
                    await _repository.DeleteAsync(t => t.Id == i.Id);
                }
            }
            await _repository.DeleteAsync(t => t.Id == input.Id);
        }
        private async Task DeleltDepartAsync(ICollection<OrganizationUnit> org)
        {
            foreach (var item in org)
            {
                var i = _repository.GetAllIncluding(p => p.Children).FirstOrDefault(p => p.Id == item.Id);
                if (i.Children != null && i.Children.Count > 0)
                {
                    await DeleltDepartAsync(i.Children);
                }
                await _repository.DeleteAsync(t => t.Id == i.Id);
            }
        }
        public override async Task<JHTOrganzationDto> UpdateAsync(JHTOrganzationDto input)
        {
            //var model = ObjectMapper.Map<JHTOrganzation>(input);

            var model = await _repository.GetAsync(input.Id);
            if (model.ParentId != input.ParentId)
            {
                await _organizationUnitManager.MoveAsync(model.Id, input.ParentId);
            }
            model.DisplayName = input.DisplayName;
            model.ShortName = input.ShortName;
            model.SortNumber = input.SortNumber;
            return input;
        }
        public override async Task<JHTOrganzationDto> GetAsync(EntityDto<long> input)
        {
            var model = await base.GetAsync(input);
            return model;
        }

        public PageData<JHTOrganzationDto> SearchOrganzation(JHTPageAjaxResquest<OrganzationConditionDto> org)
        {
            var req = _repository.GetAll().WhereIf(!string.IsNullOrEmpty(org.Condition.KeyWord), t => t.DisplayName.Contains(org.Condition.KeyWord) || t.ShortName.Contains(org.Condition.KeyWord));
            var result = new PageData<JHTOrganzationDto>()
            {
                Total = req.Count(),
                List = ObjectMapper.ProjectTo<JHTOrganzationDto>(req.OrderBy(t => t.DisplayName).PageBy(org.SkipCount, org.PageSize)).ToList()
            };
            return result;
        }
        public bool MoveOrganizationUnit(MoveOrganzationDto org)
        {
            var data = _organizationUnitManager.MoveOrg(org.NewParentId, org.Id);

            return data;
        }
        public async Task<List<OrganizationUnitTreeDto>> GetOrganizationUnitTreeAsync()
        {
            var orgList = await _organizationUnitManager.FindChildrenAsync(null, true);
            List<OrganizationUnitTreeDto> result = new List<OrganizationUnitTreeDto>();
            foreach (var item in orgList)
            {
                result.Add(new OrganizationUnitTreeDto()
                {
                    Id = item.Id,
                    ShortName = item.ShortName,
                    DisplayName = item.DisplayName,
                    SortNumber = item.SortNumber,
                    IsDisabled = false,
                    IsChecked = false,
                    ChildrenDepart = item.Children != null ? await RecursionOrganizationUnitTreeDtoAsync(item.Children.ToList()) : null
                });
            }
            return result;
        }
        public async Task<List<OrganizationUnitTreeDto>> GetOrganizationUnitTreeAsync(long id = 0)
        {
            var orgList = await _organizationUnitManager.FindChildrenAsync(id == 0 ? null : id, false);
            List<OrganizationUnitTreeDto> result = new List<OrganizationUnitTreeDto>();
            foreach (var item in orgList)
            {
                result.Add(new OrganizationUnitTreeDto()
                {
                    Id = item.Id,
                    ShortName = item.ShortName,
                    DisplayName = item.DisplayName,
                    SortNumber = item.SortNumber,
                    IsDisabled = false,
                    IsChecked = false,
                    IsLeaf = _organizationUnitManager.FindChildren(item.Id).Count == 0,
                });
            }
            return result;
        }

        private async Task<List<OrganizationUnitTreeDto>> RecursionOrganizationUnitTreeDtoAsync(List<OrganizationUnit> orgList)
        {
            List<OrganizationUnitTreeDto> returnDto = new List<OrganizationUnitTreeDto>();
            if (orgList != null && orgList.Count > 0)
            {
                foreach (var item in orgList)
                {
                    var model = (JHTOrganzation.JHTOrganzation)item;
                    var childList = await _organizationUnitManager.FindChildrenAsync(model.Id, false);

                    returnDto.Add(new OrganizationUnitTreeDto()
                    {
                        Id = item.Id,
                        ShortName = model.ShortName,
                        DisplayName = model.DisplayName,
                        SortNumber = model.SortNumber,
                        IsDisabled = false,
                        IsChecked = false,
                        ChildrenDepart = childList.Count > 0 ? await RecursionOrganizationUnitTreeDtoAsync(childList.ToList<OrganizationUnit>()) : null
                    });
                }
            }
            return returnDto;
        }
    }
}

