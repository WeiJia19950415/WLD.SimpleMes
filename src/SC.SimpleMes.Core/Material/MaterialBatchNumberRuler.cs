using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Material
{
    public class MaterialBatchNumberRuler : Entity<long>
    {
        public long MaterialCategoryInfoId { get; set; }
        public MaterialCategory MaterialCategoryInfo { get; set; }

        public string GenerateType { get; set; }

        /// <summary>
        /// 是否为序列号
        /// </summary>
        public bool IsSerailNumber { get; set; }

        /// <summary>
        /// 流水号规则
        /// </summary>
        public FlowNumberRulerEnum FlowNumberRuler { get; set; }

        /// <summary>
        /// 流水号长度
        /// </summary>
        public int FlowNumberRulerLength { get; set; }

        /// <summary>
        /// 按产线计算流水数
        /// </summary>
        public bool ComputePerProductLine { get; set; } = true;

        /// <summary>
        /// 转换为特殊的流水号
        /// </summary>
        /// <param name="flowNumber"></param>
        public static int ConvertToFlowNumber(string flowNumber)
        {
            int flowNumberOut = 1;
            if (int.TryParse(flowNumber, out flowNumberOut) && flowNumberOut < 10)
            {
                return flowNumberOut;
            }

            int targetCharAscII = (char)(Encoding.ASCII.GetBytes(flowNumber)[0]);
            int ZCharAscIIValue = 90;
            if (targetCharAscII <= 90 && targetCharAscII >= 65)
            {
                int stepCount = ZCharAscIIValue - targetCharAscII;
                return flowNumberOut = 35 - stepCount;
            }

            if (targetCharAscII >= 97 && targetCharAscII <= 122)
            {
                int stepCount = ZCharAscIIValue - targetCharAscII;
                return flowNumberOut = 61 - stepCount;
            }

            throw new Exception("特殊流水号转换失败");
        }

        public static string ConvertToFlowNumberString(int flowNumber)
        {
            if (flowNumber < 10)
            {
                return flowNumber.ToString();
            }

            if (flowNumber > 9 && flowNumber <= 35)
            {
                return ((char)(65 + (flowNumber - 10))).ToString();
            }

            if (flowNumber > 35 && flowNumber <= 61)
            {
                return ((char)(97 + flowNumber - 35)).ToString();
            }

            throw new Exception("特殊流水号转换失败");
        }


    }
    public enum FlowNumberRulerEnum
    {
        日 = 1,
        月 = 2,
        年
    }

}
