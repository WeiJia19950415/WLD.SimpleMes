using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.DTO;
using SC.SimpleMes.Material.Dto;
using SC.SimpleMes.WorkOrder;

namespace SC.SimpleMes.Material
{
    /// <summary>
    /// 裁切物料配置
    /// </summary>
    public class CutMaterialConfigAppService : AsyncCrudAppService<CutMaterialConfig, CutMaterialConfigDto, long, CommonPageRequestDto, CutMaterialConfigDto, CutMaterialConfigDto>,
        ICutMaterialConfigAppService
    {

        private readonly IRepository<MaterialInfo, long> _materialRep;
        private readonly IRepository<WorkOrderInfo, long> _workOrderRep;
        private readonly MaterialManager _materialManager;
        public CutMaterialConfigAppService(IRepository<CutMaterialConfig, long> repository,
           MaterialManager materialManager,
           IRepository<WorkOrderInfo, long> workOrderRep,
            IRepository<MaterialInfo, long> materialRep) : base(repository)
        {
            _materialRep = materialRep;
            _workOrderRep = workOrderRep;
            _materialManager = materialManager;
        }


        protected override IQueryable<CutMaterialConfig> CreateFilteredQuery(CommonPageRequestDto input)
        {
            var query = this.Repository.GetAllIncluding(p => p.UsedProduct);
            var queryConditon = input.QueryConditionObj as CutMaterialConfigConditionDto;
            query = query
                .WhereIf(string.IsNullOrEmpty(queryConditon.KeyWord),
                p => p.UsedProduct.MaterialName.Contains(queryConditon.KeyWord) || p.ConfigMaterialName.Contains(input.KeyWord) || p.ConfigMaterialNumber.Contains(queryConditon.KeyWord))
                .WhereIf(queryConditon.UseProductId > 0, p => p.UsedProductId == queryConditon.UseProductId);

            return query;
        }


        public override Task<CutMaterialConfigDto> CreateAsync(CutMaterialConfigDto input)
        {
            // 产品与裁切物料的配比应该唯一

            if (this.Repository.GetAll().Any(p => p.UsedProductId == input.UsedProductId && p.ConfigMaterialNumber == input.ConfigMaterialNumber))
            {

                throw new UserFriendlyException("该裁切物料与产品配置已经存在！");
            }

            var productInfo = _materialRep.FirstOrDefault(p => p.Id == input.UsedProductId);
            input.ProductMaterialNumber = productInfo.MaterialNumber;
            return base.CreateAsync(input);
        }

        public override Task<CutMaterialConfigDto> UpdateAsync(CutMaterialConfigDto input)
        {
            if (this.Repository.GetAll().Any(p => p.UsedProductId == input.UsedProductId &&
            p.ConfigMaterialNumber == input.ConfigMaterialNumber && p.Id != input.Id))
            {
                throw new UserFriendlyException("该裁切物料与产品配置已经存在！");
            }

            var productInfo = _materialRep.FirstOrDefault(p => p.Id == input.UsedProductId);
            input.ProductMaterialNumber = productInfo.MaterialNumber;

            return base.UpdateAsync(input);
        }

        public CutMaterialConfigDto LoadCutMaterialConfig(CutMaterialConfigDto materialConfigDto)
        {
            if (materialConfigDto.UsedProductId == 0 && !string.IsNullOrEmpty(materialConfigDto.WorkOrderNumber))
            {
                materialConfigDto.UsedProductId = _workOrderRep.FirstOrDefault(p => p.OrderNumber == materialConfigDto.WorkOrderNumber).MaterialInfoId;
            }

            var cutConfigDto = _materialManager.LoadCutMaterialConfig(materialConfigDto.UsedProductId, materialConfigDto.ConfigMaterialNumber);

            //    this.Repository.FirstOrDefault(p => p.UsedProductId == materialConfigDto.UsedProductId & p.ConfigMaterialNumber == materialConfigDto.ConfigMaterialNumber);
            //if (cutConfigDto == null)
            //{
            //    // 没有产品配置，则看是否有同种类的产品
            //    var product = _materialRep.FirstOrDefault(p => p.Id == materialConfigDto.UsedProductId);
            //    var parentCategory = MaterialCategory.GetParentCode(product.MaterialNumber);
            //    cutConfigDto = this.Repository.FirstOrDefault(p => p.ProductMaterialNumber.StartsWith(parentCategory) && p.ConfigMaterialNumber == materialConfigDto.ConfigMaterialNumber);

            //    if (cutConfigDto == null)
            //    {
            //        // 是否有同类型的产品
            //        var configMaterialParantCategory = MaterialCategory.GetParentCode(materialConfigDto.ConfigMaterialNumber);
            //        cutConfigDto = this.Repository.FirstOrDefault(p => p.ProductMaterialNumber.StartsWith(parentCategory) && p.ConfigMaterialNumber.StartsWith(configMaterialParantCategory));
            //    }

            //    //if(cutConfigDto == null)
            //    //{
            //    //    cutConfigDto=new CutMaterialConfig() { ConversionRatio=1,}
            //    //}
            //}

            return ObjectMapper.Map<CutMaterialConfigDto>(cutConfigDto);
        }
    }
}
