using Abp.Linq.Extensions;
using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Configuration;

namespace SC.SimpleMes.Material
{
    /// <summary>
    /// 通用的物料编码规则
    /// 物料ID+工厂代码（1位）+产线编码（2位）+ 班别代码（1位） + 生产日期（2+1+1）+流水码4位
    /// </summary>
    public class CommonBatchNumberGenerator : IMaterialBatchNumberGenerate, ITransientDependency
    {
        public CommonBatchNumberGenerator()
        {
        }

        public virtual string BatchNumberPrefix { get; set; } = "BO-";
        public virtual bool IsSerirasNumber { get; set; } = false;
        public string FactoryNumber { get; set; }

        /// 班次信息
        /// </summary>
        public ShiftInfoDto ShiftInfo { get; set; }
        public string ProductLineNumber { get; set; }
        public string WorkStationNumber { get; set; }
        public string FlowNumber { get; set; }

        public long MaterialInfoId { get; set; }
        public MaterialBatchNumberRuler Ruler { get; set; }
        public IQueryable<MaterialBatchNumber> Query { get; set; }
        public long ProductLineId { get; set; }

        public virtual string GenerateMaterialBatchNumber()
        {
            var dayInfo = GetEncryptDayInfo(this.ShiftInfo);
            return $"{BatchNumberPrefix}{MaterialInfoId}{FactoryNumber}{ProductLineNumber}{this.ShiftInfo.ShiftCode}{dayInfo}{FlowNumber}";
        }




        /// <summary>
        /// 获取加密日期信息
        /// </summary>
        /// <returns></returns>
        protected static string GetEncryptDayInfo(ShiftInfoDto shiftInfo)
        {
            string year = (DateTime.Now.Year - 2020).ToString().PadLeft(2, '0');
            var now = GetShiftStartDay(shiftInfo);

            string monthInfo = GetEncyptMonthInfo(now.Month);
            string dayInfo = GetEncyptDateInfo(now.Day);

            return $"{year}{monthInfo}{dayInfo}";
        }

        public static DateTime GetShiftStartDay(ShiftInfoDto shiftInfo)
        {
            var now = DateTime.Now;
            if (shiftInfo.IsAcrrossDay && now.TimeOfDay < shiftInfo.StartWorkTime && now.TimeOfDay > TimeSpan.Parse("00:00:00"))
            {
                now = now.AddDays(-1);
            }

            return now.Date;
        }


        protected static string GetEncyptMonthInfo(int? month)
        {
            if (month == null)
            {
                month = DateTime.Now.Month;
            }

            string monthInfo = "A";
            if (month != 1)
            {
                int stepCount = month.GetValueOrDefault() - 1;
                int targetCharAscII = (char)(Encoding.ASCII.GetBytes(monthInfo)[0] + stepCount);
                if (targetCharAscII < (int)'I')
                {
                    monthInfo = ((char)targetCharAscII).ToString();
                }
                else if (targetCharAscII >= 'I' && targetCharAscII < 'O')
                {
                    monthInfo = ((char)(targetCharAscII + 1)).ToString();
                }
                else if (targetCharAscII >= 'O')
                {
                    monthInfo = ((char)(targetCharAscII + 2)).ToString();
                }
            }

            return monthInfo;
        }


        protected static int RevertMonthInfo(string month)
        {
            for (int i = 1; i <= 12; i++)
            {
                if (GetEncyptMonthInfo(i) == month)
                {
                    return i;
                }
            }

            throw new Exception("非法的月份格式");
        }

        protected static int RevertEncyptDateInfo(string Day)
        {
            int dayInfo = 1;
            if (int.TryParse(Day, out dayInfo))
            {
                return dayInfo;
            }

            for (int i = 10; i <= 31; i++)
            {
                if (GetEncyptDateInfo(i) == Day)
                {
                    return i;
                }
            }

            throw new Exception("非法的日期格式");
        }


        protected static string GetEncyptDateInfo(int? date)
        {
            if (date == null)
            {
                date = DateTime.Now.Day;
            }

            var dayInfo = "A";
            if (date < 10)
            {
                dayInfo = date.ToString();
            }
            else if (date > 10)
            {
                var stepCount = date - 10;
                int targetCharAscII = (char)(Encoding.ASCII.GetBytes(dayInfo)[0] + stepCount);
                if (targetCharAscII < (int)'I')
                {
                    dayInfo = ((char)targetCharAscII).ToString();
                }
                else if (targetCharAscII >= 'I' && targetCharAscII < 'N')
                {
                    dayInfo = ((char)(targetCharAscII + 1)).ToString();
                }
                else if (targetCharAscII >= 'N')
                {
                    dayInfo = ((char)(targetCharAscII + 2)).ToString();
                }
            }

            return dayInfo;
        }

        public virtual MaterialBatchNumber GetLastBatchNumberInfo()
        {
            MaterialBatchNumber lastBatchNumberInfo = null;
            switch (this.Ruler.FlowNumberRuler)
            {
                case FlowNumberRulerEnum.日:
                    var nowDate = DateTime.Now.Date;
                    lastBatchNumberInfo = this.Query.Where(p => p.CreationTime > nowDate).OrderByDescending(p => p.FlowNumber).FirstOrDefault();
                    break;
                case FlowNumberRulerEnum.月:
                    var nowMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date;
                    lastBatchNumberInfo = this.Query.Where(p => p.CreationTime > nowMonth).OrderByDescending(p => p.FlowNumber).FirstOrDefault(); ;
                    break;
                case FlowNumberRulerEnum.年:
                    var yeraMonth = new DateTime(DateTime.Now.Year, 1, 1).Date;
                    lastBatchNumberInfo = this.Query.Where(p => p.CreationTime > yeraMonth).OrderByDescending(p => p.FlowNumber).FirstOrDefault();
                    break;
                default:
                    break;
            }

            return lastBatchNumberInfo;
        }

        public bool CheckRepeatFlowNumber(string batchNumber)
        {
            var flowNumber = GetFlowNumber(batchNumber);
            switch (this.Ruler.FlowNumberRuler)
            {
                case FlowNumberRulerEnum.日:
                    return Query.Any(p => p.CreationTime > DateTime.Now.Date && (p.BatchNumber == batchNumber || p.FlowNumber == flowNumber));
                case FlowNumberRulerEnum.月:
                    var nowMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date;
                    return Query.Any(p => p.CreationTime > nowMonth && (p.BatchNumber == batchNumber || p.FlowNumber == flowNumber));
                case FlowNumberRulerEnum.年:
                    var yeraMonth = new DateTime(DateTime.Now.Year, 1, 1).Date;
                    return Query.Any(p => p.CreationTime > yeraMonth && (p.BatchNumber == batchNumber || p.FlowNumber == flowNumber));
                default:
                    return false;
            }
        }

        public int GetFlowNumber(string batchNumber)
        {
            string flowNumber = batchNumber.Substring(batchNumber.Length - this.Ruler.FlowNumberRulerLength, this.FlowNumber.Length);
            if (Ruler.FlowNumberRulerLength == 1)
            {
                return MaterialBatchNumberRuler.ConvertToFlowNumber(flowNumber);
            }

            return int.Parse(flowNumber);
        }

        public virtual void InitQueryInfo(IQueryable<MaterialBatchNumber> initQuery, string categoryCode)
        {
            this.Query = initQuery
                .WhereIf(!string.IsNullOrEmpty(categoryCode), p => p.MaterialNumber.StartsWith(categoryCode))
                .WhereIf(this.Ruler.ComputePerProductLine, p => p.CreateProductLineId == this.ProductLineId);
        }

    }
}
