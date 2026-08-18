using Abp.Auditing;
using Abp.Authorization;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.WorkStation;
using WLD.SimpleMes.WorkStation.Dto;

namespace WLD.SimpleMes.Controllers
{
    /// <summary>
    /// 车间信息
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AbpAuthorize]
    public class WorkShopController : SimpleMesControllerBase
    {
        private readonly IWorkShopAppService _workShopInfoAppService;
        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkShopController(IWorkShopAppService workShpAppService)
        {
            _workShopInfoAppService = workShpAppService;
        }

        /// <summary>
        /// 获取车间信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<PageData<WorkShopInfoDto>>> GetWorkShop([FromBody] JHTPageAjaxResquest<CommonConditionData> where)
        {
            var result = await _workShopInfoAppService.GetAllAsync(new DTO.CommonPageRequestDto()
            {
                KeyWord = where.Condition.KeyWord,
                MaxResultCount = where.PageSize,
                SkipCount = where.SkipCount,
            });
            return new JHTPageAjaxRespone<PageData<WorkShopInfoDto>>()
            {
                Data = new PageData<WorkShopInfoDto>()
                {
                    List = result.Items.ToList(),
                    Total = result.TotalCount,
                }
            };
        }

        /// <summary>
        /// 创建车间信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> CreateWorkShopInf([FromBody] WorkShopInfoDto workshopInfo)
        {
            await _workShopInfoAppService.CreateAsync(workshopInfo);
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
        public async Task<JHTAjaxResponse> UpdateWorkShopInf([FromBody] WorkShopInfoDto workshopInfo)
        {
            await _workShopInfoAppService.UpdateAsync(workshopInfo);

            return new JHTAjaxResponse()
            {
                Msg = "修改成功",
            };
        }

    }
}
