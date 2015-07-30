using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using MongoRepository;
using Neat.Service;

namespace Neat.Infrastructure.WebApi.Controllers
{
    public abstract class BaseReadOnlyController<T> : BaseApiController where T : class, IEntity<string>, new()
    {
        private readonly IDomainService<T> _domainService;

        public BaseReadOnlyController(IDomainService<T> domainService)
        {
            _domainService = domainService;
        }

        // GET api/{T}
        [EnableQuery]
        public IQueryable<T> Get()
        {
            return _domainService.GetAll();
        }

        // GET api/{T}/{id}
        public T Get(string id)
        {
            return _domainService.GetById(id);
        }

        // GET api/{T}/getempty
        [HttpGet]
        public T GetEmpty()
        {
            return new T();
        }
    }
}