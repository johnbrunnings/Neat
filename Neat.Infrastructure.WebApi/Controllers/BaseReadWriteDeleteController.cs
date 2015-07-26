using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using MongoRepository;
using Neat.Service;

namespace Neat.Infrastructure.WebApi.Controllers
{
    public class BaseReadWriteDeleteController<T> : BaseApiController where T : class, IEntity<string>, new()
    {
        private readonly IDomainService<T> _domainService;

        public BaseReadWriteDeleteController(IDomainService<T> domainService)
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

        // DELETE api/{T}/{id}
        public void Delete(string id)
        {
            _domainService.Delete(id);
        }

        // GET api/{T}/{id}
        [HttpGet]
        public T GetEmpty()
        {
            return new T();
        }
    }
}