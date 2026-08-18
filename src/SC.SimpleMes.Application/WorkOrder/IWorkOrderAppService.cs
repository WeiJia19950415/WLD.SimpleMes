using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JHT.Abp.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.DTO;
using SC.SimpleMes.DynamicForms.DTO;
using SC.SimpleMes.Material;
using SC.SimpleMes.Material.Dto;
using SC.SimpleMes.WorkOrder.DTO;
using SC.SimpleMes.WorkProcess.Dto;
using SC.SimpleMes.WorkProcessSetBom.Dto;
using SC.SimpleMes.WorkStation.Dto;

namespace SC.SimpleMes.WorkOrder
{
    public interface IWorkOrderAppService : IAsyncCrudAppService<WorkOrderInfoDto, long, CommonPageRequestDto, CreateUpdateWorkOrderInfoDto, CreateUpdateWorkOrderInfoDto>, IApplicationService
    {
        /// <summary>
        /// 下发订单
        /// </summary>
        /// <param name="workOrderInfoDto"></param>
        /// <returns></returns>
        public Task<JHTAjaxResponse> IssuedWorkOrderAsync(IssuedWorkOrderInfoDto workOrderInfoDto);

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        public JHTAjaxResponse CancelOrder(EntityDto<long> entityDto);

        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        public JHTAjaxResponse CloseOrder(EntityDto<long> entityDto);

        /// <summary>
        /// 获取工单执行状态
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        List<FinishedProductStatusDto> LoadFinishedProductStatusDtos(EntityDto<long> entityDto);

        /// <summary>
        /// 生成生产订单
        /// </summary>
        /// <returns></returns>
        JHTAjaxResponse<string> GenerateWorkOrderNumber();

        /// <summary>
        /// 获取当前订单的物料编号
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        public List<MaterialBatchNumberDto> LoadWorkOrderBatchNumbers(EntityDto<long> entityDto);

        /// <summary>
        /// 创建当前订单的物料的批次号
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        public JHTAjaxResponse<MaterialBatchNumberDto> CreateWorkOrderBatchNumber(CreateWorkOrderBatchNumberDto entityDto);

        /// <summary>
        /// 撤回下发工单
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        JHTAjaxResponse RevertIssuedWorkOrder(EntityDto<long> entityDto);

        /// <summary>
        /// 根据生产订单查询订单信息
        /// </summary>
        /// <param name="orderNumer"></param>
        /// <returns></returns>
        public WorkOrderInfoDto GetWorkOrderInfoByOrderNumber(string orderNumer);
        bool CheckMaterialBatchNumberWorkProcess(string materialBatchNumber, long currentWorkProcessId, long workProcessSetId, ref string message);
        /// <summary>
        /// 获取产品状态
        /// </summary>
        /// <param name="materialBatchNumber"></param>
        /// <returns></returns>
        ProduceStatusEnum? GetProduceStatus(string materialBatchNumber);

        /// <summary>
        /// 创建工单BOM -- 详情复制工艺BOM
        /// </summary>
        /// <param name="workOrderBomDto"></param>
        Task CreateWorkOrderBOMAsync(WorkOrderBomDto workOrderBomDto);

        /// <summary>
        /// 重置工单BOM
        /// </summary>
        /// <returns></returns>
        Task ResetWorkOrderBOM(long WorkOrderId);

        /// <summary>
        /// 根据工单BOM获取工艺BOM
        /// </summary>
        /// <returns></returns>
        WorkProcessSetBomDto GetSetBOMByWorkOrdBOM(long WorkOrderId);
        List<OrderMaterialProduceStatuDto> LoadWorkOrderProductPercentInfo(EntityDto<long> entityDto);
        List<WorkProcessOperatorRecordDto> LoadWorkOrderOperatorRecordInfo(EntityDto<string> entityDto);
        List<WorkProcessMaterialRecordDto> LoadWorkOrderMaterialRecordHistory(EntityDto<string> entityDto);
        JHTAjaxResponse PauseWorkOrder(EntityDto<long> entityDto);
        List<FormInfoRecordDto> LoadWorkOrderFillReportList(EntityDto<string> entityDto);
        List<WorkProcessMaterialRecordDto> LoadWorkOrderFinalyUsedMaterial(EntityDto<string> entityDto);
        JHTAjaxResponse RecoverWorkOrder(EntityDto<long> entityDto);
        ProductLineDto LoadSelectSnProdcutInfo(EntityDto<string> entityDto);

        /// <summary>
        /// 获取产线未完工订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<WorkOrderInfoDto> GetProductLineWorkOrderInfoByProductLineId(long? id);
        Task<JHTAjaxResponse> CheckIsBomMaterialAsync(string workOrderNumber, string materialNumber);

        /// <summary>
        /// 根据工单Id获取工单BOMItem信息
        /// </summary>
        /// <param name="workOrderId"></param>
        /// <returns></returns>
        Task<WorkOrderBomDto> GetWorkOrderBomInfoAsync(EntityDto<long> workOrderId);
        void UpdateWorkOrderBOMItems(WorkOrderBomDto workOrderBomDto);
        Task<bool> IsCutMaterialEnough(string workOrderNumber, long currentWorProcessId, string materialNumber, bool needRecord = false);
        Task<PagedResultDto<WorkOrderInfoDto>> SearchOverUsedWorOrderInfoAsync(CommonPageRequestDto commonPageRequestDto);
        List<WorkOrderInfoDto> GetCloseWorkOrderInfoByOrderNumber(string orderNumber);
        bool DoWorkOrderHaveRepairdProduct(string orderNumber);
        JHTAjaxResponse<WorkOrderPickingMaterilInfoDto> GetWorkOrderPickingMaterilInfo(WorkOrderPickingMaterilInfoDto workOrderPickingMaterilInfoDto);

        /// <summary>
        /// 客制信息
        /// </summary>
        /// <param name="customerProductInfo"></param>
        /// <returns></returns>
        Task<JHTAjaxResponse> SetWorkOrderCustomerProductInfo(CustomerProductInfoDto customerProductInfo);
    }
}
