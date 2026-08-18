using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.BOM.Dto;
using SC.SimpleMes.DTO;
using SC.SimpleMes.WorkStation.Dto;

namespace SC.SimpleMes.BOM
{
    public interface IBOMAppService:IAsyncCrudAppService<BomDto, long, CommonPageRequestDto, BomAddDto, BomUpdateDto>
    {
        /// <summary>
        /// 根据工艺BOM获取标准BOM详情
        /// </summary>
        /// <param name="SetBomId"></param>
        /// <returns></returns>
        Task<List<BomItemDto>> GetBySetBomAsync(EntityDto<long> SetBomId);

        /// <summary>
        /// 根据工艺BOM获取标准BOM关键物料详情
        /// </summary>
        /// <param name="SetBomId"></param>
        /// <returns></returns>
        Task<List<BomItemDto>> GetBySetBomToImportantAsync(EntityDto<long> SetBomId);

        /// <summary>
        /// 获取车间，产线，工位的级联信息
        /// </summary>
        /// <returns></returns>
        List<UICascaderModel<BomDto, long>> GetBOMInCascader();

        /// <summary>
        /// 获取当前物料标准BOM 未配置工艺BOM的 BOM数据
        /// </summary>
        /// <param name="materialNumber"></param>
        /// <returns></returns>
        List<BomDto> GetUnConfigWorkProcessSetBom(string materialNumber);
        Task SetBomIsCurrentAsync(EntityDto<long> entityDto);
    }
}
