using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using JHT.Abp.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Authorization;
using SC.SimpleMes.Authorization.Users;
using SC.SimpleMes.BOM;
using SC.SimpleMes.DTO;
using SC.SimpleMes.K3DBInfo;
using SC.SimpleMes.Material.Dto;
using SC.SimpleMes.Report.Dto;

namespace SC.SimpleMes.Material
{
    public class MaterialAppService :
        AsyncCrudAppService<MaterialInfo, MaterialInfoDto, long, CommonPageRequestDto, MaterialInfoDto, MaterialInfoDto>, IMaterialAppService
    {
        private readonly MaterialManager _materialManager;
        private readonly BomUnitManager _bomUnitManager;
        private readonly IRepository<MaterialCategory, long> _categoryRep;
        private readonly IRepository<K3MaterialInfo, int> _k3MaterialInfoRep;
        private readonly UserManager _userManager;
        private readonly IRepository<ERPInStockInfo, long> _erpInStockInfoRep;
        private readonly IRepository<MaterialBatchNumber, long> _batchNumberRep;
        private readonly IRepository<ERPInStockInfoOperateRecord, long> _batchNumberOperatoerRecord;
        private readonly IRepository<WarningOverUsedERPInStockInfo, long> _overUseERPInStockInfoRep;
        public MaterialAppService(
            IRepository<MaterialInfo, long> repository, MaterialManager materialManager,
            IRepository<MaterialCategory, long> categoryRep,
        IRepository<K3MaterialInfo, int> k3MaterialInfoRep,
        UserManager userManager,
        IRepository<ERPInStockInfo, long> erpInStockInfoRep,
        IRepository<ERPInStockInfoOperateRecord, long> batchNumberOperatoerRecord,
        IRepository<MaterialBatchNumber, long> batchNumberRep,
        IRepository<WarningOverUsedERPInStockInfo, long> overUseERPInStockInfoRep,
        BomUnitManager bomUnitManager) : base(repository)
        {
            _materialManager = materialManager;
            _bomUnitManager = bomUnitManager;
            _categoryRep = categoryRep;
            _k3MaterialInfoRep = k3MaterialInfoRep;
            _overUseERPInStockInfoRep = overUseERPInStockInfoRep;
            _userManager = userManager;
            _erpInStockInfoRep = erpInStockInfoRep;
            _batchNumberRep = batchNumberRep;
            _batchNumberOperatoerRecord = batchNumberOperatoerRecord;
        }


        protected override IQueryable<MaterialInfo> CreateFilteredQuery(CommonPageRequestDto input)
        {
            var query = this.Repository.GetAllIncluding(p => p.BelongCategory);
            var conditonDto = input.QueryConditionObj as MaterialConditionDto;
            if (conditonDto != null)
            {
                query = query
                 .WhereIf(!string.IsNullOrEmpty(conditonDto.KeyWord), p => p.MaterialName.Contains(conditonDto.KeyWord) || p.MaterialNumber.Contains(conditonDto.KeyWord))
                 .WhereIf(conditonDto.MaterialType != null && conditonDto.MaterialType.Count > 0, p => conditonDto.MaterialType.Contains(p.MaterialType));
            }

            return query;
        }

        public List<MaterialInfoDto> LoadFromK3()
        {
            return ObjectMapper.Map<List<MaterialInfoDto>>(_k3MaterialInfoRep.GetAll().Where(p => p.FFullNumber.StartsWith("D03")).OrderBy(p => p.FNumber).Take(10).ToList());
        }

        [AbpAuthorize(PermissionNames.Page_Material, PermissionNames.BaseInfo_Edit)]
        public override Task<MaterialInfoDto> CreateAsync(MaterialInfoDto input)
        {
            if (_materialManager.CheckUniqueMaterialNumber(input.MaterialNumber))
            {
                throw new UserFriendlyException("该物料编号已被使用");
            }

            if (!string.IsNullOrEmpty(input.CategoryCode))
            {

                var categoryInfo = _categoryRep.FirstOrDefault(p => p.CategoryCode == input.CategoryCode);
                if (categoryInfo == null)
                {
                    throw new UserFriendlyException("该物料分类无效");
                }

                input.BelongCategoryId = categoryInfo.Id;
            }

            return base.CreateAsync(input);
        }

        [AbpAuthorize(PermissionNames.Page_Material, PermissionNames.BaseInfo_Edit)]
        public override Task DeleteAsync(EntityDto<long> input)
        {
            if (_bomUnitManager.CheckMaterialIsUsedInBom(input.Id))
            {
                throw new UserFriendlyException("该物料已经被使用，不允许修改资料");
            }

            return base.DeleteAsync(input);
        }

        [AbpAuthorize(PermissionNames.Page_Material)]
        public override Task<MaterialInfoDto> UpdateAsync(MaterialInfoDto input)
        {
            // 只允许修改物料类型
            var dataInfo = this.Repository.FirstOrDefault(p => p.Id == input.Id);
            dataInfo.MaterialType = input.MaterialType;

            //if (_materialManager.CheckUniqueMaterialNumber(input.MaterialNumber, input.Id) && input.MaterialType != MaterialTypeEnum.在制品)
            //{
            //    throw new UserFriendlyException("该物料编号已被使用");
            //}

            //if (_bomUnitManager.CheckMaterialIsUsedInBom(input.Id))
            //{
            //    throw new UserFriendlyException("该物料已经被使用，不允许修改资料");
            //}

            //if (!string.IsNullOrEmpty(input.CategoryCode))
            //{
            //    var categoryInfo = _categoryRep.FirstOrDefault(p => p.CategoryCode == input.CategoryCode);
            //    if (categoryInfo == null)
            //    {
            //        throw new UserFriendlyException("该物料分类无效");
            //    }
            //    input.BelongCategoryId = categoryInfo.Id;
            //}

            return base.UpdateAsync(input);
        }

        public async Task<JHTAjaxResponse> MarkBatchNoOverUseInfoAsync(View_BatchMaterialUsedReportDto request)
        {
            JHTAjaxResponse jHTAjaxResponse = new JHTAjaxResponse();
            var overInstockInfo = await _overUseERPInStockInfoRep.FirstOrDefaultAsync(p => p.BatchNo == request.BatchNo);
            if (overInstockInfo != null && overInstockInfo.RemarkUserId != null && !string.IsNullOrEmpty(overInstockInfo.Remark))
            {
                jHTAjaxResponse.Msg = "该批次物料已经被备注超用信息";
                jHTAjaxResponse.Code = 500;
                return jHTAjaxResponse;
            }

            if (overInstockInfo == null)
            {
                jHTAjaxResponse.Msg = "该批次物料尚未超用";
                jHTAjaxResponse.Code = 500;
                return jHTAjaxResponse;
            }

            var userInfo = await _userManager.FindByIdAsync(AbpSession.UserId.GetValueOrDefault());
            overInstockInfo.Remark = request.Remark;
            overInstockInfo.RemarkUserId = AbpSession.UserId;
            overInstockInfo.RemarkDateTime = DateTime.Now;
            overInstockInfo.RemarkUserName = userInfo.Name;

            return jHTAjaxResponse;
        }

        public async Task<JHTAjaxResponse> SetMaterialStatuAsync(MaterialBatchNumberDto request)
        {
            JHTAjaxResponse result = new JHTAjaxResponse();
            var user = await this._userManager.FindByIdAsync(AbpSession.UserId.GetValueOrDefault());
            var instokcInfo = await _erpInStockInfoRep.FirstOrDefaultAsync(p => p.MaterialNumber == request.MaterialNumber && p.BatchNo == request.BatchNumber);
            if (instokcInfo != null && instokcInfo.MaterialStatu == request.MaterialStatu)
            {
                result.Msg = $"当前该批次物料状态已为{request.MaterialStatu}，无需处理!";
                return result;
            }

            if (instokcInfo != null && instokcInfo.MaterialStatu != request.MaterialStatu)
            {
                instokcInfo.MaterialStatu = request.MaterialStatu.GetValueOrDefault();
                result.Msg = "已解除该批次物料的封存";
            }

        

            var batchNumber = await _batchNumberRep.FirstOrDefaultAsync(p => p.MaterialNumber == request.MaterialNumber && p.BatchNumber == request.BatchNumber);
            if (batchNumber != null && batchNumber.MaterialStatu == request.MaterialStatu)
            {
                result.Msg = $"当前该批次物料状态已为{request.MaterialStatu}，无需处理!";
                return result;
            }

            if (batchNumber != null)
            {
                batchNumber.MaterialStatu = request.MaterialStatu.GetValueOrDefault();
                result.Msg = "已解除该批次物料的封存";
            }



            // 增加操作记录
            if (instokcInfo != null || batchNumber != null)
            {

                _batchNumberOperatoerRecord.Insert(new ERPInStockInfoOperateRecord()
                {
                    BatchNo = request.BatchNumber,
                    MaterialName = instokcInfo != null ? instokcInfo.MaterialName : batchNumber.MaterialName,
                    MaterialNumber = request.MaterialNumber,
                    OperateDesp = $"将批次物料状态置为{request.MaterialStatu}",
                    OperateTime = DateTime.Now,
                    OperatorId = AbpSession.UserId.GetValueOrDefault(),
                    Operator = user.Name
                });
            }
            return result;
        }
    }
}
