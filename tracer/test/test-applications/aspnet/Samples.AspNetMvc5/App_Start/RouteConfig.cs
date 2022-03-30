using System.Web.Mvc;
using System.Web.Routing;

namespace Samples.AspNetMvc5
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "GraphQL",
                url: "graphql/{*slug}",
                defaults: new { controller = "Graphql", action = "Query"},
                namespaces: new [] { "Samples.AspNetMvc4.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                          {
                              controller = "Home",
                              action = "Index",
                              id = UrlParameter.Optional
                          });
        }
    }
}
