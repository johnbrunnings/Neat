using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using MongoRepository;
using Neat.Infrastructure.WebApi.Filter;
using Neat.Service;

namespace Neat.Infrastructure.WebApi.Controllers
{
    [ApiAuthorizationFilter]
    public abstract class BaseReadOnlyController<T> : BaseApiController where T : class, IEntity<string>, new()
    {
        private readonly IDomainService<T> _domainService;

        public BaseReadOnlyController(IDomainService<T> domainService)
        {
            _domainService = domainService;
        }

        // GET /{T}/getall
        [EnableQuery]
        [HttpGet]
        public IQueryable<T> GetAll()
        {
            return _domainService.GetAll();
        }

        // GET /{T}/{id}
        public T Get(string id)
        {
            return _domainService.GetById(id);
        }

        // GET /{T}
        [HttpGet]
        public T GetEmpty()
        {
            return new T();
        }
    }
}