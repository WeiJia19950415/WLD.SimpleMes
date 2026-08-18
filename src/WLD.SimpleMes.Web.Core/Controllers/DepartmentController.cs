using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.Authorization;
using WLD.SimpleMes.Authorization;
using WLD.SimpleMes.JHTOrganzations.Dto;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Controllers
{
    /// <summary>
    /// 部门管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [AbpAuthorize]
    public class DepartmentController : SimpleMesControllerBase
    {
        private readonly IJHTOrganzationAppService _organzationAppService;
        public DepartmentController(IJHTOrganzationAppService organzationAppService)
        {
            _organzationAppService = organzationAppService;
        }

        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<JHTOrganzationDto>> CreatOrganzation([FromBody] JHTOrganzationDto org)
        {
            return new JHTAjaxResponse<JHTOrganzationDto>()
            {
                Data = await _organzationAppService.CreateAsync(org)
            };
        }

        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_OrgMange)]
        public async Task<JHTAjaxResponse<JHTOrganzationDto>> UpdateOrganzation([FromBody] JHTOrganzationDto org)
        {
            if (org.Id == org.ParentId)
            {
                return new JHTAjaxResponse<JHTOrganzationDto>()
                {
                    Code = 500,
                    Msg = "父级不可设置为自己!"

                };
            }
            return new JHTAjaxResponse<JHTOrganzationDto>()
            {
                Data = await _organzationAppService.UpdateAsync(org)

            };
        }

        /// <summary>
        /// 部门详情
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTAjaxResponse<JHTOrganzationDto>> OrganzationDetail([FromBody] EntityDto<long> org)
        {
            return new JHTAjaxResponse<JHTOrganzationDto>()
            {
                Data = await _organzationAppService.GetAsync(org)
            };
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_OrgMange)]
        public async Task<JHTAjaxResponse<bool>> DelOrganzation([FromBody] EntityDto<long> org)
        {
            await _organzationAppService.DeleteAsync(org);
            return new JHTAjaxResponse<bool>()
            {
                Data = true
            };
        }


        /// <summary>
        /// 移动部门
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_OrgMange)]
        public JHTAjaxResponse<bool> MoveOrganzation([FromBody] MoveOrganzationDto org)
        {

            return new JHTAjaxResponse<bool>()
            {
                Data = _organzationAppService.MoveOrganizationUnit(org)
            };
        }


        /// <summary>
        ///查询部门
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public JHTPageAjaxRespone<PageData<JHTOrganzationDto>> SearchOrganzationAsync([FromBody] JHTPageAjaxResquest<OrganzationConditionDto> org)
        {
            org.PageSize = int.MaxValue;
            return new JHTPageAjaxRespone<PageData<JHTOrganzationDto>>()
            {
                Data = _organzationAppService.SearchOrganzation(org)
            };
        }

        /// <summary>
        ///查询部门
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<List<OrganizationUnitTreeDto>>> GetOrganzationTree()
        {
            var data = await _organzationAppService.GetOrganizationUnitTreeAsync();
            return new JHTPageAjaxRespone<List<OrganizationUnitTreeDto>>()
            {
                Data = data
            };
        }

        /// <summary>
        /// 级联查询部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<List<OrganizationUnitTreeDto>>> GetCasecadeOrganzationTree([FromBody]EntityDto<long> id )
        {
            var data = await _organzationAppService.GetOrganizationUnitTreeAsync(id.Id);
            return new JHTPageAjaxRespone<List<OrganizationUnitTreeDto>>()
            {
                Data = data
            };
        }
    }
}

