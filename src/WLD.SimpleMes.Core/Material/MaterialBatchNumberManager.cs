using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Configuration;
using WLD.SimpleMes.K3DBInfo;
using WLD.SimpleMes.LineSideWarehouse;
using WLD.SimpleMes.Material.DomainEvent;
using WLD.SimpleMes.Material.SerialNumberGenerator;
using WLD.SimpleMes.WorkProcess;
using WLD.SimpleMes.WorkStation;

namespace WLD.SimpleMes.Material
{
    public class MaterialBatchNumberManager : ITransientDependency
    {
        private readonly IRepository<MaterialBatchNumber, long> _repsository;
        private readonly IRepository<MaterialInfo, long> _materialsRep;
        private readonly IRepository<LineSideMaterialInfo, long> _lineSideMaterialInfoRep;
        private readonly IRepository<MaterialBatchNumberRuler, long> _rulerRep;
        private readonly IRepository<WorkProcessMaterialRecord, long> _workProcessMaterialRecordRep;
        private readonly IRepository<ERPInStockInfo, long> _inStockInfo;
        private readonly IocManager _iocManager;

        private MaterialBatchNumberRuler ruler;
        private IMaterialBatchNumberGenerate materialBatchNumberGenerate;
        public MaterialBatchNumberManager(
            IRepository<MaterialBatchNumber, long> repository,
            IRepository<LineSideMaterialInfo, long> lineSideMaterialInfoRep,
            IocManager iocManager,
            IRepository<MaterialBatchNumberRuler, long> rulerRep,
            IRepository<WorkProcessMaterialRecord, long> workProcessMaterialRecordRep,
            IRepository<ERPInStockInfo, long> inStockInfo,
            IRepository<MaterialInfo, long> materialRep)
        {
            _repsository = repository;
            _materialsRep = materialRep;
            _rulerRep = rulerRep;
            _iocManager = iocManager;
            _inStockInfo = inStockInfo;
            _lineSideMaterialInfoRep = lineSideMaterialInfoRep;
            _workProcessMaterialRecordRep = workProcessMaterialRecordRep;
        }

        public void IniteMaterialBatchNumberManager(
            long belongMaterialId,
            string factoryNumber,
            ProductLine productLine,
            string workStationNumber = "",
            bool isLineSideMaterial = false,
            WorkProcessTypeEnum workProcessTypeEnum = WorkProcessTypeEnum.标准工序,
             ShiftInfoDto shiftInfo = null)
        {
            string categoryCode = "";
            if (isLineSideMaterial)
            {
                var lineMaterialInfo = _lineSideMaterialInfoRep.FirstOrDefault(p => p.Id == belongMaterialId);
                categoryCode = lineMaterialInfo.MaterialNumber;
                ruler = GetRuler(null, categoryCode, true, workProcessTypeEnum: workProcessTypeEnum);
            }
            else
            {
                var materialInfo = _materialsRep.GetAllIncluding(p => p.BelongCategory).FirstOrDefault(p => p.Id == belongMaterialId);
                categoryCode = materialInfo.BelongCategory.CategoryCode;
                ruler = GetRuler(materialInfo, categoryCode, workProcessTypeEnum: workProcessTypeEnum);
                if (ruler.MaterialCategoryInfo != null)
                {
                    categoryCode = ruler.MaterialCategoryInfo.CategoryCode;
                }
            }

            var batchNumberGenerate = _iocManager.Resolve(Type.GetType(ruler.GenerateType)) as IMaterialBatchNumberGenerate;
            batchNumberGenerate.Ruler = ruler;
            batchNumberGenerate.ProductLineNumber = productLine.ProductLineNumber;
            batchNumberGenerate.ProductLineId = productLine.Id;
            batchNumberGenerate.WorkStationNumber = workStationNumber;
            batchNumberGenerate.ShiftInfo = shiftInfo;
            batchNumberGenerate.MaterialInfoId = belongMaterialId;
            batchNumberGenerate.FactoryNumber = factoryNumber;
            batchNumberGenerate.InitQueryInfo(_repsository.GetAll(), categoryCode);
            materialBatchNumberGenerate = batchNumberGenerate;
        }

        public string GenerateMaterialBatchNumber(long belongMaterialId,
            string factoryNumber, ProductLine productLine,
            out int flowCount, string workStationNumber = "",
            ShiftInfoDto shiftInfo = null,
            bool isLineSideMaterial = false,
            WorkProcessTypeEnum workProcessTypeEnum = WorkProcessTypeEnum.标准工序)
        {
            this.IniteMaterialBatchNumberManager(belongMaterialId, factoryNumber, productLine, workStationNumber,isLineSideMaterial, shiftInfo: shiftInfo, workProcessTypeEnum: workProcessTypeEnum);
            var lastBatchNumberInfo = materialBatchNumberGenerate.GetLastBatchNumberInfo();
            materialBatchNumberGenerate.FlowNumber = "0000";
            if (lastBatchNumberInfo != null)
            {
                materialBatchNumberGenerate.FlowNumber = lastBatchNumberInfo.BatchNumber.Substring(lastBatchNumberInfo.BatchNumber.Length - ruler.FlowNumberRulerLength, ruler.FlowNumberRulerLength);
            }

            string batchNumber = "";
            int repeatCount = 0;
            do
            {
                if (ruler.FlowNumberRulerLength > 1)
                {
                    // 该序列号重复
                    if (int.TryParse(materialBatchNumberGenerate.FlowNumber, out flowCount) == false)
                    {
                        flowCount = 0;
                    }

                    materialBatchNumberGenerate.FlowNumber = (flowCount + 1).ToString().PadLeft(ruler.FlowNumberRulerLength, '0');
                    batchNumber = materialBatchNumberGenerate.GenerateMaterialBatchNumber();
                }
                else
                {
                    flowCount = MaterialBatchNumberRuler.ConvertToFlowNumber(materialBatchNumberGenerate.FlowNumber);
                    materialBatchNumberGenerate.FlowNumber = MaterialBatchNumberRuler.ConvertToFlowNumberString(++flowCount);
                    batchNumber = materialBatchNumberGenerate.GenerateMaterialBatchNumber();
                }

                repeatCount++;
            }
            while (materialBatchNumberGenerate.CheckRepeatFlowNumber(batchNumber) && repeatCount < 10);

            if (materialBatchNumberGenerate.CheckRepeatFlowNumber(batchNumber))
            {
                throw new UserFriendlyException("批次号/序列号生成错误，请联系管理员！");
            }

            // flowCount = int.Parse(materialBatchNumberGenerate.FlowNumber);
            _iocManager.Release(materialBatchNumberGenerate);

            return batchNumber;
        }

        private MaterialBatchNumberRuler GetRuler(MaterialInfo materialInfo, string categoryCode, bool isLineSideMaterilInfo = false, WorkProcessTypeEnum workProcessTypeEnum = WorkProcessTypeEnum.标准工序)
        {
            MaterialBatchNumberRuler ruler = null;
            if (isLineSideMaterilInfo == false && workProcessTypeEnum == WorkProcessTypeEnum.标准工序)
            {   // 获取产品的批次号编码规则
                ruler = _rulerRep.FirstOrDefault(p => p.MaterialCategoryInfoId == materialInfo.BelongCategoryId);
                if (ruler == null && MaterialCategory.GetParentCode(categoryCode).StartsWith(MaterialCategory.DefaultStacksCategoryCode))
                {
                    ruler = _rulerRep.GetAllIncluding(p => p.MaterialCategoryInfo).FirstOrDefault(p => p.MaterialCategoryInfo.CategoryCode == MaterialCategory.DefaultStacksCategoryCode);
                }
            }

            if (isLineSideMaterilInfo)
            {
                ruler = new MaterialBatchNumberRuler()
                {
                    FlowNumberRuler = FlowNumberRulerEnum.日,
                    FlowNumberRulerLength = 4,
                    GenerateType = typeof(LineSideMaterialGenerator).FullName,
                };
            }

            // 返回通用编码规则
            if (ruler == null)
            {
                ruler = new MaterialBatchNumberRuler()
                {
                    FlowNumberRuler = FlowNumberRulerEnum.日,
                    FlowNumberRulerLength = 4,
                    GenerateType = typeof(CommonBatchNumberGenerator).FullName,
                };
            }

            return ruler;
        }

        public MaterialBatchNumber GetMaterialBatchNumberInfo(string batchNumber)
        {
            return _repsository.FirstOrDefault(p => p.BatchNumber == batchNumber);
        }

        public MaterialBatchNumber InsertMaterialBatchNumber(MaterialBatchNumber materialBatchNumber)
        {
            if (_repsository.GetAll().Any(p => p.MaterialId == materialBatchNumber.MaterialId && p.BatchNumber == materialBatchNumber.BatchNumber))
            {
                throw new UserFriendlyException("该物料批次号已经存在，请稍后尝试");
            }

            return _repsository.Insert(materialBatchNumber);
        }

        public void SetBatchNumberProductLine(long id, long? belongProductLineId)
        {
            var dataBatchNumber = _repsository.FirstOrDefault(p => p.Id == id);
            dataBatchNumber.CreateProductLineId = belongProductLineId;
        }

        public int GetMaterialBatchNumberFlower(string batchNumber)
        {
            return int.Parse(batchNumber.Substring(batchNumber.Length - ruler.FlowNumberRulerLength, ruler.FlowNumberRulerLength));
        }

        public List<WorkProcessMaterialRecord> GetPrepareWorkProcessMaterialUseInfo(string workOrderNumber, string materialNumber, long workProcessId)
        {

            return _workProcessMaterialRecordRep.GetAll()
                       .Where(p => p.OrderNumber == workOrderNumber && p.WorkProcessId == workProcessId && p.InputMaterialNumber == materialNumber)
                       .GroupBy(p => new { p.OrderNumber, p.InputMaterilId, p.InputUnitName })
                       .Select(p => new WorkProcessMaterialRecord()
                       {
                           OrderNumber = p.Key.OrderNumber,
                           InputMaterilId = p.Key.InputMaterilId,
                           InputMaterialCount = p.Sum(d => d.InputMaterialCount),
                           InputUnitName = p.Key.InputUnitName,
                           BOMMaterialCount = p.Sum(d => d.BOMMaterialCount)
                       }).ToList();
        }

        /// <summary>
        /// 检查批次物料是否可用
        /// </summary>
        /// <param name="materialBatchNumber"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public bool CheckBatchNumberIsDiscard(string materialBatchNumber)
        {
            var batchNumber = _repsository.FirstOrDefault(p => p.BatchNumber == materialBatchNumber);
            if (batchNumber != null)
            {
                if (batchNumber.MaterialStatu == MaterialStatuEnum.封存 || batchNumber.MaterialStatu == MaterialStatuEnum.全部报废)
                {
                    string tipInfo = batchNumber.MaterialStatu == MaterialStatuEnum.封存 ? "请与质量部门联系确认" : "不允许继续使用";

                    throw new UserFriendlyException($"物料：{batchNumber.MaterialNumber},批次号：{batchNumber.BatchNumber}，状态为【{batchNumber.MaterialStatu}】，{tipInfo}！");
                }

                if (!string.IsNullOrEmpty(batchNumber.FromErpBatchNumber))
                {
                    var instockEntity = _inStockInfo.FirstOrDefault(p => p.BatchNo == batchNumber.FromErpBatchNumber);
                    string tipInfo = instockEntity.MaterialStatu == MaterialStatuEnum.封存 ? "请与质量部门联系确认" : "不允许继续使用";
                    if (instockEntity.MaterialStatu == MaterialStatuEnum.封存 || instockEntity.MaterialStatu == MaterialStatuEnum.全部报废)
                    {
                        throw new UserFriendlyException($"物料：{batchNumber.MaterialNumber},源批次号：{batchNumber.FromErpBatchNumber}，状态为【{instockEntity.MaterialStatu}】，{tipInfo}！");
                    }
                }
            }

            return true;
        }
    }
}
