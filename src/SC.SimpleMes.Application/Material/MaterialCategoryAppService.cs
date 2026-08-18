using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Authorization;
using SC.SimpleMes.DTO;
using SC.SimpleMes.Material.Dto;

namespace SC.SimpleMes.Material
{
    public class MaterialCategoryAppService : AsyncCrudAppService<MaterialCategory, MaterialCategoryDto, long, CommonPageRequestDto, MaterialCategoryDto, MaterialCategoryDto>
        , IMaterialCategoryAppService
    {
        private readonly MaterialCategoryManager _materialCategoryManager;
        public MaterialCategoryAppService(IRepository<MaterialCategory, long> repository, MaterialCategoryManager materialCategoryManager) : base(repository)
        {
            _materialCategoryManager = materialCategoryManager;
        }

        [AbpAuthorize(PermissionNames.Page_Material_Category, PermissionNames.BaseInfo_Edit)]
        public override Task<MaterialCategoryDto> CreateAsync(MaterialCategoryDto input)
        {
            if (_materialCategoryManager.IsUniqueCategoryCode(input.CategoryCode) == false)
            {
                throw new UserFriendlyException("该分类表编码已经被使用");
            }

            if (!string.IsNullOrEmpty(input.CategoryCode))
            {
                var parentCode = MaterialCategory.GetParentCode(input.CategoryCode);

                if (!string.IsNullOrEmpty(parentCode))
                {
                    var parantCode = this.Repository.FirstOrDefault(p => p.CategoryCode == parentCode);
                    input.ParentCategoryId = parantCode?.Id;

                    input.FullCategoryName = MaterialCategory.GetFullCatgoryName(parantCode.FullCategoryName, input.CategoryName);
                }
                else
                {
                    input.FullCategoryName = input.CategoryName;
                }
            }

            input.TenantId = AbpSession.TenantId;

            return base.CreateAsync(input);
        }

        public Task<List<MaterialCategoryDto>> LoadAllProductCategoryAsync()
        {
            var result = this.Repository.GetAll().Where(p => p.CategoryCode.StartsWith("D02") || p.CategoryCode.StartsWith("D01")).ToList();
            return Task.FromResult(ObjectMapper.Map<List<MaterialCategoryDto>>(result));
        }

        public List<UICascaderModel<string, string>> LoadCascadeMaterialCategory(string categGoryCode)
        {
            var parentCode = this.Repository.FirstOrDefault(p => p.CategoryCode == categGoryCode);
            return this.Repository.GetAll()
                .WhereIf(parentCode != null, p => p.ParentCategoryId == parentCode.Id)
                .WhereIf(parentCode == null, p => p.ParentCategoryId == null || p.ParentCategoryId == 0)
                .Select(
                 p => new UICascaderModel<string, string>()
                 {
                     Label = p.CategoryName,
                     Value = p.CategoryCode,
                 }
                ).ToList();

        }

        [AbpAuthorize(PermissionNames.Page_Material_Category)]
        public override async Task<MaterialCategoryDto> UpdateAsync(MaterialCategoryDto input)
        {
            if (_materialCategoryManager.IsUniqueCategoryCode(input.CategoryCode, input.Id) == false)
            {
                throw new UserFriendlyException("该分类表编码已经被使用");
            }

            var dataInfo = Repository.FirstOrDefault(p => p.Id == input.Id);
            if (_materialCategoryManager.IsUsed(dataInfo.CategoryCode))
            {
                throw new UserFriendlyException("该分类表编码或子类分组已经被使用,不允许修改");
            }

            //dataInfo.CategoryName = input.CategoryName;
            //dataInfo.CategoryDescription = input.CategoryDescription;
            dataInfo.IsKeyMaterial = input.IsKeyMaterial;
            if (!string.Equals(dataInfo.CategoryCode, input.CategoryCode))
            {
                _materialCategoryManager.ChangeParentCode(dataInfo, input.CategoryCode);
            }

            await UnitOfWorkManager.Current.SaveChangesAsync();
            return input;
        }


        protected override IQueryable<MaterialCategory> CreateFilteredQuery(CommonPageRequestDto input)
        {
            var query = base.CreateFilteredQuery(input);
            var conditionInfo = input.QueryConditionObj as CommonConditionData;
            query = query.WhereIf(!string.IsNullOrEmpty(conditionInfo.KeyWord), p => p.CategoryName.Contains(conditionInfo.KeyWord) || p.CategoryCode.Contains(conditionInfo.KeyWord) || p.FullCategoryName.Contains(conditionInfo.KeyWord));

            return query;
        }

    }
}
