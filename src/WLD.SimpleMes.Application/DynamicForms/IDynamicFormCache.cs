using Abp.Domain.Entities.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.DynamicForms.DTO;

namespace WLD.SimpleMes.DynamicForms
{
    public interface IDynamicFormCache : IEntityCache<FormTemplateInfoDto, long>
    {
    }
}
