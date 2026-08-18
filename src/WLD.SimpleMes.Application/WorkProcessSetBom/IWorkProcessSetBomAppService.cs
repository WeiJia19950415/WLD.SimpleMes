using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.WorkProcessSetBom.Dto;

namespace WLD.SimpleMes.WorkProcessSetBom
{
    public interface IWorkProcessSetBomAppService : IAsyncCrudAppService<WorkProcessSetBomDto, long, CommonPageRequestDto, WorkProcessSetBomDto, WorkProcessSetBomDto>
    {
        /// <summary>
        /// 获取工艺配置详情
        /// </summary>
        /// <param name="SetBomId"></param>
        /// <returns></returns>
        List<WorkProcessSetBomItemByShowDto> GetWorkProcessSetBomItemByShowDtos(EntityDto<long> SetBomId);

        /// <summary>
        /// 配置工艺BOM
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task ConfigWorkProcessBomAsync(ConfigWorkProcessBomDto dto);

        /// <summary>
        /// 根据物料ID获取工艺BOM列表
        /// </summary>
        /// <param name="MaterialIds"></param>
        /// <returns></returns>
        List<WorkProcessSetBomDto> GetWorkProcessSetBomDtosByMaterial(long MaterialIds);
    }
}
