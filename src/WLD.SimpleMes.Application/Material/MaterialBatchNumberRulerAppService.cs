using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Authorization;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.Material.Dto;

namespace WLD.SimpleMes.Material
{
    public class MaterialBatchNumberRulerAppService : AsyncCrudAppService<MaterialBatchNumberRuler, MaterialBatchNumberRulerDto, long, CommonPageRequestDto, MaterialBatchNumberRulerDto, MaterialBatchNumberRulerDto>, IMaterialBatchNumberRulerAppService
    {
        private readonly MaterialBatchNumberRulerManager _materialBatchNumberRulerManager;
        public MaterialBatchNumberRulerAppService(IRepository<MaterialBatchNumberRuler, long> repository, MaterialBatchNumberRulerManager materialBatchNumberRulerManager) : base(repository)
        {
            _materialBatchNumberRulerManager = materialBatchNumberRulerManager;
        }


        protected override IQueryable<MaterialBatchNumberRuler> CreateFilteredQuery(CommonPageRequestDto input)
        {
            var condtionData = input.QueryConditionObj as CommonConditionData;

           var query = this.Repository
                .GetAllIncluding(p => p.MaterialCategoryInfo)
                .WhereIf(!string.IsNullOrEmpty(condtionData.KeyWord), p => p.MaterialCategoryInfo.CategoryName.Contains(input.KeyWord) || p.MaterialCategoryInfo.CategoryCode.Contains(condtionData.KeyWord));

            return query;
        }


        [AbpAuthorize(PermissionNames.Page_Material_Ruler)]
        public override Task<MaterialBatchNumberRulerDto> CreateAsync(MaterialBatchNumberRulerDto input)
        {
            if (_materialBatchNumberRulerManager.IsExistMaterialBatchNumerRuler(input.MaterialCategoryInfoId))
            {
                throw new UserFriendlyException("该物料分组批次号规则已存在，请勿重复添加");
            }

            
            return base.CreateAsync(input);
        }

        [AbpAuthorize(PermissionNames.Page_Material_Ruler)]
        public override async Task<MaterialBatchNumberRulerDto> UpdateAsync(MaterialBatchNumberRulerDto input)
        {
            var dataInfo = Repository.FirstOrDefault(p => p.Id == input.Id);
            dataInfo.FlowNumberRulerLength = input.FlowNumberRulerLength;
            dataInfo.FlowNumberRuler = input.FlowNumberRuler;
            dataInfo.IsSerailNumber = input.IsSerailNumber;
            dataInfo.GenerateType = input.GenerateType;
            dataInfo.ComputePerProductLine = input.ComputePerProductLine;
            await UnitOfWorkManager.Current.SaveChangesAsync();

            return input;
        }
    }
}
