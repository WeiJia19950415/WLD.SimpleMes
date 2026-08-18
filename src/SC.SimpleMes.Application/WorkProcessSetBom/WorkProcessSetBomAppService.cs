using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.BOM;
using SC.SimpleMes.DTO;
using SC.SimpleMes.Material;
using SC.SimpleMes.WorkProcessSetBom.Dto;

namespace SC.SimpleMes.WorkProcessSetBom
{
    public class WorkProcessSetBomAppService : AsyncCrudAppService<BOM.WorkProcessSetBom, WorkProcessSetBomDto, long, CommonPageRequestDto, WorkProcessSetBomDto, WorkProcessSetBomDto>, IWorkProcessSetBomAppService
    {
        private readonly BomUnitManager _bomUnitManager;
        private readonly MaterialManager _materialManager;
        public WorkProcessSetBomAppService(IRepository<BOM.WorkProcessSetBom, long> repository,
             BomUnitManager bomUnitManager,
             MaterialManager materialManager) : base(repository)
        {
            _bomUnitManager = bomUnitManager;
            _materialManager = materialManager;
        }

        protected override IQueryable<BOM.WorkProcessSetBom> CreateFilteredQuery(CommonPageRequestDto input)
        {
            var condition = input.QueryConditionObj as CommonConditionData;
           var query = this.Repository.GetAllIncluding(p => p.BelongWorkProcessSet, x => x.ReferenceBom);
            if (!string.IsNullOrEmpty(condition.KeyWord))
            {
                query = query.Where(p => p.BelongWorkProcessSet.SetName.Contains(condition.KeyWord)||p.ReferenceBom.MaterialNumber.Contains(condition.KeyWord) || p.ReferenceBom.MaterialName.Contains(condition.KeyWord) || p.Version.Contains(condition.KeyWord));
            }
            return query;
        }

        public List<WorkProcessSetBomItemByShowDto> GetWorkProcessSetBomItemByShowDtos(EntityDto<long> SetBomId)
        {
            List<WorkProcessSetBomItemByShowDto> ret = new List<WorkProcessSetBomItemByShowDto>();
            var WorkProcessInfos = _bomUnitManager.GetWorkProcessSetBomBySetDetail(SetBomId.Id);
            var setBOMItem = _bomUnitManager.GetWorkProcessSetBomItems(SetBomId.Id, WorkProcessInfos);
            foreach (var item in WorkProcessInfos)
            {
                var retAdd = ObjectMapper.Map<WorkProcessSetBomItemByShowDto>(item);
                var addSetBomItem = setBOMItem.Where(p => p.BelongWorkProcessSetBomId == SetBomId.Id && p.BelongWorkProcessId == item.Id).ToList();
                retAdd.BomItem = ObjectMapper.Map<List<ProcessBomItem>>(addSetBomItem);
                ret.Add(retAdd);
            }
            return ret;
        }
          
        public async Task ConfigWorkProcessBomAsync(ConfigWorkProcessBomDto dto)
        {
            List<WorkProcessSetBomItem> addItem = new List<WorkProcessSetBomItem>();
            foreach (var item in dto.Item)
            {
                foreach (var addItems in item.BomItem)
                {
                    addItem.Add(
                        new WorkProcessSetBomItem
                        {
                            BelongWorkProcessId = item.Id,
                            BelongWorkProcessSetBomId = dto.Id,
                            InputMaterialCount = addItems.FormCount,
                            InputMaterialId = _materialManager.GetMaterialIdByNumber(addItems.FormMaterialNumber, AbpSession.TenantId)
                        });
                }
            }
            await _bomUnitManager.DelWorkProcessSetBomItemByIdAsync(dto.Id);
            await _bomUnitManager.AddWorkProcessSetBomItem(addItem);
            CurrentUnitOfWork.SaveChanges();
        }


        public async override Task<WorkProcessSetBomDto> CreateAsync(WorkProcessSetBomDto input)
        {
            if (string.IsNullOrEmpty(input.Version))
            {
                input.Version = "V1";
            }
            BOM.WorkProcessSetBom add = ObjectMapper.Map<BOM.WorkProcessSetBom>(input);            
            var data = this.Repository.GetAll().Where(p => p.ReferenceBomId == add.ReferenceBomId && p.BelongWorkProcessSetId == add.BelongWorkProcessSetId && p.Version == add.Version).FirstOrDefault();
            if (data != null)
            {
                throw new UserFriendlyException("工艺BOM版本已存在");
            }
            add.TenantId = AbpSession.TenantId;
            await this.Repository.InsertAndGetIdAsync(add);
            return input;
        }

        public List<WorkProcessSetBomDto> GetWorkProcessSetBomDtosByMaterial(long MaterialIds)
        {
            var data = this.Repository.GetAllIncluding(p => p.ReferenceBom).Where(p => p.ReferenceBom.MaterialId == MaterialIds).ToList();
            return ObjectMapper.Map<List<WorkProcessSetBomDto>>(data);
        }
    }
}
