using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using JHT.Abp.CommonModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.BatchNoByInStockInfo.Dto;
using WLD.SimpleMes.Material;
using WLD.SimpleMes.Material.Dto;
using WLD.SimpleMes.Report.Dto;

namespace WLD.SimpleMes.BatchNoByInStockInfo
{
    public class BatchNoByInStockInfoAppService : SimpleMesAppServiceBase, IBatchNoByInStockInfoAppService
    {

        private readonly IRepository<ERPInStockInfo, long> _repository;
        private readonly IRepository<BatchNumberPrintRecord, long> _batchNumberPrintRecords;
        public BatchNoByInStockInfoAppService(IRepository<ERPInStockInfo, long> repository,
            IRepository<BatchNumberPrintRecord, long> batchNumberPrintRecords)
        {
            _repository = repository;
            _batchNumberPrintRecords = batchNumberPrintRecords;
        }


        public async Task PrintBatchNo(PrintBatchNoDto printBatchNoDto)
        {
            var up = await _repository.GetAsync(printBatchNoDto.Id);
            up.WhetherPrint = true;
            BatchNumberPrintRecord add = new BatchNumberPrintRecord()
            {
                BatchNumber = up.BatchNo,
                MaterialName = up.MaterialName,
                MaterialNumber = up.MaterialNumber,
                OperatorId = AbpSession.UserId.GetValueOrDefault(),
                OperatorName = "",
                PrintCounts = printBatchNoDto.PrintCounts,
                PrintTime = DateTime.Now,
            };
            await _batchNumberPrintRecords.InsertAsync(add);
        }

        public async Task<PageData<BatchNoByInStockInfoDto>> SearchBatchNoByInStockInfo(JHTPageAjaxResquest<QueryDto> whereDto)
        {
            var where = whereDto.Condition;
            var req = _repository.GetAll()
                .WhereIf(where.WhetherPrint != null, p => p.WhetherPrint == where.WhetherPrint)
                .Where(p => p.ReceiptQuantity > 0)
                .WhereIf(!string.IsNullOrEmpty(where.KeyWord), p => p.BatchNo.Contains(where.KeyWord) ||
                p.MaterialName.Contains(where.KeyWord) || p.MaterialNumber.Contains(where.KeyWord) ||
                p.WarehousingNumber.Contains(where.KeyWord) || p.Supplier.Contains(where.KeyWord))
                .WhereIf(where.StartTime != null && where.EndTime != null, p => p.WarehousingTime >= where.StartTime && p.WarehousingTime <= where.EndTime);
            var result = new PageData<BatchNoByInStockInfoDto>()
            {
                Total = req.Count(),
                List = await ObjectMapper.ProjectTo<BatchNoByInStockInfoDto>(req.OrderByDescending(p => p.WarehousingTime).ThenByDescending(p=>p.BatchNo).PageBy(whereDto.SkipCount, whereDto.PageSize)).ToListAsync()
            };
            return result;
        }


    }
}
