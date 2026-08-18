using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.WorkProcess;

namespace WLD.SimpleMes.WorkOrder
{
    public class WorkOrderManager : ITransientDependency
    {
        private readonly IRepository<WorkOrderInfo, long> _workOrderInfoRepository;
        private readonly IRepository<WorkProcessSetProductRelation, long> _productProcessSetRep;
        private readonly IRepository<OrderMaterialProduceStatu, long> _orderMaterialStatuRep;
        private readonly IRepository<WorkProcessOperatorRecord, long> _processOperatorRecordRep;
        public WorkOrderManager(IRepository<WorkOrderInfo, long> repository,
            IRepository<OrderMaterialProduceStatu, long> orderMaterialStatuRep,
            IRepository<WorkProcessOperatorRecord, long> processOperatorRecordRep,
            IRepository<WorkProcessSetProductRelation, long> productProcessSetRep)
        {
            _workOrderInfoRepository = repository;
            _productProcessSetRep = productProcessSetRep;
            _processOperatorRecordRep = processOperatorRecordRep;
            _orderMaterialStatuRep = orderMaterialStatuRep;
        }



        /// <summary>
        /// 生成工单编号
        /// </summary>
        /// <returns></returns>
        public string GeneratWorkOrderNumber()
        {
            var nowDate = DateTime.Now.Date;
            int flowNumber = _workOrderInfoRepository.GetAll().Where(p => p.CreationTime >= nowDate).Count() + 1;//流水号4位
            string dateInfo = DateTime.Now.ToString("yyyyMMddHHmmss");
            return $"{WorkOrderInfo.MOPrefix}-{dateInfo}-{flowNumber.ToString().PadLeft(4, '0')}";
        }

        /// <summary>
        /// 下发工单
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="workShopId"></param>
        /// <param name="productLineId"></param>
        public bool IssuedWorkOrder(WorkOrderInfo workOrderInfo, long workShopId, long productLineId)
        {
            if (workOrderInfo.WorkOrderStatu != WorkOrderStatuEnum.未开始)
            {
                return false;
            }
            workOrderInfo.SetWorkOrderStatu(WorkOrderStatuEnum.已下发);
            workOrderInfo.ProduceLineId = productLineId;
            workOrderInfo.ProduceWorkShopId = workShopId;
            return true;
        }

        public OrderMaterialProduceStatu GetMaterialProduceStatu(string materialBatchNumber)
        {
            return _orderMaterialStatuRep.FirstOrDefault(p => p.MaterialBatchNumber == materialBatchNumber);
        }

        public int GetMaterialFinishedProduces(string workWorderNumber)
        {
            return _orderMaterialStatuRep.Count(p => p.WorkOrderNumber == workWorderNumber && p.ProduceStatus == ProduceStatusEnum.已完成);
        }

        public decimal GetMaterialFinishedProducesWithCurrentMatrialCount(string workWorderNumber)
        {
            return _orderMaterialStatuRep.GetAll().Where(a => a.WorkOrderNumber == workWorderNumber &&
            a.ProduceStatus == ProduceStatusEnum.已完成).Select(a => a.CurrentMatrialCount).Sum();
        }

        public int GetMaterialProductingProduces(string workWorderNumber)
        {
            return _orderMaterialStatuRep.Count(p =>
            p.WorkOrderNumber == workWorderNumber && (p.ProduceStatus != ProduceStatusEnum.未开始));
        }
        public decimal GetMaterialProductingProducesWithCurrentMatrialCount(string workWorderNumber)
        {
            return _orderMaterialStatuRep.GetAll().Where(a => a.WorkOrderNumber == workWorderNumber &&
              a.ProduceStatus != ProduceStatusEnum.未开始).Select(a => a.CurrentMatrialCount).Sum();
        }


        public OrderMaterialProduceStatu SetMaterilStatu(OrderMaterialProduceStatu produceStatu)
        {
            var result = produceStatu.Id == 0 ? _orderMaterialStatuRep.FirstOrDefault(p => p.MaterialBatchNumber == produceStatu.MaterialBatchNumber) : produceStatu;
            if (result != null)
            {
                result.CurrentWorkProcessId = produceStatu.CurrentWorkProcessId;
                result.NormalWorkProcessId = produceStatu.NormalWorkProcessId;
                result.HaveRepaired = result.HaveRepaired ? true : produceStatu.HaveRepaired;// 维修过就是一直维修过
                result.LeftWorkProcessCount = produceStatu.LeftWorkProcessCount > 0 ? produceStatu.LeftWorkProcessCount : result.LeftWorkProcessCount;
                result.ProduceStatus = produceStatu.ProduceStatus;
                result.IsCurrentWorkProcessDone = produceStatu.IsCurrentWorkProcessDone;
                result.IsLastFqcRepaired = produceStatu.IsLastFqcRepaired;
                result.TestCounts = produceStatu.TestCounts;
                result.FailCounts = produceStatu.FailCounts;
            }
            else
            {
                result = produceStatu;
                result.Id = _orderMaterialStatuRep.InsertAndGetId(produceStatu);
            }


            return result;
        }

        public WorkOrderInfo GetWorkOrderByOrderNumber(string fromOrderNumber)
        {
            return this._workOrderInfoRepository.GetAllIncluding(p => p.MaterialInfo).FirstOrDefault(p => p.OrderNumber == fromOrderNumber);
        }

        public bool IsCurrentWorkProcesFinshed(long currentWorkProcessId, string productMaterialBatchNumber)
        {
            return !_processOperatorRecordRep.GetAll().Any(p => p.WorkProcessId == currentWorkProcessId && p.BatchNumber == productMaterialBatchNumber && p.EndTime == null);
        }
    }
}
