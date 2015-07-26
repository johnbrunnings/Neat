using System.Linq;
using MongoRepository;
using Neat.Application;

namespace Neat.Service
{
    public class DomainService<T> : IDomainService<T> where T : class, IEntity<string>, new()
    {
        private readonly IDomainApplication<T> _domainApplication;

        public DomainService(IDomainApplication<T> domainApplication)
        {
            _domainApplication = domainApplication;
        }

        public IQueryable<T> GetAll()
        {
            return _domainApplication.GetAll();
        }

        public T GetById(string id)
        {
            return _domainApplication.GetById(id);
        }

        public void Add(T entity)
        {
            _domainApplication.Add(entity);
        }

        public void Update(T entity)
        {
            _domainApplication.Update(entity);
        }

        public void Delete(T entity)
        {
            _domainApplication.Delete(entity);
        }

        public void Delete(string id)
        {
            _domainApplication.Delete(id);
        }
    }
}