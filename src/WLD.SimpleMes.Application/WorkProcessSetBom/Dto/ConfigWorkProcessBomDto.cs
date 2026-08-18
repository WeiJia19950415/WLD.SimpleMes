using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkProcessSetBom.Dto
{
    public class ConfigWorkProcessBomDto
    {
        /// <summary>
        /// 工艺BOMId
        /// </summary>
        public long Id { get; set; }

        public List<ConfigWorkProcessSetItemDto> Item { get; set; }
    }

    public class ConfigWorkProcessSetItemDto
    {
        /// <summary>
        /// 工序ID
        /// </summary>
        public long Id { get; set; }

        public string ProcessName { get; set; }

        public string ProcessNumber { get; set; }

        public bool CanJump { get; set; }

        public bool IsEnable { get; set; }

        public List<ConfigWorkProcessSetBOMItemDto> BomItem { get; set; }
    }


    public class ConfigWorkProcessSetBOMItemDto
    {
        public decimal FormCount { get; set; }

        public string FormMaterialName { get; set; }

        public string FormMaterialNumber { get; set; }
    }
}
