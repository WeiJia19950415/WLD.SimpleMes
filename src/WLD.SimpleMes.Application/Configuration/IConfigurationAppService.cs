using System.Collections.Generic;
using System.Threading.Tasks;
using WLD.SimpleMes.Configuration.Dto;

namespace WLD.SimpleMes.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);

        ShiftInfoDto GetCurrentShiftInfo();

        DDTestReportDto GetDDTestReportConfig(string materialNumber);

        DDPrintFixedInfoDto GetDDPrintFixedInfo(string materialNumber);

        List<DDPrintFixedInfoDto> GetDDPrintFixedInfo();
    }
}

