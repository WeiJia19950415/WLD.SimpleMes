using Abp.AspNetCore.Mvc.Results.Wrapping;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WLD.SimpleMes.Startup
{
    public class JHTAbpEmptyActionResultWrapper : IAbpActionResultWrapper
    {
        public void Wrap(FilterContext context)
        {
            switch (context)
            {
                case ResultExecutingContext resultExecutingContext:
                    resultExecutingContext.Result = new ObjectResult(new JHTAjaxResponse());
                    return;

                case PageHandlerExecutedContext pageHandlerExecutedContext:
                    pageHandlerExecutedContext.Result = new ObjectResult(new JHTAjaxResponse());
                    return;
            }
        }
    }
}

