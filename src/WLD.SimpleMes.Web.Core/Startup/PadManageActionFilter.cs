using Abp.Application.Features;
using Abp.Dependency;
using Abp.Runtime.Session;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Material;
using WLD.SimpleMes.Models;
using WLD.SimpleMes.WorkOrder;
using WLD.SimpleMes.WorkProcess;
using WLD.SimpleMes.WorkStation;

namespace WLD.SimpleMes.Startup
{
    public class PadManageActionFilter : IAsyncActionFilter, ITransientDependency
    {
        private readonly IMaterialBatchNumberCache _materialBatchNumberCache;

        private readonly WorkStationManager _workStationManager;
        private readonly WorkProcessAppService _workProcessAppService;
        private readonly IAbpSession _abpSession;
        private readonly IFeatureChecker _featureChecker;


        public PadManageActionFilter(
            IMaterialBatchNumberCache materialBatchNumberCache,
            IWorkOrderAppService workOrderAppService,
            WorkStationManager workStationManager,
            IAbpSession abpSession,
            WorkProcessAppService workProcessAppService,
            IFeatureChecker featureChecker)
        {
            _materialBatchNumberCache = materialBatchNumberCache;
            _workStationManager = workStationManager;
            _abpSession = abpSession;
            _workProcessAppService = workProcessAppService;
            _featureChecker = featureChecker;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public async void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            var requestModel = context.ActionArguments.Values.FirstOrDefault() as PadManageRequestModel;

            if (_featureChecker.IsEnabled("JHT.QuartzFeature") == false)
            {
                ajaxResponse.Msg = "定时任务未授权，请联系管理员！";
                ajaxResponse.Code = 500;
                context.Result = new JsonResult(ajaxResponse);
                return;
            }

            if (requestModel == null)
            {
                ajaxResponse.Msg = "接口参数类型设置错误，请联系管理员！";
                ajaxResponse.Code = 500;
                context.Result = new JsonResult(ajaxResponse);
                return;
            }

            if (!string.IsNullOrEmpty(requestModel.ProductMaterialBatchNumber))
            {
                var materialBatchNumberInfo = _materialBatchNumberCache.GetByMaterialBatchNumber(requestModel.ProductMaterialBatchNumber);
                if (materialBatchNumberInfo == null)
                {
                    ajaxResponse.Msg = "批次号不存在，请重新输入！";
                    ajaxResponse.Code = 500;
                    context.Result = new JsonResult(ajaxResponse);
                    return;
                }
            }


            if (!_workStationManager.IsMangerWorkStation(_abpSession.UserId.GetValueOrDefault(), requestModel.CurrentWorkStaionId) && requestModel.CurrentWorkStaionId > 0)
            {
                ajaxResponse.Msg = $"您无权操作该工位，请联系班组长";
                ajaxResponse.Code = 500;
                context.Result = new JsonResult(ajaxResponse);
                return;
            }

            await next();
        }
    }
}
