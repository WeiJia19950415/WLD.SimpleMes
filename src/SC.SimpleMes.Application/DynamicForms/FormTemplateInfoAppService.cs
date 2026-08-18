using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.Linq.Extensions;
using JHT.Abp.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Authorization.Users;
using SC.SimpleMes.DTO;
using SC.SimpleMes.DynamicForms.DomainEvent;
using SC.SimpleMes.DynamicForms.DTO;
using SC.SimpleMes.WorkProcess;
using SC.SimpleMes.WorkProcess.Dto;

namespace SC.SimpleMes.DynamicForms
{
    public class FormTemplateInfoAppService : AsyncCrudAppService<FormTemplateInfo, FormTemplateInfoDto, long, CommonPageRequestDto, FormTemplateInfoDto, FormTemplateInfoDto>
        , IFormTemplateInfoAppService
    {
        private readonly FormTemplateInfoManager _formTemplateInfoManager;
        private readonly UserManager _userManager;
        private readonly IEventBus _eventBus;

        public FormTemplateInfoAppService(
            IRepository<FormTemplateInfo, long> repository,
            UserManager userManager,
            IEventBus eventBus,
            FormTemplateInfoManager formTemplateInfoManager) : base(repository)
        {
            _formTemplateInfoManager = formTemplateInfoManager;
            _userManager = userManager;
            _eventBus = eventBus;
        }

        public override Task<PagedResultDto<FormTemplateInfoDto>> GetAllAsync(CommonPageRequestDto input)
        {
            PagedResultDto<FormTemplateInfoDto> resultDto = new PagedResultDto<FormTemplateInfoDto>();
            var condtion = input.QueryConditionObj as CommonConditionData;
            var query = this.Repository
                .GetAll()
                .Where(p => p.IsCurrent == true)
                .WhereIf(!string.IsNullOrEmpty(condtion.KeyWord), p => p.FormsName.Contains(condtion.KeyWord));
            resultDto.TotalCount = query.Count();
            resultDto.Items = query.Skip(input.SkipCount).Take(input.MaxResultCount).Select(p => new FormTemplateInfoDto()
            {
                CreatorName = p.CreatorName,
                FormsName = p.FormsName,
                IsCurrent = p.IsCurrent,
                Version = p.Version,
                CreationTime = p.CreationTime,
                Id = p.Id,
                TenantId = p.TenantId,
            }).ToList();

            return Task.FromResult(resultDto);
        }

        public List<FormTemplateBasicInfoDto> SearchFromtelateInfoHistory(EntityDto<string> entityDto)
        {
            if (string.IsNullOrEmpty(entityDto.Id))
            {
                return new List<FormTemplateBasicInfoDto>();
            }

            return this.Repository.GetAll()
                .Where(x => x.FormsName == entityDto.Id)
                .OrderByDescending(p => p.IsCurrent).ThenByDescending(p => p.CreationTime).Select(p => new FormTemplateBasicInfoDto()
                {
                    CreatorName = p.CreatorName,
                    FormsName = p.FormsName,
                    Version = p.Version,
                    CreationTime = p.CreationTime,
                    IsCurrent = p.IsCurrent,
                    TenantId = p.TenantId,
                    Id = p.Id
                }).ToList();
        }

        public override async Task<FormTemplateInfoDto> CreateAsync(FormTemplateInfoDto input)
        {
            input.Version = DateTime.Now.ToString("yyyyMMddHHmmss");
            input.TenantId = AbpSession.TenantId;
            input.Id = 0;
            input.CreatorName = (await _userManager.FindByIdAsync(AbpSession.UserId.GetValueOrDefault())).Name;

            if (Repository.GetAll().Any(p => p.FormsName == input.FormsName) == false)
            {
                input.IsCurrent=true;
            }

            input.Id = await Repository.InsertAndGetIdAsync(ObjectMapper.Map<FormTemplateInfo>(input));

            return input;
        }

        public override async Task<FormTemplateInfoDto> UpdateAsync(FormTemplateInfoDto input)
        {
            long oldId = input.Id;
            var result = await this.CreateAsync(input);
            if (input.IsCurrent)
            {
                _formTemplateInfoManager.SetCurrent(result.Id, result.FormsName);
                // 修改之前的Templateid的关联表
                _eventBus.Trigger<FormTemplateInfoUpdateEvent>(new FormTemplateInfoUpdateEvent()
                {
                    NewFormTemplateId = result.Id,
                    OldFormTemplateId = oldId,
                });
            }
            return result;
        }

        public JHTAjaxResponse<FormInfoRecordDto> LoadFormInfoRecordInfo(InputOperatorRecordInfo inputOperatorRecordInfo, FormUseTypeEnum formUseType = FormUseTypeEnum.标准工序填报)
        {
            JHTAjaxResponse<FormInfoRecordDto> ajaxResponse = new JHTAjaxResponse<FormInfoRecordDto>();
            ajaxResponse.Data = ObjectMapper.Map<FormInfoRecordDto>(_formTemplateInfoManager.LoadFormInfoRecord(inputOperatorRecordInfo.WorkProcessId, inputOperatorRecordInfo.OperatroMaterilBatchNumber, formUseType));
            if (ajaxResponse.Data == null)
            {
                ajaxResponse.Data = new FormInfoRecordDto();
            }
            ajaxResponse.Data.FormTemplateInfo = ObjectMapper.Map<FormTemplateInfoDto>(_formTemplateInfoManager.GetFormTemplateInfoByProcessId(inputOperatorRecordInfo.WorkProcessId));
            return ajaxResponse;
        }
    }
}
