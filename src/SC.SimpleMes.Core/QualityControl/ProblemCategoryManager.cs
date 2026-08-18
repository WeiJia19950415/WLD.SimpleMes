using Abp.Dependency;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.QualityControl
{
    public class ProblemCategoryManager : ITransientDependency
    {
        private readonly IRepository<ProblemCategory, long> _repsository;

        private readonly IRepository<QualityProblemDefine, long> _poblemInfoRepository;
        public ProblemCategoryManager(IRepository<ProblemCategory, long> repository, IRepository<QualityProblemDefine, long> poblemInfoRepository)
        {
            _repsository = repository;
            _poblemInfoRepository = poblemInfoRepository;
        }

        /// <summary>
        /// 分类编码是否唯一
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsUniqueCategoryCode(string cateogryCode, long id = 0)
        {
            if (id == 0)
            {
                return !_repsository.GetAll().Any(p => p.CategoryCode == cateogryCode);
            }

            return !_repsository.GetAll().Any(p => p.CategoryCode == cateogryCode && p.Id != id);

        }

        /// <summary>
        /// 分类名称是否唯一
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsUniqueCategoryName(string categoryName, long id = 0)
        {
            if (id == 0)
            {
                return !_repsository.GetAll().Any(p => p.CategoryName == categoryName);
            }

            return !_repsository.GetAll().Any(p => p.CategoryName == categoryName && p.Id != id);
        }

        public bool IsUsed(long id)
        {
            return _poblemInfoRepository.GetAll().Any(p => p.ProblemCategoryId == id);
        }

        public void ChangeCategoryCode(ProblemCategory dataInfo, string newCategoryCode)
        {
            var parentCode = ProblemCategory.GetParentCode(newCategoryCode);
            var parantCode = this._repsository.FirstOrDefault(p => p.CategoryCode == parentCode);
            var oldCategoryCode = dataInfo.CategoryCode;
            var oldFullCategoryName = dataInfo.FullCategoryName;
            
            dataInfo.CategoryCode = newCategoryCode;
            dataInfo.FullCategoryName = ProblemCategory.GetFullCatgoryName(parantCode.FullCategoryName, dataInfo.CategoryName);
            
            // 切换父类
            if (dataInfo.ParentCategoryId != parantCode?.Id)
            {
                dataInfo.ParentCategoryId= parantCode?.Id;
                var childCategory = this._repsository.GetAll().Where(p => p.CategoryCode.StartsWith(oldCategoryCode) && p.CategoryCode != oldCategoryCode);
                foreach (var item in childCategory)
                {
                    item.CategoryCode = item.CategoryCode.Replace(oldCategoryCode, newCategoryCode);
                    item.FullCategoryName = item.FullCategoryName.Replace(oldFullCategoryName, dataInfo.FullCategoryName);
                }
            }
        }
    }
}
