using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Configuration;

namespace WLD.SimpleMes.Material
{
    public interface IMaterialBatchNumberGenerate
    {
        /// <summary>
        /// 工厂代码
        /// </summary>
        public string FactoryNumber { get; set; }

        /// <summary>
        /// 产线编号
        /// </summary>
        public string ProductLineNumber { get; set; }

        /// <summary>
        /// 产线Id
        /// </summary>
        public long ProductLineId { get; set; }
        /// <summary>
        /// 工位编号
        /// </summary>
        public string WorkStationNumber { get; set; }

        /// <summary>
        /// 流水号
        /// </summary>
        public string FlowNumber { get; set; }

        public ShiftInfoDto ShiftInfo { get; set; } 

        public long MaterialInfoId { get; set; }

        public MaterialBatchNumberRuler Ruler { get; set; }

        public IQueryable<MaterialBatchNumber> Query { get; set; }
        string GenerateMaterialBatchNumber();
        MaterialBatchNumber GetLastBatchNumberInfo();
        bool CheckRepeatFlowNumber(string batchNumber);

        /// <summary>
        /// 重置基础查询信息
        /// </summary>
        void InitQueryInfo(IQueryable<MaterialBatchNumber> initQuery, string categoryCode);


    }
}
