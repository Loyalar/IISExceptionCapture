using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;

namespace IISExceptionCapture.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Filters.Add(new GlobalExceptionFilterAttribute());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
        public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
        {
            public override void OnException(HttpActionExecutedContext context)
            {
                var exceptionType = context.Exception.GetType().ToString();
                var exception = context.Exception.InnerException ?? context.Exception;

                try
                {
                    if (exception != null)
                        Logger.WriteToFile(exception.Message, exception);
                }
                finally
                {
                    base.OnException(context);
                }
            }
        }
    }
}
