using Abp.AspNetCore.Mvc.Results.Wrapping;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace WLD.SimpleMes.Startup
{
    public class JHTAbpObjectActionResultWrapper : IAbpActionResultWrapper
    {
        public void Wrap(FilterContext context)
        {
            ObjectResult objectResult = null;

            switch (context)
            {
                case ResultExecutingContext resultExecutingContext:
                    objectResult = resultExecutingContext.Result as ObjectResult;
                    break;

                case PageHandlerExecutedContext pageHandlerExecutedContext:
                    objectResult = pageHandlerExecutedContext.Result as ObjectResult;
                    break;
            }

            if (objectResult == null)
            {
                throw new ArgumentException("Action Result should be JsonResult!");
            }

            if (!(objectResult.Value is JHTAjaxResponseBase))
            {
                objectResult.Value = new JHTAjaxResponse(objectResult.Value);
                objectResult.DeclaredType = typeof(JHTAjaxResponse);
            }
        }
    }
}

