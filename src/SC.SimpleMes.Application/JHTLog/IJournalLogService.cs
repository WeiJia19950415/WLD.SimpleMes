using SC.SimpleMes.Log.Dto;
using JHT.Abp.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.JHTLog
{
    public interface IJournalLogService
    {

        /// <summary>
        /// 查询审计日志
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        PageData<AuditLogDto> GetAuditLogs(JHTPageAjaxResquest<AuditLogConditionDto> where);
        /// <summary>
        /// 获取审计日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AuditLogDto GetAuditLog(long id);

        /// <summary>
        /// 查询登录日志
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        PageData<UserLoginAttemptDto> GetUserLoginAttempts(JHTPageAjaxResquest<UserLoginAttemptConditionDto> where);
        /// <summary>
        /// 获取登录日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserLoginAttemptDto GetUserLoginAttempt(long id);

        /// <summary>
        /// 查询任务日志
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        PageData<NquartzJobLogDto> GetNquartzJobLogs(JHTPageAjaxResquest<NquartzJobConditionDto> where);

        /// <summary>
        /// 查询某个单一任务精确的执行日志
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        PageData<NquartzJobLogDto> GetNquartzJobConditionPreciseDto(JHTPageAjaxResquest<NquartzJobConditionPreciseDto> where);
        /// <summary>
        /// 定时清理日志
        /// </summary>
        /// <param name="logDays"></param>
        /// <returns></returns>
        Task ClearLog(int logDays);
    }
}

