using Abp.Dependency;
using Abp.Linq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SC.SimpleMes.Material
{
    /// <summary>
    /// 电堆编号生产规则
    /// 工厂代码（1位）+产线编码（2位）+ 班别代码（1位） + 生产日期（2+1+1）+流水码4位
    /// </summary>
    public class StackSerialNumberGenerator : CommonBatchNumberGenerator, ITransientDependency
    {
        public override string BatchNumberPrefix { get; set; } = "";
        public override bool IsSerirasNumber { get; set; } = true;

        public override string GenerateMaterialBatchNumber()
        {
            var dayInfo = GetEncryptDayInfo(this.ShiftInfo);
            return $"{BatchNumberPrefix}{FactoryNumber}{ProductLineNumber}{this.ShiftInfo.ShiftCode}{dayInfo}{FlowNumber}";
        }


        public static DateTime ParseProductDateTime(string searilNumber)
        {
            if (searilNumber.Length == 12)
            {
                int year = 2020 + int.Parse(searilNumber.Substring(4, 2));
                int month = RevertMonthInfo(searilNumber.Substring(6, 1));
                int day = RevertEncyptDateInfo(searilNumber.Substring(7, 1));
                return new DateTime(year, month, day);
            }

            return DateTime.MinValue;

        }


        public override MaterialBatchNumber GetLastBatchNumberInfo()
        {
            return base.GetLastBatchNumberInfo();
        }

        public override void InitQueryInfo(IQueryable<MaterialBatchNumber> initQuery, string categoryCode)
        {
            this.Query = initQuery
               .WhereIf(!string.IsNullOrEmpty(categoryCode), p => p.MaterialNumber.StartsWith(categoryCode))
               .WhereIf(this.Ruler.ComputePerProductLine, p => p.CreateProductLineId == this.ProductLineId);
        }
    }
}
