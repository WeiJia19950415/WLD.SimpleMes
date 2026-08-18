using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Organizations;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Authorization.Users;
using WLD.SimpleMes.Migrations;
using WLD.SimpleMes.Models;
using WLD.SimpleMes.Users.Dto;
using WLD.SimpleMes.WorkProcess.Dto;
using WLD.SimpleMes.WorkStation;
using WLD.SimpleMes.WorkStation.Dto;

namespace WLD.SimpleMes.Controllers
{
    [Route("api/[controller]/[action]")]
    [AbpAuthorize]
    public class PadManageWorkStationController : SimpleMesControllerBase
    {
        private readonly IWorkStationAppService _workStationAppService;
        private readonly UserManager _userManager;
        public PadManageWorkStationController(IWorkStationAppService workStationAppService, UserManager userManager)
        {
            _workStationAppService = workStationAppService;
            _userManager = userManager;
        }

        [HttpPost]
        public JHTAjaxResponse<ManagerWorkStationModel> GetManagerWorkStation()
        {

            JHTAjaxResponse<ManagerWorkStationModel> ajaxResponse = new JHTAjaxResponse<ManagerWorkStationModel>();
            var mangeWorkStation = _workStationAppService.GetUserWorkStations().Data;
            var productLineNames = mangeWorkStation.Select(p => p.ProductLineName).Distinct().ToList();
            List<ProductLineDto> productLineDtos = new List<ProductLineDto>();

            ajaxResponse.Data = new ManagerWorkStationModel()
            {
                ManagedWorkStations = mangeWorkStation,
                ManageProductLineNames = productLineNames
            };
            return ajaxResponse;
        }


        [HttpPost]
        public JHTAjaxResponse<List<WorkProcessInfoDto>> GetManagerWorkProcess([FromBody] EntityDto<long> entityDto)
        {
            return _workStationAppService.GetWorkStationsProcess(entityDto, false);
        }



        [HttpPost]
        public async Task<JHTAjaxResponse<WorkStationInfoDto>> GetWorkStationDetailAsync([FromBody] EntityDto<long> entityDto)
        {
            JHTAjaxResponse<WorkStationInfoDto> ajaxResponse = new JHTAjaxResponse<WorkStationInfoDto>();
            ajaxResponse.Data = ObjectMapper.Map<WorkStationInfoDto>(await _workStationAppService.GetAsync(entityDto));
            return ajaxResponse;
        }

        [HttpPost]
        public JHTAjaxResponse<List<UserDto>> LoadWorkStationManageUser([FromBody] PadManageRequestModel requestModel)
        {
            JHTAjaxResponse<List<UserDto>> ajaxResponse = new JHTAjaxResponse<List<UserDto>>();
            var orgIds = _userManager.GetUserOrganizations(AbpSession.UserId.GetValueOrDefault());
            ajaxResponse.Data = _workStationAppService.LoadWorkStationManageUser(requestModel.CurrentWorkStaionId, orgIds.FirstOrDefault());
            return ajaxResponse;
        }
    }
}
