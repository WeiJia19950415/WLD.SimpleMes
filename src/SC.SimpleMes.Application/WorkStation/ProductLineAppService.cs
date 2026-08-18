using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Authorization;
using SC.SimpleMes.Authorization.Users;
using SC.SimpleMes.DTO;
using SC.SimpleMes.MultiTenancy;
using SC.SimpleMes.WorkStation.Dto;

namespace SC.SimpleMes.WorkStation
{
    public class ProductLineAppService : AsyncCrudAppService<ProductLine, ProductLineDto, long, CommonPageRequestDto, ProductLineDto, ProductLineDto>, IProductLineAppService
    {
        private readonly IRepository<ProductLineUserRelation, long> _productLineUserRep;
        private ProductLineManager _productLineManager;
        private readonly IRepository<User, long> _userRep;
        public ProductLineAppService(
            IRepository<ProductLine, long> repository,
            IRepository<ProductLineUserRelation, long> productLineUserRep,
            ProductLineManager productLineManager,
            TenantManager tenantManager,
            IRepository<User, long> userRep) : base(repository)
        {
            this._productLineUserRep = productLineUserRep;
            this._userRep = userRep;
            this._productLineManager = productLineManager;
        }

        protected override IQueryable<ProductLine> CreateFilteredQuery(CommonPageRequestDto input)
        {
            ProductLineConditionDto productLineConditionDto = input.QueryConditionObj as ProductLineConditionDto;
            var query = base.CreateFilteredQuery(input);
            query = query.Include(p => p.BelongWorkShop)
              .WhereIf(!string.IsNullOrEmpty(productLineConditionDto.KeyWord), p => p.ProductLineName.Contains(productLineConditionDto.KeyWord) || p.ProductLineNumber.Contains(productLineConditionDto.KeyWord))
              .WhereIf(productLineConditionDto.ProductLineState != null, p => p.ProductLineState == productLineConditionDto.ProductLineState)
              .WhereIf(productLineConditionDto.BelongWorkShopId.HasValue, p => p.BelongWorkShopId == productLineConditionDto.BelongWorkShopId)
              ;

            return query;
        }

        protected override IQueryable<ProductLine> ApplySorting(IQueryable<ProductLine> query, CommonPageRequestDto input)
        {
            return query.OrderBy(p=>p.Id);
        }

        [AbpAuthorize(PermissionNames.Page_ProductLineManange)]
        public override Task<ProductLineDto> UpdateAsync(ProductLineDto input)
        {
            if (_productLineManager.CheckProductLineNumberIsUnique(input.ProductLineNumber, input.Id) == false)
            {
                throw new UserFriendlyException("该产线编号已被使用");
            }
            input.TenantId = input.TenantId.HasValue ? input.TenantId : AbpSession.TenantId;
            return base.UpdateAsync(input);
        }


        [AbpAuthorize(PermissionNames.Page_ProductLineManange)]
        public override Task<ProductLineDto> CreateAsync(ProductLineDto input)
        {
            if (_productLineManager.CheckProductLineNumberIsUnique(input.ProductLineNumber, input.Id) == false)
            {
                throw new UserFriendlyException("该产线编号已被使用");
            }
            input.TenantId = AbpSession.TenantId;
            return base.CreateAsync(input);
        }

        [AbpAuthorize(PermissionNames.Page_CofingProductLineUser)]
        public async Task<bool> BingUserAndWorkProcessAsync(TransferDto dto)
        {
            List<ProductLineUserRelation> newdata = new List<ProductLineUserRelation>();
            foreach (var item in dto.selectList)
            {
                newdata.Add(new ProductLineUserRelation()
                {
                    ProductLineId = dto.Id,
                    UserInfoId = item
                });
            }

            await _productLineManager.BindOperatorUser(newdata, dto.Id);
            return true;
        }

        public TransferDto GetUserListAndBingUser(EntityDto dto)
        {
            TransferDto ret = new TransferDto();
            var userList = _userRep.GetAll().ToList();
            List<TransferItemDto> items = ObjectMapper.Map<List<TransferItemDto>>(userList);
            ret.Id = dto.Id;
            ret.allList = items;
            ret.selectList = _productLineManager.GetProductLineMangeUsersId(dto.Id);
            return ret;
        }

        public Task<List<ProductLineDto>> GetMangedProductLinesAsync()
        {
            var result = _productLineManager.GetUserMangedProductLines(AbpSession.UserId);
            List<ProductLineDto> dtoResult = ObjectMapper.Map<List<ProductLineDto>>(result);
            return Task.FromResult(dtoResult);
        }

        public override async Task<ProductLineDto> GetAsync(EntityDto<long> input)
        {
            return ObjectMapper.Map<ProductLineDto>(await Repository.GetAllIncluding(p => p.BelongWorkShop).FirstOrDefaultAsync(p => p.Id == input.Id));
        }
    }
}
