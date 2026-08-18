using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Material;

namespace SC.SimpleMes.EntityFrameworkCore.Seed
{
    public class MaterialBuilder
    {
        private readonly SimpleMesDbContext _context;
        private readonly int _tenantId;

        public MaterialBuilder(SimpleMesDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateMaterialCategory();
        }

        protected void CreateMaterialCategory()
        {
            var category = _context.MaterialCategories.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.CategoryCode == MaterialCategory.DefaultStacksCategoryCode);
            if (category == null)
            {
                category = _context.MaterialCategories.Add(new MaterialCategory()
                {
                    CategoryCode = MaterialCategory.DefaultStacksCategoryCode,
                    CategoryDescription = "成品_电堆",
                    CategoryName = "电堆",
                    FullCategoryName = "成品_电堆",
                    IsKeyMaterial = false,
                    TenantId = _tenantId,
                }).Entity;

                _context.SaveChanges();
            }

            var feature = _context.TenantFeatureSettings.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == "JHT.QuartzFeature");
            if ((feature == null))
            {
                feature = _context.TenantFeatureSettings.Add(new Abp.MultiTenancy.TenantFeatureSetting()
                {
                    TenantId = _tenantId,
                    Name = "JHT.QuartzFeature",
                    Value = "true",

                }).Entity;
            }
        }

    }
}
