using Abp.Dependency;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.QualityControl.Dto;

namespace WLD.SimpleMes.QualityControl
{
    public interface IProblemCategoryCache : ITransientDependency, IEventHandler<EntityChangedEventData<ProblemCategory>>
    {
        List<UICascaderModel<string, string>> LoadAllProbleCasclaeInfo();

        List<ProblemCategoryDto> GetAllProblemCategory();
    }
}
