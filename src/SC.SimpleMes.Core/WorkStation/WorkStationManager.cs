using Abp.Authorization.Users;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Authorization.Users;

namespace SC.SimpleMes.WorkStation
{
    public class WorkStationManager : ITransientDependency
    {
        private readonly IRepository<WorkStationInfo, long> _repository;
        private readonly IRepository<WorkStationUserRelation, long> _workStationUserRep;
        private readonly IRepository<ProductLineUserRelation, long> _productLineUserRep;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRep;
        public WorkStationManager(IRepository<WorkStationInfo, long> repository,
            IRepository<WorkStationUserRelation, long> workStationUserRep,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRep,
            IRepository<ProductLineUserRelation, long> productLineUserRep)
        {
            _repository = repository;
            _workStationUserRep = workStationUserRep;
            _productLineUserRep = productLineUserRep;
            _userOrganizationUnitRep = userOrganizationUnitRep;
        }

        public bool IsUniqueWorkStationNumber(string workStationNumber, long? workStationId = 0)
        {
            if (workStationId == 0 || workStationId == null)
            {
                return !_repository.GetAll().Any(p => p.WorkStationNumber == workStationNumber);
            }

            return !_repository.GetAll().Any(p => p.WorkStationNumber == workStationNumber && p.Id != workStationId);

        }

        public List<long> GetManagedWorkStationUserIds(int workStationId)
        {
            return _workStationUserRep
                .GetAll()
                .Where(p => p.WorkStationInfoId == workStationId)
                .Select(p => p.UserInfoId).ToList();
        }

        public List<User> GetManagedWorkStationUser(long workStationId, long? depId)
        {
            var userIds = _userOrganizationUnitRep.GetAll().Where(p => p.OrganizationUnitId == depId).Select(p => p.UserId).ToList();
            return _workStationUserRep
                .GetAllIncluding(p => p.UserInfo)
                .Where(p => p.WorkStationInfoId == workStationId && userIds.Contains(p.UserInfoId))
                .WhereIf(depId != null, p => userIds.Contains(p.UserInfoId))
                .Select(p => p.UserInfo)
                .ToList();
        }

        public List<WorkStationInfo> GetManagedWorkStation(long userId)
        {
            List<WorkStationInfo> workStationInfos = new List<WorkStationInfo>();
            List<long> manageProductLine = _productLineUserRep.GetAll().Where(p => p.UserInfoId == userId).Select(p => p.ProductLineId).ToList();
            if (manageProductLine.Count > 0)
            {
                workStationInfos = _repository.GetAllIncluding(p => p.BelongProductLine).Where(p => manageProductLine.Contains(p.BelongProductLineId.Value)).ToList();
            }

            var userWorkStation = _workStationUserRep
                .GetAllIncluding(p => p.WorkStationInfo, p => p.WorkStationInfo.BelongProductLine)
                .Where(p => p.UserInfoId == userId).Select(p => p.WorkStationInfo).ToList();

            return workStationInfos.Union(userWorkStation).ToList();
        }

        public bool IsMangerWorkStation(long userId, long workStationId)
        {
            bool result = false;
            List<long> manageProductLine = _productLineUserRep.GetAll().Where(p => p.UserInfoId == userId).Select(p => p.ProductLineId).ToList();
            if (manageProductLine.Count > 0)
            {
                result = _repository.GetAll().Any(p => manageProductLine.Contains(p.BelongProductLineId.Value) && p.Id == workStationId);
            }

            if (result == false)
            {
                result = _workStationUserRep.GetAll().Any(p => p.UserInfoId == userId && p.WorkStationInfoId == workStationId);
            }
            return result;
        }

        public async Task<bool> BingUserAndWorkStationAsync(List<WorkStationUserRelation> newdata, long workStationId)
        {
            var dataBaseUsers = _workStationUserRep.GetAll().Where(p => p.WorkStationInfoId == workStationId).ToList();
            if (newdata == null || newdata.Count == 0)
            {
                foreach (var item in dataBaseUsers)
                {
                    await _workStationUserRep.DeleteAsync(item);
                }

                return true;
            }


            //增加绑定用户
            foreach (var item in newdata)
            {
                if (dataBaseUsers.Any(d => d.UserInfoId == item.UserInfoId) == false)
                {
                    await _workStationUserRep.InsertAsync(item);
                }
            }

            // 删除增绑定用户

            foreach (var item in dataBaseUsers)
            {
                if (newdata.Any(d => d.UserInfoId == item.UserInfoId) == false)
                {
                    await _workStationUserRep.DeleteAsync(item);
                }
            }

            return true;
        }
    }
}
