using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Events.Bus.Handlers;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Authorization.Users;
using SC.SimpleMes.WorkOrder;

namespace SC.SimpleMes.Material.DomainEvent
{
    public class ERPBatchNumberOverUsedEventHandler : IAsyncEventHandler<ERPBatchNumberOverUsedEventData>, ITransientDependency
    {
        private readonly IRepository<WarningOverUsedERPInStockInfo, long> _warningOverUseInStockInfoRep;
        private readonly UserManager _userManager;
        private readonly OrganizationUnitManager _organizationUnitManager;
        public ERPBatchNumberOverUsedEventHandler(IRepository<WarningOverUsedERPInStockInfo, long> warningOverUseInStockInfoRep, UserManager userManager, OrganizationUnitManager organizationUnitManager)
        {
            _warningOverUseInStockInfoRep = warningOverUseInStockInfoRep;
            _userManager = userManager;
            _organizationUnitManager = organizationUnitManager;
        }

        [UnitOfWork]
        public virtual async Task HandleEventAsync(ERPBatchNumberOverUsedEventData eventData)
        {
            var record = _warningOverUseInStockInfoRep.FirstOrDefault(p => p.BatchNo == eventData.BatchNumber);
            if (record != null)
            {
                record.LastWarningTime = DateTime.Now;
                record.ActualUseAmount = eventData.ActualUseAmount;
            }
            else
            {
                User user = null;
                OrganizationUnit organizationUnit = null;
                if (eventData.FirstNoticeUserId > 0)
                {
                    user = await _userManager.FindByIdAsync(eventData.FirstNoticeUserId);
                    organizationUnit = _userManager.GetOrganizationUnits(user).FirstOrDefault();
                }
                _warningOverUseInStockInfoRep.Insert(new WarningOverUsedERPInStockInfo()
                {
                    BatchNo = eventData.BatchNumber,
                    LastWarningTime = DateTime.Now,
                    FirstWarningTime = DateTime.Now,
                    ActualUseAmount = eventData.ActualUseAmount,
                    FirstNoticeUserId = eventData.FirstNoticeUserId,
                    FirstNoticeUser = user != null ? user.Name : "",
                    BelongDepartmentName = organizationUnit != null ? organizationUnit.DisplayName : "",
                });
            }
        }


    }

    public class ERPBatchNumberOverUsedEventData : EventData
    {
        public string BatchNumber { get; set; }

        public decimal ActualUseAmount { get; set; }
        public long FirstNoticeUserId { get; set; }

        public string FirstNoticeUser { get; }

        public string BelongDepartmentName { get; }

    }
}
