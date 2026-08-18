using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using JHT.Abp.CommonModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Authorization;
using SC.SimpleMes.Authorization.Users;
using SC.SimpleMes.DTO;
using SC.SimpleMes.Users.Dto;
using SC.SimpleMes.WorkProcess;
using SC.SimpleMes.WorkProcess.Dto;
using SC.SimpleMes.WorkStation.Dto;

namespace SC.SimpleMes.WorkStation
{
    public class WorkStationAppService : AsyncCrudAppService<WorkStationInfo, WorkStationInfoDto, long, CommonPageRequestDto, CreateUpdateWorkStationInfoDto, CreateUpdateWorkStationInfoDto>, IWorkStationAppService
    {
        private readonly WorkStationManager _workStationManager;
        private readonly WorkProcessInfoManager _workProcessManager;
        private readonly IRepository<User, long> _userRep;
        
        public WorkStationAppService(IRepository<WorkStationInfo, long> repository, WorkStationManager workStationManager, WorkProcessInfoManager workProcessManager, IRepository<User, long> userRep) : base(repository)
        {
            _workStationManager = workStationManager;
            _workProcessManager = workProcessManager;
            _userRep = userRep;
        }

        protected override IQueryable<WorkStationInfo> CreateFilteredQuery(CommonPageRequestDto input)
        {
            WorkStationConditionDto conditionDto = input.QueryConditionObj as WorkStationConditionDto;
            var query =
                 this.Repository.GetAll().Include(p => p.BelongWorkShop)
                 .Include(p => p.BelongProductLine)
                 .WhereIf(!string.IsNullOrEmpty(conditionDto.KeyWord), p => p.WorkStationName.Contains(conditionDto.KeyWord) || p.WorkStationNumber.Contains(conditionDto.KeyWord))
                 .WhereIf(conditionDto.BelongWorkShopId > 0, p => p.BelongWorkShopId == conditionDto.BelongWorkShopId)
                 .WhereIf(conditionDto.BelongProductLineId > 0, p => p.BelongProductLineId == conditionDto.BelongProductLineId)
                 ;

            return query;
        }

        public override async Task<WorkStationInfoDto> GetAsync(EntityDto<long> input)
        {
            var reuslt = await this.Repository.GetAllIncluding(p => p.BelongProductLine, prop => prop.BelongWorkShop).FirstOrDefaultAsync(p => p.Id == input.Id);
            return ObjectMapper.Map<WorkStationInfoDto>(reuslt);
        }

        public TransferDto GetUserListAndBingUser(EntityDto dto)
        {
            TransferDto ret = new TransferDto();
            var userList = _userRep.GetAll().ToList();
            List<TransferItemDto> items = ObjectMapper.Map<List<TransferItemDto>>(userList);
            ret.Id = dto.Id;
            ret.allList = items;
            ret.selectList = _workStationManager.GetManagedWorkStationUserIds(dto.Id);
            return ret;
        }

        [AbpAuthorize(PermissionNames.Page_CofingWorkStationUser)]
        public async Task<bool> BingUserAndWorkStationAsync(TransferDto dto)
        {
            List<WorkStationUserRelation> newdata = new List<WorkStationUserRelation>();
            foreach (var item in dto.selectList)
            {
                newdata.Add(new WorkStationUserRelation()
                {
                    WorkStationInfoId = dto.Id,
                    UserInfoId = item
                });
            }

            await _workStationManager.BingUserAndWorkStationAsync(newdata, dto.Id);
            return true;
        }

        [AbpAuthorize(PermissionNames.Page_WorkStationManage)]
        public override Task<WorkStationInfoDto> CreateAsync(CreateUpdateWorkStationInfoDto input)
        {
            if (_workStationManager.IsUniqueWorkStationNumber(input.WorkStationNumber) == false)
            {
                throw new UserFriendlyException("该工位编号已被使用");
            }

            input.TenantId = AbpSession.TenantId;
            return base.CreateAsync(input);
        }


        [AbpAuthorize(PermissionNames.Page_WorkStationManage)]
        public override Task<WorkStationInfoDto> UpdateAsync(CreateUpdateWorkStationInfoDto input)
        {
            if (_workStationManager.IsUniqueWorkStationNumber(input.WorkStationNumber, input.Id) == false)
            {
                throw new UserFriendlyException("该工位编号已被使用");
            }

            input.TenantId = input.TenantId.HasValue ? input.TenantId : AbpSession.TenantId;
            return base.UpdateAsync(input);
        }


        public List<UserDto> GetWorkStationsUsers(EntityDto<long> dto, long? depId = null)
        {
            var users = _workStationManager.GetManagedWorkStationUser(dto.Id, depId);
            return ObjectMapper.Map<List<UserDto>>(users);
        }

        /// <summary>
        /// 工位进行级联展示
        /// </summary>
        /// <returns></returns>
        public List<UICascaderModel<WorkStationInfoDto, long>> GetWorkStationsInCascader()
        {
            List<UICascaderModel<WorkStationInfoDto, long>> result = new List<UICascaderModel<WorkStationInfoDto, long>>();
            var allWorkStationInfos = this.Repository.GetAllIncluding(p => p.BelongProductLine, prop => prop.BelongWorkShop).AsNoTracking().ToList();
            var workShopInfos = allWorkStationInfos.Select(p => p.BelongWorkShop).ToList().GroupBy(p => p.Id);
            var productLines = allWorkStationInfos.Select(p => p.BelongProductLine).ToList().GroupBy(p => p.Id);
            foreach (var item in workShopInfos)
            {
                var workShops = item.FirstOrDefault();
                result.Add(new UICascaderModel<WorkStationInfoDto, long>()
                {
                    Value = workShops.Id,
                    Leaf = false,
                    Label = workShops.WorkShopName,
                    Children = BuildingProductLineChildren(item.Key, productLines, allWorkStationInfos),
                });
            }

            return result;
        }

        private List<UICascaderModel<WorkStationInfoDto, long>> BuildingProductLineChildren(long workShopId, IEnumerable<IGrouping<long, ProductLine>> productLines, List<WorkStationInfo> allWorkStationInfos)
        {
            List<UICascaderModel<WorkStationInfoDto, long>> result = new List<UICascaderModel<WorkStationInfoDto, long>>();
            foreach (var item in productLines)
            {
                var productLine = item.FirstOrDefault();
                if (productLine.BelongWorkShopId == workShopId)
                {
                    result.Add(new UICascaderModel<WorkStationInfoDto, long>()
                    {
                        Value = productLine.Id,
                        Leaf = false,
                        Label = productLine.ProductLineName,
                        Children = BuildingWorkStationChildren(item.Key, allWorkStationInfos),
                    });
                }
            }

            return result;
        }

        private List<UICascaderModel<WorkStationInfoDto, long>> BuildingWorkStationChildren(long key, List<WorkStationInfo> allWorkStationInfos)
        {
            List<UICascaderModel<WorkStationInfoDto, long>> result = new List<UICascaderModel<WorkStationInfoDto, long>>();
            foreach (var item in allWorkStationInfos)
            {
                if (item.BelongProductLineId == key)
                {
                    result.Add(new UICascaderModel<WorkStationInfoDto, long>()
                    {
                        Value = item.Id,
                        Leaf = true,
                        Label = item.WorkStationName,
                    });
                }
            }

            return result;
        }

        public JHTAjaxResponse<List<WorkStationInfoDto>> GetUserWorkStations()
        {
            JHTAjaxResponse<List<WorkStationInfoDto>> ajaxResponse = new JHTAjaxResponse<List<WorkStationInfoDto>>();
            ajaxResponse.Data = ObjectMapper.Map<List<WorkStationInfoDto>>(_workStationManager.GetManagedWorkStation(AbpSession.UserId.GetValueOrDefault()));
            return ajaxResponse;
        }

        public JHTAjaxResponse<List<WorkProcessInfoDto>> GetWorkStationsProcess(EntityDto<long> entityDto, bool includeDisaled = true)
        {
            JHTAjaxResponse<List<WorkProcessInfoDto>> response = new JHTAjaxResponse<List<WorkProcessInfoDto>>();
            if (_workStationManager.IsMangerWorkStation(AbpSession.UserId.GetValueOrDefault(), entityDto.Id))
            {
                var managedWorkProcess = _workProcessManager.LoadWorkProcessInfoByStationId(entityDto.Id);
                if (!PermissionChecker.IsGranted(PermissionNames.Page_QualityManager_QC))
                {
                    managedWorkProcess = managedWorkProcess.Where(p => p.WorkProcessType != WorkProcessTypeEnum.FQC).ToList();
                }

                if (includeDisaled == false)
                {
                    managedWorkProcess = managedWorkProcess.Where(p => p.IsEnable == true).ToList();
                }

                response.Data = ObjectMapper.Map<List<WorkProcessInfoDto>>(managedWorkProcess.OrderBy(p => p.ProcessNumber)).ToList();
            }

            return response;
        }

        public JHTAjaxResponse<List<WorkProcessInfoDto>> GetWorkStationsBelongProcessNoQC(EntityDto<long> entityDto)
        {
            JHTAjaxResponse<List<WorkProcessInfoDto>> response = new JHTAjaxResponse<List<WorkProcessInfoDto>>();
            var managedWorkProcess = _workProcessManager.LoadWorkProcessInfoByStationId(entityDto.Id);
            managedWorkProcess = managedWorkProcess.Where(p => p.WorkProcessType != WorkProcessTypeEnum.FQC).ToList();
            response.Data = ObjectMapper.Map<List<WorkProcessInfoDto>>(managedWorkProcess.OrderBy(p => p.ProcessNumber)).ToList();
            return response;
        }

        public List<UserDto> LoadWorkStationManageUser(long currentWorkStaionId, long? depId = null)
        {
            return ObjectMapper.Map<List<UserDto>>(this._workStationManager.GetManagedWorkStationUser(currentWorkStaionId, depId));
        }
    }
}
