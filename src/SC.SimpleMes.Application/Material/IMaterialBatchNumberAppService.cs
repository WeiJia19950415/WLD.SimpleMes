using Abp.Application.Services;
using JHT.Abp.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.BatchNoByInStockInfo.Dto;
using SC.SimpleMes.DTO;
using SC.SimpleMes.Material.Dto;

namespace SC.SimpleMes.Material
{
    public interface IMaterialBatchNumberAppService : IAsyncCrudAppService<MaterialBatchNumberDto, long, CommonPageRequestDto, MaterialBatchNumberDto, MaterialBatchNumberDto>, IApplicationService
    {
        Task<JHTAjaxResponse<MaterialBatchNumberDto>> GetByProductMaterialBatchNumberAsync(string materialBatchNumber);
        Task<JHTAjaxResponse<ERPInStockInfoDto>> LoadErpBatchNumberAsync(string erpInstockBatchNumber);
        Task<JHTAjaxResponse> AddPrintBatchNumberRecordAsync(PrintBatchNoDto printBatch);

        Task<JHTAjaxResponse<MaterialInfoDto>> CheckMaterialCanUseInWorkProcessAsync(long currentWorkProcessId, string materialNumber);
        bool CheckERPBacthNumberMaterialIsUsedOut( string erpInstockBatchNumber, decimal bomBatchNumberCount = 0, bool needSaveTip = true);
        Task<PageData<MaterialBatchNumberDto>> LoadCreatedBatchNumberAsync(JHTPageAjaxResquest<MaterialBatchNumberConditionDto> where);
        Task<List<MaterialBatchNumberExportDto>> LoadCreatedBatchNumberExportDtoAsync(JHTPageAjaxResquest<MaterialBatchNumberConditionDto> where);

        /// <summary>
        /// 检查物料批次号是否已经被使用
        /// </summary>
        /// <param name="batchNumber"></param>
        /// <returns></returns>
        bool CanMaterialBatchNumberBeUse(string batchNumber,out string message);

        /// <summary>
        /// 产品是否已经入库
        /// </summary>
        /// <param name="batchNumber"></param>
        /// <returns></returns>
        bool IsProductHaveInstocked(string batchNumber);
    }
}
