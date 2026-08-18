using Abp.Auditing;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Log
{
    public class JHTAuditLogStore : IAuditingStore, ITransientDependency
    {
        private readonly IRepository<JHTAuditLog, long> _auditLogRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public JHTAuditLogStore(
            IRepository<JHTAuditLog, long> auditLogRepository,
            IUnitOfWorkManager unitOfWorkManager
           )
        {
            _auditLogRepository = auditLogRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public void Save(AuditInfo auditInfo)
        {
            var data = JHTAuditLog.CreateFromAuditInfo(auditInfo);
            _auditLogRepository.Insert(data);

        }

        public async Task SaveAsync(AuditInfo auditInfo)
        {
            var data = JHTAuditLog.CreateFromAuditInfo(auditInfo);
            await _auditLogRepository.InsertAsync(data);

        }
    }
}

