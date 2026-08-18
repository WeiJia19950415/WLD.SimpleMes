using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.QualityControl
{
    /// <summary>
    /// 问题记录
    /// </summary>
    public class ProblemRecord : Entity<long>, ICreationAudited, IHasCreationTime, IExtendableObject
    {
        public const string RelationImages = "RelationImages";

        /// <summary>
        /// 关联工单
        /// </summary>
        public string WorkOrderNumber { get; set; }

        #region 问题相关

        /// <summary>
        /// 归属问题id
        /// </summary>
        public long BelongProblemDefineId { get; set; }

        /// <summary>
        /// 问题定义编号
        /// </summary>
        public string QualityProblemDefineNumber { get; set; }

        /// <summary>
        /// 问题具体描述
        /// </summary>
        public string DetailDescretion { get; set; }

        /// <summary>
        /// 原因分析
        /// </summary>
        public string ReasonAnlysis { get; set; }


        /// <summary>
        /// 关联的物料编号
        /// </summary>
        public string MaterialNumber { get; set; }

        /// <summary>
        /// 关联的物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 关联产品序列号【非必填】
        /// </summary>
        public string BatchMaterilaNumber { get; set; }

        /// <summary>
        /// 检查数量【按Bom计算】
        /// </summary>
        public decimal CheckCount { get; set; } = 1;

        /// <summary>
        /// 检查的组装数量
        /// </summary>
        public decimal? CheckWarpCount { get; set; }

        /// <summary>
        /// 问题数量【按Bom计算】
        /// </summary>
        public decimal ProblemCount { get; set; } = 1;

        /// <summary>
        /// 问题数量【按包装单位】
        /// </summary>
        public decimal? ProblemWarpCount { get; set; }

        /// <summary>
        /// 包装单位
        /// </summary>
        public string WarpUnitName { get; set; }

        /// <summary>
        /// 物料单位
        /// </summary>
        public string UnitName { get; set; }

        #endregion

        #region 发生的位置

        /// <summary>
        /// 所属产线
        /// </summary>
        public long BelongProductLineId { get; set; }

        /// <summary>
        /// 所属工位
        /// </summary>
        public long BelongWorkStaionId { get; set; }

        /// <summary>
        /// 上报工序Id
        /// </summary>
        public long BelongWorkProcessId { get; set; }


        /// <summary>
        /// 工序名称
        /// </summary>
        public string WorkProcessName { get; set; }


        #endregion

        #region 责任判定

        /// <summary>
        /// 责任工序Id
        /// </summary>
        public long? ResponsibleWorkProcessId { get; set; }

        /// <summary>
        /// 发生工序编号
        /// </summary>
        public string OnWorkProcessNumber { get; set; }

        /// <summary>
        /// 责任部门ID
        /// </summary>
        public long? ResponsibleDepartmentId { get; set; }

        /// <summary>
        /// 责任班组
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 责任判定人员
        /// </summary>
        public long? AuditorId { get; set; }

        /// <summary>
        /// 责任判定人员姓名
        /// </summary>
        public string AuditorName { get; set; }

        /// <summary>
        /// 判定时间
        /// </summary>
        public DateTime? AuditTime { get; set; }
        #endregion

        /// <summary>
        /// 是否生效
        /// </summary>
        public bool IsEffect { get; set; }

        public string GetBelongCategorCode()
        {
            if (!string.IsNullOrEmpty(QualityProblemDefineNumber))
            {
                var endIndex = this.QualityProblemDefineNumber.LastIndexOf(ProblemCategory.CategoryCodeSepator);
                return this.QualityProblemDefineNumber.Substring(0, endIndex);
            }

            return string.Empty;
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public long? CreatorUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }


        /// <summary>
        /// 关闭时间
        /// </summary>
        public DateTime? CloseTime { get; set; }

        public string ExtensionData { get; set; }

        /// <summary>
        /// 上报人
        /// </summary>
        public string Createor { get; set; }

        /// <summary>
        /// 是否关闭
        /// </summary>
        public bool IsClosed { get; set; }

        public List<string> GetImgs()
        {
            return this.GetData<List<string>>(RelationImages);
        }

        public void SetImgs(List<string> relationImgUrls)
        {
            this.SetData(RelationImages, relationImgUrls);
        }
    }
}
