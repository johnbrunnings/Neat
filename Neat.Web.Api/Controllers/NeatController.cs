using Neat.Infrastructure.WebApi.Controllers;
using Neat.Model;
using Neat.Service;

namespace Neat.Web.Api.Controllers
{
    public class NeatController : BaseReadWriteDeleteController<NeatExample>
    {
        public NeatController(IDomainService<NeatExample> domainService)
            : base(domainService)
        {}
    }
}