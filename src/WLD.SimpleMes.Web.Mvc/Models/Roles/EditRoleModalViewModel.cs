using Abp.AutoMapper;
using WLD.SimpleMes.Roles.Dto;
using WLD.SimpleMes.Web.Models.Common;

namespace WLD.SimpleMes.Web.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class EditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool HasPermission(FlatPermissionDto permission)
        {
            return GrantedPermissionNames.Contains(permission.Name);
        }
    }
}

