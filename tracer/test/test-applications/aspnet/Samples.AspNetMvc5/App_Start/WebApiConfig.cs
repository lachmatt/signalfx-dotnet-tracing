using Samples.AspNetMvc5.Handlers;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Samples.AspNetMvc5
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            string enableSystemDiagnosticsTracing = ConfigurationManager.AppSettings["EnableSystemDiagnosticsTracing"] ??
                                                    Environment.GetEnvironmentVariable("EnableSystemDiagnosticsTracing");

            if (string.Equals(enableSystemDiagnosticsTracing, "true", StringComparison.OrdinalIgnoreCase))
            {
                config.EnableSystemDiagnosticsTracing();
            }

            config.MapHttpAttributeRoutes();

            config.MessageHandlers.Add(new HttpCodeSettingHandler());

            // Replace default exception handler
            // config.Services.Replace(typeof(IExceptionHandler), new CustomTracingExceptionHandler());

            // Add global message handler
            config.MessageHandlers.Add(new PassThroughQuerySuccessMessageHandler());

            // Convention-based routing.
            config.Routes.MapHttpRoute(
                name: "ApiConventions",
                routeTemplate: "api2/{action}/{value}",
                defaults: new
                {
                    controller = "Conventions", 
                    value = RouteParameter.Optional
                });

            // Add a new /handler-api base path that will be handled by a per-route message handler
            config.Routes.MapHttpRoute(
                name: "Route2",
                routeTemplate: "handler-api/{controller}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional
                },
                constraints: null,
                handler: new TerminatingQuerySuccessMessageHandler()  // per-route message handler
            );
        }
    }

    public class HttpCodeSettingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }
    }
}
