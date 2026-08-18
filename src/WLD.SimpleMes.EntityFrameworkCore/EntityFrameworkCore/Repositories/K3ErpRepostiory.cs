using Abp.Dapper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.WorkProcess.Repository;
using WLD.SimpleMes.WorkProcess;
using WLD.SimpleMes.K3DBInfo;
using Abp.Data;
using Abp.Domain.Uow;
using Dapper;
using Org.BouncyCastle.Asn1.Tsp;

namespace WLD.SimpleMes.EntityFrameworkCore.Repositories
{
    public class K3ErpRepostiory : DapperEfRepositoryBase<K3ERPDbContext, K3MaterialInfo, int>, IK3ErpRepostiory
    {
        public K3ErpRepostiory(IActiveTransactionProvider activeTransactionProvider, ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(activeTransactionProvider, currentUnitOfWorkProvider)
        {
        }

        const string getSnInstockInfo_sql_template = "select D1.FSerialNum as SNumber ,D2.* from (" +
            " select E.FSNListID,F.FSerialNum,E.FTranTypeID from ICSerialFlow as E left join  ICSerial as F " +
            " on E.FSerialID=F.FSerialID where  E.FStatus=1 and E.FTranTypeID=2 and F.FSerialNum=@FSerialNum)as D1  left join" +
            " ( select A.FBillNo as InStockBillNo,A.FDate as WarehousingTime,B.FBillNo as InStockWorkOrderNumber,C.FName as ProjectName,C.FNumber as ProjectNumber,D.FSnListID  " +
            " ,F.FName as MaterialName,F.FNumber as MaterialNumber,G.FName as UnitName,G.FName as UseUnitName" +
            " from ICStockBill as A" +
            " left join ICStockBillEntry as D on A.FInterID=D.FInterID left join ICMO as B on B.FBillNo=D.FSourceBillNo  " +
            " left join t_Supplier as E on A.FSupplyID=E.FItemID " +
            " left join t_ICItem  as F on D.FItemID=F.FItemID " +
            " left join t_UnitGroup as G on F.FStoreUnitID=G.FDefaultUnitID " +
            " left join t_Item_3002 as C on B.[FHeadSelfJ0193]=C.FItemID where D.FSnListID>0 and A.FTranType=2 ) as D2" +
            " on D1.FSNListID=D2.FSnListID";
        public SNInStockInfo GetSNInStockInfo(string snInfo)
        {

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("FSerialNum", string.IsNullOrEmpty(snInfo) ? "" : snInfo);
            return this.GetConnection().QueryFirstOrDefault<SNInStockInfo>(getSnInstockInfo_sql_template, dynamicParameters, this.GetActiveTransaction());
        }

        const string getWorkOrderPickingMaterilInfoSql = "select " +
            " Sum(FQty) as PickingCount  ," +
            " FUnitGroupID as UniteName ," +
            " FSourceBillNo as WorkOrderNumber," +
            " FNumber as MaterialNumber " +
            " from vwICBill_11  with (nolock)" +
            " where FSourceBillNo=@WorkOrderNumber and FNumber=@MaterilNumber " +
            " group by FNumber,FSourceBillNo,FUnitGroupID ";

        public WorkOrderPickingMaterilInfo GetWorkOrderPickingMaterilInfo(string workOrderNumber, string materilNumber)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("WorkOrderNumber", workOrderNumber);
            dynamicParameters.Add("MaterilNumber", materilNumber);
            return this.GetConnection().QueryFirstOrDefault<WorkOrderPickingMaterilInfo>(getWorkOrderPickingMaterilInfoSql, dynamicParameters, this.GetActiveTransaction());
        }

    }
}
