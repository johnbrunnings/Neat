using System.Web.Http;
using Neat.Infrastructure.WebApi.Attribute;

namespace Neat.Infrastructure.WebApi.Controllers
{
    [CustomCORSPolicy]
    public abstract class BaseApiController : ApiController
    {}
}