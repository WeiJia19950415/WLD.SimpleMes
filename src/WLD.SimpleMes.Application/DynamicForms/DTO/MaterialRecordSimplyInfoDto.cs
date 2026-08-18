using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.DynamicForms.DTO
{
    public class MaterialRecordSimplyInfoDto
    {
        /// <summary>
        /// 物料名称
        /// </summary>
        public string InputMatreialName { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        public string MaterialNumber { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime WarehousingTime { get; set; }

        /// <summary>
        /// 入库批次号
        /// </summary>
        public string BatchNo { get; set; }
    }
}
