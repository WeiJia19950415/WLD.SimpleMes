using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JHT.Abp.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.DTO;
using SC.SimpleMes.DynamicForms.DTO;
using SC.SimpleMes.WorkProcess;

namespace SC.SimpleMes.DynamicForms
{
    public interface IFormTemplateInfoAppService : IAsyncCrudAppService<FormTemplateInfoDto, long, CommonPageRequestDto, FormTemplateInfoDto, FormTemplateInfoDto>
    {
        List<FormTemplateBasicInfoDto> SearchFromtelateInfoHistory(EntityDto<string> entityDto);
        JHTAjaxResponse<FormInfoRecordDto> LoadFormInfoRecordInfo(WorkProcess.Dto.InputOperatorRecordInfo inputOperatorRecordInfo, FormUseTypeEnum formUseType = FormUseTypeEnum.标准工序填报);
    }
}
