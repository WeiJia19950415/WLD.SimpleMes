using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JHT.Abp.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.DynamicForms.DTO;
using WLD.SimpleMes.WorkProcess;

namespace WLD.SimpleMes.DynamicForms
{
    public interface IFormTemplateInfoAppService : IAsyncCrudAppService<FormTemplateInfoDto, long, CommonPageRequestDto, FormTemplateInfoDto, FormTemplateInfoDto>
    {
        List<FormTemplateBasicInfoDto> SearchFromtelateInfoHistory(EntityDto<string> entityDto);
        JHTAjaxResponse<FormInfoRecordDto> LoadFormInfoRecordInfo(WorkProcess.Dto.InputOperatorRecordInfo inputOperatorRecordInfo, FormUseTypeEnum formUseType = FormUseTypeEnum.标准工序填报);
    }
}
