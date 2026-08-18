using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Handlers;
using Abp.UI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.DynamicForms.DomainEvent;
using WLD.SimpleMes.WorkProcess.Repository;

namespace WLD.SimpleMes.WorkProcess
{
    /// <summary>
    /// 工艺管理
    /// </summary>
    public class ProcessSetManager : ITransientDependency, IEventHandler<FormTemplateInfoUpdateEvent>
    {
        private readonly IRepository<WorkProcessSet, long> _repository;
        private readonly IRepository<WorkProcessSetProductRelation, long> _productRep;
        private readonly IRepository<WorkProcessFormInfoRelation, long> _formRepository;

        private readonly IWorkProcessDapperRepository _workProcessDapperRepository;
        public ProcessSetManager(
            IRepository<WorkProcessSet, long> repository,
            IRepository<WorkProcessSetProductRelation, long> productRep,
            IRepository<WorkProcessFormInfoRelation, long> formRepository,
            IWorkProcessDapperRepository workProcessDapperRepository
            )
        {
            _repository = repository;
            _formRepository = formRepository;
            _productRep = productRep;
            _workProcessDapperRepository = workProcessDapperRepository;
        }


        /// <summary>
        /// 工序是否被使用
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public bool IsProcessIsUsed(long workProcessId)
        {
            return false;
            // return _detailRepository.GetAll().Any(p => p.BelongWorkProcessInfoId == workProcessId);
        }


        /// <summary>
        /// 确保工艺名称和版本号唯一
        /// </summary>
        /// <param name="setName"></param>
        /// <param name="setVersion"></param>
        /// <param name="setId"></param>
        /// <returns></returns>
        public bool CheckUinque(string setName, string setVersion, long setId = 0)
        {
            if (setId > 0)
            {
                return !_repository.GetAll().Any(p => p.Id != setId && p.SetVersion == setVersion && p.SetName != setName);
            }

            return !_repository.GetAll().Any(p => p.SetVersion == setVersion && p.SetName != setName);
        }

        public void AddWorkProcessSetBasicInfo(long id, string setName, string setVersion, string descreption)
        {
            var dataInfo = _repository.FirstOrDefault(p => p.Id == id);
            dataInfo.SetVersion = setVersion;
            dataInfo.SetName = setName;
            dataInfo.Descreption = descreption;
        }


        /// <summary>
        /// 复制工艺
        /// </summary>
        /// <param name="setInfo"></param>
        /// <returns></returns>
        public async Task<WorkProcessSet> CopyWorkProcessSetAsync(WorkProcessSet setInfo)
        {
            setInfo.SetVersion = $"V{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            setInfo.Id = await _repository.InsertAndGetIdAsync(setInfo);

            var dataJobject = JObject.Parse(setInfo.GraphData);
            var metaInfo = dataJobject.SelectTokens("$.nodeList[*].meta");
            List<WorkProcessSetDetail> workProcessSetDetails = new List<WorkProcessSetDetail>();
            foreach (var meta in metaInfo)
            {
                meta["belongWorkProcessSetId"] = setInfo.Id;
                meta["setName"] = setInfo.SetName;
                meta["setVersion"] = setInfo.SetVersion;
                var detail = meta.ToObject<WorkProcessSetDetail>();
                detail.BelongWorkProcessSetId = setInfo.Id;
                workProcessSetDetails.Add(detail);
            }

            setInfo.GraphData = dataJobject.ToString();
            setInfo.SetWorkProcessSetConfigs(workProcessSetDetails);
            return setInfo;
        }

        /// <summary>
        /// 工艺是否被引用
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public bool IsProcessSetIsUsed(long processSetId)
        {
            return _productRep.GetAll().Any(p => p.BelongWorkProcessSetId == processSetId);
        }

        /// <summary>
        /// 绑定产品与工艺关系
        /// </summary>
        /// <param name="processSetProductRelation"></param>
        /// <returns></returns>
        public async Task<WorkProcessSetProductRelation> SetProductProcessSetAsync(WorkProcessSetProductRelation processSetProductRelation)
        {
            processSetProductRelation.Id = await _productRep.InsertAndGetIdAsync(processSetProductRelation);
            return processSetProductRelation;
        }

        /// <summary>
        /// 是否已存在相关的产品工艺关系
        /// </summary>
        /// <param name="workProcessSetProductRelation"></param>
        /// <returns></returns>
        public bool IsExistProductProcessSet(string workProcessSetName, string workProcessSetVersion, long materialId)
        {
            return _productRep.GetAllIncluding(p => p.BelongWorkProcessSet)
                .Any(p => p.BelongWorkProcessSet.SetName == workProcessSetName && p.MaterialInfoId == materialId && p.BelongWorkProcessSet.SetVersion == workProcessSetVersion);
        }

        /// <summary>
        /// 是否已存在相关的产品工艺关系
        /// </summary>
        /// <param name="workProcessSetProductRelation"></param>
        /// <returns></returns>
        public bool IsExistProductProcessSet(long processSetId, long materialId)
        {
            return _productRep.GetAll()
                .Any(p => p.BelongWorkProcessSetId == processSetId && p.MaterialInfoId == materialId);
        }

        /// <summary>
        /// 仅更新产品工艺配置数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="graphData"></param>
        public void UpdateConfigDataOnly(long id, string graphData)
        {
            var dataInfo = _repository.FirstOrDefault(p => p.Id == id);
            dataInfo.GraphData = graphData;
            List<WorkProcessSetDetail> workProcessSetDetails = new List<WorkProcessSetDetail>();

            var dataJobject = JObject.Parse(dataInfo.GraphData);
            var metaInfo = dataJobject.SelectTokens("$.nodeList[*].meta");
            foreach (var meta in metaInfo)
            {
                var detail = meta.ToObject<WorkProcessSetDetail>();
                detail.BelongWorkProcessSetId = dataInfo.Id;
                workProcessSetDetails.Add(detail);
            }

            if (workProcessSetDetails.Select(p => p.BelongWorkProcessInfoId).Distinct().Count() != workProcessSetDetails.Count)
            {
                throw new UserFriendlyException("同一工艺中，工序不能重复！");
            }

            dataInfo.SetWorkProcessSetConfigs(workProcessSetDetails);
        }

        /// <summary>
        /// 设置该产品工艺配置为当前工艺配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task SetCurrentProductProcessSetAsync(long id)
        {
            var currentDataInfo = await this._productRep.FirstOrDefaultAsync(p => p.Id == id);
            currentDataInfo.IsCurrent = true;

            _workProcessDapperRepository.UpdateWorkProcessSetProuductRelationUnCurrentExcept(currentDataInfo.BelongWorkProcessSetId, currentDataInfo.MaterialInfoId);
        }

   

        public void HandleEvent(FormTemplateInfoUpdateEvent eventData)
        {
            _workProcessDapperRepository.UpdateFormRelation(eventData.OldFormTemplateId, eventData.NewFormTemplateId);
        }

    }
}
