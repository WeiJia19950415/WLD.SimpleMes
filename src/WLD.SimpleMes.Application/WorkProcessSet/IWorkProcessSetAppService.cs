using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JHT.Abp.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.WorkProcess;
using WLD.SimpleMes.WorkProcessSet.Dto;

namespace WLD.SimpleMes.WorkProcessSet
{
    public interface IWorkProcessSetAppService : IAsyncCrudAppService<WorkProcessSetInfoDto, long, DTO.CommonPageRequestDto, WorkProcessSetInfoDto, WorkProcessSetInfoDto>, IApplicationService
    {
        /// <summary>
        /// 复制工艺
        /// </summary>
        /// <param name="entityDto"></param>
        public Task<WorkProcessSetInfoDto> CopyWorkProcessSetAsync(EntityDto<long> entityDto);

        /// <summary>
        /// 设置产品与工艺进行绑定
        /// </summary>
        /// <param name="setProductProcessSetDto"></param>
        /// <returns></returns>
        public Task<JHTAjaxResponse> SetProductProcessSetAsync(SetProductProcessSetDto setProductProcessSetDto);

        /// <summary>
        /// 更新配置信息
        /// </summary>
        /// <param name="productWorkProcessConfigDetailDto"></param>
        /// <returns></returns>
        Task UpdateWorkProcessConfigDataAsync(WorkProcessSetInfoDto productWorkProcessConfigDetailDto);

        PageData<WorkProcessSetInfoDto> GetWorkProcessSetPageData(JHTPageAjaxResquest<CommonConditionData> pageAjaxResquest);

        /// <summary>
        /// 加载产品配置信息
        /// </summary>
        /// <param name="pageAjaxResquest"></param>
        /// <returns></returns>
        PageData<ProductWorkProcessSetDto> LoadProductWorkProcessConfig(JHTPageAjaxResquest<WorkProcessProductConditionDto> pageAjaxResquest);
        Task<JHTAjaxResponse> DelProductCurrentWorkProcessSetAsync(EntityDto entityDto);
        Task<JHTAjaxResponse> SetProductCurrentWorkProcessSetAsync(SetProductProcessSetDto dataModel);
        Task<JHTAjaxResponse> UpdateProdutctWorkProcessSetAsync(SetProductProcessSetDto creatModel);
        /// <summary>
        /// 获取工艺名称-版本，联级选择器
        /// </summary>
        /// <returns></returns>
        public List<UICascaderModel<WorkProcessSetInfoDto, long>> GetProcessSetInCascader();
        List<WorkProcessSetDetail> LoadProductWorkProcessConfigByMaterialId(EntityDto materilId);
    }
}
