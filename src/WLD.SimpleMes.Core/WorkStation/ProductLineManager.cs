using Abp.Dependency;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkStation
{
    public class ProductLineManager : ITransientDependency
    {
        private readonly IRepository<ProductLineUserRelation, long> _productLineUserRep;
        private readonly IRepository<ProductLine, long> _productLineRep;
        public ProductLineManager(IRepository<ProductLineUserRelation, long> productLineUserRep, IRepository<ProductLine, long> productLineRep)
        {
            this._productLineRep = productLineRep;
            this._productLineUserRep = productLineUserRep;
        }

        /// <summary>
        /// 检查产线编号是否唯一
        /// </summary>
        /// <param name="productLineNumber"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckProductLineNumberIsUnique(string productLineNumber, long? id)
        {
            if (id == null || id == 0)
            {
                return !this._productLineRep.GetAll().Any(p => p.ProductLineNumber == productLineNumber);
            }

            return !this._productLineRep.GetAll().Any(p => p.ProductLineNumber == productLineNumber && p.Id != id);
        }

        /// <summary>
        /// 绑定产线操作人员
        /// </summary>
        /// <param name="bindUsers"></param>
        /// <returns></returns>
        public async Task<bool> BindOperatorUser(List<ProductLineUserRelation> bindUsers,long productLineId)
        {
            var dataBaseUsers = _productLineUserRep.GetAll().Where(p => p.ProductLineId == productLineId).ToList();

            if (bindUsers == null || bindUsers.Count == 0)
            {
                foreach (var item in dataBaseUsers)
                {
                    await _productLineUserRep.DeleteAsync(item);
                }
                return true;
            }


            // 移除绑定用户
            foreach (var item in bindUsers)
            {
                if (dataBaseUsers.Any(d => d.UserInfoId == item.UserInfoId) == false)
                {
                    await _productLineUserRep.InsertAsync(item);
                }
            }

            // 添加新增绑定用户
            foreach (var item in dataBaseUsers)
            {
                if (bindUsers.Any(d => d.UserInfoId == item.UserInfoId) == false)
                {
                    await _productLineUserRep.DeleteAsync(item);
                }
            }

            return true;
        }

        /// <summary>
        /// 获取用户管辖的产线
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<ProductLine> GetUserMangedProductLines(long? userId)
        {
            return this._productLineUserRep
                .GetAllIncluding(p => p.ProductLine)
                .Where(p => p.UserInfoId == userId)
                .Select(p => p.ProductLine).ToList();
        }


        /// <summary>
        /// 获取管辖产线的用户Id
        /// </summary>
        /// <param name="productLineId"></param>
        /// <returns></returns>
        public List<long> GetProductLineMangeUsersId(long productLineId)
        {
            return this._productLineUserRep
                .GetAll()
                .Where(p => p.ProductLineId == productLineId)
                .Select(p => p.UserInfoId).ToList();
        }
    }
}
