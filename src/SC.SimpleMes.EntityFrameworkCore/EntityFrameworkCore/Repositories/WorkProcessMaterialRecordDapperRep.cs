using Abp.Dapper.Repositories;
using Abp.Data;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.WorkProcess;
using SC.SimpleMes.WorkProcess.Repository;
using Dapper;
using DapperExtensions;
using SC.SimpleMes.QualityControl;

namespace SC.SimpleMes.EntityFrameworkCore.Repositories
{
    public class WorkProcessMaterialRecordDapperRep : DapperEfRepositoryBase<LogReportDbContext, WorkProcessMaterialRecord, long>,
        IWorkProcessMaterialRecordDapperRep
    {
        public WorkProcessMaterialRecordDapperRep(IActiveTransactionProvider activeTransactionProvider, ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(activeTransactionProvider, currentUnitOfWorkProvider)
        {
        }



        public void BatchInsertMaterialRecord(List<WorkProcessMaterialRecord> materialRecords)
        {
            if (materialRecords == null || materialRecords.Count == 0)
            {
                return;
            }

            var firstMaterilaRecord = materialRecords.First();

            // 删除原有记录，重新生产
            this.GetConnection().Execute(@"delete from WorkProcessMaterialRecords where WorkProcessId=@WorkProcessId and ProductBatchNumber=@ProductBatchNumber",
                new
                {
                    WorkProcessId = firstMaterilaRecord.WorkProcessId,
                    ProductBatchNumber = firstMaterilaRecord.ProductBatchNumber
                }, this.GetActiveTransaction());

            var insertData = materialRecords.Select(p =>
             new
             {
                 WrokShopId = p.WrokShopId,
                 ProductLineId = p.ProductLineId,
                 OrderNumber = p.OrderNumber,
                 WorkProcessId = p.WorkProcessId,
                 WorkProcessName = p.WorkProcessName,
                 WorkStationId = p.WorkStationId,
                 WorkStationName = p.WorkStationName,
                 InputMaterilId = p.InputMaterilId,
                 InputMaterialBatchNumber = p.InputMaterialBatchNumber,
                 InputMaterialCount = p.InputMaterialCount,
                 ProductBatchNumber = p.ProductBatchNumber,
                 OutRangeCount = p.OutRangeCount,
                 InputMaterialNumber = p.InputMaterialNumber,
                 Supplier = p.Supplier,
                 WarehousingTime = p.WarehousingTime,
                 BatchNo = p.BatchNo,
                 InputMaterialName = p.InputMaterialName,
                 InputUnitName = p.InputUnitName,
                 BOMUnitName = p.BOMUnitName,
                 BOMMaterialCount = p.BOMMaterialCount,
                 CreateTime = DateTime.Now,
                 IsRepairedInput = p.IsRepairedInput
             }).ToArray();
            this.GetConnection().Execute(@"INSERT INTO [WorkProcessMaterialRecords] ([WrokShopId]
           ,[ProductLineId]
           ,[OrderNumber]
           ,[ProductBatchNumber]
           ,[WorkProcessId]
           ,[WorkProcessName]
           ,[WorkStationId]
           ,[WorkStationName]
           ,[InputMaterilId]
           ,[InputMaterialBatchNumber]
           ,[InputMaterialCount]
           ,[InputMaterialNumber]
           ,[Supplier]
           ,[WarehousingTime]
           ,[BatchNo]
           ,[InputMaterialName]
           ,[InputUnitName] 
           ,[OutRangeCount]
           ,[BOMUnitName]
           ,[BOMMaterialCount]
           ,[CreateTime]
            ,[IsRepairedInput]
            )
            VALUES (
@WrokShopId,@ProductLineId,@OrderNumber,@ProductBatchNumber,@WorkProcessId,@WorkProcessName,@WorkStationId,
@WorkStationName,@InputMaterilId,@InputMaterialBatchNumber,@InputMaterialCount,@InputMaterialNumber,@Supplier,@WarehousingTime,@BatchNo,
@InputMaterialName,@InputUnitName,@OutRangeCount,@BOMUnitName,@BOMMaterialCount,@CreateTime,@IsRepairedInput
            )", insertData, this.GetActiveTransaction());


        }

        public void BatchInsertMaterialRecordHistory(List<WorkProcessMaterialRecordHistory> materialRecords)
        {
            var insertData = materialRecords.Select(p =>
            new
            {
                WrokShopId = p.WrokShopId,
                ProductLineId = p.ProductLineId,
                OrderNumber = p.OrderNumber,
                WorkProcessId = p.WorkProcessId,
                WorkProcessName = p.WorkProcessName,
                WorkStationId = p.WorkStationId,
                WorkStationName = p.WorkStationName,
                InputMaterilId = p.InputMaterilId,
                InputMaterialBatchNumber = p.InputMaterialBatchNumber,
                InputMaterialCount = p.InputMaterialCount,
                ProductBatchNumber = p.ProductBatchNumber,
                OutRangeCount = p.OutRangeCount,
                InputMaterialNumber = p.InputMaterialNumber,
                Supplier = p.Supplier,
                WarehousingTime = p.WarehousingTime,
                BatchNo = p.BatchNo,
                InputMaterialName = p.InputMaterialName,
                InputUnitName = p.InputUnitName,
                BOMUnitName = p.BOMUnitName,
                BOMMaterialCount = p.BOMMaterialCount,
                ChangeReason = p.ChangeReason,
                CreateTime = DateTime.Now,
            }).ToArray();

            this.GetConnection().Execute(@"INSERT INTO [WorkProcessMaterialRecordHistory] ([WrokShopId]
           ,[ProductLineId]
           ,[OrderNumber]
           ,[ProductBatchNumber]
           ,[WorkProcessId]
           ,[WorkProcessName]
           ,[WorkStationId]
           ,[WorkStationName]
           ,[InputMaterilId]
           ,[InputMaterialBatchNumber]
           ,[InputMaterialCount]
           ,[InputMaterialNumber]
           ,[Supplier]
           ,[WarehousingTime]
           ,[BatchNo]
           ,[InputMaterialName]
           ,[InputUnitName] 
           ,[OutRangeCount]
           ,[BOMUnitName]
           ,[BOMMaterialCount]
           ,[ChangeReason]
           ,[CreateTime] )
            VALUES (
@WrokShopId,@ProductLineId,@OrderNumber,@ProductBatchNumber,@WorkProcessId,@WorkProcessName,@WorkStationId,@WorkStationName,
@InputMaterilId,@InputMaterialBatchNumber,@InputMaterialCount,@InputMaterialNumber,@Supplier,@WarehousingTime,@BatchNo
,@InputMaterialName,@InputUnitName,@OutRangeCount,@BOMUnitName,@BOMMaterialCount,@ChangeReason,@CreateTime
            )", materialRecords, this.GetActiveTransaction());
        }


        /// <summary>
        /// 批量删除产品使用到的物料信息
        /// </summary>
        /// <param name="batchNumber"></param>
        public void BatchDelMaterialRecord(string batchNumber)
        {
            this.GetConnection().Execute(@"Delete from WorkProcessMaterialRecordHistory  where ProductBatchNumber=@ProductBatchNumber", new { ProductBatchNumber = batchNumber }, this.GetActiveTransaction());
            this.GetConnection().Execute(@"Delete from WorkProcessMaterialRecords  where ProductBatchNumber=@ProductBatchNumber", new { ProductBatchNumber = batchNumber }, this.GetActiveTransaction());
        }

        /// <summary>
        /// 批量插入物料数据
        /// </summary>
        /// <param name="materialDiscardRecords"></param>
        public void BatchInsertMaterialDiscardRecords(List<MaterialDiscardRecord> materialDiscardRecords)
        {
            var insertData = materialDiscardRecords.Select(p =>
           new
           {
               RecordDate = DateTime.Now,
               ProblemRecordId = p.ProblemRecordId,
               MaterialNumber = p.MaterialNumber,
               MaterialName = p.MaterialName,
               BatchNumber = p.BatchNumber,
               ErpBatchNumber = p.ErpBatchNumber,
               Supplier = p.Supplier,
               DiccardCount = p.DiccardCount,
               UnitName = p.UnitName,
               RecordUserId = p.RecordUserId,
               RecordUserName = p.RecordUserName,
               DiccardWarpCount = p.DiccardWarpCount,
               WrapUnitName = p.WrapUnitName,
               DiscardType = p.DiscardType,
               DeiscardReasonDescreption = p.DeiscardReasonDescreption,
               ProblemDefineId = p.ProblemDefineId,
               ProblemDefineNumber = p.ProblemDefineNumber,
               WorkOrderNumber = p.WorkOrderNumber,
               CreateTime = DateTime.Now,
           }).ToArray();

            StringBuilder builder = new StringBuilder();
            builder.Append("Insert INTO [MaterialDiscardRecord] (WorkOrderNumber,[ProblemRecordId] ,[MaterialNumber],[MaterialName]," +
                "[BatchNumber],[ErpBatchNumber] ,[Supplier]  ,[DiccardCount],[UnitName],DiccardWarpCount,WrapUnitName,[RecordUserId],[RecordUserName]," +
                "[DiscardType],[DeiscardReasonDescreption],[ProblemDefineId],[ProblemDefineNumber],[RecordDate])" +
                "  VALUES (@WorkOrderNumber,@ProblemRecordId ,@MaterialNumber,@MaterialName," +
                "@BatchNumber,@ErpBatchNumber ,@Supplier  ,@DiccardCount,@UnitName,@DiccardWarpCount,@WrapUnitName,@RecordUserId,@RecordUserName," +
                "@DiscardType,@DeiscardReasonDescreption,@ProblemDefineId,@ProblemDefineNumber,@RecordDate" +
                ")");
            this.GetConnection()
                .Execute(builder.ToString(),
                insertData,
                this.GetActiveTransaction());
        }
    }
}
