using Abp.AspNetCore.Mvc.Results.Wrapping;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace WLD.SimpleMes.Startup
{
    public class JHTAbpJsonActionResultWrapper : IAbpActionResultWrapper
    {
        public void Wrap(FilterContext context)
        {
            JsonResult jsonResult = null;

            switch (context)
            {
                case ResultExecutingContext resultExecutingContext:
                    jsonResult = resultExecutingContext.Result as JsonResult;
                    break;

                case PageHandlerExecutedContext pageHandlerExecutedContext:
                    jsonResult = pageHandlerExecutedContext.Result as JsonResult;
                    break;
            }

            if (jsonResult == null)
            {
                throw new ArgumentException("Action Result should be JsonResult!");
            }

            if (!(jsonResult.Value is JHTAjaxResponseBase))
            {
                jsonResult.Value = new JHTAjaxResponse(jsonResult.Value);
            }
        }
    }
}

