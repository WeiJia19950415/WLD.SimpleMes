using Abp.Dependency;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Material
{
    public class MaterialBatchNumberRulerManager : ITransientDependency
    {
        private readonly IRepository<MaterialBatchNumberRuler, long> _repository;
        public MaterialBatchNumberRulerManager(IRepository<MaterialBatchNumberRuler, long> repository)
        {
            _repository = repository;
        }

        public bool IsExistMaterialBatchNumerRuler(long materialId)
        {
            return _repository.GetAll().Any(p=>p.MaterialCategoryInfoId == materialId);
        }
    }
}
