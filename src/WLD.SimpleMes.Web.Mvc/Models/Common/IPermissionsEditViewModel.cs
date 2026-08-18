using System.Collections.Generic;
using WLD.SimpleMes.Roles.Dto;

namespace WLD.SimpleMes.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}
