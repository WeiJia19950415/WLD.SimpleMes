using Abp.Application.Services.Dto;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.IRepository;
using WLD.SimpleMes.Material;
using WLD.SimpleMes.WorkOrder;
using WLD.SimpleMes.WorkProcess;

namespace WLD.SimpleMes.BOM
{
    public class BomUnitManager : ITransientDependency
    {
        /// <summary>
        /// 标准BOM
        /// </summary>
        private readonly IRepository<BomInfo, long> _bomInfo;
        /// <summary>
        /// 标准BOM的Item
        /// </summary>
        private readonly IRepository<BomItemInfo, long> _bomItemInfo;
        /// <summary>
        /// 工艺BOM
        /// </summary>
        private readonly IRepository<WorkProcessSetBom, long> _workProcessSetBom;

        private readonly IRepository<WorkProcessSetProductRelation, long> _workProcessSetProductRealtionRep;
        /// <summary>
        /// 工单BOM
        /// </summary>
        private readonly IRepository<WorkOrderBom, long> _workOrderBom;

        /// <summary>
        /// 工单BOM详情
        /// </summary>
        private readonly IRepository<WorkOrderBomItem, long> _workOrderBomItem;
        /// <summary>
        /// 工序
        /// </summary>
        private readonly IRepository<WorkProcessInfo, long> _workProcessInfo;

        private readonly IRepository<WorkProcessSetBomItem, long> _workProcessSetBomItem;

        private readonly IBomItemRepsoitory _bomItemRepsoitory;

        public BomUnitManager(IRepository<BomInfo, long> bomInfo
            , IRepository<BomItemInfo, long> bomItemInfo
            , IBomItemRepsoitory bomItemRepsoitory
            , IRepository<WorkProcessSetBom, long> workProcessSetBom
            , IRepository<WorkProcessInfo, long> workProcessInfo
            , IRepository<WorkOrderBomItem, long> workOrderBomItem
            , IRepository<WorkOrderBom, long> workOrderBom
            , IRepository<WorkProcessSetProductRelation, long> workProcessSetRep
            , IRepository<WorkProcessSetBomItem, long> workProcessSetBomItem)
        {
            _bomInfo = bomInfo;
            _bomItemInfo = bomItemInfo;
            _bomItemRepsoitory = bomItemRepsoitory;
            _workProcessSetBom = workProcessSetBom;
            _workProcessInfo = workProcessInfo;
            _workOrderBom = workOrderBom;
            _workOrderBomItem = workOrderBomItem;
            _workProcessSetBomItem = workProcessSetBomItem;
            _workProcessSetProductRealtionRep = workProcessSetRep;
        }

        /// <summary>
        /// 获取一个实例对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<BomInfo> GetAsync(long Id)
        {
            return await _bomInfo.GetAsync(Id);
        }
        /// <summary>
        /// 检擦Bom是否已经被引用
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>返回false表示未被引用，可执行删除或修改操作</returns>
        public bool CheckBomWhetherUsed([NotNull]long Id)
        {
            var workProcessSetBom = this._workProcessSetBom.GetAllIncluding(p => p.BelongWorkProcessSet).Where(p => p.ReferenceBomId == Id).FirstOrDefault();
            if (workProcessSetBom != null)
            {
                throw new UserFriendlyException("该BOM已经被工艺BOM引用，禁止修改或删除操作;<br/>引用的工艺:" +
                    workProcessSetBom.BelongWorkProcessSet.SetName);
            }
            return false;
        }

        /// <summary>
        /// 检查这个BomInfo是否可进行新增或删除操作
        /// </summary>
        /// <param name="bomInfo"></param>
        /// <returns></returns>
        public bool CheckBOMAddOrUpdate(BomInfo bomInfo)
        {
            if (string.IsNullOrEmpty(bomInfo.MaterialNumber) || string.IsNullOrEmpty(bomInfo.MaterialName) || bomInfo.MaterialId <= 0)
            {
                throw new UserFriendlyException("请选择BOM的所属物料！");
            }
            foreach (var item in bomInfo.BomItems)
            {
                if (string.IsNullOrEmpty(item.FormMaterialNumber) || string.IsNullOrEmpty(item.FormMaterialName) || item.FormMaterialId <= 0)
                {
                    throw new UserFriendlyException("BOM详情中有物料未选择！");
                }
                if (item.FormCount <= 0)
                {
                    throw new UserFriendlyException(item.FormMaterialName + "的物料配比数量不能小于或等于0");
                }
                if (item.LossFactor < 0 || item.LossFactor >= 100)
                {
                    throw new UserFriendlyException(item.FormMaterialName + "耗损系数不能小于0或大于100");
                }
            }
            return true;
        }

        /// <summary>
        /// 获取使用了该物料的Bom
        /// </summary>
        /// <param name="MaterialNumber">物料编码</param>
        /// <returns></returns>
        public List<BomInfo> GetBomInfoByItem(string MaterialNumber)
        {
            return this._bomItemInfo.GetAllIncluding(p => p.BelongBom)
                .Where(p => p.FormMaterialNumber.Equals(MaterialNumber))
                .Select(p => p.BelongBom).ToList();
        }

        /// <summary>
        /// 根据工艺BOM获取标准BOM详情
        /// </summary>
        /// <param name="SetBomId"></param>
        /// <returns></returns>
        public async Task<List<BomItemInfo>> GetBomItemInfosBySetBomIdAsync(long SetBomId)
        {
            var setBom = await _workProcessSetBom.GetAsync(SetBomId);
            return _bomItemInfo.GetAll().Where(p => p.BelongBom.Id == setBom.ReferenceBomId).ToList();
        }

        /// <summary>
        /// 检查物料是否被使用
        /// 1、被BOM表使用
        /// 2、构成BOM中的项
        /// </summary>
        /// <param name="materialNumber"></param>
        /// <returns></returns>
        public bool CheckMaterialIsUsedInBom(long materialId)
        {

            return _bomInfo.GetAll().Any(p => p.MaterialId == materialId) || _bomItemInfo.GetAll().Any(p => p.FormMaterialId == materialId);
        }
        /// <summary>
        /// 获取该物料对应的所有工艺Bom
        /// </summary>
        /// <param name="MaterialNumber"></param>
        /// <returns></returns>
        public List<WorkProcessSetBom> GetWorkProcessSetBom(string MaterialNumber)
        {
            return this._workProcessSetBom.GetAllIncluding(p => p.ReferenceBom, x => x.BelongWorkProcessSet)
                .Where(p => p.ReferenceBom.MaterialNumber.Equals(MaterialNumber)).ToList();
        }

        public List<BomInfo> GetBomByMaterialNumber(string materialNumber)
        {
            return this._bomInfo.GetAll().Where(p => p.MaterialNumber == materialNumber).ToList();
        }

        public async Task SetBomIsCurrentAsync(EntityDto<long> entityDto)
        {
            var nowBom = await _bomInfo.FirstOrDefaultAsync(p => p.Id == entityDto.Id);
            var allBom = _bomInfo.GetAll().Where(p => p.MaterialId == nowBom.MaterialId && p.IsCurrent).ToList();
            if (!nowBom.IsCurrent)
            {
                nowBom.IsCurrent = true;
            }

            allBom.ForEach(p => p.IsCurrent = false);


        }



        /// <summary>
        /// 获取该物料对应的所有工单Bom
        /// </summary>
        /// <param name="MaterialNumber"></param>
        /// <returns></returns>
        public List<WorkOrderBom> GetWorkOrderBom(string MaterialNumber)
        {
            return this._workOrderBom.GetAllIncluding(p => p.WorkOrderInfo, x => x.WorkProcessSetBom)
                .Where(p => p.WorkOrderNumber.Equals(MaterialNumber)).ToList();
        }
        /// <summary>
        /// 新增无Bom
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        public async Task<BomInfo> CreateBomAsync(BomInfo add)
        {
            return await _bomInfo.InsertAsync(add);
        }
        /// <summary>
        /// 添加Bom详情
        /// </summary>
        /// <param name="addItme">Bom详情</param>
        /// <returns></returns>
        public async Task CreateBomIteamAsync(List<BomItemInfo> addItme)
        {
            await _bomItemRepsoitory.BatchInsertBomItemAsync(addItme);
        }
        /// <summary>
        /// 清空Bom详情
        /// </summary>
        /// <param name="BomInfoId">BomId</param>
        /// <returns></returns>
        public async Task DeleteBomIteamAsync(long BomInfoId)
        {
            await _bomItemRepsoitory.BatchDeleteBomItemAsync(BomInfoId);
        }

        /// <summary>
        /// 获取工艺Bom所对应的所有工序
        /// </summary>
        /// <param name="BelongWorkProcessSetBomId">工艺BOM_ID</param>
        /// <returns></returns>
        public List<WorkProcessInfo> GetWorkProcessSetBomBySetDetail(long BelongWorkProcessSetBomId)
        {
            var workProcessSetBom = _workProcessSetBom.GetAllIncluding(p => p.BelongWorkProcessSet).FirstOrDefault(p => p.Id == BelongWorkProcessSetBomId);
            var belongWorkProcess = workProcessSetBom.BelongWorkProcessSet;
            var list = belongWorkProcess.GetWorkProcessSetDetails().Select(p => p.BelongWorkProcessInfoId).ToList();
            return _workProcessInfo.GetAll().Where(p => list.Contains(p.Id)).ToList();
        }

        /// <summary>
        /// 获取工艺详情
        /// </summary>
        /// <param name="BelongWorkProcessSetBomId">工艺BOM_ID</param>
        /// <param name="workProcessInfos">下属的所有工序</param>
        /// <returns></returns>
        public List<WorkProcessSetBomItem> GetWorkProcessSetBomItems(long BelongWorkProcessSetBomId, List<WorkProcessInfo> workProcessInfos)
        {
            return _workProcessSetBomItem.GetAllIncluding(p => p.InputMaterial, x => x.BelongWorkProcess)
                .Where(p => p.BelongWorkProcessSetBomId == BelongWorkProcessSetBomId).ToList();
        }

        /// <summary>
        /// 产品工艺配置是否在使用
        /// </summary>
        /// <param name="materialId"></param>
        /// <param name="workProcessSetId"></param>
        /// <returns></returns>
        public bool IsProductProcessSetInUsed(long materialId, long workProcessSetId)
        {
            return _workProcessSetBom.GetAllIncluding(p => p.ReferenceBom).Any(p => p.BelongWorkProcessSetId == workProcessSetId && p.ReferenceBom.MaterialId == materialId);
        }

        /// <summary>
        /// 清空工艺BOM详情
        /// </summary>
        /// <param name="WorkProcessSetBomId"></param>
        /// <returns></returns>
        public async Task DelWorkProcessSetBomItemByIdAsync(long WorkProcessSetBomId)
        {
            await _bomItemRepsoitory.BatchDelWorkProcessSetBomItemByIdAsync(WorkProcessSetBomId);
        }

        public async Task AddWorkProcessSetBomItem(List<WorkProcessSetBomItem> addItem)
        {
            foreach (var item in addItem)
            {
                await _workProcessSetBomItem.InsertAsync(item);
            }
        }

        /// <summary>
        /// 创建工单BOM，并对详情复制工艺BOM
        /// </summary>
        /// <param name="workOrderBom"></param>
        /// <returns></returns>
        public async Task<long> CreateWorkOrderBOM(WorkOrderBom workOrderBom)
        {
            workOrderBom.WorkProcessSetBom = await _workProcessSetBom.GetAsync(workOrderBom.WorkProcessSetBomId);
            long Id = await _workOrderBom.InsertAndGetIdAsync(workOrderBom);
            var setBomItem = _workProcessSetBomItem.GetAll().Where(p => p.BelongWorkProcessSetBomId == workOrderBom.WorkProcessSetBomId).ToList();
            List<WorkOrderBomItem> addItem = new List<WorkOrderBomItem>();
            foreach (var item in setBomItem)
            {
                addItem.Add(new WorkOrderBomItem()
                {
                    BelongWorkProcessId = item.BelongWorkProcessId,
                    BelongWorkOrderBomId = workOrderBom.Id,
                    CreationTime = DateTime.Now,
                    InputMaterialCount = item.InputMaterialCount,
                    InputMaterialId = item.InputMaterialId,
                });
            }
            foreach (var item in addItem)
            {
                await _workOrderBomItem.InsertAsync(item);
            }
            return Id;
        }

        /// <summary>
        /// 清除工单BOM
        /// </summary>
        /// <param name="WorkOrderId"></param>
        /// <returns></returns>
        public async Task ResetWorkOrderBOM(long WorkOrderId)
        {
            var del = _workOrderBom.GetAll().Where(p => p.WorkOrderId == WorkOrderId).FirstOrDefault();
            var delItem = _workOrderBomItem.GetAll().Where(p => p.BelongWorkOrderBomId == del.Id).ToList();
            foreach (var item in delItem)
            {
                await _workOrderBomItem.DeleteAsync(item);
            }
            await _workOrderBom.DeleteAsync(del);
        }

        /// <summary>
        /// 根据工单BOM获取工艺BOM
        /// </summary>
        /// <param name="WorkOrderBOMId"></param>
        /// <returns></returns>
        public WorkProcessSetBom GetSetBOMByWorkOrdBOM(long WorkOrderBOMId)
        {
            WorkOrderBom workOrderBom = _workOrderBom.Get(WorkOrderBOMId);
            return _workProcessSetBom.Get(workOrderBom.WorkProcessSetBomId);
        }

        public WorkProcessSetBom GetSetBOMByMaterialId(long workProcessSetId, long bomId)
        {
            return this._workProcessSetBom.FirstOrDefault(p => p.BelongWorkProcessSetId == workProcessSetId && p.ReferenceBomId == bomId);
        }

        /// <summary>
        /// 获取在用BOM
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<BomInfo> GetCurrentBomByMaterialId(long materialId)
        {
            return this._bomInfo.GetAll().Where(p => p.MaterialId == materialId && p.IsCurrent == true).ToList();

        }

        /// <summary>
        /// 根据物料id，获取当前正在使用的工艺信息
        /// </summary>
        /// <param name="materialInfoId"></param>
        /// <returns></returns>
        public WorkProcessSet GetMaterialCurrentWorkProcessSetByMmaterialId(long materialInfoId)
        {
            return _workProcessSetProductRealtionRep.GetAllIncluding(p => p.BelongWorkProcessSet)
                 .Where(p => p.MaterialInfoId == materialInfoId && p.IsCurrent == true)
                 .Select(p => p.BelongWorkProcessSet).FirstOrDefault();

        }

        /// <summary>
        ///  根据工艺id,BOMid 获取所使用的工艺BOM
        /// </summary>
        /// <param name="bOMId"></param>
        /// <param name="processSetId"></param>
        /// <returns></returns>
        public WorkProcessSetBom GetCurrentWorkProcessSetBomInfoBy(long? bOMId, long processSetId)
        {
            return this._workProcessSetBom.FirstOrDefault(p => p.BelongWorkProcessSetId == processSetId && p.ReferenceBomId == bOMId);
        }

        /// <summary>
        /// 物料是否存在于工单BOM中
        /// </summary>
        /// <param name="workOrderBomId"></param>
        /// <param name="materialNumber"></param>
        /// <returns></returns>
        public bool IsMaterialInWorkOrderBom(long? workOrderBomId, string materialNumber)
        {
            return this._workOrderBomItem.GetAllIncluding(p => p.InputMaterial)
                .Any(p => p.BelongWorkOrderBomId == workOrderBomId && p.InputMaterial.MaterialNumber == materialNumber);
        }

        /// <summary>
        /// 获取工单BOM信息
        /// </summary>
        /// <param name="workBomId"></param>
        /// <returns></returns>
        public List<WorkOrderBomItem> GetWorkOrderBomItems(long workBomId)
        {
            return _workOrderBomItem
                .GetAllIncluding(p => p.InputMaterial, p => p.BelongWorkProcess)
                .Where(p => p.BelongWorkOrderBomId == workBomId).ToList();
        }

        public void AddWorkOrderBomItems(List<WorkOrderBomItem> bomItems)
        {
            foreach (var item in bomItems)
            {
                _workOrderBomItem.Insert(item);
            }
        }

        public void DelWorkOrderBomItem(long id)
        {
            var bomInfo = GetWorkOrderBomItems(id);
            foreach (var item in bomInfo)
            {
                _workOrderBomItem.Delete(item);
            }
        }

        public async Task<List<BomItemInfo>> GetBomItemInfosByBomIdAsync(long bomId)
        {
            return await _bomItemInfo.GetAll().Where(p => p.BelongBomId == bomId).ToListAsync();
        }
    }
}
