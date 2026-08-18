using Abp.Dapper.Repositories;
using Abp.Dependency;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using SC.SimpleMes.WorkProcess.Repository;
using Abp.UI;
using SC.SimpleMes.QualityControl;
using Microsoft.EntityFrameworkCore;
using SC.SimpleMes.Material;
using Abp.Collections.Extensions;

namespace SC.SimpleMes.WorkProcess
{
    public class WorkProcessInfoManager : ITransientDependency
    {
        private readonly IRepository<WorkProcessInfo, long> _repository;

        private readonly IRepository<WorkProcessStationRelation, long> _stationRelation;

        private readonly IWorkProcessDapperRepository _dapperRepository;

        private readonly IRepository<WorkProcessMaterialRecord, long> _materialRecordRepository;

        private readonly IRepository<WorkProcessFormInfoRelation, long> _formRealtionRep;

        private readonly IRepository<WorkProcessOperatorRecord, long> _workProcessOperatorRep;

        private readonly IRepository<ProblemRecord, long> _problemRecordRep;

        private readonly IRepository<MaterialBatchNumber, long> _materialBatchNumberRep;

        private readonly IWorkProcessMaterialRecordDapperRep _workProcessMaterialRecordDapperRep;

        private readonly IRepository<MaterialDiscardRecord, long> _materialDiscardRecordRep;
        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkProcessInfoManager(
            IRepository<WorkProcessInfo, long> repository,
            IWorkProcessDapperRepository dapperRepository,
            IRepository<WorkProcessMaterialRecord, long> materialRecordRepository,
            IRepository<WorkProcessFormInfoRelation, long> formRealtionRep,
            IRepository<WorkProcessOperatorRecord, long> workProcessOperatorRep,
            IWorkProcessMaterialRecordDapperRep workProcessMaterialRecordDapperRep,
            IRepository<ProblemRecord, long> problemRecordRep,
            IRepository<MaterialBatchNumber, long> materialBatchNumberRep,
            IRepository<MaterialDiscardRecord, long> materialDiscardRecordRep,
            IRepository<WorkProcessStationRelation, long> stationRelation)
        {
            _repository = repository;
            _workProcessOperatorRep = workProcessOperatorRep;
            _materialRecordRepository = materialRecordRepository;
            _stationRelation = stationRelation;
            _dapperRepository = dapperRepository;
            _formRealtionRep = formRealtionRep;
            _problemRecordRep = problemRecordRep;
            _materialBatchNumberRep = materialBatchNumberRep;
            _workProcessMaterialRecordDapperRep = workProcessMaterialRecordDapperRep;
            _materialDiscardRecordRep = materialDiscardRecordRep;
        }

        /// <summary>
        /// 检查工序编号是否唯一
        /// </summary>
        /// <returns></returns>
        public bool CheckUniqueWorkProcessNumber(string workProcessNumber, long workProcessId = 0)
        {
            if (workProcessId == 0)
            {
                return !_repository.GetAll().Any(p => p.ProcessNumber == workProcessNumber);
            }

            return !_repository.GetAll().Any(p => p.ProcessNumber == workProcessNumber && p.Id != workProcessId);
        }

        /// <summary>
        /// 添加工序信息
        /// </summary>
        /// <param name="workProcess"></param>
        /// <param name="relations"></param>
        /// <returns></returns>
        public async Task<WorkProcessInfo> AddWorkProcessInfoAsync(WorkProcessInfo workProcess, List<WorkProcessStationRelation> relations)
        {
            var processId = await _repository.InsertAndGetIdAsync(workProcess);
            workProcess.Id = processId;
            relations.ForEach(p =>
            {
                p.BelongWorkProcessId = processId;
            });

            _dapperRepository.BatchInsert(relations);
            return workProcess;
        }

        /// <summary>
        /// 更新工序，及工序与工位的关系
        /// </summary>
        /// <param name="workProcess"></param>
        /// <param name="relations"></param>
        /// <returns></returns>
        public async Task UpdateWorkProcessAsync(WorkProcessInfo workProcess, List<WorkProcessStationRelation> relations)
        {
            await _repository.UpdateAsync(workProcess);
            _dapperRepository.DeleteWorkProcessStationByProcessId(workProcess.Id);
            _dapperRepository.BatchInsert(relations);
        }

        /// <summary>
        /// 启用禁用工序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task ToggleEnableWorkProcessAsync(long id)
        {
            var data = await _repository.GetAsync(id);
            data.IsEnable = !data.IsEnable;
        }

        /// <summary>
        /// 当前批次号是否已经被使用
        /// </summary>
        /// <param name="inputMaterialBatchNumber"></param>
        /// <returns></returns>
        public bool CanMaterialBatchNumberBeUse(string inputMaterialBatchNumber, out string message, decimal inputCout = 0, long procesRecordId = 0)
        {
            message = string.Empty;
            var batchNumberInfo = _materialBatchNumberRep.FirstOrDefault(p => p.BatchNumber == inputMaterialBatchNumber);
            if (batchNumberInfo == null)
            {
                return true;
            }

            var costMaterialCount = _materialRecordRepository.GetAll()
                .Where(p => p.InputMaterialBatchNumber == inputMaterialBatchNumber)
                .WhereIf(procesRecordId > 0, p => p.WorkProcessId == procesRecordId)// 查询本工序使用到的该批次物料数量
                .Sum(p => p.BOMMaterialCount);

            var discardMaterialCount = _materialDiscardRecordRep.GetAll()
                .Where(p => p.BatchNumber == inputMaterialBatchNumber)
                .Sum(p => p.DiccardCount)
                ;
            var nowUserCount = costMaterialCount + inputCout + discardMaterialCount;
            batchNumberInfo.BOMMaterialCount = batchNumberInfo.BOMMaterialCount == 0 ? batchNumberInfo.MatrialCount : batchNumberInfo.BOMMaterialCount;
            if (batchNumberInfo != null && nowUserCount <= batchNumberInfo.BOMMaterialCount)
            {
                return true;
            }

            if (batchNumberInfo != null && batchNumberInfo.BOMMaterialCount > costMaterialCount && batchNumberInfo.BOMMaterialCount < nowUserCount)
            {
                message = $"物料{batchNumberInfo.MaterialName},批次{inputMaterialBatchNumber}投入量超过了最大允许投入量";
                return false;
            }

            if (batchNumberInfo != null && batchNumberInfo.BOMMaterialCount <= nowUserCount)
            {
                message = $"物料{batchNumberInfo.MaterialName},批次{inputMaterialBatchNumber}已经使用完毕，请勿重复投入";
                return false;
            }

            return true;
        }

        /// <summary>
        /// 设置工序可加工的物料
        /// </summary>
        /// <param name="id"></param>
        /// <param name="configMaterialIds"></param>
        /// <returns></returns>
        public async Task SetConfigMaterialAsync(long id, List<long> configMaterialIds)
        {
            var data = await _repository.GetAsync(id);
            data.SetConfigMaterials(configMaterialIds);
        }

        /// <summary>
        /// 加载当前工序配置的表单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<WorkProcessFormInfoRelation> LoadWorkProcessRelationForms(long id)
        {
            return _formRealtionRep.GetAllIncluding(p => p.BelongFormInfo).Where(p => p.BelongWorkProcessId == id && p.BelongFormInfo.IsCurrent == true).ToList();
        }

        /// <summary>
        /// 设置工序关联的表单信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="formTemplateId"></param>
        /// <returns></returns>
        public void SetWorkProcessFormTemlate(List<WorkProcessFormInfoRelation> workProcessFormInfoRelations)
        {
            _dapperRepository.BatchInsertFormTemplate(workProcessFormInfoRelations);
        }

        public bool IsUsedFormTemplateId(long workProcessId, long formTemplateId, FormUseTypeEnum formUseTypeEnum)
        {
            return _formRealtionRep.GetAll().Any(p => p.BelongFormInfoId == formTemplateId && p.BelongWorkProcessId == workProcessId && p.FormUseType == formUseTypeEnum);
        }

        public async Task ToggleWorkProcessFormEnabledAsync(long id)
        {
            var dataRelation = await _formRealtionRep.FirstOrDefaultAsync(p => p.Id == id);
            dataRelation.IsEnabled = !dataRelation.IsEnabled;
        }

        public async Task SetWorkProcessFormUseTypeAsync(long id, FormUseTypeEnum formUseType)
        {
            var dataRelation = await _formRealtionRep.FirstOrDefaultAsync(p => p.Id == id);
            dataRelation.FormUseType = formUseType;
        }

        public List<WorkProcessInfo> LoadWorkProcessInfoByStationId(long id)
        {
            return _stationRelation.GetAllIncluding(p => p.BelongWorkProcess).Where(p => p.BelongWorkStationId == id).ToList().Select(p => p.BelongWorkProcess).ToList();
        }

        public long StartWorkProcessOperatorRecord(WorkProcessOperatorRecord workProcessOperatorRecord)
        {
            var operatorRecord = _workProcessOperatorRep.GetAll()
                 .FirstOrDefault(
                 p => p.WorkProcessId == workProcessOperatorRecord.WorkProcessId
                 && p.WorkStationId == workProcessOperatorRecord.WorkStationId
                 && p.BatchNumber == workProcessOperatorRecord.BatchNumber
                 && p.WorkProcessOperateType == workProcessOperatorRecord.WorkProcessOperateType
                 && p.EndTime == null
                 );
            if (operatorRecord == null)
            {
                return _workProcessOperatorRep.InsertAndGetId(workProcessOperatorRecord);
            }
            else
            {
                return operatorRecord.Id;
            }
        }

        public void EndWorkProcessOperatorRecord(WorkProcessOperatorRecord workProcessOperatorRecord, string wipBatchNumber = "")
        {
            WorkProcessOperatorRecord operatorRecord = _workProcessOperatorRep.FirstOrDefault(p => p.Id == workProcessOperatorRecord.Id);
            if (operatorRecord == null)
            {
                operatorRecord = _workProcessOperatorRep.GetAll()
               .OrderByDescending(p => p.StartTime).FirstOrDefault(
               p => p.WorkProcessId == workProcessOperatorRecord.WorkProcessId
               && p.WorkStationId == workProcessOperatorRecord.WorkStationId
               && p.BatchNumber == workProcessOperatorRecord.BatchNumber
               && p.WorkProcessOperateType == workProcessOperatorRecord.WorkProcessOperateType
               && p.EndTime == null
               );
            }

            if (operatorRecord != null)
            {
                if (operatorRecord.CurrentOperatroAccountId == 0)
                {
                    operatorRecord.CurrentOperatroAccountId = workProcessOperatorRecord.CurrentOperatroAccountId;
                    operatorRecord.WorkStationId = workProcessOperatorRecord.WorkStationId;
                    operatorRecord.ProductLineId = workProcessOperatorRecord.ProductLineId;
                }

                operatorRecord.IsNormalFinish = workProcessOperatorRecord.IsNormalFinish;
                operatorRecord.EndTime = DateTime.Now;
                if (!string.IsNullOrEmpty(wipBatchNumber))
                {
                    operatorRecord.BatchNumber = wipBatchNumber;
                }

                operatorRecord.OperatorDescreption = workProcessOperatorRecord.OperatorDescreption;
                operatorRecord.CostTimeSeconds = (long)(DateTime.Now - operatorRecord.StartTime).TotalSeconds;
            }
        }

        public void AddMaterilRecord(WorkProcessMaterialRecord workProcessMaterialRecord)
        {
            _materialRecordRepository.Insert(workProcessMaterialRecord);
        }

        public void BatchAddMaterilRecord(List<WorkProcessMaterialRecord> listInputMateril)
        {
            if (listInputMateril.Count > 0)
            {
                _workProcessMaterialRecordDapperRep.BatchInsertMaterialRecord(listInputMateril);
            }
        }

        public void BatchAddMaterilRecordHistory(List<WorkProcessMaterialRecordHistory> listInputMateril)
        {
            if (listInputMateril.Count > 0)
            {
                _workProcessMaterialRecordDapperRep.BatchInsertMaterialRecordHistory(listInputMateril);
            }
        }

        public WorkProcessOperatorRecord LoadWorkProcessRecord(long workProcessId, string operatroMaterilBatchNumber, WorkProcessOperateTypeEnum processOperateType)
        {
            return _workProcessOperatorRep.GetAll().OrderByDescending(p => p.StartTime).FirstOrDefault(p => p.WorkProcessId == workProcessId && p.BatchNumber == operatroMaterilBatchNumber && p.WorkProcessOperateType == processOperateType);
        }



        public long SaveProblemRecord(ProblemRecord problemRecord)
        {
            if (!string.IsNullOrEmpty(problemRecord.BatchMaterilaNumber))
            {
                if (_problemRecordRep.GetAll().Any(p => p.IsClosed == false && p.BatchMaterilaNumber == problemRecord.BatchMaterilaNumber && p.BelongProblemDefineId == problemRecord.BelongProblemDefineId))
                {
                    throw new UserFriendlyException("该类型的问题已经存在，请勿重复提交");
                }
            }

            return _problemRecordRep.InsertAndGetId(problemRecord);
        }

        public List<WorkProcessMaterialRecord> LoadWorkProcessMaterilRecord(long workProcessId, string operatroMaterilBatchNumber)
        {
            return _materialRecordRepository.GetAll().AsNoTracking().Where(p => p.WorkProcessId == workProcessId && p.ProductBatchNumber == operatroMaterilBatchNumber).ToList();
        }

        public void BatchAddMaterilDiscardRecords(List<MaterialDiscardRecord> materialDiscardRecords)
        {
            if (materialDiscardRecords.Count > 0)
            {
                _workProcessMaterialRecordDapperRep.BatchInsertMaterialDiscardRecords(materialDiscardRecords);
            }
        }
    }
}
