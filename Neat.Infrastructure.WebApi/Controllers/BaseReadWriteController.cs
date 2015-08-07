using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using MongoRepository;
using Neat.Service;

namespace Neat.Infrastructure.WebApi.Controllers
{
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
        public IQueryable<T> GetAll()
        {
            return _domainService.GetAll();
        }

        // GET /{T}/{id}
        public T Get(string id)
        {
            return _domainService.GetById(id);
        }

        // POST /{T}
        public void Post([FromBody]T entity)
        {
            _domainService.Add(entity);
        }

        // PUT /{T}
        public void Put([FromBody]T entity)
        {
            _domainService.Update(entity);
        }

        // GET /{T}/{id}
        [HttpGet]
        public T GetEmpty()
        {
            return new T();
        }
    }
}