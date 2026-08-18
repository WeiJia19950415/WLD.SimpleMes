using Abp.AspNetCore.Mvc.Extensions;
using Abp.AspNetCore.Mvc.Results;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Events.Bus;
using Abp.Events.Bus.Exceptions;
using Abp.Web.Models;
using Castle.Core.Logging;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Startup
{
    public class JHTAbpAuthorizationFilter : IAsyncAuthorizationFilter, ITransientDependency
    {
        public ILogger Logger { get; set; }

        private readonly IAuthorizationHelper _authorizationHelper;
        private readonly IErrorInfoBuilder _errorInfoBuilder;
        private readonly IEventBus _eventBus;

        public JHTAbpAuthorizationFilter(
            IAuthorizationHelper authorizationHelper,
            IErrorInfoBuilder errorInfoBuilder,
            IEventBus eventBus)
        {
            _authorizationHelper = authorizationHelper;
            _errorInfoBuilder = errorInfoBuilder;
            _eventBus = eventBus;
            Logger = NullLogger.Instance;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var endpoint = context?.HttpContext?.GetEndpoint();
            // Allow Anonymous skips all authorization
            if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
            {
                return;
            }

            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }

            //TODO: Avoid using try/catch, use conditional checking
            try
            {
                await _authorizationHelper.AuthorizeAsync(
                    context.ActionDescriptor.GetMethodInfo(),
                    context.ActionDescriptor.GetMethodInfo().DeclaringType
                );
            }
            catch (AbpAuthorizationException ex)
            {
                Logger.Warn(ex.ToString(), ex);

                _eventBus.Trigger(this, new AbpHandledExceptionData(ex));

                var statuCode = context.HttpContext.User.Identity.IsAuthenticated
                            ? (int)System.Net.HttpStatusCode.Forbidden
                            : (int)System.Net.HttpStatusCode.Unauthorized;
                if (ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
                {
                    var errorInfo = _errorInfoBuilder.BuildForException(ex);
                    string message = string.IsNullOrEmpty(errorInfo.Details) ? errorInfo.Message : errorInfo.Details;
                    context.Result = new ObjectResult(new JHTAjaxResponse(statuCode, message, true))
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };
                }
                else
                {
                    context.Result = new ChallengeResult();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString(), ex);

                _eventBus.Trigger(this, new AbpHandledExceptionData(ex));

                if (ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
                {
                    var errorInfo = _errorInfoBuilder.BuildForException(ex);
                    string message = string.IsNullOrEmpty(errorInfo.Details) ? errorInfo.Message : errorInfo.Details;
                    context.Result = new ObjectResult(new JHTAjaxResponse((int)System.Net.HttpStatusCode.InternalServerError, message))
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };
                }
                else
                {
                    //TODO: How to return Error page?
                    context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.InternalServerError);
                }
            }
        }
    }
}

