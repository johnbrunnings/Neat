using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using MongoRepository;
using Neat.Service;

namespace Neat.Infrastructure.WebApi.Controllers
{
    public class BaseReadWriteController<T> : BaseApiController where T : class, IEntity<string>, new()
    {
        private readonly IDomainService<T> _domainService;

        public BaseReadWriteController(IDomainService<T> domainService)
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

        // POST api/{T}
        public void Post([FromBody]T entity)
        {
            _domainService.Add(entity);
        }

        // PUT api/{T}
        public void Put([FromBody]T entity)
        {
            _domainService.Update(entity);
        }

        // GET api/{T}/{id}
        [HttpGet]
        public T GetEmpty()
        {
            return new T();
        }
    }
}