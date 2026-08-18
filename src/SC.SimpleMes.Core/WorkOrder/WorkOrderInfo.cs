using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.BOM;
using SC.SimpleMes.Material;
using SC.SimpleMes.WorkStation;

namespace SC.SimpleMes.WorkOrder
{
    /// <summary>
    /// 工单信息
    /// </summary>
    public class WorkOrderInfo : FullAuditedEntity<long>, IMayHaveTenant, IExtendableObject
    {
        public const string MOPrefix = "WO";


        /// <summary>
        /// 工单编号：前缀+生产时间+流水号4位【自动补齐】
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 来源工单：销售订单
        /// </summary>
        public string FromOrderNumber { get; set; }

        /// <summary>
        /// 使用的标准BOMId
        /// </summary>
        public long? BOMId { get; set; }

        /// <summary>
        /// 生产产品Id
        /// </summary>
        public long MaterialInfoId { get; set; }

        /// <summary>
        /// 生产产品信息
        /// </summary>
        public MaterialInfo MaterialInfo { get; set; }

        /// <summary>
        /// 工单BomId
        /// </summary>
        public long? WorkOrderBomId { get; set; }

        /// <summary>
        /// 工单Bom
        /// </summary>
        public WorkOrderBom WorkOrderBom { get; set; }

        /// <summary>
        /// 生产车间
        /// </summary>
        public long? ProduceWorkShopId { get; set; }

        public WorkShopInfo ProduceWorkShop { get; set; }

        /// <summary>
        /// 生产产线
        /// </summary>
        public long? ProduceLineId { get; set; }

        public ProductLine ProduceLine { get; set; }

        /// <summary>
        /// 生产数量
        /// </summary>
        public decimal ProduceCount { get; set; }

        /// <summary>
        /// 已投产数量
        /// </summary>
        public decimal ProdcuingCount { get; set; }

        /// <summary>
        /// 已完工数量
        /// </summary>
        public decimal FinishedCount { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime PlanStartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime PlanEndTime { get; set; }

        /// <summary>
        /// 实际开始时间
        /// </summary>
        public DateTime? ActrualStartTime { get; set; }

        /// <summary>
        /// 实际结束时间
        /// </summary>
        public DateTime? ActuralEndTime { get; set; }

        /// <summary>
        /// 交货时间
        /// </summary>
        public DateTime? DeliveryTime { get; set; }

        /// <summary>
        /// 工单状态
        /// </summary>
        public WorkOrderStatuEnum WorkOrderStatu { get; private set; } = WorkOrderStatuEnum.未开始;

        /// <summary>
        /// 归属租户
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 工艺ID
        /// </summary>
        public long? WorkProcessSetId { get; set; }


        /// <summary>
        /// 设置订单状态
        /// </summary>
        /// <param name="workOrderStatu"></param>
        /// <returns></returns>
        public bool SetWorkOrderStatu(WorkOrderStatuEnum workOrderStatu)
        {
            if (this.WorkOrderStatu == WorkOrderStatuEnum.未开始 && workOrderStatu == WorkOrderStatuEnum.已取消)
            {
                this.WorkOrderStatu = WorkOrderStatuEnum.已取消;
                return true;
            }

            if (this.WorkOrderStatu == WorkOrderStatuEnum.未开始 && workOrderStatu == WorkOrderStatuEnum.已下发)
            {
                this.WorkOrderStatu = WorkOrderStatuEnum.已下发;
                return true;
            }

            if (this.WorkOrderStatu == WorkOrderStatuEnum.已下发 && workOrderStatu == WorkOrderStatuEnum.未开始)
            {
                this.WorkOrderStatu = WorkOrderStatuEnum.未开始;
                this.ProduceLineId = null;
                this.ProduceWorkShopId = null;
                this.WorkProcessSetId = null;
                this.WorkOrderBomId = null;
                //this.BOMId = null;
                return true;
            }

            if (this.WorkOrderStatu == WorkOrderStatuEnum.已下发 && workOrderStatu == WorkOrderStatuEnum.生产中)
            {
                this.WorkOrderStatu = WorkOrderStatuEnum.生产中;
                return true;
            }

            if (this.WorkOrderStatu == WorkOrderStatuEnum.生产中 && workOrderStatu == WorkOrderStatuEnum.已关闭)
            {
                this.WorkOrderStatu = WorkOrderStatuEnum.已关闭;
                return true;
            }

            if (this.WorkOrderStatu == WorkOrderStatuEnum.生产中 && workOrderStatu == WorkOrderStatuEnum.已暂停)
            {
                this.WorkOrderStatu = WorkOrderStatuEnum.已暂停;
                return true;
            }

            if (this.WorkOrderStatu == WorkOrderStatuEnum.已暂停 && workOrderStatu == WorkOrderStatuEnum.生产中)
            {
                this.WorkOrderStatu = WorkOrderStatuEnum.生产中;
                return true;
            }


            if (this.WorkOrderStatu == WorkOrderStatuEnum.已关闭 && workOrderStatu == WorkOrderStatuEnum.生产中)
            {
                this.WorkOrderStatu = WorkOrderStatuEnum.生产中;
                return true;
            }


            return false;
        }


        /// <summary>
        /// 项目ID
        /// </summary>
        public long? ProjectId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectNumber { get; set; }

        /// <summary>
        /// 工单备注说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        public string ExtensionData { get; set; }

        const string CustomerProductInfoConst = "CustomerProductInfo";


        public CustomerProductInfo CustomerProductInfo
        {
            get
            {
                return this.GetCustomerProductInfo();
            }
            set
            {
                this.SetCustomerProductInfo(value);
            }
        }


        /// <summary>
        /// 设置客户产品信息
        /// 用于铭牌打印、报告导出、电堆序列号二维码打印、大屏界面展示
        /// </summary>
        /// <returns></returns>
        public CustomerProductInfo GetCustomerProductInfo()
        {
            return this.GetData<CustomerProductInfo>(CustomerProductInfoConst);
        }

        /// <summary>
        /// 获取客户产品信息
        /// </summary>
        /// <param name="customerProductInfo"></param>
        public void SetCustomerProductInfo(CustomerProductInfo customerProductInfo)
        {
            this.SetData(CustomerProductInfoConst, customerProductInfo);
        }
    }

    /// <summary>
    /// 客户产品信息
    /// </summary>
    public class CustomerProductInfo
    {
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialNumber { get; set; }
    }

    public enum WorkOrderStatuEnum
    {
        已取消 = -1,
        未开始 = 1,
        已下发 = 2,
        生产中 = 3,
        已关闭 = 4,
        已暂停 = 5,
    }
}
