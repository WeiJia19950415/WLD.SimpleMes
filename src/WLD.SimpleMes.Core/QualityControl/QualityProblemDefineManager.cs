using Abp.Dependency;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.QualityControl
{
    public class QualityProblemDefineManager : ITransientDependency
    {
        private readonly IRepository<QualityProblemDefine, long> _repository;
        private readonly IRepository<ProblemRecord, long> _recordRep;
        public QualityProblemDefineManager(IRepository<QualityProblemDefine, long> repository, IRepository<ProblemRecord, long> recordRep)
        {
            _repository = repository;
            _recordRep = recordRep;
        }

        public bool IsExistDefineName(long problemCategoryId, string name, long id = 0)
        {
            if (id == 0)
            {
                return _repository.GetAllIncluding(p => p.ProblemCategory).Any(p => p.ProblemCategoryId == problemCategoryId && p.ProbleName == name);
            }

            return _repository.GetAllIncluding(p => p.ProblemCategory).Any(p => p.ProblemCategoryId == problemCategoryId && p.ProbleName == name && p.Id != id);
        }

        public bool IsUsed(long id)
        {
            return _recordRep.GetAll().Any(p => p.BelongProblemDefineId == id);
        }

        public List<QualityProblemDefine> GetProblemDefineByCatetoeryCode(string categoryCode)
        {
            return _repository.GetAll().Where(p => p.QualityProblemNumber.StartsWith(categoryCode)).ToList();
        }
    }
}
