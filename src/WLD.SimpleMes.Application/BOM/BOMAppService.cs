using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Authorization;
using WLD.SimpleMes.BOM.Dto;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.Material;

namespace WLD.SimpleMes.BOM
{
    public class BOMAppService : AsyncCrudAppService<BomInfo, BomDto, long, CommonPageRequestDto, BomAddDto, BomUpdateDto>, IBOMAppService
    {

        private readonly BomUnitManager _bomUnitManager;
        private readonly IRepository<BomInfo, long> _repository;
        private readonly IRepository<WorkProcessSetBom, long> _processSetBomRepository;
        private readonly MaterialCategoryManager _materialCategoryManager;

        public BOMAppService(IRepository<BomInfo, long> repository,
            BomUnitManager bomUnitManager, IRepository<WorkProcessSetBom, long> processSetBomRepository,
            MaterialCategoryManager materialCategoryManager) : base(repository)
        {
            _bomUnitManager = bomUnitManager;
            _repository = repository;
            _materialCategoryManager = materialCategoryManager;
            _processSetBomRepository = processSetBomRepository;
        }


        [AbpAuthorize(PermissionNames.Page_BomManager, PermissionNames.BaseInfo_Edit)]
        public override async Task<BomDto> CreateAsync(BomAddDto input)
        {
            BomInfo info = ObjectMapper.Map<BomInfo>(input);
            List<BomItemInfo> itemInfos = ObjectMapper.Map<List<BomItemInfo>>(input.BomItemDtos);
            info.BomItems = itemInfos;
            info.TenantId = AbpSession.TenantId.GetValueOrDefault();
            _bomUnitManager.CheckBOMAddOrUpdate(info);
            var ret = await _bomUnitManager.CreateBomAsync(info);
            return ObjectMapper.Map<BomDto>(ret);
        }

        [AbpAuthorize(PermissionNames.Page_BomManager, PermissionNames.BaseInfo_Edit)]
        public override async Task DeleteAsync(EntityDto<long> input)
        {
            if (!_bomUnitManager.CheckBomWhetherUsed(input.Id))
            {
                await _bomUnitManager.DeleteBomIteamAsync(input.Id);
            }
        }

        protected override IQueryable<BomInfo> CreateFilteredQuery(CommonPageRequestDto input)
        {
            var query = this.Repository.GetAllIncluding(p => p.BomItems, x => x.Material);
            var conditionData = input.QueryConditionObj as CommonConditionData;
            if (!string.IsNullOrEmpty(conditionData.KeyWord))
            {
                query = query.Where(p => p.MaterialNumber.Contains(conditionData.KeyWord));
            }
            return query;
        }


        public override async Task<BomDto> GetAsync(EntityDto<long> input)
        {
            var ret = await _bomUnitManager.GetAsync(input.Id);
            return ObjectMapper.Map<BomDto>(ret);
        }

        [AbpAuthorize(PermissionNames.Page_BomManager, PermissionNames.BaseInfo_Edit)]
        public override async Task<BomDto> UpdateAsync(BomUpdateDto input)
        {
            if (!_bomUnitManager.CheckBomWhetherUsed(input.Id))
            {
                BomInfo info = ObjectMapper.Map<BomInfo>(input);
                List<BomItemInfo> itemInfos = ObjectMapper.Map<List<BomItemInfo>>(input.BomItemDtos);
                info.BomItems = itemInfos;
                info.TenantId = AbpSession.TenantId;
                _bomUnitManager.CheckBOMAddOrUpdate(info);
                await _bomUnitManager.DeleteBomIteamAsync(input.Id);
                var up = await _repository.UpdateAsync(info);
                return ObjectMapper.Map<BomDto>(up);
            }
            return null;
        }

        public async Task<List<BomItemDto>> GetBySetBomAsync(EntityDto<long> SetBomId)
        {
            var data = await _bomUnitManager.GetBomItemInfosBySetBomIdAsync(SetBomId.Id);
            return ObjectMapper.Map<List<BomItemDto>>(data);
        }

        public async Task<List<BomItemDto>> GetBySetBomToImportantAsync(EntityDto<long> SetBomId)
        {
            var data = await _bomUnitManager.GetBomItemInfosBySetBomIdAsync(SetBomId.Id);
            var ScreenId = _materialCategoryManager.ScreenImportant(data.Select(p => p.FormMaterialId).ToList());
            return ObjectMapper.Map<List<BomItemDto>>(data.Where(p => ScreenId.Contains(p.FormMaterialId)).ToList());
        }

        public List<UICascaderModel<BomDto, long>> GetBOMInCascader()
        {
            List<UICascaderModel<BomDto, long>> result = new List<UICascaderModel<BomDto, long>>();
            var allBom = this.Repository.GetAllIncluding(p => p.Material).ToList();
            var materialInfos = allBom.Select(p => p.Material).ToList().GroupBy(p => p.Id);
            foreach (var item in materialInfos)
            {
                var mate = item.FirstOrDefault();
                var list = allBom.Where(p => p.MaterialId == mate.Id).ToList();
                List<UICascaderModel<BomDto, long>> childrens = new List<UICascaderModel<BomDto, long>>();
                foreach (var children in list)
                {
                    childrens.Add(new UICascaderModel<BomDto, long>()
                    {
                        Children = null,
                        Label = children.Version,
                        Leaf = false,
                        Value = children.Id
                    });
                }
                result.Add(new UICascaderModel<BomDto, long>()
                {
                    Value = mate.Id,
                    Leaf = false,
                    Label = mate.MaterialName,
                    Children = childrens
                });
            }
            return result;
        }

        /// <summary>
        /// 获取当前物料标准BOM 未配置工艺BOM的 BOM数据
        /// </summary>
        /// <param name="materialNumber"></param>
        /// <returns></returns>
        public List<BomDto> GetUnConfigWorkProcessSetBom(string materialNumber)
        {
            List<BomDto> bomDtos = new List<BomDto>();
            var standBomInfos = _bomUnitManager.GetBomByMaterialNumber(materialNumber);
            var workProcesSetBom = _bomUnitManager.GetWorkProcessSetBom(materialNumber);
            foreach (var item in standBomInfos)
            {
                if (workProcesSetBom.Count(p => p.ReferenceBomId == item.Id) == 0)
                {
                    bomDtos.Add(ObjectMapper.Map<BomDto>(item));
                }
            }

            return bomDtos;
        }

        public async Task SetBomIsCurrentAsync(EntityDto<long> entityDto)
        {
            await _bomUnitManager.SetBomIsCurrentAsync(entityDto);
            UnitOfWorkManager.Current.SaveChanges();
        }
    }
}
