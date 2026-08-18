using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.LineSideWarehouse;

namespace WLD.SimpleMes.OnLineStock.Dto
{
    [AutoMap(typeof(LineSideMaterialInfo))]
    public class LineSideMaterialInfoDto : EntityDto<long>
    {
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 物料单位
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specification { get; set; }

        public string MaterialNumber { get; set; }


        public string ShowMaterialNumber
        {
            get
            {
                if (!string.IsNullOrEmpty(MaterialNumber) && !string.IsNullOrEmpty(BelongCategoryNumber))
                {
                    var materialNumberInfo = MaterialNumber.Split('.');
                    return materialNumberInfo[materialNumberInfo.Length - 1];
                }

                return string.Empty;
            }
        }

        public string BelongCategoryNumber { get; set; }
    }
}
