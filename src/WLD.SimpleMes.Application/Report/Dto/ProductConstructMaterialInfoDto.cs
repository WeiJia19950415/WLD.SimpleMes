using Abp.Domain.Entities;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.WorkOrder;

namespace WLD.SimpleMes.Report.Dto
{
    public class ProductConstructMaterialInfoDto : Entity<long>
    {

        public string BatchNo { get; set; }


        public string MaterialName { get; set; }

        public string MaterialNumber { get; set; }

        public string InputMaterialNumber { get; set; }

        public string InputMaterialName { get; set; }

        public string MaterialBatchNumber { get; set; }
        public ProduceStatusEnum ProduceStatus { get; set; }

        public string ProduceStatusString
        {
            get
            {
                return this.ProduceStatus.ToString();
            }
        }

        public string ProductLineName { get; set; }

        public string ProcessName { get; set; }

        public string ProjectName { get; set; }

        public string ProjectNumber { get; set; }

    }
}
