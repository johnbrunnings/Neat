using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace Neat.Infrastructure.WebApi.HttpMethod
{
    public class PatchHttpMethodConstraint : HttpMethodConstraint
    {
        protected override bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values,
            HttpRouteDirection routeDirection)
        {
            if (request.Method == new System.Net.Http.HttpMethod("PATCH"))
                return true;
            return base.Match(request, route, parameterName, values, routeDirection);
        }
    }
}