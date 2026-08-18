using Abp.Dependency;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Material
{
    public class MaterialCategoryManager : ITransientDependency
    {
        private readonly IRepository<MaterialCategory, long> _repsository;

        private readonly IRepository<MaterialInfo, long> _materialInfoRepository;

        public MaterialCategoryManager(IRepository<MaterialCategory, long> repository, IRepository<MaterialInfo, long> materialInfoRepository)
        {
            _repsository = repository;
            _materialInfoRepository = materialInfoRepository;
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

        public bool IsUsed(string categoryCode)
        {

            return _materialInfoRepository.GetAll().Any(p => p.MaterialCategoryCode.StartsWith(categoryCode));
        }

        public void ChangeParentCode(MaterialCategory dataInfo, string newCategoryCode)
        {
            var parentCode = MaterialCategory.GetParentCode(newCategoryCode);
            var parantCode = this._repsository.FirstOrDefault(p => p.CategoryCode == parentCode);
            var oldCategoryCode = dataInfo.CategoryCode;
            var oldFullCategoryName = dataInfo.FullCategoryName;
            dataInfo.CategoryCode = newCategoryCode;
            dataInfo.FullCategoryName = MaterialCategory.GetFullCatgoryName(parantCode.FullCategoryName, dataInfo.CategoryName);
            
            if (dataInfo.ParentCategoryId != parantCode?.Id)
            {
                dataInfo.ParentCategoryId = parantCode?.Id;
                var childCategory = this._repsository.GetAll().Where(p => p.CategoryCode.StartsWith(oldCategoryCode) && p.CategoryCode != oldCategoryCode);
                foreach (var item in childCategory)
                {
                    item.CategoryCode = item.CategoryCode.Replace(oldCategoryCode, newCategoryCode);
                    item.FullCategoryName = item.FullCategoryName.Replace(oldFullCategoryName, dataInfo.FullCategoryName);
                }
            }
        }

        /// <summary>
        /// 筛选重要的物料
        /// </summary>
        /// <param name="allFormMaterialId"></param>
        /// <returns></returns>
        public List<long> ScreenImportant(List<long> allFormMaterialId)
        {
           return _materialInfoRepository.GetAllIncluding(p => p.BelongCategory)
                .Where(p => allFormMaterialId.Contains(p.Id) && p.BelongCategory.IsKeyMaterial == true)
                .Select(p => p.Id).ToList();
        }
    }
}
