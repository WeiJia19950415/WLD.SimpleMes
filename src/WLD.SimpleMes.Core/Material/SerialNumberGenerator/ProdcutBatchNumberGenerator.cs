using Abp.Linq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Material
{
    public class ProdcutBatchNumberGenerator : CommonBatchNumberGenerator
    {
        public override string BatchNumberPrefix { get; set; } = "";
        public override bool IsSerirasNumber { get; set; } = false;
        protected string PrefixNumber { get; set; }
        public ProdcutBatchNumberGenerator()
        {

        }

        public override string GenerateMaterialBatchNumber()
        {
            return $"{PrefixNumber}{FlowNumber}";
        }

        protected string GetProductEncyptMonthInfo(int? moth)
        {
            var dayInfo = "A";
            if (moth < 10)
            {
                dayInfo = moth.ToString();
            }
            else if (moth > 10)
            {
                var stepCount = moth - 10;
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

        public override void InitQueryInfo(IQueryable<MaterialBatchNumber> initQuery, string categoryCode)
        {
            var now = GetShiftStartDay(this.ShiftInfo);

            var monthInfo = GetProductEncyptMonthInfo(now.Month);

            PrefixNumber = $"{FactoryNumber}{ProductLineNumber}{now.ToString("yy")}{monthInfo}{now.Date.ToString("dd")}{this.ShiftInfo.ShiftCode}";
            this.Query = initQuery
                .WhereIf(this.Ruler.ComputePerProductLine, p => p.CreateProductLineId == this.ProductLineId)
                .Where(p => p.BatchNumber.StartsWith(PrefixNumber));

        }
    }
}
