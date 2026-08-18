using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JHT.Abp.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.JHTOrganzations.Dto
{
    public interface IJHTOrganzationAppService : IAsyncCrudAppService<JHTOrganzationDto, long, PagedResultRequestDto, JHTOrganzationDto, JHTOrganzationDto>
    {
        /// <summary>
        /// 查询组织关系
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        PageData<JHTOrganzationDto> SearchOrganzation(JHTPageAjaxResquest<OrganzationConditionDto> org);

        /// <summary>
        /// 移动部门
        /// </summary>
        /// <param name="newPrentId">新的父级部门ID</param>
        /// <param name="orgId">移动的部门ID</param>
        /// <returns></returns>
        bool MoveOrganizationUnit(MoveOrganzationDto org);
        /// <summary>
        /// 获取部门树形
        /// </summary>
        /// <returns></returns>
        Task<List<OrganizationUnitTreeDto>> GetOrganizationUnitTreeAsync(long id);

        Task<List<OrganizationUnitTreeDto>> GetOrganizationUnitTreeAsync();
    }
}

