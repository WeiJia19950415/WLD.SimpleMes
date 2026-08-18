using Abp.Domain.Repositories;
using Abp.Events.Bus.Entities;
using Abp.ObjectMapping;
using Abp.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.DTO;
using SC.SimpleMes.QualityControl.Dto;

namespace SC.SimpleMes.QualityControl
{
    public class ProblemCategoryCache : IProblemCategoryCache
    {
        protected readonly string ProblemCategoryCacheKey = typeof(ProblemCategoryCache).FullName;
        protected readonly string ProblemCategoryCacheCascaldKey = typeof(ProblemCategoryCache).FullName + "_Cacascald";
        protected readonly ITypedCache<string, List<ProblemCategoryDto>> InternalCache;
        protected readonly ITypedCache<string, List<UICascaderModel<string, string>>> CascaldCache;
        private readonly IRepository<ProblemCategory, long> _repository;
        protected readonly IObjectMapper ObjectMapper;

        public ProblemCategoryCache(ICacheManager cacheManager, IRepository<ProblemCategory, long> repository, IObjectMapper objectMapper)
        {
            _repository = repository;
            ObjectMapper = objectMapper;
            InternalCache = cacheManager.GetCache<string, List<ProblemCategoryDto>>(this.ProblemCategoryCacheKey);
            CascaldCache = cacheManager.GetCache<string, List<UICascaderModel<string, string>>>(this.ProblemCategoryCacheCascaldKey);
        }

        public void HandleEvent(EntityChangedEventData<ProblemCategory> eventData)
        {
            InternalCache.Remove(ProblemCategoryCacheKey);
            CascaldCache.Remove(ProblemCategoryCacheCascaldKey);
        }

        public List<UICascaderModel<string, string>> LoadAllProbleCasclaeInfo()
        {
            List<UICascaderModel<string, string>> resutl = new List<UICascaderModel<string, string>>();

            return CascaldCache.Get(ProblemCategoryCacheCascaldKey, p =>
            {
                var categoryies = GetAllProblemCategory();
                if (categoryies != null)
                {
                    var topLevel = categoryies.Where(p => p.ParentCategoryId == null || p.ParentCategoryId == 0).ToList();
                    foreach (var item in topLevel)
                    {
                        var subItem = new UICascaderModel<string, string>() { Label = item.CategoryName, Value = item.CategoryCode };
                        subItem.Children = LoadChildren(categoryies, item.Id);
                        // item.Children = ;
                        resutl.Add(subItem);
                    }
                }

                return resutl;

            });

        }

        public List<ProblemCategoryDto> GetAllProblemCategory()
        {
            return InternalCache.Get(ProblemCategoryCacheKey, p =>
             {
                 return ObjectMapper.Map<List<ProblemCategoryDto>>(_repository.GetAll().ToList());
             });
        }

        public List<UICascaderModel<string, string>> LoadChildren(List<ProblemCategoryDto> categoryies, long parentId)
        {
            var children = categoryies.Where(p => p.ParentCategoryId == parentId).ToList();
            if (children != null)
            {
                List<UICascaderModel<string, string>> cascaderModels = new List<UICascaderModel<string, string>>();
                foreach (var item in children)
                {
                    var subItem = new UICascaderModel<string, string>() { Label = item.CategoryName, Value = item.CategoryCode };
                    subItem.Children = LoadChildren(categoryies, item.Id);
                    cascaderModels.Add(subItem);
                }

                return cascaderModels;
            }

            return null;
        }
    }
}
