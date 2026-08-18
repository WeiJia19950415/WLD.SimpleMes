using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Authorization;
using SC.SimpleMes.DTO;
using SC.SimpleMes.WorkStation.Dto;

namespace SC.SimpleMes.WorkStation
{
    public class WorkShopAppService : AsyncCrudAppService<WorkShopInfo, WorkShopInfoDto, long, CommonPageRequestDto, WorkShopInfoDto, WorkShopInfoDto>, IWorkShopAppService
    {
        public WorkShopAppService(IRepository<WorkShopInfo, long> repository) : base(repository)
        {
        }

        protected override IQueryable<WorkShopInfo> CreateFilteredQuery(CommonPageRequestDto input)
        {
            var query = base.CreateFilteredQuery(input);
            if (!string.IsNullOrEmpty(input.KeyWord))
            {
                query = query.Where(p => p.WorkShopName.Contains(input.KeyWord) || p.WorkShopNumber.Contains(input.KeyWord));
            }

            return query;
        }

        [AbpAuthorize(PermissionNames.Pages_WorkShopMange)]
        public override Task<WorkShopInfoDto> CreateAsync(WorkShopInfoDto input)
        {
            if (this.Repository.Count(p => p.WorkShopNumber == input.WorkShopNumber) > 0)
            {
                throw new UserFriendlyException("该车间编号已被使用");
            }

            input.TenantId = AbpSession.TenantId;
            return base.CreateAsync(input);
        }

        [AbpAuthorize(PermissionNames.Pages_WorkShopMange)]
        public override Task<WorkShopInfoDto> UpdateAsync(WorkShopInfoDto input)
        {
            if (this.Repository.Count(p => p.WorkShopNumber == input.WorkShopNumber && p.Id != input.Id) > 0)
            {
                throw new UserFriendlyException("该车间编号已被使用");
            }

            input.TenantId = input.TenantId.HasValue ? input.TenantId : AbpSession.TenantId;

            return base.UpdateAsync(input);
        }
    }
}
