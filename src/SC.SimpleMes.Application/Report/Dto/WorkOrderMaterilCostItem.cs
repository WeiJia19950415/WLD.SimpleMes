using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Report.Dto
{
    /// <summary>
    /// 工单物料裁切使用报表
    /// </summary>
    public class WorkOrderMaterilCostItem
    {
        /// <summary>
        /// 工单编号
        /// </summary>
        public string WorkOrderNumber { get; set; }

        /// <summary>
        /// 工序名称
        /// </summary>
        public string WorkProcessName { get; set; }

        /// <summary>
        /// 构成物料Id
        /// </summary>
        public long FormMaterialId { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        public string FormMaterialNumber { get; set; }

        // <summary>
        /// 物料名称
        /// </summary>
        public string FormMaterialName { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 工单加工数量
        /// </summary>
        public decimal WorkOrderCount { get; set; }

        /// <summary>
        /// 相关物料数量 默认为1
        /// </summary>
        public decimal MatrialCount { get; set; } = 1;

        /// <summary>
        /// 包装单位
        /// </summary>
        public string WrapUniteName { get; set; } = "";

        /// <summary>
        /// 投入物料数量(BOM单中的数量)
        /// </summary>
        public decimal BOMMaterialCount { get; set; }

        /// <summary>
        /// 投入数量单位(BOM单中的单位)
        /// </summary>
        public string BOMUnitName { get; set; }

        /// <summary>
        /// 使用比
        /// </summary>
        public string UsedRate
        {
            get
            {
                if (WorkOrderCount == 0)
                {
                    return "临时用料";
                }
                return (BOMMaterialCount / WorkOrderCount).ToString("0.00");
            }
        }
    }
}
