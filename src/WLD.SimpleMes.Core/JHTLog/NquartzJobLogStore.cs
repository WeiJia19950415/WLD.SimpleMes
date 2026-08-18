using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.JHTLog
{
    public class NquartzJobLogStore : ITransientDependency
    {
        private readonly IRepository<NquartzJobLog, long> _logRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public NquartzJobLogStore(
             IRepository<NquartzJobLog, long> logRepository,
             IUnitOfWorkManager unitOfWorkManager
            )
        {
            _logRepository = logRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        [UnitOfWork]
        public virtual void Save(NquartzJobLog log)
        {
            _logRepository.Insert(log);
        }

        [UnitOfWork]
        public virtual async Task SaveAsync(NquartzJobLog log)
        {
            await _logRepository.InsertAsync(log);
        }
    }
}

