using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.DTO;

using SC.SimpleMes.QualityControl.Dto;

namespace SC.SimpleMes.QualityControl
{
    public class ProblemCategoryAppService : AsyncCrudAppService<ProblemCategory, ProblemCategoryDto, long, CommonPageRequestDto, ProblemCategoryDto, ProblemCategoryDto>, IProblemCategoryAppService
    {
        private readonly ProblemCategoryManager _problemCategoryManager;
        public ProblemCategoryAppService(IRepository<ProblemCategory, long> repository, ProblemCategoryManager problemCategoryManager) : base(repository)
        {
            _problemCategoryManager = problemCategoryManager;
        }

        public override Task<ProblemCategoryDto> CreateAsync(ProblemCategoryDto input)
        {
            if (_problemCategoryManager.IsUniqueCategoryCode(input.CategoryCode) == false)
            {
                throw new UserFriendlyException("该分类表编码已经被使用");
            }

            if (!string.IsNullOrEmpty(input.CategoryCode))
            {
                var parentCode = ProblemCategory.GetParentCode(input.CategoryCode);

                if (!string.IsNullOrEmpty(parentCode))
                {
                    var parantCode = this.Repository.FirstOrDefault(p => p.CategoryCode == parentCode);
                    input.ParentCategoryId = parantCode?.Id;

                    input.FullCategoryName = ProblemCategory.GetFullCatgoryName(parantCode.FullCategoryName, input.CategoryName);
                }
                else
                {
                    input.FullCategoryName = input.CategoryName;
                }
            }

            return base.CreateAsync(input);
        }

        public List<UICascaderModel<string, string>> LoadCascadeProblemCategory(string categGoryCode)
        {
            var parentCode = this.Repository.FirstOrDefault(p => p.CategoryCode == categGoryCode);
            return this.Repository.GetAll()
                .WhereIf(parentCode != null, p => p.ParentCategoryId == parentCode.Id)
                .WhereIf(parentCode == null, p => p.ParentCategoryId == null)
                .Select(
                 p => new UICascaderModel<string, string>()
                 {
                     Label = p.CategoryName,
                     Value = p.CategoryCode,
                 }
                ).ToList();
        }

        public override async Task<ProblemCategoryDto> UpdateAsync(ProblemCategoryDto input)
        {
            if (_problemCategoryManager.IsUniqueCategoryCode(input.CategoryCode, input.Id) == false)
            {
                throw new UserFriendlyException("该分类表编码已经被使用");
            }

            if (_problemCategoryManager.IsUsed(input.Id))
            {
                throw new UserFriendlyException("该分类表编码已经被使用,不允许修改");
            }

            var dataInfo = Repository.FirstOrDefault(p => p.Id == input.Id);
            dataInfo.CategoryDescription = input.CategoryDescription;
            dataInfo.CategoryName = input.CategoryName;
            if (!string.Equals(dataInfo.CategoryCode, input.CategoryCode))
            {
                _problemCategoryManager.ChangeCategoryCode(dataInfo, input.CategoryCode);
            }
          
            await this.UnitOfWorkManager.Current.SaveChangesAsync();
            return input;
        }

        protected override IQueryable<ProblemCategory> CreateFilteredQuery(CommonPageRequestDto input)
        {
            var query = base.CreateFilteredQuery(input);
            var conditionInfo = input.QueryConditionObj as CommonConditionData;
            query = query.WhereIf(!string.IsNullOrEmpty(conditionInfo.KeyWord), p => p.CategoryName.Contains(input.KeyWord) || p.CategoryCode.Contains(input.KeyWord) || p.FullCategoryName.Contains(input.KeyWord));
            return query;
        }

    }
}
