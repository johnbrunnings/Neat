using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.Practices.Unity;
using Neat.Infrastructure.Security;

namespace Neat.Infrastructure.WebApi.Filter
{
    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    public class ApiAuthorizationFilter : ActionFilterAttribute
    {
        [Dependency]
        public ISecurityAuthorizationProvider SecurityAuthorizationProvider { get; set; }

        [Dependency]
        public ISecurityUserProvider SecurityUserProvider { get; set; }

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            VerifyAuthTokenHeader(filterContext.Request);

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            SetupAuthTokenHeader(actionExecutedContext);

            base.OnActionExecuted(actionExecutedContext);
        }

        private void VerifyAuthTokenHeader(HttpRequestMessage request)
        {
            var headers = request.Headers;
            var authTokenHeader = headers.GetValues("X-Auth-Token").FirstOrDefault();
            if (authTokenHeader != null)
            {
                SecurityAuthorizationProvider.GetAuthorizationForAccessToken(authTokenHeader);
            }
        }

        private void SetupAuthTokenHeader(HttpActionExecutedContext actionExecutedContext)
        {
            var response = actionExecutedContext.Response;
            if (response != null)
            {
                var user = SecurityUserProvider.GetCurrentUser();
                if (user != null)
                {
                    var accessToken = SecurityAuthorizationProvider.GetAccessTokenForLoggedInUser(user.Id);

                    response.Headers.Add("X-Auth-Token", accessToken);
                }
            }
        }
    }
}