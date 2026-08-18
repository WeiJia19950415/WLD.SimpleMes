using Abp.Authorization;
using Abp.UI;
using Castle.Core.Internal;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.DynamicForms;
using SC.SimpleMes.DynamicForms.DTO;
using SC.SimpleMes.Report;

namespace SC.SimpleMes.Controllers
{
    /// <summary>
    /// 批次号管理接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    //[AbpAuthorize]
    //[ApiController]
    public class QuickPickDDController : SimpleMesControllerBase
    {
        public readonly IReportAppService _reportAppService;
        public QuickPickDDController(IReportAppService reportAppService)
        {
            _reportAppService = reportAppService;
        }


        private readonly int maxLoopCount = 10;

        private readonly int maxTakeCount = 1;

        public class MapResultItem
        {
            public string ANumber { get; set; }

            public string BNumber { get; set; }

            public decimal ANZ { get; set; } = 0;

            public decimal BNZ { get; set; } = 0;

            public decimal Total
            {
                get
                {
                    return ANZ + BNZ;
                }
            }

            public decimal AvgDistance { get; set; }

            public int CH { get; set; }

            public int XH { get; set; }
        }

        public class DDZConfig
        {
            public DDZConfig()
            {
                AMapResultItem = new List<MapResultItem>();
            }

            public List<MapResultItem> AMapResultItem { get; set; }

            public decimal TotalNZ
            {
                get
                {
                    return AMapResultItem.Sum(p => p.Total);
                }
            }
        }

        [HttpPost]
        public async Task<JHTAjaxResponse> QuickPickUpDD(int needDDCount = 8, string AMaterialNumber = "D02.001.004.0000200", string BMaterialNumber = "D02.001.004.0000100")
        {
            List<MapResultItem> mapResult = new List<MapResultItem>();
            var AallResult = await _reportAppService.LoadDDImportantInfosAsync(new JHTPageAjaxResquest<Report.Dto.ReportQueryConditonDto>()
            {
                Condition = new Report.Dto.ReportQueryConditonDto() { MaterialId = 46383, IsInStock = 1 },
                Page = 0,
                PageSize = needDDCount * maxTakeCount
            });

            if (AallResult.List.Count < needDDCount)
            {
                throw new UserFriendlyException("A堆数量不够，请减少配对数量");
            }

            var BallResult = await _reportAppService.LoadDDImportantInfosAsync(new JHTPageAjaxResquest<Report.Dto.ReportQueryConditonDto>()
            {
                Condition = new Report.Dto.ReportQueryConditonDto() { MaterialId = 46390, IsInStock = 1 },
                Page = 0,
                PageSize = needDDCount * maxTakeCount
            });

            if (BallResult.List.Count < needDDCount)
            {
                throw new UserFriendlyException("B堆数量不够，请减少配对数量");
            }

            AallResult.List.ForEach(p =>
            {
                p.DischargeAvgInternalResistance = p.DischargeAvgInternalResistance * 1000 * 32 / 2800;
            });

            BallResult.List.ForEach(p =>
            {
                p.DischargeAvgInternalResistance = p.DischargeAvgInternalResistance * 1000 * 32 / 2800;
            });

            DDCompare dCompare = new DDCompare();
            AallResult.List.Sort(dCompare);// A电堆顺序
            BallResult.List.Sort(dCompare);// B电堆倒序
            BallResult.List.Reverse();
            List<decimal> resultDZ = new List<decimal>();
            var pickUpCount = BallResult.List.Count > AallResult.List.Count ? AallResult.List.Count : BallResult.List.Count;
            for (int i = 0; i < pickUpCount; i++)
            {
                resultDZ.Add(BallResult.List[i].DischargeAvgInternalResistance + AallResult.List[i].DischargeAvgInternalResistance);
                mapResult.Add(new MapResultItem()
                {
                    BNumber = BallResult.List[i].BelongMaterialBatchNumber,
                    BNZ = BallResult.List[i].DischargeAvgInternalResistance,
                    ANumber = AallResult.List[i].BelongMaterialBatchNumber,
                    ANZ = AallResult.List[i].DischargeAvgInternalResistance,
                });
            }

            resultDZ.Sort();
            int loopCount = 0;

            // 电堆配对
            while (resultDZ.Max() - resultDZ.Min() > 2 && maxLoopCount > loopCount)
            {
                var avgCount = resultDZ.Average();
                var absCountArray = new decimal[resultDZ.Count];
                for (int i = 0; i < resultDZ.Count - 1; i++)
                {
                    absCountArray[i] = Math.Abs(resultDZ[i] - avgCount);
                    mapResult[i].AvgDistance = absCountArray[i];
                }

                var maxAvg = mapResult.Find(p => p.AvgDistance == absCountArray.Max());
                var minAvg = mapResult.Find(p => p.AvgDistance == absCountArray.Min());
                MapResultItem middle = new MapResultItem()
                {
                    ANumber = maxAvg.ANumber,
                    BNumber = maxAvg.BNumber,
                    ANZ = maxAvg.ANZ,
                    BNZ = maxAvg.BNZ,
                };

                // 交换最大值与最小值之间的匹配
                maxAvg.BNumber = minAvg.BNumber;
                maxAvg.BNZ = minAvg.BNZ;

                minAvg.BNZ = middle.BNZ;
                minAvg.BNumber = middle.BNumber;

                resultDZ.Clear();
                resultDZ = mapResult.Select(p => p.Total).ToList();
                resultDZ.Sort();
                loopCount++;
            }

            // 成串
            List<DDZConfig> config = new List<DDZConfig>();
            var sortedDD = mapResult.OrderBy(p => p.Total).ToList();
            for (var i = 0; i < sortedDD.Count / 2; i++)
            {
                sortedDD[i].CH = i;
                sortedDD[sortedDD.Count - i].CH = i;
                config.Add(new DDZConfig()
                {
                    AMapResultItem = new List<MapResultItem>() { sortedDD[i], sortedDD[sortedDD.Count - i] }
                });
            }

            // 串成组

            return new JHTAjaxResponse()
            {
                Code = 200,
                Data = mapResult
            };
        }


        public class DDCompare : IComparer<DDImportantInfoDto>
        {
            public int Compare(DDImportantInfoDto x, DDImportantInfoDto y)
            {
                return x.DischargeAvgInternalResistance.CompareTo(y.DischargeAvgInternalResistance);
            }
        }
    }
}
