using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Material.Dto;
using WLD.SimpleMes.Users.Dto;

namespace WLD.SimpleMes.Models.PadManageRequest
{
    public class BuildSubMaterialBatchNumberRequest : PadManageRequestModel
    {
        public List<MaterialBatchNumberDto> InputMatreilInfos { get; set; }

        public List<UserDto> Creator { get; set; }

        public long? OnlineMaterialInfoId { get; set; }

        public decimal MatrialCount { get; set; }

        public long? OperateRecordId { get; set; }

        public bool IsRepairedInput { get; set; }
    }
}
