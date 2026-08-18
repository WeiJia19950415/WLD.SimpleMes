using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Material.Dto
{
    public class MaterialConditionDto
    {
        public string KeyWord { get; set; }
        public List<MaterialTypeEnum> MaterialType { get; set; }
    }
}
