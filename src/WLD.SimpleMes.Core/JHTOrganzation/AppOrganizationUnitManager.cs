using Abp.Domain.Repositories;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.JHTOrganzation
{
    public class AppOrganizationUnitManager : OrganizationUnitManager
    {
        private readonly IRepository<JHTOrganzation, long> JHTOrgUnitRepository;
        public AppOrganizationUnitManager(
            IRepository<JHTOrganzation, long> jhtRepository,
            IRepository<OrganizationUnit, long> organizationUnitRepository)
            : base(organizationUnitRepository)
        {
            this.JHTOrgUnitRepository = jhtRepository;
        }

        /// <summary>
        /// 移动部门
        /// </summary>
        /// <param name="newParentId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public bool MoveOrg(long? newParentId, long orgId)
        {
            string NewCode = GenerateOrgCode(newParentId);
            var org = JHTOrgUnitRepository.Get(orgId);
            org.Code = NewCode;
            org.ParentId = newParentId;
            JHTOrgUnitRepository.Update(org);
            CurrentUnitOfWork.SaveChanges();
            ModifyOrgChildrenCode(orgId);
            return true;
        }
        /// <summary>
        /// 修改子部门的Code
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="OldParentCode"></param>
        /// <param name="NewParentCode"></param>
        /// <returns></returns>
        private void ModifyOrgChildrenCode(long orgId)
        {
            var ChildreList = FindChildren(orgId, false);
            if (ChildreList != null && ChildreList.Count > 0)
            {
                foreach (var item in ChildreList)
                {
                    var model = JHTOrgUnitRepository.Get(item.Id);
                    model.Code = GenerateOrgCode(orgId);
                    JHTOrgUnitRepository.Update(model);
                    CurrentUnitOfWork.SaveChanges();
                    ModifyOrgChildrenCode(item.Id);
                }
            }
        }
        /// <summary>
        /// 生成组织机构代码
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public string GenerateOrgCode(long? parentId)
        {
            var resultCode = "";
            if (parentId != null && parentId != 0)
            {
                var brotherOrgUto = OrganizationUnitRepository.GetAll().OrderByDescending(p => p.CreationTime).FirstOrDefault(p => p.ParentId == parentId);
                var paretnOrg = OrganizationUnitRepository.FirstOrDefault(parentId.Value);
                if (brotherOrgUto == null)
                {
                    resultCode = JHTOrganzation.AppendCode(paretnOrg.Code, JHTOrganzation.CreateCode(1));
                }
                else
                {

                    if (brotherOrgUto.Code.StartsWith(paretnOrg.Code))
                    {
                        resultCode = JHTOrganzation.CalculateNextCode(brotherOrgUto.Code);
                    }
                    else
                    {

                        var maxcode = OrganizationUnitRepository.GetAll().OrderByDescending(p => p.Code).FirstOrDefault(p => p.Code.StartsWith(paretnOrg.Code) && p.ParentId == parentId);
                        if (maxcode == null)
                        {

                            resultCode = JHTOrganzation.AppendCode(paretnOrg.Code, JHTOrganzation.CreateCode(1));
                        }
                        else
                        {
                            resultCode = JHTOrganzation.CalculateNextCode(maxcode.Code);
                        }
                    }

                }
            }

            if (parentId == null || parentId == 0)
            {
                parentId = null;
                var brotherOrgUto = OrganizationUnitRepository.GetAll().OrderByDescending(p => p.CreationTime).FirstOrDefault(p => p.ParentId == parentId);
                if (brotherOrgUto == null)
                {
                    resultCode = JHTOrganzation.CreateCode(1);
                }
                else
                {
                    resultCode = JHTOrganzation.CalculateNextCode(brotherOrgUto.Code);
                }
            }

            return resultCode;
        }

        public JHTOrganzation FindById(long id)
        {
            return this.JHTOrgUnitRepository.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// 查询部门子集
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="recursive">是否需要递归</param>
        /// <returns></returns>
        public new async Task<List<JHTOrganzation>> FindChildrenAsync(long? parentId, bool recursive = false)
        {
            if (!recursive)
            {
                return await JHTOrgUnitRepository.GetAllListAsync(ou => ou.ParentId == parentId);
            }

            if (!parentId.HasValue)
            {
                return JHTOrgUnitRepository.GetAllIncluding(p => p.Children).Where(p => p.ParentId == parentId && p.IsDeleted == false).ToList();
            }

            var code = await GetCodeAsync(parentId.Value);

            return await JHTOrgUnitRepository.GetAllListAsync(
                ou => ou.Code.StartsWith(code) && ou.Id != parentId.Value
            );
        }
    }
}

