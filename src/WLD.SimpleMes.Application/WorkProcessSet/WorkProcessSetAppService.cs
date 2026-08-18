using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using JHT.Abp.CommonModels;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Authorization;
using WLD.SimpleMes.BOM;
using WLD.SimpleMes.BOM.Dto;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.WorkProcess;
using WLD.SimpleMes.WorkProcess.Dto;
using WLD.SimpleMes.WorkProcessSet.Dto;

namespace WLD.SimpleMes.WorkProcessSet
{
    public class WorkProcessSetAppService : AsyncCrudAppService<WorkProcess.WorkProcessSet, WorkProcessSetInfoDto, long, DTO.CommonPageRequestDto, WorkProcessSetInfoDto, WorkProcessSetInfoDto>, IWorkProcessSetAppService
    {
        private readonly ProcessSetManager _processSetManager;
        private readonly BomUnitManager _bomUnitManager;
        private readonly IRepository<WorkProcessSetProductRelation, long> _workProcessProductRepository;
        public WorkProcessSetAppService(
            IRepository<WorkProcess.WorkProcessSet, long> repository,
            IRepository<WorkProcessSetProductRelation, long> workProcessProductRepository,
            BomUnitManager bomUnitManager,
            ProcessSetManager processSetManager) : base(repository)
        {
            _processSetManager = processSetManager;
            _bomUnitManager = bomUnitManager;
            _workProcessProductRepository = workProcessProductRepository;
        }

        protected override IQueryable<WorkProcess.WorkProcessSet> CreateFilteredQuery(CommonPageRequestDto input)
        {
            var query = base.CreateFilteredQuery(input);
            var conditon = input.QueryConditionObj as CommonConditionData;
            query = query.WhereIf(!string.IsNullOrEmpty(conditon.KeyWord), p => p.SetName.Contains(conditon.KeyWord));
            return query;
        }

        public PageData<WorkProcessSetInfoDto> GetWorkProcessSetPageData(JHTPageAjaxResquest<CommonConditionData> pageAjaxResquest)
        {
            var conditon = pageAjaxResquest.Condition as CommonConditionData;
            var query = this.Repository.GetAll().WhereIf(!string.IsNullOrEmpty(conditon.KeyWord), p => p.SetName.Contains(conditon.KeyWord));
            var result = new PageData<WorkProcessSetInfoDto>()
            {

                List = query.Skip(pageAjaxResquest.SkipCount).Take(pageAjaxResquest.PageSize).Select(p => new WorkProcessSetInfoDto()
                {
                    Descreption = p.Descreption,
                    SetName = p.SetName,
                    SetVersion = p.SetVersion,
                    Id = p.Id,
                    TenantId = p.TenantId,
                }).ToList(),
                Total = query.Count(),

            };

            return result;
        }

        [AbpAuthorize(PermissionNames.Page_ProcessSetManage)]
        public override Task<WorkProcessSetInfoDto> CreateAsync(WorkProcessSetInfoDto input)
        {
            if (_processSetManager.CheckUinque(input.SetName, input.SetVersion) == false)
            {
                throw new UserFriendlyException("工艺名称与版本重复");
            }
            input.TenantId = AbpSession.TenantId;
            return base.CreateAsync(input);
        }

        [AbpAuthorize(PermissionNames.Page_ProcessSetManage)]
        public override async Task<WorkProcessSetInfoDto> UpdateAsync(WorkProcessSetInfoDto input)
        {
            if (_processSetManager.CheckUinque(input.SetName, input.SetVersion, input.Id) == false)
            {
                throw new UserFriendlyException("工艺名称与版本重复");
            }

            _processSetManager.AddWorkProcessSetBasicInfo(input.Id, input.SetName, input.SetVersion, input.Descreption);

            return input;
        }

        [AbpAuthorize(PermissionNames.Page_ProcessSetManage)]
        public override Task DeleteAsync(EntityDto<long> input)
        {
            if (_processSetManager.IsProcessSetIsUsed(input.Id))
            {
                throw new UserFriendlyException("该工艺已经被使用不允许删除！");
            }

            return base.DeleteAsync(input);
        }

        [AbpAuthorize(PermissionNames.Page_ProcessSetManage)]
        public async Task<WorkProcessSetInfoDto> CopyWorkProcessSetAsync(EntityDto<long> entityDto)
        {
            var processSet = Repository.FirstOrDefault(p => p.Id == entityDto.Id);
            return ObjectMapper.Map<WorkProcessSetInfoDto>(await _processSetManager.CopyWorkProcessSetAsync(new WorkProcess.WorkProcessSet()
            {
                ExtensionData = processSet.ExtensionData,
                GraphData = processSet.GraphData,
                CreationTime = DateTime.Now,
                CreatorUserId = AbpSession.UserId,
                SetName = processSet.SetName,
                TenantId = AbpSession.TenantId,
            }));
        }

        [AbpAuthorize(PermissionNames.Page_ProcessSetManage)]
        public async Task<JHTAjaxResponse> SetProductProcessSetAsync(SetProductProcessSetDto setProductProcessSetDto)
        {
            JHTAjaxResponse response = new JHTAjaxResponse();
            if (_processSetManager.IsExistProductProcessSet(setProductProcessSetDto.BelongWorkProcessSetId, setProductProcessSetDto.MaterialInfoId))
            {
                response.Msg = "已绑定该产品与工艺，请勿重复添加！";
                response.Code = 500;
                return response;
            }

            var processSet = Repository.FirstOrDefault(p => p.Id == setProductProcessSetDto.BelongWorkProcessSetId);
            if (string.IsNullOrEmpty(processSet.ExtensionData))
            {
                response.Msg = "工艺未绑定相关配置，请先完善工艺流程！";
                response.Code = 500;
                return response;
            }

            await _processSetManager.SetProductProcessSetAsync(new WorkProcessSetProductRelation()
            {
                BelongWorkProcessSetId = setProductProcessSetDto.BelongWorkProcessSetId,
                MaterialInfoId = setProductProcessSetDto.MaterialInfoId,
                CreationTime = DateTime.Now,
                CreatorUserId = AbpSession.UserId,
                IsCurrent = false
            });

            response.Msg = "操作成功";
            return response;
        }

        [AbpAuthorize(PermissionNames.Page_ProcessSetManage)]
        public async Task UpdateWorkProcessConfigDataAsync(WorkProcessSetInfoDto productWorkProcessConfigDetailDto)
        {
            if (string.IsNullOrEmpty(productWorkProcessConfigDetailDto.GraphData))
            {
                throw new UserFriendlyException("工艺配置数据缺失，请完善后再保存！");
            }

            if (_processSetManager.IsProcessSetIsUsed(productWorkProcessConfigDetailDto.Id))
            {
                throw new UserFriendlyException("该工艺已经被使用不允许修改！");
            }

            _processSetManager.UpdateConfigDataOnly(productWorkProcessConfigDetailDto.Id, productWorkProcessConfigDetailDto.GraphData);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public PageData<ProductWorkProcessSetDto> LoadProductWorkProcessConfig(JHTPageAjaxResquest<WorkProcessProductConditionDto> pageAjaxResquest)
        {
            var queryCondtion = pageAjaxResquest.Condition;
            var query = _workProcessProductRepository.GetAllIncluding(p => p.MaterialInfo, d => d.BelongWorkProcessSet);
            query = query
                .WhereIf(queryCondtion.WorkProcesSetId != null, p => p.BelongWorkProcessSetId == queryCondtion.WorkProcesSetId)
                .WhereIf(queryCondtion.MaterialId != null, p => p.MaterialInfoId == queryCondtion.MaterialId)
                .WhereIf(!string.IsNullOrEmpty(queryCondtion.KeyWord),p=>p.MaterialInfo.MaterialName.Contains(queryCondtion.KeyWord))
                ;
                

            PageData<ProductWorkProcessSetDto> pageData = new PageData<ProductWorkProcessSetDto>();
            pageData.Total = query.Count();
            pageData.List = ObjectMapper.Map<List<ProductWorkProcessSetDto>>(query.Skip(pageAjaxResquest.SkipCount).Take(pageAjaxResquest.PageSize).ToList());

            return pageData;
        }

        [AbpAuthorize(PermissionNames.Page_ProcessSetManage)]
        public async Task<JHTAjaxResponse> DelProductCurrentWorkProcessSetAsync(EntityDto entityDto)
        {
            JHTAjaxResponse jHTAjaxResponse = new JHTAjaxResponse();
            var dataRelation = await _workProcessProductRepository.FirstOrDefaultAsync(p => p.Id == entityDto.Id);
            if (_bomUnitManager.IsProductProcessSetInUsed(dataRelation.MaterialInfoId, dataRelation.BelongWorkProcessSetId))
            {
                jHTAjaxResponse.Msg = "该产品工艺已经用于工艺BOM，请先删除对应的工艺BOM！";
                return jHTAjaxResponse;
            }

            await _workProcessProductRepository.DeleteAsync(entityDto.Id);
            jHTAjaxResponse.Msg = "操作成功";
            return jHTAjaxResponse;
        }

        [AbpAuthorize(PermissionNames.Page_ProcessSetManage)]
        public async Task<JHTAjaxResponse> SetProductCurrentWorkProcessSetAsync(SetProductProcessSetDto dataModel)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            await _processSetManager.SetCurrentProductProcessSetAsync(dataModel.Id);
            ajaxResponse.Msg = "操作成功";
            return ajaxResponse;
        }

        [AbpAuthorize(PermissionNames.Page_ProcessSetManage)]
        public async Task<JHTAjaxResponse> UpdateProdutctWorkProcessSetAsync(SetProductProcessSetDto creatModel)
        {
            JHTAjaxResponse jHTAjaxResponse = new JHTAjaxResponse();
            var dataRelation = await _workProcessProductRepository.FirstOrDefaultAsync(p => p.Id == creatModel.Id);
            if (_bomUnitManager.IsProductProcessSetInUsed(dataRelation.MaterialInfoId, dataRelation.BelongWorkProcessSetId))
            {
                jHTAjaxResponse.Msg = "该产品工艺已经用于工艺BOM，请先删除对应的工艺BOM,才允许修改对应关系！";
                return jHTAjaxResponse;
            }

            dataRelation.BelongWorkProcessSetId = creatModel.BelongWorkProcessSetId;
            dataRelation.MaterialInfoId = creatModel.MaterialInfoId;

            UnitOfWorkManager.Current.SaveChanges();
            return jHTAjaxResponse;
        }

        public List<UICascaderModel<WorkProcessSetInfoDto, long>> GetProcessSetInCascader()
        {
            List<UICascaderModel<WorkProcessSetInfoDto, long>> result = new List<UICascaderModel<WorkProcessSetInfoDto, long>>();
            var allSet = this.Repository.GetAll().ToList();
            var allProSetNames = allSet.Select(p => p.SetName).ToList().Distinct();
            foreach (var item in allProSetNames)
            {
                long i = 0;
                var list = allSet.Where(p => p.SetName == item).ToList();
                List<UICascaderModel<WorkProcessSetInfoDto, long>> childrens = new List<UICascaderModel<WorkProcessSetInfoDto, long>>();
                foreach (var children in list)
                {
                    childrens.Add(new UICascaderModel<WorkProcessSetInfoDto, long>()
                    {
                        Children = null,
                        Label = children.SetVersion,
                        Leaf = false,
                        Value = children.Id
                    });
                }
                result.Add(new UICascaderModel<WorkProcessSetInfoDto, long>()
                {
                    Value = i,
                    Leaf = false,
                    Label = item,
                    Children = childrens
                });
                i++;
            }
            return result;
        }

        public List<WorkProcessSetDetail> LoadProductWorkProcessConfigByMaterialId(EntityDto materilId)
        {
            var productRelation = _workProcessProductRepository
                .GetAllIncluding(p => p.BelongWorkProcessSet)
                .FirstOrDefault(p => p.IsCurrent && p.MaterialInfoId == materilId.Id);


            if (productRelation == null)
            {
                return new List<WorkProcessSetDetail>();
            }

            return productRelation.BelongWorkProcessSet.GetWorkProcessSetDetails();

        }
    }
}
