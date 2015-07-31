using System.Data;
using System.Linq;
using MongoDB.Driver.Linq;
using MongoRepository;

namespace Neat.Data.Mongo
{
    public class MongoRepository<T> : IRepository<T> where T : class, IEntity<string>, new()
    {
        private readonly MongoRepository.IRepository<T> _repository;

        public MongoRepository()
        {
            _repository = new MongoRepository.MongoRepository<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _repository.Collection.AsQueryable();
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
            if (string.IsNullOrEmpty(entity.Id))
            {
                throw new DataException("Entity must provide an Id for an Update!");
            }
            var existingEntity = GetById(entity.Id);
            if (existingEntity == null)
            {
                throw new DataException(string.Format("No entity exists with Id {0}!", entity.Id));
            }
            _repository.Update(entity);
        }

        public void Delete(T entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                throw new DataException("Entity must provide an Id for an Delete!");
            }
            var existingEntity = GetById(entity.Id);
            if (existingEntity == null)
            {
                throw new DataException(string.Format("No entity exists with Id {0}!", entity.Id));
            }
            _repository.Delete(entity);
        }

        public void Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new DataException("Entity must provide an Id for a Delete!");
            }
            var existingEntity = GetById(id);
            if (existingEntity == null)
            {
                throw new DataException(string.Format("No entity exists with Id {0}!", id));
            }
            _repository.Delete(id);
        }
    }
}