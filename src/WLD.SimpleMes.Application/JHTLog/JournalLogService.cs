using Abp.Application.Services;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using WLD.SimpleMes.Log;
using WLD.SimpleMes.Log.Dto;
using JHT.Abp.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.JHTLog
{
    [DisableAuditing]
    public class JournalLogService : ApplicationService, IJournalLogService
    {
        private readonly IRepository<NquartzJobLog, long> _joblogRepository;
        private readonly IRepository<UserLoginAttempt, long> _userLoginAttemptRepository;
        private readonly IRepository<JHTAuditLog, long> _auditLogRepository;

        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public JournalLogService(
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository,
            IRepository<JHTAuditLog, long> auditLogRepository,
            IRepository<NquartzJobLog, long> joblogRepository)
        {

            _userLoginAttemptRepository = userLoginAttemptRepository;
            _auditLogRepository = auditLogRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _joblogRepository = joblogRepository;
        }

        public AuditLogDto GetAuditLog(long id)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                return ObjectMapper.Map<AuditLogDto>(_auditLogRepository.Get(id));
            }
        }

        public PageData<AuditLogDto> GetAuditLogs(JHTPageAjaxResquest<AuditLogConditionDto> where)
        {
            PageData<AuditLogDto> pagedResultDto = new PageData<AuditLogDto>();
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var queryResult = _auditLogRepository.GetAll().Where(p => p.ExecutionTime.Date == where.Condition.ExecutionTime.Date)
                .WhereIf(!string.IsNullOrEmpty(where.Condition.KeyWord), p => p.MethodName == where.Condition.KeyWord || p.ServiceName == where.Condition.KeyWord)
                .WhereIf(where.Condition.IsExecution != null, p => (where.Condition.IsExecution.Value == true ? p.Exception != null : p.Exception == null))
                .WhereIf(AbpSession.TenantId.HasValue, p => p.TenantId == AbpSession.TenantId.GetValueOrDefault());
                pagedResultDto.Total = queryResult.Count();
                queryResult = queryResult.OrderByDescending(p => p.ExecutionTime);
                var data = queryResult.PageBy(where.SkipCount, where.PageSize).ToList();
                pagedResultDto.List = ObjectMapper.Map<List<AuditLogDto>>(data);
            }
            return pagedResultDto;
        }

        public UserLoginAttemptDto GetUserLoginAttempt(long id)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                return ObjectMapper.Map<UserLoginAttemptDto>(_userLoginAttemptRepository.Get(id));
            }
        }

        public PageData<UserLoginAttemptDto> GetUserLoginAttempts(JHTPageAjaxResquest<UserLoginAttemptConditionDto> where)
        {
            PageData<UserLoginAttemptDto> pagedResultDto = new PageData<UserLoginAttemptDto>();
            var end = where.Condition.CreationTime.AddDays(1);
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var queryResult = _userLoginAttemptRepository.GetAll()
                    .Where(p => p.CreationTime >= where.Condition.CreationTime && p.CreationTime < end)
                    .WhereIf(AbpSession.TenantId != null, p => p.TenantId == AbpSession.TenantId.GetValueOrDefault())
                    .WhereIf(!string.IsNullOrEmpty(where.Condition.KeyWord), p => p.UserNameOrEmailAddress == where.Condition.KeyWord);
                pagedResultDto.Total = queryResult.Count();
                queryResult = queryResult.OrderByDescending(p => p.CreationTime);
                var data = queryResult.PageBy(where.SkipCount, where.PageSize).ToList();
                pagedResultDto.List = ObjectMapper.Map<List<UserLoginAttemptDto>>(data);
            }
            return pagedResultDto;

        }
        public PageData<NquartzJobLogDto> GetNquartzJobLogs(JHTPageAjaxResquest<NquartzJobConditionDto> where)
        {
            PageData<NquartzJobLogDto> pagedResultDto = new PageData<NquartzJobLogDto>();
            var end = where.Condition.ExcuteDate.AddDays(1);
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var queryResult = _joblogRepository.GetAll()
                    .Where(p => p.BeginExcuteTime >= where.Condition.ExcuteDate && p.BeginExcuteTime < end)
                    .WhereIf(where.Condition.JobResult != null, p => p.JobResult == where.Condition.JobResult)
                    .WhereIf(!string.IsNullOrEmpty(where.Condition.KeyWord), p => p.JobGroup.Contains(where.Condition.KeyWord) || p.JobName.Contains(where.Condition.KeyWord));
                pagedResultDto.Total = queryResult.Count();
                queryResult = queryResult.OrderByDescending(p => p.BeginExcuteTime);
                var data = queryResult.PageBy(where.SkipCount, where.PageSize).ToList();
                pagedResultDto.List = ObjectMapper.Map<List<NquartzJobLogDto>>(data);
            }
            return pagedResultDto;
        }

        public PageData<NquartzJobLogDto> GetNquartzJobConditionPreciseDto(JHTPageAjaxResquest<NquartzJobConditionPreciseDto> where)
        {
            PageData<NquartzJobLogDto> pagedResultDto = new PageData<NquartzJobLogDto>();
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var queryResult = _joblogRepository.GetAll()
                    .WhereIf(where.Condition.IsExcpetion, p => p.JobResult == JobResultEnum.Fail)
                    .Where(p => p.JobGroup.Equals(where.Condition.JobGroup) && p.JobName.Equals(where.Condition.JobName));
                pagedResultDto.Total = queryResult.Count();
                queryResult = queryResult.OrderByDescending(p => p.BeginExcuteTime);
                var data = queryResult.PageBy(where.SkipCount, where.PageSize).ToList();
                pagedResultDto.List = ObjectMapper.Map<List<NquartzJobLogDto>>(data);
            }
            return pagedResultDto;
        }
        public async Task ClearLog(int logDays)
        {
            var date = DateTime.Now.Date.AddDays(-logDays);
            await _auditLogRepository.DeleteAsync(t => t.ExecutionTime <= date);
            await _joblogRepository.DeleteAsync(t => t.BeginExcuteTime <= date);
        }
    }
}

