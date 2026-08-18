using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Json;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.DynamicForms.Repository;
using WLD.SimpleMes.LineSideWarehouse;
using WLD.SimpleMes.Material.SerialNumberGenerator;
using WLD.SimpleMes.WorkOrder;
using WLD.SimpleMes.WorkProcess;
using static WLD.SimpleMes.DynamicForms.DDImportantInfos;

namespace WLD.SimpleMes.DynamicForms
{
    public class FormTemplateInfoManager : ITransientDependency
    {
        private readonly IRepository<FormTemplateInfo, long> _repository;
        private readonly IRepository<WorkProcessFormInfoRelation, long> _formRelationRep;
        private readonly IRepository<FormInfoRecord, long> _recordRepository;
        private readonly IRepository<WorkProcessMaterialRecord, long> _recordMaterialRep;
        private readonly IRepository<DDImportantInfos, long> _ddlImportantInfRep;
        private readonly IFormTemplateInfoDapperRepository _dapperRepository;
        private readonly IRepository<WorkOrderInfo, long> _workOrderInfo;
        private readonly IocManager _iocManager;
        public FormTemplateInfoManager(IRepository<FormTemplateInfo, long> repository,
            IRepository<FormInfoRecord, long> recordRepository,
            IRepository<WorkProcessFormInfoRelation, long> formRelationRep,
            IRepository<DDImportantInfos, long> ddlImportantInfRep,
            IRepository<WorkProcessMaterialRecord, long> recordMaterialRep,
            IocManager iocManager,
            IRepository<WorkOrderInfo, long> workOrderInfo,
            IFormTemplateInfoDapperRepository dapperRepository)
        {
            _repository = repository;
            _dapperRepository = dapperRepository;
            _recordRepository = recordRepository;
            _formRelationRep = formRelationRep;
            _workOrderInfo = workOrderInfo;
            _iocManager = iocManager;
            _ddlImportantInfRep = ddlImportantInfRep;
            _recordMaterialRep = recordMaterialRep;
        }

        public bool IsUniqueForm(string formName, string formVersion, long formId = 0)
        {
            if (formId == 0)
            {
                return _repository.GetAll().Any(p => p.Version == formVersion && p.FormsName == formName);
            }

            return _repository.GetAll().Any(p => p.Version == formVersion && p.FormsName == formName && p.Id != formId);
        }

        public void SetCurrent(long id, string formsName)
        {
            _dapperRepository.SetCurrent(id, formsName);
        }

        public FormInfoRecord LoadFormInfoRecord(long workProcessId, string operatroMaterilBatchNumber, FormUseTypeEnum fromUseType)
        {
            return _recordRepository.GetAll().OrderByDescending(p => p.OperatorTime).FirstOrDefault(p => p.BelongWorkProcessId == workProcessId && p.BelongMaterialBatchNumber == operatroMaterilBatchNumber && p.FormUseType == fromUseType);
        }

        public FormTemplateInfo GetFormTemplateInfoByProcessId(long workProcessId, FormUseTypeEnum formUseType = FormUseTypeEnum.标准工序填报)
        {
            return _formRelationRep.GetAllIncluding(p => p.BelongFormInfo)
                .Where(p => p.BelongWorkProcessId == workProcessId && p.FormUseType == formUseType && p.IsEnabled == true).Select(p => p.BelongFormInfo).FirstOrDefault();
        }

        public FormInfoRecord AddFromInfoRecord(FormInfoRecord formInfoRecord)
        {
            var formTemplate = this._repository.FirstOrDefault(p => p.Id == formInfoRecord.BelongFormId);
            if (!string.IsNullOrEmpty(formTemplate.SaveEntityType) && formInfoRecord.IsDraft == false)
            {
                var entityType = Type.GetType(formTemplate.SaveEntityType);
                var insertData = formInfoRecord.FormRecordData.FromJsonString(entityType, new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });

                var workOrderInfo = _workOrderInfo.FirstOrDefault(p => p.OrderNumber == formInfoRecord.BelongOrderNumber);
                var saveEntity = insertData as BaseSaveEntityInfo;
                saveEntity.BelongProductLineName = formInfoRecord.BelongProductLineName;
                saveEntity.BelongOrderNumber = formInfoRecord.BelongOrderNumber;
                saveEntity.BelongMaterialBatchNumber = formInfoRecord.BelongMaterialBatchNumber;
                saveEntity.MaterialNumber = formInfoRecord.MaterialNumber;
                saveEntity.MaterialId = formInfoRecord.MaterialId;
                saveEntity.MatreialName = formInfoRecord.MatreialName;
                saveEntity.BelongProductLineId = formInfoRecord.BelongProductLineId;
                saveEntity.RecordDate = DateTime.Now;

                if (entityType == typeof(DDImportantInfos))
                {
                    //_ddlImportantInfRep.Delete(p => p.BelongMaterialBatchNumber == formInfoRecord.BelongMaterialBatchNumber);
                    var insertInfo = insertData as DDImportantInfos;
                    var materialList = _recordMaterialRep.GetAll().Where(p => p.ProductBatchNumber == formInfoRecord.BelongMaterialBatchNumber).Select(p => new MaterialRecordSimplyInfo()
                    {
                        MaterialNumber = p.InputMaterialNumber,
                        MatreialName = p.InputMaterialName,
                        BatchNo = p.BatchNo,
                        Supplier = p.Supplier,
                        WarehousingTime = p.WarehousingTime,
                    }).ToList();

                    var lineSideMateralInfo = new List<MaterialRecordSimplyInfo>();
                    foreach (var material in materialList)
                    {
                        if (material.MaterialNumber.StartsWith(LineSideMaterialInfo.MaterialPrefix) || material.BatchNo.IndexOf(LineSideMaterialGenerator.WIP) >= 0)
                        {
                            // 暂不考虑递归处理
                            lineSideMateralInfo.AddRange(_recordMaterialRep.GetAll().Where(p => p.ProductBatchNumber == material.BatchNo)
                                .Select(p => new MaterialRecordSimplyInfo()
                                {
                                    MaterialNumber = p.InputMaterialNumber,
                                    MatreialName = p.InputMaterialName,
                                    BatchNo = p.BatchNo,
                                    Supplier = p.Supplier,
                                    WarehousingTime = p.WarehousingTime,
                                }).ToList());
                        }
                    }

                    materialList.AddRange(lineSideMateralInfo);
                    insertInfo.CheckDate = DateTime.Now;
                    insertInfo.CheckorId = formInfoRecord.OperatorUserId;
                    insertInfo.Checkor = formInfoRecord.Operator;
                    insertInfo.IsAudited = false;
                    insertInfo.ProjectName = workOrderInfo.ProjectName;
                    insertInfo.ProjectNumber = workOrderInfo.ProjectNumber;
                    insertInfo.SetMaterialRecordInfo(materialList);
                    var jobjectData = JObject.Parse(formInfoRecord.FormRecordData);
                    var uloadUrls = jobjectData.SelectToken("$.uploadUrls").ToObject<List<UploadUrlInfos>>();
                    insertInfo.SetUploadImgUrls(uloadUrls);

                    _ddlImportantInfRep.Insert(insertInfo);
                }
            }

            var dataInfo = _recordRepository.GetAllIncluding().AsNoTracking().FirstOrDefault(p => p.Id == formInfoRecord.Id);
            if (formInfoRecord.Id > 0 && dataInfo.IsDraft)
            {
                // 数据库里面是草稿状态，则更新
                return _recordRepository.Update(formInfoRecord);
            }
            else
            {
                formInfoRecord.Id = 0;
                var formInfoId = _recordRepository.InsertAndGetId(formInfoRecord);
                formInfoRecord.Id = formInfoId;
                return formInfoRecord;
            }
        }
    }
}

