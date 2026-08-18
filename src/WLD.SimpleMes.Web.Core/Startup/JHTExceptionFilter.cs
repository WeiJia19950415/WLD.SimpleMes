using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.Mvc.Extensions;
using Abp.AspNetCore.Mvc.Results;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Events.Bus;
using Abp.Events.Bus.Exceptions;
using Abp.Logging;
using Abp.Runtime.Validation;
using Abp.Web.Models;
using Castle.Core.Logging;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Startup
{
    public class JHTExceptionFilter : IAsyncExceptionFilter, IExceptionFilter, ITransientDependency
    {
        public ILogger Logger { get; set; }

        public IEventBus EventBus { get; set; }

        private readonly IErrorInfoBuilder _errorInfoBuilder;
        private readonly IAbpAspNetCoreConfiguration _configuration;

        public JHTExceptionFilter(IErrorInfoBuilder errorInfoBuilder, IAbpAspNetCoreConfiguration configuration)
        {
            _errorInfoBuilder = errorInfoBuilder;
            _configuration = configuration;

            Logger = NullLogger.Instance;
            EventBus = NullEventBus.Instance;
        }

        public void OnException(ExceptionContext context)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }

            var memberInfo = context.ActionDescriptor.GetMethodInfo();
            var defaultValue = _configuration.DefaultWrapResultAttribute;

            var wrapResultAttribute = memberInfo.GetCustomAttributes(true).OfType<WrapResultAttribute>().FirstOrDefault()
                   ?? memberInfo.ReflectedType?.GetTypeInfo().GetCustomAttributes(true).OfType<WrapResultAttribute>().FirstOrDefault()
                   ?? defaultValue;

            if (wrapResultAttribute.LogError)
            {
                LogHelper.LogException(Logger, context.Exception);
            }

            HandleAndWrapException(context, wrapResultAttribute);
        }

        protected virtual void HandleAndWrapException(ExceptionContext context, WrapResultAttribute wrapResultAttribute)
        {
            if (!ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
            {
                return;
            }

            GetStatusCode(context, wrapResultAttribute.WrapOnError);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;

            if (!wrapResultAttribute.WrapOnError)
            {
                return;
            }
            var errorInfo = _errorInfoBuilder.BuildForException(context.Exception);
            string validErrorInfo = "";
            if (errorInfo.ValidationErrors != null)
            {
                validErrorInfo = "验证错误信息如下：" + string.Join(",", errorInfo.ValidationErrors.Select(p => p.Message));
            }

            var message = !string.IsNullOrEmpty(errorInfo.Details) ? errorInfo.Details : errorInfo.Message;

            message = $"{message}{validErrorInfo}";
            context.Result = new ObjectResult(
                new JHTAjaxResponse(
                    (int)HttpStatusCode.InternalServerError,
                    message,
                    context.Exception is AbpAuthorizationException
                )
            );

            EventBus.Trigger(this, new AbpHandledExceptionData(context.Exception));

            context.Exception = null; //Handled!
        }

        protected virtual int GetStatusCode(ExceptionContext context, bool wrapOnError)
        {
            if (context.Exception is AbpAuthorizationException)
            {
                return context.HttpContext.User.Identity.IsAuthenticated
                    ? (int)HttpStatusCode.Forbidden
                    : (int)HttpStatusCode.Unauthorized;
            }

            if (context.Exception is AbpValidationException)
            {
                return (int)HttpStatusCode.BadRequest;
            }

            if (context.Exception is EntityNotFoundException)
            {
                return (int)HttpStatusCode.NotFound;
            }

            if (wrapOnError)
            {
                return (int)HttpStatusCode.InternalServerError;
            }

            return context.HttpContext.Response.StatusCode;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }
    }
}

