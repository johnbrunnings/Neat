using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http.Cors;
using Microsoft.Practices.ServiceLocation;
using Neat.Infrastructure.WebApi.Context;

namespace Neat.Infrastructure.WebApi.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class CustomCORSPolicyAttribute : System.Attribute, ICorsPolicyProvider
    {
        public async Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var corsRequestContext = request.GetCorsRequestContext();
            var originRequested = corsRequestContext.Origin;
            if (await IsOriginApproved(originRequested))
            {
                // Grant CORS request
                var policy = new CorsPolicy();
                policy.Origins.Add(originRequested);
                policy.Methods.Add("GET");
                policy.Methods.Add("POST");
                policy.Methods.Add("PUT");
                policy.Methods.Add("DELETE");
                policy.Methods.Add("PATCH");
                policy.Methods.Add("OPTIONS");
                policy.Headers.Add("accept");
                policy.Headers.Add("content-type");
                policy.Headers.Add("X-Auth-Token");
                policy.Headers.Add("Origin");
                policy.ExposedHeaders.Add("DataServiceVersion");
                policy.ExposedHeaders.Add("MaxDataServiceVersion");
                return policy;
            }
            // Reject CORS request
            return null;
        }

        private async Task<bool> IsOriginApproved(string originRequested)
        {
            var domains = ServiceLocator.Current.GetInstance<ICORSContext>().Domains;
            return domains != null && domains.Contains(originRequested);
        }
    }
}