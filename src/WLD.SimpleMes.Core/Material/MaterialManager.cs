using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Material.DomainEvent;

namespace WLD.SimpleMes.Material
{
    public class MaterialManager : ITransientDependency
    {
        private readonly IRepository<MaterialInfo, long> _materialRep;
        private readonly IRepository<CutMaterialConfig, long> _cutMaterialConfigRep;
        public MaterialManager(IRepository<MaterialInfo, long> materialRep, IRepository<CutMaterialConfig, long> cutMaterialConfigRep)
        {
            _materialRep = materialRep;
            _cutMaterialConfigRep = cutMaterialConfigRep;
        }

        /// <summary>
        /// 检查对应物料编号是否唯一
        /// </summary>
        /// <param name="materialNumber"></param>
        /// <param name="maerialId"></param>
        /// <returns></returns>
        public bool CheckUniqueMaterialNumber(string materialNumber, long maerialId = 0)
        {
            if (string.IsNullOrEmpty(materialNumber))
            {
                return false;
            }

            if (maerialId == 0)
            {
                return _materialRep.GetAll().Any(p => p.MaterialNumber == materialNumber);
            }

            return _materialRep.GetAll().Any(p => p.MaterialNumber == materialNumber && p.Id != maerialId);
        }

        /// <summary>
        /// 物料是否被使用
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public bool IsUsedByBom(long materialId)
        {
            // 如果物料有被BOM引
            return false;
        }

        /// <summary>
        /// 根据物料编码获取ID
        /// </summary>
        /// <param name="materialNumber"></param>
        /// <returns></returns>
        public long GetMaterialIdByNumber(string materialNumber, int? TenantId)
        {
            return _materialRep.GetAll().FirstOrDefault(p => p.MaterialNumber == materialNumber && p.TenantId == TenantId).Id;
        }

        public CutMaterialConfig LoadCutMaterialConfig(long proudctId,string configMaterialNumber)
        {
            var cutConfigDto = this._cutMaterialConfigRep.FirstOrDefault(p => p.UsedProductId == proudctId && p.ConfigMaterialNumber == configMaterialNumber);
            if (cutConfigDto == null)
            {
                // 没有产品配置，则看是否有同种类的产品
                var product = _materialRep.FirstOrDefault(p => p.Id == proudctId);
                var parentCategory = MaterialCategory.GetParentCode(product.MaterialNumber);
                cutConfigDto = this._cutMaterialConfigRep.FirstOrDefault(p => p.ProductMaterialNumber.StartsWith(parentCategory) && p.ConfigMaterialNumber == configMaterialNumber);

                if (cutConfigDto == null)
                {
                    // 是否有同类型的产品
                    var configMaterialParantCategory = MaterialCategory.GetParentCode(configMaterialNumber);
                    cutConfigDto = this._cutMaterialConfigRep.FirstOrDefault(p => p.ProductMaterialNumber.StartsWith(parentCategory) && p.ConfigMaterialNumber.StartsWith(configMaterialParantCategory));
                }
            }

            return cutConfigDto;
        }


    }
}
