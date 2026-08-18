using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Json;
using Abp.Runtime.Session;
using WLD.SimpleMes.Configuration.Dto;

namespace WLD.SimpleMes.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : SimpleMesAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }


        public ShiftInfoDto GetCurrentShiftInfo()
        {
            var shiftInfos = this.SettingManager.GetSettingValue(AppSettingNames.ShiftInfo).FromJsonString<List<ShiftInfoDto>>();
            foreach (var item in shiftInfos)
            {
                TimeSpan nowTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                if (item.IsAcrrossDay)
                {
                    if (nowTime >= item.StartWorkTime || nowTime <= item.OffWorkTime)
                    {
                        return item;
                    }
                }

                if (nowTime >= item.StartWorkTime && nowTime <= item.OffWorkTime)
                {
                    return item;
                }
            }

            throw new Exception("该时间段未配置班次信息！");
        }

        public DDTestReportDto GetDDTestReportConfig(string materialNumber)
        {
            var reportDtoInfos = SettingManager.GetSettingValue(AppSettingNames.DDTestReportConfig).FromJsonString<List<DDTestReportDto>>();
            return reportDtoInfos.FirstOrDefault(p => materialNumber.StartsWith(p.MaterialNumberCategory));
        }

        public List<DDTestReportDto> DDTestReportConfigs()
        {
            return SettingManager.GetSettingValue(AppSettingNames.DDTestReportConfig).FromJsonString<List<DDTestReportDto>>();
        }

        public DDPrintFixedInfoDto GetDDPrintFixedInfo(string materialNumber)
        {
            var reportDtoInfos = SettingManager.GetSettingValue(AppSettingNames.DDPrintFixedInfos).FromJsonString<List<DDPrintFixedInfoDto>>();
            return reportDtoInfos.FirstOrDefault(p => materialNumber.StartsWith(p.MaterialNumberCategory));
        }

        public List<DDPrintFixedInfoDto> GetDDPrintFixedInfo()
        {
            return SettingManager.GetSettingValue(AppSettingNames.DDTestReportConfig).FromJsonString<List<DDPrintFixedInfoDto>>();
        }

    }
}

