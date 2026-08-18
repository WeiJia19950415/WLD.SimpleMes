using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JHT.Abp.CommonModels;
using SC.SimpleMes.Roles.Dto;

namespace SC.SimpleMes.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedRoleResultRequestDto, CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();

        Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input);

        Task<ListResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input);
        /// <summary>
        /// 查询角色分页
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<PageData<RoleDto>> SearchRole(JHTPageAjaxResquest<RoleConditionDto> where);
        /// <summary>
        /// 获取所有的权限
        /// </summary>
        /// <returns></returns>
        ListResultDto<FlatPermissionDto> GetFlatPermissionList();
        /// <summary>
        /// 获取所有的权限(树形)
        /// </summary>
        /// <returns></returns>
        ListResultDto<FlatPermissionTreeDto> GetFlatPermissionTreeList();
    }
}

