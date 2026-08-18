using System.Collections.Generic;
using System.Threading.Tasks;
using SC.SimpleMes.Configuration.Dto;

namespace SC.SimpleMes.Configuration
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

