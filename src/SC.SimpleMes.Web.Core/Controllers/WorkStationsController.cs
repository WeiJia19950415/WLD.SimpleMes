using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.DTO;
using SC.SimpleMes.WorkProcess.Dto;
using SC.SimpleMes.WorkStation;
using SC.SimpleMes.WorkStation.Dto;

namespace SC.SimpleMes.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AbpAuthorize]
    public class WorkStationsController : SimpleMesControllerBase
    {
        private readonly IWorkStationAppService _workStationAppService;
        public WorkStationsController(IWorkStationAppService workStationAppService)
        {
            _workStationAppService = workStationAppService;
        }

        /// <summary>
        /// 获取工位信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<PageData<WorkStationInfoDto>>> GetWorkStationsAsync([FromBody] JHTPageAjaxResquest<WorkStationConditionDto> where)
        {
            var result = await _workStationAppService.GetAllAsync(new DTO.CommonPageRequestDto()
            {
                QueryConditionObj = where.Condition,
                MaxResultCount = where.PageSize,
                SkipCount = where.SkipCount,
            });

            return new JHTPageAjaxRespone<PageData<WorkStationInfoDto>>()
            {
                Data = new PageData<WorkStationInfoDto>()
                {
                    List = result.Items.ToList(),
                    Total = result.TotalCount,
                }
            };
        }

        /// <summary>
        /// 查询工位员工关系
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTAjaxResponse<TransferDto> GetUserListAndBingUser([FromBody] EntityDto dto)
        {
            return new JHTAjaxResponse<TransferDto>()
            {
                Data = _workStationAppService.GetUserListAndBingUser(dto)
            };
        }

        /// <summary>
        /// 绑定员工-工位关系
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<bool>> BingUserAndWorkStationAsync([FromBody] TransferDto dto)
        {
            return new JHTAjaxResponse<bool>()
            {
                Data = await _workStationAppService.BingUserAndWorkStationAsync(dto)
            };
        }

        /// <summary>
        /// 创建车间信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> CreateWorkStationsAsync([FromBody] CreateUpdateWorkStationInfoDto workshopInfo)
        {
            await _workStationAppService.CreateAsync(workshopInfo);
            return new JHTAjaxResponse()
            {
                Msg = "添加成功",
            };
        }

        /// <summary>
        /// 更新车间信息
        /// </summary>
        /// <param name="workshopInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> UpdateWorkStationsAsync([FromBody] CreateUpdateWorkStationInfoDto workshopInfo)
        {
            await _workStationAppService.UpdateAsync(workshopInfo);

            return new JHTAjaxResponse()
            {
                Msg = "修改成功",
            };
        }

        /// <summary>
        /// 获取车间，产线，工位的级联信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTAjaxResponse<List<UICascaderModel<WorkStationInfoDto, long>>>> GetCascaderAllWorkStationsAsync()
        {
            var workStations = _workStationAppService.GetWorkStationsInCascader();
            List<UICascaderModel<WorkStationInfoDto, long>> result = new List<UICascaderModel<WorkStationInfoDto, long>>();
            return new JHTAjaxResponse<List<UICascaderModel<WorkStationInfoDto, long>>>() { Data = workStations };
        }

        [HttpPost]
        public JHTAjaxResponse<List<WorkProcessInfoDto>> GetWorkStationsBelongProcessNoQC(EntityDto<long> entityDto)
        {
            return _workStationAppService.GetWorkStationsBelongProcessNoQC(entityDto);
        }
    }
}
