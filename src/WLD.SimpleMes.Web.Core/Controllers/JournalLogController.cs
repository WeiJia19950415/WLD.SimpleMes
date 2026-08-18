using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using WLD.SimpleMes.Authorization;
using WLD.SimpleMes.JHTLog;
using WLD.SimpleMes.Log.Dto;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;

namespace WLD.SimpleMes.Controllers
{
    /// <summary>
    ///  日志管理
    /// </summary>

    [Route("api/[controller]/[action]")]
    [AbpMvcAuthorize(PermissionNames.Pages_JournalLog)]
    [DisableAuditing]
    public class JournalLogController : SimpleMesControllerBase
    {
        private readonly IJournalLogService _abpLogService;

        public JournalLogController(IJournalLogService abpLogService)
        {
            _abpLogService = abpLogService;
        }

        /// <summary>
        /// 审计日志
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTAjaxResponse<AuditLogDto> GetAuditLog([FromBody] EntityDto<long> entityDto)
        {
            return new JHTAjaxResponse<AuditLogDto>() { Data = _abpLogService.GetAuditLog(entityDto.Id) };
        }

        /// <summary>
        /// 审计日志列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTPageAjaxRespone<PageData<AuditLogDto>> GetAuditLogs([FromBody] JHTPageAjaxResquest<AuditLogConditionDto> where)
        {
            return new JHTPageAjaxRespone<PageData<AuditLogDto>>()
            {
                Data = _abpLogService.GetAuditLogs(where)
            };
        }

        /// <summary>
        /// 登录日志
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTAjaxResponse<UserLoginAttemptDto> GetUserLoginAttempt([FromBody] EntityDto<long> entityDto)
        {
            return new JHTAjaxResponse<UserLoginAttemptDto>() { Data = _abpLogService.GetUserLoginAttempt(entityDto.Id) };
        }

        /// <summary>
        /// 登录日志列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTPageAjaxRespone<PageData<UserLoginAttemptDto>> GetUserLoginAttempts([FromBody] JHTPageAjaxResquest<UserLoginAttemptConditionDto> where)
        {
            return new JHTPageAjaxRespone<PageData<UserLoginAttemptDto>>()
            {
                Data = _abpLogService.GetUserLoginAttempts(where)
            };
        }

        /// <summary>
        /// 任务日志列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTPageAjaxRespone<PageData<NquartzJobLogDto>> GetNquartzJobLogs([FromBody] JHTPageAjaxResquest<NquartzJobConditionDto> where)
        {
            return new JHTPageAjaxRespone<PageData<NquartzJobLogDto>>()
            {
                Data = _abpLogService.GetNquartzJobLogs(where)
            };
        }

        /// <summary>
        /// 查询某个单一任务精确的执行日志
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTPageAjaxRespone<PageData<NquartzJobLogDto>> GetNquartzJobConditionPreciseDto([FromBody] JHTPageAjaxResquest<NquartzJobConditionPreciseDto> where)
        {
            return new JHTPageAjaxRespone<PageData<NquartzJobLogDto>>()
            {
                Data = _abpLogService.GetNquartzJobConditionPreciseDto(where)
            };
        }
    }
}

