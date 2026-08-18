using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Authorization;
using SC.SimpleMes.BatchNoByInStockInfo;
using SC.SimpleMes.BatchNoByInStockInfo.Dto;
using SC.SimpleMes.Material.Dto;
using SC.SimpleMes.Report.Dto;

namespace SC.SimpleMes.Controllers
{
    /// <summary>
    /// ERP入库单批次号打印
    /// </summary>
    [Route("api/[controller]/[action]")]
    [AbpAuthorize]
    [AbpMvcAuthorize(PermissionNames.Page_BatchNoByInStockInfo)]
    public class BatchNoByInStockInfoController : SimpleMesControllerBase
    {
        private readonly IBatchNoByInStockInfoAppService _batchNoByInStockInfoAppService;
        public BatchNoByInStockInfoController(IBatchNoByInStockInfoAppService batchNoByInStockInfoAppService)
        {
            _batchNoByInStockInfoAppService = batchNoByInStockInfoAppService;
        }


        [HttpPost]
        public async Task<PageData<BatchNoByInStockInfoDto>> SearchBatchNoByInStockInfo([FromBody] JHTPageAjaxResquest<QueryDto> whereDto)
        {
            PageData<BatchNoByInStockInfoDto> result = new PageData<BatchNoByInStockInfoDto>();
            result = await _batchNoByInStockInfoAppService.SearchBatchNoByInStockInfo(whereDto);
            return result;
        }


        [HttpPost]
        public async Task PrintBatchNo([FromBody] PrintBatchNoDto printBatchNoDto)
        {
            await _batchNoByInStockInfoAppService.PrintBatchNo(printBatchNoDto);
        }

     
    }
}
