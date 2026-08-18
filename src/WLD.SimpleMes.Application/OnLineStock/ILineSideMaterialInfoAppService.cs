using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JHT.Abp.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.OnLineStock.Dto;

namespace WLD.SimpleMes.OnLineStock
{
    public interface ILineSideMaterialInfoAppService: IAsyncCrudAppService<LineSideMaterialInfoDto,long, CommonPageRequestDto, LineSideMaterialInfoDto, LineSideMaterialInfoDto>, IApplicationService
    {
        /// <summary>
        /// 新增操作记录
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<bool> AddOperatorRecord (LineSideMaterialOperatorRecordDto record);

        /// <summary>
        /// 作废记录
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<bool> RemoveOperatorRecord(EntityDto<long> input);

        /// <summary>
        /// 查询记录列表
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        Task<PageData<View_LineSideMaterialOperatorRecordDto>> SearchOperatorRecordInfo(JHTPageAjaxResquest<SearchOperatorRecordWhereDto> where);

        /// <summary>
        /// 查询记录列表
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        PageData<LineSideMaterialStatisticsDto> SearchOperatorRecordStatistics(JHTPageAjaxResquest<LineSideMaterialStatisticsWhereDto> info);

        /// <summary>
        /// 线边库即时库存查询
        /// </summary>
        /// <returns></returns>
        PageData<RealInventory> SearchRealInventories(JHTPageAjaxResquest<CommonConditionData> input);

        /// <summary>
        /// 更新线边库物料BOM信息
        /// </summary>
        /// <param name="bomItems"></param>
        /// <returns></returns>
        Task<bool> UpdateLineMaterilInfoBomItems(List<LineSideMaterialInfoBomItemDto> bomItems);

        /// <summary>
        /// 获取线边库物料BOM信息
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        List<LineSideMaterialInfoBomItemDto> GetLineSideMaterialInfoBomItemDtosByMaterilId(EntityDto<long> entityDto);
    }
}
