using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.WorkOrder.DTO
{
    public class CreateWorkOrderBatchNumberDto : EntityDto<long>
    {
        public long? CreateWorkStationId {  get; set; }
        public string CreateWorkStationName { get; set; }

        /// <summary>
        /// 班次diam
        /// </summary>
        public string ShiftCode { get; set; }

        public long? ProductLineId { get; set; }

        public string BatchNumber { get; set; }

        public long MaterialCount { get; set; } = 1;
    }
}
