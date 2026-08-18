using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.OnLineStock.Dto
{
    public class LineSideMaterialStatisticsWhereDto
    {
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string WorkOrderNumber { get; set; }

        /// <summary>
        /// 在制品ID
        /// </summary>
        public  long? MaterialInfoId { get; set; }

        public string[] DateRange { get; set; }


        /// </summary>
        public void ParseTime()
        {
            if (DateRange != null && DateRange.Length > 1)
            {
                if (DateTime.TryParse(DateRange[0], out var startDate))
                {
                    this.StartTime = startDate.Date;
                }

                if (DateTime.TryParse(DateRange[1], out var endDate))
                {
                    this.EndTime = endDate.Date.AddDays(1);
                }
            }
            else
            {
                this.StartTime = null;
                this.EndTime = null;
            }
        }
    }
}
