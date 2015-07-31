using System.Linq;
using MongoRepository;

namespace Neat.Application
{
    public class DomainApplication<T> : IDomainApplication<T> where T : class, IEntity<string>, new()
    {
        private readonly Data.IRepository<T> _repository;

        public DomainApplication(Data.IRepository<T> repository)
        {
            _repository = repository;
        }

        public IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T GetById(string id)
        {
            return _repository.GetById(id);
        }

        public T Add(T entity)
        {
            return _repository.Add(entity);
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
        }

        public void Delete(T entity)
        {
            _repository.Delete(entity);
        }

        public void Delete(string id)
        {
            _repository.Delete(id);
        }
    }
}