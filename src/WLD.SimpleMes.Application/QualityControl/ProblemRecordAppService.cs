using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using JHT.Abp.CommonModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.QualityControl.Dto;
using WLD.SimpleMes.Report.Dto;
using WLD.SimpleMes.WorkProcess.Dto;

namespace WLD.SimpleMes.QualityControl
{
    public class ProblemRecordAppService : AsyncCrudAppService<ProblemRecord, ProblemRecordDto, long, CommonPageRequestDto, ProblemRecordDto, ProblemRecordDto>,
        IProblemRecordAppService
    {
        private readonly IRepository<ProblemDealRecord, long> _problemDealRecordRepository;
        private readonly IRepository<View_ProblemRecord, long> _viewProblemRecordRep;
        private readonly IRepository<ProblemRecord, long> _problemRecordRep;
        private readonly IProblemCategoryCache _problemCategoryCache;
        private readonly IProblemDefineCache _problemDefineCache;
        public ProblemRecordAppService(
            IRepository<ProblemRecord, long> repository,
            IRepository<View_ProblemRecord, long> viewProblemRecord,
            IProblemCategoryCache problemCategoryCache,
            IProblemDefineCache problemDefineCache,
            IRepository<ProblemRecord, long> problemRecordRep,
            IRepository<ProblemDealRecord, long> problemDealRecordRepository) : base(repository)
        {
            _problemDealRecordRepository = problemDealRecordRepository;
            _viewProblemRecordRep = viewProblemRecord;
            _problemCategoryCache = problemCategoryCache;
            _problemDefineCache = problemDefineCache;
            _problemRecordRep = problemRecordRep;
        }

        public ProblemDealRecordDto LoadCurrentWorkProcessProblemDealRecord(InputOperatorRecordInfo input)
        {
            var problem = this.Repository.FirstOrDefault(p => p.IsClosed == false && p.BatchMaterilaNumber == input.OperatroMaterilBatchNumber);
            if (problem == null)
            {
                return new ProblemDealRecordDto();
            }

            var dealRecord = _problemDealRecordRepository.FirstOrDefault(p => p.ProblemRecordId == problem.Id);
            if (dealRecord == null)
            {
                return new ProblemDealRecordDto();
            }

            return ObjectMapper.Map<ProblemDealRecordDto>(dealRecord);
        }

        public List<ProblemRecordDto> LoadCurrentWorkProcessProblemRecord(string operatroMaterilBatchNumber)
        {
            var problem = this.Repository.GetAll().Where(p => p.IsClosed == false && p.BatchMaterilaNumber == operatroMaterilBatchNumber).ToList();
            if (problem.Count == 0 || problem == null)
            {
                problem = this.Repository.GetAll().Where(p => p.BatchMaterilaNumber == operatroMaterilBatchNumber).ToList();
            }

            if (problem.Count == 0 || problem == null)
            {
                return new List<ProblemRecordDto>();
            }

            var problemDto = ObjectMapper.Map<List<ProblemRecordDto>>(problem);
            var problemRecordIds = problemDto.Select(p => p.Id).ToList();
            var allCategoryCahce = _problemCategoryCache.GetAllProblemCategory();
            var problemRecordDtos = ObjectMapper.Map<List<ProblemDealRecordDto>>(_problemDealRecordRepository.GetAll().Where(p => problemRecordIds.Contains(p.ProblemRecordId)));

            foreach (var item in problemDto)
            {
                item.BelongProblemCategoryFullName = allCategoryCahce.FirstOrDefault(p => p.CategoryCode == item.BelongProblemCategoryCode).FullCategoryName;
                item.BelongProblemDefineName = _problemDefineCache.Get(item.BelongProblemDefineId).ProbleName;
                var recordDto = problemRecordDtos.FirstOrDefault(p => p.ProblemRecordId == item.Id);
                item.DealRecordDto = recordDto == null ? new ProblemDealRecordDto() : recordDto;
            }

            return problemDto;
        }

        public ProblemDealRecordDto LoadProblemDealRecordByProblemId(EntityDto<long> entityDto)
        {
            var problem = _problemRecordRep.FirstOrDefault(p => p.Id == entityDto.Id);
            var result = ObjectMapper.Map<ProblemDealRecordDto>(_problemDealRecordRepository.FirstOrDefault(p => p.ProblemRecordId == entityDto.Id));
            if (result != null && problem != null)
            {
                result.RelationImgs = problem.GetImgs();
            }
            else
            {

                result = new ProblemDealRecordDto()
                {
                    OperatorDescreption = "",
                    RelationImgs = problem.GetImgs(),
                };
            }

            return result;
        }

        public List<ProblemDealRecordDto> LoadProblemDealRecords(EntityDto<string> id)
        {
            List<ProblemDealRecordDto> results = new List<ProblemDealRecordDto>();
            var problemRecords = ObjectMapper.Map<List<View_ProblemRecordDto>>(this._viewProblemRecordRep.GetAll().Where(p => p.BatchMaterilaNumber == id.Id).ToList());
            List<long> problemRecordIds = problemRecords.Select(p => p.RecordId.GetValueOrDefault()).ToList();
            results = ObjectMapper.Map<List<ProblemDealRecordDto>>(_problemDealRecordRepository.GetAll().Where(p => problemRecordIds.Contains(p.ProblemRecordId)));
            foreach (var item in results)
            {
                var record = problemRecords.FirstOrDefault(p => p.RecordId == item.ProblemRecordId);
                item.Record = ObjectMapper.Map<ProblemRecordDto>(record);
            }

            return results;
        }

        public async Task<PageData<View_ProblemRecordDto>> LoadQualityDetailsRecordsAsync(JHTPageAjaxResquest<ProblemRecordQueryCondition> where)
        {
            PageData<View_ProblemRecordDto> returnData = new PageData<View_ProblemRecordDto>();
            var conditon = where.Condition;
            conditon.ParseTime();
            var query = _viewProblemRecordRep
                        .GetAll()
                        .WhereIf(conditon.StartDate != null, p => p.CreationTime >= conditon.StartDate)
                        .WhereIf(conditon.EndDate != null, p => p.CreationTime <= conditon.EndDate)
                        .WhereIf(conditon.ProblemDealType != null, p => p.ProblemDealType == conditon.ProblemDealType)
                        .WhereIf(conditon.MaterialId != null, p => p.MaterialId == conditon.MaterialId)
                        .WhereIf(conditon.ProblemDefineId != null, p => p.ProblemDefineId == conditon.ProblemDefineId)
                        .WhereIf(!string.IsNullOrEmpty(conditon.ProblemCategoryCode), p => p.CategoryCode.StartsWith(conditon.ProblemCategoryCode))
                        .WhereIf(!string.IsNullOrEmpty(conditon.ProductCategory), p => p.MaterialNumber.StartsWith(conditon.ProductCategory))
                        .WhereIf(conditon.ProductLineId != null, p => p.ProductLineId == conditon.ProductLineId)
                        .WhereIf(!string.IsNullOrEmpty(conditon.KeyWord), p => p.BatchMaterilaNumber.Contains(conditon.KeyWord) || p.WorkOrderNumber.Contains(conditon.KeyWord))
                        ;
            returnData.Total = await query.CountAsync();
            returnData.List = ObjectMapper.Map<List<View_ProblemRecordDto>>(query.AsNoTracking().OrderByDescending(p => p.CreationTime).ThenByDescending(p => p.IsClosed).Skip(where.SkipCount).Take(where.PageSize).ToList());

            return returnData;
        }
    }
}
