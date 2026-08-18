using Abp.Dependency;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.DTO;
using SC.SimpleMes.QualityControl.Dto;

namespace SC.SimpleMes.QualityControl
{
    public interface IProblemCategoryCache : ITransientDependency, IEventHandler<EntityChangedEventData<ProblemCategory>>
    {
        List<UICascaderModel<string, string>> LoadAllProbleCasclaeInfo();

        List<ProblemCategoryDto> GetAllProblemCategory();
    }
}
