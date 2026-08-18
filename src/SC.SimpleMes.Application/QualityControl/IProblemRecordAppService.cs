using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JHT.Abp.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.DTO;
using SC.SimpleMes.QualityControl.Dto;
using SC.SimpleMes.Report.Dto;
using SC.SimpleMes.WorkProcess.Dto;

namespace SC.SimpleMes.QualityControl
{
    public interface IProblemRecordAppService : IAsyncCrudAppService<ProblemRecordDto, long, CommonPageRequestDto, ProblemRecordDto, ProblemRecordDto>, IApplicationService
    {
        /// <summary>
        /// 加载当前工序的异常记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ProblemRecordDto> LoadCurrentWorkProcessProblemRecord(string productBatchNumber);

        /// <summary>
        /// 加载当前问题的处理解决方案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ProblemDealRecordDto LoadCurrentWorkProcessProblemDealRecord(InputOperatorRecordInfo input);


        Task<PageData<View_ProblemRecordDto>> LoadQualityDetailsRecordsAsync(JHTPageAjaxResquest<ProblemRecordQueryCondition> where);

        ProblemDealRecordDto LoadProblemDealRecordByProblemId(EntityDto<long> entityDto);
        List<ProblemDealRecordDto> LoadProblemDealRecords(EntityDto<string> id);
    }
}
