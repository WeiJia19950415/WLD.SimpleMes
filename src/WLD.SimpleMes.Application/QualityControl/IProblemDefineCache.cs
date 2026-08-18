using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.QualityControl.Dto;

namespace WLD.SimpleMes.QualityControl
{
    public interface IProblemDefineCache : IEntityCache<ProblemDefineDto, long>
    {

    }
}
