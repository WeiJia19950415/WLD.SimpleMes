using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Log.Dto
{
    [AutoMapFrom(typeof(JHTAuditLog))]
    public class AuditLogDto : EntityDto<long>
    {

        /// <summary>
        /// 所属应用ID 
        /// </summary>
        public string ApplicationId { get; set; }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        public string BrowserInfo { get; set; }
        /// <summary>
        /// 客户端地址
        /// </summary>
        public string ClientIpAddress { get; set; }
        /// <summary>
        /// 客户端名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 网络参数
        /// </summary>
        public string CustomData { get; set; }
        /// <summary>
        /// 发生的异常信息
        /// </summary>
        public string Exception { get; set; }
        [Ignore]
        public string IsException
        {
            get
            {
                if (string.IsNullOrEmpty(this.Exception))
                {
                    return "正常";
                }
                else
                {
                    return "异常";
                }
            }
        }
        /// <summary>
        /// 容器的执行时间
        /// </summary>
        public int ExecutionDuration { get; set; }
        /// <summary>
        /// 请求发生时间
        /// </summary>
        public DateTime ExecutionTime { get; set; }
        public string ExecutionTimeStr { get { return ExecutionTime.ToString("yyyy-MM-dd HH:mm:ss"); } }
        /// <summary>
        /// 冒充的租户？？？
        /// </summary>
        public int? ImpersonatorTenantId { get; set; }
        /// <summary>
        /// 冒充的用户？？？
        /// </summary>
        public int? ImpersonatorUserId { get; set; }
        /// <summary>
        /// 请求的方法名
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 请求携带的参数
        /// </summary>
        public string Parameters { get; set; }
        /// <summary>
        /// 控制器地址
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 租户ID
        /// </summary>
        public int? TenantId { get; set; }

        public long? UserId { get; set; }
        /// <summary>
        /// 返回结果
        /// </summary>
        public string ReturnValue { get; set; }

        public string AppName { get; set; }
    }
}

