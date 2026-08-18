using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using SC.SimpleMes.Authorization;
using SC.SimpleMes.Roles;
using SC.SimpleMes.Roles.Dto;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Controllers
{
    /// <summary>
    ///  角色管理
    /// </summary>
    [Authorize]
    [Route("api/[controller]/[action]")]
    [AbpMvcAuthorize(PermissionNames.Pages_Roles)]
    public class RoleController : SimpleMesControllerBase
    {
        private readonly IRoleAppService _roleAppService;


        public RoleController(IRoleAppService roleAppService
           )
        {
            _roleAppService = roleAppService;

        }

        /// <summary>
        /// 获取角色分页列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<PageData<RoleDto>>> SearchRole([FromBody] JHTPageAjaxResquest<RoleConditionDto> where)
        {
            return new JHTPageAjaxRespone<PageData<RoleDto>>()
            {
                Data = await _roleAppService.SearchRole(where)
            };
        }
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<RoleDto>> CreateRole([FromBody] CreateRoleDto dto)
        {
            return new JHTAjaxResponse<RoleDto>()
            {
                Data = await _roleAppService.CreateAsync(dto)
            };
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<RoleDto>> UpdateRole([FromBody] RoleDto dto)
        {
            return new JHTAjaxResponse<RoleDto>()
            {
                Data = await _roleAppService.UpdateAsync(dto)
            };
        }
        /// <summary>
        /// 获取角色详情
        /// </summary>
        /// <param name="dto">如果新增页面，传空调用</param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTAjaxResponse<GetRoleForEditOutput>> GetRole([FromBody] EntityDto dto)
        {
            return new JHTAjaxResponse<GetRoleForEditOutput>()
            {
                Data = await _roleAppService.GetRoleForEdit(dto)
            };
        }
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> DeleteRole([FromBody] EntityDto<int> dto)
        {
            await _roleAppService.DeleteAsync(dto);
            return new JHTAjaxResponse();
        }
        /// <summary>
        /// 获取所有的权限(评级列表)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTAjaxResponse<List<FlatPermissionDto>> GetFlatPermissionList()
        {
            var data = _roleAppService.GetFlatPermissionList();
            return new JHTAjaxResponse<List<FlatPermissionDto>>() { Data = data.Items.ToList() };
        }

        /// <summary>
        /// 获取所有的权限(树形)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTAjaxResponse<List<FlatPermissionTreeDto>> GetFlatPermissionTreeList()
        {
            var data = _roleAppService.GetFlatPermissionTreeList();
            return new JHTAjaxResponse<List<FlatPermissionTreeDto>>() { Data = data.Items.ToList() };
        }
    }
}

