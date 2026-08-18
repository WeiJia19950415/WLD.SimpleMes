using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.WorkOrder;
using WLD.SimpleMes.WorkProcess.Dto;

namespace WLD.SimpleMes.Material.Dto
{
    [ExcelExporter(Author = "四川伟力得", ExcelOutputType = ExcelOutputTypes.None, AutoFitAllColumn = false, AutoFitMaxRows = 5000, AutoCenter = true)]
    public class MaterialBatchNumberExportDto
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }

        public string MaterialNumber { get; set; }

        public string MaterialName { get; set; }

        /// <summary>
        /// 批次编码：每种类型的批次编码生成规则不一致
        /// </summary>
        public string BatchNumber { get; set; }


        /// <summary>
        /// ERP中的原材料批次号
        /// </summary>
        public string FromErpBatchNumber { get; set; }

        /// <summary>
        /// 打印次数
        /// </summary>
        public int PrintTimes { get; set; }

        /// <summary>
        /// 相关物料数量 默认为1
        /// </summary>
        public decimal MatrialCount { get; set; } = 1;

        /// <summary>
        /// 包装单位
        /// </summary>
        public string WrapUniteName { get; set; } = "";

        /// <summary>
        /// 创建工位名称
        /// </summary>
        public string CreateWorkStationName { get; set; }
        /// <summary>
        /// 订单来源
        /// 生产订单：则需要进行关联检查
        /// 采购入库订单
        /// </summary>
        public string FromOrderNumber { get; set; }

        /// <summary>
        /// 最后一次打印时间
        /// </summary>
        /// <summary>
        /// 创建时间
        /// </summary>
        [ExporterHeader(DisplayName = "打印日期", Format = "yyyy-MM-dd")]
        public DateTime? LastPrintTime { get; set; }

        /// <summary>
        /// 投入物料数量(BOM单中的数量)
        /// </summary>
        [ExporterHeader(DisplayName = "加工数量")]
        public decimal BOMMaterialCount { get; set; }

        /// <summary>
        /// 投入数量单位(BOM单中的单位)
        /// </summary>
        [ExporterHeader(DisplayName = "单位")]
        public string BOMUnitName { get; set; }

        [ExporterHeader(DisplayName = "创建时间", Format = "yyyy-MM-dd HH:mm:ss" )]
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// 该二维码是否被使用
        /// </summary>
        [ExporterHeader(IsIgnore =true)]
        public int IsUsed { get; set; }

        [ExporterHeader(DisplayName = "创建时间", Format = "yyyy-MM-dd HH:mm:ss")]
        public string IsUsedString
        {
            get
            {
                if (this.IsUsed == 1)
                {
                    return "已使用";
                }

                return "未使用";
            }
        }
    }
}
