using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using JHT.Abp.CommonModels;
using Microsoft.EntityFrameworkCore;
using WLD.SimpleMes.Authorization.Users;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.LineSideWarehouse;
using WLD.SimpleMes.OnLineStock.Dto;
using WLD.SimpleMes.WorkOrder;
using Abp.EntityFrameworkCore.Extensions;
using WLD.SimpleMes.IRepository;

namespace WLD.SimpleMes.OnLineStock
{
    public class LineSideMaterialInfoAppService : AsyncCrudAppService<LineSideMaterialInfo, LineSideMaterialInfoDto, long, CommonPageRequestDto, LineSideMaterialInfoDto, LineSideMaterialInfoDto>, ILineSideMaterialInfoAppService
    {
        private readonly IRepository<LineSideMaterialInfo, long> _repository;
        private readonly IRepository<LineSideMaterialInfoBomItem, long> _bomRepository;
        private readonly IRepository<LineSideMaterialOperatorRecord, long> _lineSideMaterialOperatorRecord;
        private readonly IRepository<View_LineSideMaterialOperatorRecord, long> _viewLineSideMaterialOperatorRecord;
        private readonly IRepository<WorkOrderInfo, long> _workOrderInfo;
        private readonly IMaterialBOMRepsoitory _materialBOMRepsoitory;
        private readonly UserManager _userManager;
        public LineSideMaterialInfoAppService(IRepository<LineSideMaterialInfo, long> repository,
            IRepository<LineSideMaterialOperatorRecord, long> lineSideMaterialOperatorRecord,
            UserManager userManager,
            IRepository<WorkOrderInfo, long> workOrderInfo,
            IMaterialBOMRepsoitory materialBOMRepsoitory,
            IRepository<View_LineSideMaterialOperatorRecord, long> viewLineSideMaterialOperatorRecord,
            IRepository<LineSideMaterialInfoBomItem, long> bomRepository
            ) :
            base(repository)
        {
            _repository = repository;
            _lineSideMaterialOperatorRecord = lineSideMaterialOperatorRecord;
            _viewLineSideMaterialOperatorRecord = viewLineSideMaterialOperatorRecord;
            _userManager = userManager;
            _workOrderInfo = workOrderInfo;
            _bomRepository = bomRepository;
            _materialBOMRepsoitory = materialBOMRepsoitory;
        }

        #region 线边库基础信息
        /// <summary>
        /// 检查是否已经发生过业务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private void CheckIsOccurringBusiness(EntityDto<long> input)
        {
            if (_lineSideMaterialOperatorRecord.GetAll().Where(p => p.LineSideMaterialInfoId == input.Id).Count() > 0)
            {
                throw new UserFriendlyException("物料已发生业务，禁止修改！");
            }
        }

        public async Task DeleteAsync(EntityDto<long> input)
        {
            CheckIsOccurringBusiness(input);
            await _repository.DeleteAsync(input.Id);
        }

        protected override IQueryable<LineSideMaterialInfo> CreateFilteredQuery(CommonPageRequestDto input)
        {
            var query = this.Repository.GetAll()
                .WhereIf(!string.IsNullOrEmpty(input.KeyWord), p => p.MaterialName.Contains(input.KeyWord)
                || p.Specification.Contains(input.KeyWord)||p.MaterialNumber.Contains(input.KeyWord));
            return query;
        }

        public async Task<LineSideMaterialInfoDto> GetAsync(EntityDto<long> input)
        {
            var data = await this.Repository.GetAsync(input.Id);
            LineSideMaterialInfoDto ret = new LineSideMaterialInfoDto()
            {
                Id = input.Id,
                MaterialName = data.MaterialName,
                Specification = data.Specification,
                UnitName = data.UnitName
            };
            return ret;
        }

        public override async Task<LineSideMaterialInfoDto> UpdateAsync(LineSideMaterialInfoDto input)
        {
            if (_repository.GetAll().Any(p => (p.MaterialName == input.MaterialName || p.MaterialNumber == input.MaterialNumber) && p.Id != input.Id))
            {
                throw new UserFriendlyException("该物料信息已存在！");
            }

            CheckIsOccurringBusiness(input);
            LineSideMaterialInfo up = new LineSideMaterialInfo()
            {
                Id = input.Id,
                MaterialName = input.MaterialName,
                UnitName = input.UnitName,
                MaterialNumber = input.MaterialNumber,
                BelongCategoryNumber = input.BelongCategoryNumber,
                Specification = input.Specification
            };
            await this.Repository.UpdateAsync(up);
            return input;
        }

        public override Task<LineSideMaterialInfoDto> CreateAsync(LineSideMaterialInfoDto input)
        {
            if (_repository.GetAll().Any(p => p.MaterialName == input.MaterialName || p.MaterialNumber == input.MaterialNumber))
            {
                throw new UserFriendlyException("该物料信息已存在！");
            }

            return base.CreateAsync(input);
        }

        #endregion

        #region 线边库业务操作

        public async Task<bool> AddOperatorRecord(LineSideMaterialOperatorRecordDto record)
        {
            record.OpertaorName = (await _userManager.FindByIdAsync(AbpSession.UserId.GetValueOrDefault())).Name;
            var insert = this.ObjectMapper.Map<LineSideMaterialOperatorRecord>(record);
            var workOrderInfo = _workOrderInfo.FirstOrDefault(p => p.OrderNumber == record.WorkOrderNumber);
            if (workOrderInfo == null)
            {
                throw new UserFriendlyException("生产任务单不存在，请检查！");
            }

            insert.ProjectNumber = workOrderInfo.ProjectNumber;
            insert.ProjectName = workOrderInfo.ProjectName;
            if (record.StockOperatoerType == StockOperatoerType.入库)
            {
                insert.InStock(record.OperatorCount, record.OperatorStockTime, record.LineSideMaterialInfoId, record.OperatorWorkShopId, record.OpertaorId);
            }
            else
            {
                insert.HandleUserName = (await _userManager.FindByIdAsync(record.HandleUserId.GetValueOrDefault())).Name;
                var nowStockCount = _viewLineSideMaterialOperatorRecord.GetAll().Where(p => p.LineSideMaterialInfoId == record.LineSideMaterialInfoId).Sum(p => p.OperatorCount);
                if (nowStockCount < record.OperatorCount)
                {
                    throw new UserFriendlyException("库存数量不够，请核对后在进行出库操作！");
                }
                insert.OutStock(record.OperatorCount, record.OperatorStockTime, record.LineSideMaterialInfoId, record.OperatorWorkShopId, record.OpertaorId);
            }
            await _lineSideMaterialOperatorRecord.InsertAsync(insert);
            return true;
        }

        public async Task<bool> RemoveOperatorRecord(EntityDto<long> input)
        {
            await _lineSideMaterialOperatorRecord.DeleteAsync(input.Id);
            return true;
        }

        public async Task<PageData<View_LineSideMaterialOperatorRecordDto>> SearchOperatorRecordInfo(JHTPageAjaxResquest<SearchOperatorRecordWhereDto> input)
        {
            SearchOperatorRecordWhereDto where = input.Condition;
            var req = _viewLineSideMaterialOperatorRecord.GetAll()
                .WhereIf(!string.IsNullOrEmpty(where.KeyWord), p => p.MaterialName.Contains(where.KeyWord) || p.WorkOrderNumber.Contains(where.KeyWord) || p.OpertaorName.Contains(where.KeyWord) || p.ProjectNumber.Contains(where.KeyWord))
                .WhereIf(where.StartOperatorTime != null && where.EndOperatorTime != null, p => p.OperatorStockTime >= where.StartOperatorTime && p.OperatorStockTime <= where.EndOperatorTime)
                .WhereIf(where.OperatorWorkShopId != null, p => p.OperatorWorkShopId == where.OperatorWorkShopId.GetValueOrDefault())
                .WhereIf(where.OpertaorId != null, p => p.OpertaorId == where.OpertaorId.Value)
                .WhereIf(where.StockOperatoerType != null, p => p.StockOperatoerType == where.StockOperatoerType);

            var total = req.Count();
            var result = new PageData<View_LineSideMaterialOperatorRecordDto>()
            {
                Total = total,
                List = total > 0 ? await ObjectMapper.ProjectTo<View_LineSideMaterialOperatorRecordDto>(req.OrderByDescending(p => p.OperatorTime).PageBy(input.SkipCount, input.PageSize)).ToListAsync() : null
            };
            return result;
        }

        public PageData<RealInventory> SearchRealInventories(JHTPageAjaxResquest<CommonConditionData> input)
        {
            var result = new PageData<RealInventory>();
            var req = _viewLineSideMaterialOperatorRecord.GetAll();
            var query = req.GroupBy(p => new { p.LineSideMaterialInfoId, p.MaterialName, p.UnitName, p.Specification })
                .Select(p => new RealInventory()
                {
                    LineSideMaterialInfoId = p.Key.LineSideMaterialInfoId,
                    MaterialName = p.Key.MaterialName,
                    Specification = p.Key.Specification,
                    OperatorCount = p.Sum(p => p.OperatorCount),
                    UnitName = p.Key.UnitName
                });

            result.List = query.Skip(input.SkipCount).Take(input.PageSize).ToList();
            result.Total = req.GroupBy(p => new { p.LineSideMaterialInfoId, p.MaterialName, p.UnitName }).Select(p => p.Key).Count();
            return result;
        }

        public PageData<LineSideMaterialStatisticsDto> SearchOperatorRecordStatistics(JHTPageAjaxResquest<LineSideMaterialStatisticsWhereDto> input)
        {
            var result = new List<LineSideMaterialStatisticsDto>();

            input.Condition.ParseTime();

            var info = input.Condition;
            var Outputreq = _viewLineSideMaterialOperatorRecord.GetAll().Where(p => p.StockOperatoerType == StockOperatoerType.入库)
                .WhereIf(info.MaterialInfoId != null, p => p.LineSideMaterialInfoId == info.MaterialInfoId)
                .WhereIf(info.EndTime != null && info.StartTime != null, p => p.OperatorStockTime >= info.StartTime && p.OperatorStockTime <= info.EndTime)
                .WhereIf(!string.IsNullOrEmpty(info.WorkOrderNumber), p => p.WorkOrderNumber.Equals(info.WorkOrderNumber))
                .GroupBy(p => new { p.LineSideMaterialInfoId, p.MaterialName, p.UnitName, p.Specification }).Select(p => new LineSideMaterialStatisticsDto()
                {
                    MaterialName = p.Key.MaterialName,
                    UnitName = p.Key.UnitName,
                    Specification = p.Key.Specification,
                    OutputQuantity = p.Sum(d => d.OperatorCount)
                }).ToList();

            var Consumptionreq = _viewLineSideMaterialOperatorRecord.GetAll().Where(p => p.StockOperatoerType == StockOperatoerType.出库)
                .WhereIf(info.MaterialInfoId != null, p => p.LineSideMaterialInfoId == info.MaterialInfoId)
                .WhereIf(info.EndTime != null && info.StartTime != null, p => p.OperatorStockTime >= info.StartTime && p.OperatorStockTime <= info.EndTime)
                .WhereIf(!string.IsNullOrEmpty(info.WorkOrderNumber), p => p.WorkOrderNumber.Equals(info.WorkOrderNumber))
                .GroupBy(p => new { p.LineSideMaterialInfoId, p.MaterialName, p.UnitName, p.Specification }).Select(p => new LineSideMaterialStatisticsDto()
                {
                    MaterialName = p.Key.MaterialName,
                    UnitName = p.Key.UnitName,
                    Specification = p.Key.Specification,
                    ConsumptionQuantity = p.Sum(d => Math.Abs(d.OperatorCount))
                }).ToList();

            foreach (var item in Outputreq)
            {
                var consumData = Consumptionreq.FirstOrDefault(p => p.MaterialName == item.MaterialName);
                if (consumData != null)
                {
                    item.ConsumptionQuantity = consumData.ConsumptionQuantity;
                }

                result.Add(item);
            }

            foreach (var item in Consumptionreq)
            {
                if (Outputreq.Any(p => p.MaterialName == item.MaterialName) == false)
                {
                    result.Add(item);
                }
            }

            return new PageData<LineSideMaterialStatisticsDto>()
            {
                List = result,
                Total = result.Count,
            };
        }

        public async Task<bool> UpdateLineMaterilInfoBomItems(List<LineSideMaterialInfoBomItemDto> bomItems)
        {
            var materialId = bomItems.FirstOrDefault().LineSideMaterialInfoId;
            await _materialBOMRepsoitory.BatchDeleteBomItemAsync(materialId);
            var addItems = ObjectMapper.Map<List<LineSideMaterialInfoBomItem>>(bomItems);
            List<LineSideMaterialInfoBomItem> needAddItems = new List<LineSideMaterialInfoBomItem>();
            foreach (var item in addItems)
            {
                // 去除重复种类
                if (needAddItems.Any(p => p.FormMaterialCategoryId == item.FormMaterialCategoryId) == false)
                {
                    needAddItems.Add(item);
                }
            }

            await _materialBOMRepsoitory.BatchInsertBomItemAsync(needAddItems);

            return true;
        }

        public List<LineSideMaterialInfoBomItemDto> GetLineSideMaterialInfoBomItemDtosByMaterilId(EntityDto<long> entityDto)
        {
            var items = _bomRepository.GetAll().Where(p => p.LineSideMaterialInfoId == entityDto.Id).ToList();

            return ObjectMapper.Map<List<LineSideMaterialInfoBomItemDto>>(items);
        }

        #endregion
    }
}
