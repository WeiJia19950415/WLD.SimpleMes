using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.QualityControl.Dto
{
    public class ProblemDealRecordDto : EntityDto<long>
    {
        public ProblemRecordDto Record { get; set; }
        public long ProblemRecordId { get; set; }
        public long OperatorId { get; set; }
        public string OperatorName { get; set; }

        public DateTime DealTime { get; set; }

        public ProblemDealTypeEnum ProblemDealType { get; set; }

        public string ProblemDealTypeEnumString
        {
            get
            {

                return this.ProblemDealType.ToString();
            }
        }

        public long? WorkStationId { get; set; }

        public long? StartWorkProcessId { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string OperatorDescreption { get; set; }

        public string AnalysisContent { get; set; }

        public List<String> RelationImgs { get; set; }

    }
}
