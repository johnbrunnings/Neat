using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using MongoRepository;
using Neat.Infrastructure.WebApi.Filter;
using Neat.Service;

namespace Neat.Infrastructure.WebApi.Controllers
{
    [ApiAuthorizationFilter]
    public abstract class BaseReadWriteController<T> : BaseApiController where T : class, IEntity<string>, new()
    {
        private readonly IDomainService<T> _domainService;

        public BaseReadWriteController(IDomainService<T> domainService)
        {
            _domainService = domainService;
        }

        // GET /{T}/getall
        [EnableQuery]
        [HttpGet]
        public virtual IQueryable<T> GetAll()
        {
            return _domainService.GetAll();
        }

        // GET /{T}/{id}
        public virtual T Get(string id)
        {
            return _domainService.GetById(id);
        }

        // POST /{T}
        public virtual T Post([FromBody]T entity)
        {
            return _domainService.Add(entity);
        }

        // PUT /{T}
        public virtual void Put([FromBody]T entity)
        {
            _domainService.Update(entity);
        }

        // GET /{T}/{id}
        [HttpGet]
        public virtual T GetEmpty()
        {
            return new T();
        }
    }
}