using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Material
{
    public class EnergyStorageBatchNumberGenerator : CommonBatchNumberGenerator, ITransientDependency
    {
        public override string BatchNumberPrefix { get; set; } = "";
        public override bool IsSerirasNumber { get; set; } = true;

        public override MaterialBatchNumber GetLastBatchNumberInfo()
        {
            return base.GetLastBatchNumberInfo();
        }

        public override void InitQueryInfo(IQueryable<MaterialBatchNumber> initQuery, string categoryCode)
        {
            base.InitQueryInfo(initQuery, categoryCode);
        }


        public override string GenerateMaterialBatchNumber()
        {
            return $"{FactoryNumber}{ProductLineNumber}{this.ShiftInfo.ShiftCode}{DateTime.Now.ToString("yyMMdd")}{FlowNumber}";
            return $"22Z1201V{FlowNumber}";// 暂时TODO  22年 Z制造 12月01个合同 V代表储能模块
        }

    }
}
