using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.Authorization;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Authorization;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.OnLineStock;
using WLD.SimpleMes.OnLineStock.Dto;

namespace WLD.SimpleMes.Controllers.OnLineStock
{
    [Route("api/[controller]/[action]")]
    [AbpAuthorize]
    [ApiController]
    public class OnLineStockController : SimpleMesControllerBase
    {
        private readonly ILineSideMaterialInfoAppService _ilineSideMaterialInfoAppService;

        public OnLineStockController(ILineSideMaterialInfoAppService ilineSideMaterialInfoAppService)
        {
            _ilineSideMaterialInfoAppService = ilineSideMaterialInfoAppService;
        }


        #region 半成品信息维护
        /// <summary>
        /// 创建半成品信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.OnlineStock_MaterialInfoManager)]
        public async Task<JHTAjaxResponse<LineSideMaterialInfoDto>> CreateSideMaterial([FromBody] LineSideMaterialInfoDto input)
        {
            JHTAjaxResponse<LineSideMaterialInfoDto> ret = new JHTAjaxResponse<LineSideMaterialInfoDto>();
            ret.Data = await _ilineSideMaterialInfoAppService.CreateAsync(input);
            return ret;
        }

        /// <summary>
        /// 修改半成品信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.OnlineStock_MaterialInfoManager)]
        public async Task<JHTAjaxResponse<LineSideMaterialInfoDto>> UpdateSideMaterial([FromBody] LineSideMaterialInfoDto input)
        {
            JHTAjaxResponse<LineSideMaterialInfoDto> ret = new JHTAjaxResponse<LineSideMaterialInfoDto>();
            ret.Data = await _ilineSideMaterialInfoAppService.UpdateAsync(input);
            return ret;
        }

        /// <summary>
        /// 禁用半成品信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.OnlineStock_MaterialInfoManager)]
        public async Task<JHTAjaxResponse> DeleteSideMaterial([FromBody] EntityDto<long> input)
        {
            JHTAjaxResponse ret = new JHTAjaxResponse();
            await _ilineSideMaterialInfoAppService.DeleteAsync(input);
            return ret;
        }

        /// <summary>
        /// 获取半成品信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<PageData<LineSideMaterialInfoDto>>> GetMaterialInfo([FromBody] JHTPageAjaxResquest<CommonConditionData> where)
        {
            var result = await _ilineSideMaterialInfoAppService.GetAllAsync(new DTO.CommonPageRequestDto()
            {
                KeyWord = where.Condition.KeyWord,
                MaxResultCount = where.PageSize,
                SkipCount = where.SkipCount,
            });

            return new JHTPageAjaxRespone<PageData<LineSideMaterialInfoDto>>()
            {
                Data = new PageData<LineSideMaterialInfoDto>()
                {
                    List = result.Items.ToList(),
                    Total = result.TotalCount,
                }
            };
        }
        #endregion

        #region 半成品业务操作


        /// <summary>
        /// 增加线边库操作记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.OnlineStock_StockManageRecord)]
        public async Task<JHTAjaxResponse<bool>> AddOperatorRecord(LineSideMaterialOperatorRecordDto record)
        {
            JHTAjaxResponse<bool> ret = new JHTAjaxResponse<bool>();
            ret.Data = await _ilineSideMaterialInfoAppService.AddOperatorRecord(record);
            return ret;
        }

        /// <summary>
        /// 分页获取线边库操作记录
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<PageData<View_LineSideMaterialOperatorRecordDto>>> SearchOperatorRecordInfo([FromBody] JHTPageAjaxResquest<SearchOperatorRecordWhereDto> where)
        {
            JHTAjaxResponse<PageData<View_LineSideMaterialOperatorRecordDto>> ret = new JHTAjaxResponse<PageData<View_LineSideMaterialOperatorRecordDto>>();
            where.Condition.ParseTime();
            ret.Data = await _ilineSideMaterialInfoAppService.SearchOperatorRecordInfo(where);
            return ret;
        }


        /// <summary>
        /// 线边库即时库存查询
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<PageData<RealInventory>> SearchRealInventories([FromBody] JHTPageAjaxResquest<CommonConditionData> input)
        {
            JHTAjaxResponse<PageData<RealInventory>> ret = new JHTAjaxResponse<PageData<RealInventory>>();
            ret.Data = _ilineSideMaterialInfoAppService.SearchRealInventories(input);
            return ret;
        }


        /// <summary>
        /// 线边库日期 产量/消耗查询
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<List<LineSideMaterialStatisticsDto>> SearchOperatorRecordStatistics([FromBody] JHTPageAjaxResquest<LineSideMaterialStatisticsWhereDto> input)
        {
            JHTAjaxResponse<List<LineSideMaterialStatisticsDto>> ret = new JHTAjaxResponse<List<LineSideMaterialStatisticsDto>>();
            ret.Data = _ilineSideMaterialInfoAppService.SearchOperatorRecordStatistics(input).List;
            return ret;
        }
        #endregion

        #region 在制品BOM信息配置

        /// <summary>
        /// 更新在制品BOM信息
        /// </summary>
        /// <param name="bomItems"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.OnlineStock_MaterialInfoManager)]
        public async Task<JHTAjaxResponse<bool>> UpdateLineMaterilInfoBomItemsAsync([FromBody] List<LineSideMaterialInfoBomItemDto> bomItems)
        {
            return new JHTAjaxResponse<bool>()
            {
                Data = await _ilineSideMaterialInfoAppService.UpdateLineMaterilInfoBomItems(bomItems)
            };
        }


        /// <summary>
        /// 获取在制品BOM项信息
        /// </summary>
        /// <param name="bomItems"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize]
        public JHTAjaxResponse<List<LineSideMaterialInfoBomItemDto>> GetLineSideMaterialInfoBomItemDtosByMaterilId([FromBody] EntityDto<long> bomItems)
        {
            return new JHTAjaxResponse<List<LineSideMaterialInfoBomItemDto>>()
            {
                Data = _ilineSideMaterialInfoAppService.GetLineSideMaterialInfoBomItemDtosByMaterilId(bomItems)
            };
        }

        #endregion
    }
}
