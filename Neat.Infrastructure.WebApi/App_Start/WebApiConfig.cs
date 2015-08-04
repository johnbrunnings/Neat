using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using Neat.Infrastructure.WebApi.HttpMethod;

namespace Neat.Infrastructure.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //http://stackoverflow.com/questions/9499794/single-controller-with-multiple-get-methods-in-asp-net-web-api
            config.Routes.MapHttpRoute(
                "DefaultApiWithId",
                "{controller}/{id}",
                new { id = RouteParameter.Optional },
                new { id = @"\.+" });
            config.Routes.MapHttpRoute(
                "DefaultApiWithActionAndId",
                "{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });
            config.Routes.MapHttpRoute(
                "DefaultApiGet",
                "{controller}",
                new { action = "Get" },
                new { httpMethod = new HttpMethodConstraint(System.Net.Http.HttpMethod.Get) });
            config.Routes.MapHttpRoute(
                "DefaultApiPost",
                "{controller}",
                new { action = "Post" },
                new { httpMethod = new HttpMethodConstraint(System.Net.Http.HttpMethod.Post) });
            config.Routes.MapHttpRoute(
                "DefaultApiPut",
                "{controller}",
                new { action = "Put" },
                new { httpMethod = new HttpMethodConstraint(System.Net.Http.HttpMethod.Put) });
            config.Routes.MapHttpRoute(
                "DefaultApiDelete",
                "{controller}",
                new { action = "Delete" },
                new { httpMethod = new HttpMethodConstraint(System.Net.Http.HttpMethod.Delete) });
            config.Routes.MapHttpRoute(
                "DefaultApiOptions",
                "{controller}",
                new {   httpMethod = new HttpMethodConstraint(
                        System.Net.Http.HttpMethod.Options,
                        System.Net.Http.HttpMethod.Delete,
                        System.Net.Http.HttpMethod.Get,
                        System.Net.Http.HttpMethod.Post,
                        System.Net.Http.HttpMethod.Put
                        )});
            config.Routes.MapHttpRoute(
                "DefaultApiPatch",
                "{controller}",
                new { action = "Patch" },
                new { httpMethod = new PatchHttpMethodConstraint() });

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
            // Default to JSON
            var appXmlType =
                config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            config.EnableCors();
        }
    }
}