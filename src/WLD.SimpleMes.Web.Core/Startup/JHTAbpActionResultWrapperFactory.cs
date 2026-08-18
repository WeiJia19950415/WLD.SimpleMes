using Abp;
using Abp.AspNetCore.Mvc.Results.Wrapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WLD.SimpleMes.Startup
{
    public class JHTAbpActionResultWrapperFactory : IAbpActionResultWrapperFactory
    {
        public IAbpActionResultWrapper CreateFor(FilterContext context)
        {
            Check.NotNull(context, nameof(context));

            switch (context)
            {
                case ResultExecutingContext resultExecutingContext when resultExecutingContext.Result is ObjectResult:
                    return new JHTAbpObjectActionResultWrapper();

                case ResultExecutingContext resultExecutingContext when resultExecutingContext.Result is JsonResult:
                    return new JHTAbpJsonActionResultWrapper();

                case ResultExecutingContext resultExecutingContext when resultExecutingContext.Result is EmptyResult:
                    return new JHTAbpEmptyActionResultWrapper();

                case PageHandlerExecutedContext pageHandlerExecutedContext when pageHandlerExecutedContext.Result is ObjectResult:
                    return new JHTAbpObjectActionResultWrapper();

                case PageHandlerExecutedContext pageHandlerExecutedContext when pageHandlerExecutedContext.Result is JsonResult:
                    return new JHTAbpJsonActionResultWrapper();

                case PageHandlerExecutedContext pageHandlerExecutedContext when pageHandlerExecutedContext.Result is EmptyResult:
                    return new JHTAbpEmptyActionResultWrapper();

                default:
                    return new NullAbpActionResultWrapper();
            }
        }
    }
}

