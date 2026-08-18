using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JHT.Abp.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.DTO;
using SC.SimpleMes.Users.Dto;
using SC.SimpleMes.WorkProcess.Dto;
using SC.SimpleMes.WorkStation.Dto;

namespace SC.SimpleMes.WorkStation
{
    /// <summary>
    /// 工厂建模服务
    /// </summary>
    public interface IWorkStationAppService : IAsyncCrudAppService<WorkStationInfoDto, long, CommonPageRequestDto, CreateUpdateWorkStationInfoDto, CreateUpdateWorkStationInfoDto>
    {
        /// <summary>
        /// 绑定员工与工序关系
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> BingUserAndWorkStationAsync(TransferDto dto);
        JHTAjaxResponse<List<WorkStationInfoDto>> GetUserWorkStations();

        /// <summary>
        /// 获取所有用户信息与工序绑定的用户  --用于前端Transfer组件
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        TransferDto GetUserListAndBingUser(EntityDto dto);
        JHTAjaxResponse<List<WorkProcessInfoDto>> GetWorkStationsProcess(EntityDto<long> entityDto,bool includeDisaled=true);

        /// <summary>
        /// 根据工位ID获取绑定的用户信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        List<UserDto> GetWorkStationsUsers(EntityDto<long> dto, long? depId = null);

        /// <summary>
        /// 获取车间，产线，工位的级联信息
        /// </summary>
        /// <returns></returns>
        List<UICascaderModel<WorkStationInfoDto,long>> GetWorkStationsInCascader();
        List<UserDto> LoadWorkStationManageUser(long currentWorkStaionId, long? depId = null);


        JHTAjaxResponse<List<WorkProcessInfoDto>> GetWorkStationsBelongProcessNoQC(EntityDto<long> entityDto);
    }
}
