using JHT.Abp.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.BatchNoByInStockInfo.Dto;
using WLD.SimpleMes.Material.Dto;
using WLD.SimpleMes.Report.Dto;

namespace WLD.SimpleMes.BatchNoByInStockInfo
{
    public interface IBatchNoByInStockInfoAppService
    {
        /// <summary>
        /// 获取外部ERP传入的入库单信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<PageData<BatchNoByInStockInfoDto>> SearchBatchNoByInStockInfo(JHTPageAjaxResquest<QueryDto> whereDto);

        /// <summary>
        /// 打印时触发
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task PrintBatchNo(PrintBatchNoDto printBatchNoDto);
    }
}
