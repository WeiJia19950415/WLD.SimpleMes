using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.Configuration;
using WLD.SimpleMes.Authorization;
using WLD.SimpleMes.Models.SettingModel;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Configuration.Dto;
using WLD.SimpleMes.Configuration;
using Abp.Json;
using Abp.Domain.Entities;
using Abp.Application.Services.Dto;

namespace WLD.SimpleMes.Controllers
{
    /// <summary>
    ///  配置管理
    /// </summary>
    [Authorize]
    [Route("api/[controller]/[action]")]

    public class SettingController : SimpleMesControllerBase
    {
        private ISettingDefinitionManager _settingDefinitionManager;

        public SettingController(ISettingDefinitionManager settingDefinitionManager)
        {
            _settingDefinitionManager = settingDefinitionManager;
        }

        [HttpPost]
        [DisableAuditing]
        public JHTPageAjaxRespone<PageData<ISettingValue>> GetSettings([FromBody] JHTPageAjaxResquest<SettingConditionDto> where)
        {

            List<ISettingValue> result = new List<ISettingValue>();

            if (AbpSession.TenantId > 0)
            {
                result = where.Condition.ShowDefault == 1 ? SettingManager.GetAllSettingValues(SettingScopes.Tenant).ToList() : SettingManager.GetAllSettingValuesForTenant(AbpSession.TenantId.GetValueOrDefault()).ToList();
                if (where.Condition.ShowDefault == 1)
                {
                    List<ISettingValue> resultValues = new List<ISettingValue>();
                    var all = _settingDefinitionManager.GetAllSettingDefinitions();
                    all = all.Where(p => p.Scopes.HasFlag(SettingScopes.Tenant)).ToList();
                    foreach (var item in result)
                    {
                        if (all.Any(p => p.Name == item.Name))
                        {
                            resultValues.Add(item);
                        }
                    }

                    result = resultValues;
                }
            }
            else
            {
                result = SettingManager.GetAllSettingValues(SettingScopes.Application).ToList();
            }

            if (!string.IsNullOrEmpty(where.Condition.KeyWord))
            {
                result = result.Where(p => p.Name.IndexOf(where.Condition.KeyWord) >= 0).ToList();
            }

            PageData<ISettingValue> page = new PageData<ISettingValue>() { List = result.Skip(where.SkipCount).Take(where.PageSize).ToList(), Total = result.Count };

            return new JHTPageAjaxRespone<PageData<ISettingValue>>()
            {
                Data = page
            };

        }

        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Pages_Setting)]
        public async Task<JHTAjaxResponse> UpdateSettingAsync([FromBody] SettingValueDto settingValueDto)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();

            var settingDefinition = _settingDefinitionManager.GetSettingDefinition(settingValueDto.Name);
            if (settingDefinition.Scopes == SettingScopes.Application && AbpSession.TenantId > 0)
            {
                ajaxResponse.Code = 500;
                ajaxResponse.Msg = "租户不能修改Host用户设置";
                return ajaxResponse;
            }

            TimeSpan timeRslt = new TimeSpan();
            DateTime dateTime = DateTime.Now;
            switch (settingDefinition.CustomData)
            {
                case "Time":
                    if (TimeSpan.TryParse(settingValueDto.Value, out timeRslt) == false)
                    {
                        ajaxResponse.Code = 500;
                        ajaxResponse.Msg = "请输入有效的时间类型";
                        return ajaxResponse;
                    }
                    break;
                case "TimeArray":
                    var timeZone = settingValueDto.Value.Split(",");
                    foreach (var item in timeZone)
                    {
                        if (TimeSpan.TryParse(item, out timeRslt) == false)
                        {
                            ajaxResponse.Code = 500;
                            ajaxResponse.Msg = "请输入有效的时间类型";
                            return ajaxResponse;
                        }
                    }
                    break;
                case "DateRange":
                    var dateZone = settingValueDto.Value.Split("-");
                    foreach (var item in dateZone)
                    {
                        if (DateTime.TryParse(item, out dateTime) == false)
                        {
                            ajaxResponse.Code = 500;
                            ajaxResponse.Msg = "请输入有效的日期";
                            return ajaxResponse;
                        }
                    }
                    break;
            }

            if (AbpSession.TenantId > 0)
            {
                await SettingManager.ChangeSettingForTenantAsync(AbpSession.TenantId.GetValueOrDefault(), settingValueDto.Name, settingValueDto.Value);
            }
            else
            {
                await SettingManager.ChangeSettingForApplicationAsync(settingValueDto.Name, settingValueDto.Value);
            }

            return ajaxResponse;
        }

        /// <summary>
        /// 加载班次配置信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTAjaxResponse<List<ShiftInfoDto>>> LoadShiftSettingAsync()
        {
            JHTAjaxResponse<List<ShiftInfoDto>> ajaxResponse = new JHTAjaxResponse<List<ShiftInfoDto>>();
            var shiftInfo = await SettingManager.GetSettingValueAsync(AppSettingNames.ShiftInfo);
            ajaxResponse.Data = shiftInfo.FromJsonString<List<ShiftInfoDto>>();
            return ajaxResponse;
        }

        /// <summary>
        /// 更新班次配置信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Pages_Setting)]
        public async Task<JHTAjaxResponse> UpdateShiftSettingAsync([FromBody] List<ShiftInfoDto> shiftInfoDtos)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.TenantId.GetValueOrDefault(), AppSettingNames.ShiftInfo, shiftInfoDtos.ToJsonString());
            ajaxResponse.Msg = "班次信息修改成功";
            return ajaxResponse;
        }


        /// <summary>
        /// 更新电堆报表信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        [AbpMvcAuthorize(PermissionNames.Pages_Setting)]
        public async Task<JHTAjaxResponse<List<DDTestReportDto>>> LoadDDTestReportConfigAsync()
        {
            JHTAjaxResponse<List<DDTestReportDto>> ajaxResponse = new JHTAjaxResponse<List<DDTestReportDto>>();
            var shiftInfo = await SettingManager.GetSettingValueAsync(AppSettingNames.DDTestReportConfig);
            ajaxResponse.Data = shiftInfo.FromJsonString<List<DDTestReportDto>>();
            return ajaxResponse;
        }

        /// <summary>
        /// 更新电堆报告配置信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Pages_Setting)]
        public async Task<JHTAjaxResponse> UpdateSDDTestReportConfigAsync([FromBody] List<DDTestReportDto> DDTestReportDto)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.TenantId.GetValueOrDefault(), AppSettingNames.DDTestReportConfig, DDTestReportDto.ToJsonString());
            ajaxResponse.Msg = "电堆测试报告信息更新完成";
            return ajaxResponse;
        }

        /// <summary>
        /// 更新电堆报表信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        [AbpMvcAuthorize(PermissionNames.Pages_Setting)]
        public async Task<JHTAjaxResponse<List<DDPrintFixedInfoDto>>> LoadDDPrintFixedInfosAsync()
        {
            JHTAjaxResponse<List<DDPrintFixedInfoDto>> ajaxResponse = new JHTAjaxResponse<List<DDPrintFixedInfoDto>>();
            var shiftInfo = await SettingManager.GetSettingValueAsync(AppSettingNames.DDPrintFixedInfos);
            ajaxResponse.Data = shiftInfo.FromJsonString<List<DDPrintFixedInfoDto>>();
            return ajaxResponse;
        }

        [HttpPost]
        [DisableAuditing]
        public async Task<JHTAjaxResponse<DDPrintFixedInfoDto>> LoadDDPrintFixedInfosByMaterilNumberAsync([FromBody] EntityDto<string> materilNumber)
        {
            JHTAjaxResponse<DDPrintFixedInfoDto> ajaxResponse = new JHTAjaxResponse<DDPrintFixedInfoDto>();
            var shiftInfo = await SettingManager.GetSettingValueAsync(AppSettingNames.DDPrintFixedInfos);
            ajaxResponse.Data = shiftInfo.FromJsonString<List<DDPrintFixedInfoDto>>().FirstOrDefault(p => materilNumber.Id.StartsWith(p.MaterialNumberCategory));
            return ajaxResponse;
        }


        /// <summary>
        /// 更新电堆报告配置信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Pages_Setting)]
        public async Task<JHTAjaxResponse> UpdatePrintFixedInfosAsync([FromBody] List<DDPrintFixedInfoDto> DDTestReportDto)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.TenantId.GetValueOrDefault(), AppSettingNames.DDPrintFixedInfos, DDTestReportDto.ToJsonString());
            ajaxResponse.Msg = "电堆打印固定参数信息更新完成";
            return ajaxResponse;
        }
    }
}

