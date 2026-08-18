using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using JHT.Abp.CommonModels;
using JHT.CommonUtity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WLD.SimpleMes.AttachFile;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.QualityControl.Dto;
using WLD.SimpleMes.Report.Dto;

namespace WLD.SimpleMes.QualityControl
{
    public class QualityProblemDefineAppService : AsyncCrudAppService<QualityProblemDefine, ProblemDefineDto, long, CommonPageRequestDto, ProblemDefineDto, ProblemDefineDto>
        , IQualityProblemDefineAppService
    {
        private readonly QualityProblemDefineManager _qualityProblemDefineManager;
        private readonly IRepository<ProblemCategory, long> _problemCategoryRep;
        public QualityProblemDefineAppService(IRepository<QualityProblemDefine, long> repository,
            IRepository<ProblemCategory, long> problemCategoryRep,
            
        QualityProblemDefineManager qualityProblemDefineManager) : base(repository)
        {
            _qualityProblemDefineManager = qualityProblemDefineManager;
            _problemCategoryRep = problemCategoryRep;
        }

        protected override IQueryable<QualityProblemDefine> CreateFilteredQuery(CommonPageRequestDto input)
        {
            var conditonDto = input.QueryConditionObj as DTO.CommonConditionData;
            var query = this.Repository.GetAllIncluding(p => p.ProblemCategory)
                .WhereIf(!string.IsNullOrEmpty(conditonDto.KeyWord), p => p.ProbleName.Contains(conditonDto.KeyWord) || p.ProblemCategory.FullCategoryName.Contains(conditonDto.KeyWord))
                ;
            return query;
        }

        public override Task<ProblemDefineDto> CreateAsync(ProblemDefineDto input)
        {
            if (_qualityProblemDefineManager.IsExistDefineName(input.ProblemCategoryId.GetValueOrDefault(), input.ProbleName))
            {
                throw new UserFriendlyException("该分类下的问题描述已存在，勿重新提交");
            }

            input.ProblemCategoryId = _problemCategoryRep.FirstOrDefault(p => p.CategoryCode == ProblemCategory.GetParentCode(input.QualityProblemNumber)).Id;
            return base.CreateAsync(input);
        }

        public override Task<ProblemDefineDto> UpdateAsync(ProblemDefineDto input)
        {
            if (_qualityProblemDefineManager.IsExistDefineName(input.ProblemCategoryId.GetValueOrDefault(), input.ProbleName, input.Id))
            {
                throw new UserFriendlyException("该分类下的问题描述已存在，勿重新提交");
            }

            if (_qualityProblemDefineManager.IsUsed(input.Id))
            {
                throw new UserFriendlyException("该问题已经被使用，请勿删除");
            }

            input.ProblemCategoryId = _problemCategoryRep.FirstOrDefault(p => p.CategoryCode == ProblemCategory.GetParentCode(input.QualityProblemNumber)).Id;

            return base.UpdateAsync(input);
        }

        public override Task DeleteAsync(EntityDto<long> input)
        {
            if (_qualityProblemDefineManager.IsUsed(input.Id))
            {
                throw new UserFriendlyException("该问题已经被使用，请勿删除");
            }

            return base.DeleteAsync(input);
        }

        public List<ProblemDefineDto> GetProblemDefineByCatetoeryCode(string categoryCode)
        {
            return ObjectMapper.Map<List<ProblemDefineDto>>(_qualityProblemDefineManager.GetProblemDefineByCatetoeryCode(categoryCode));
        }

       
    }
}
