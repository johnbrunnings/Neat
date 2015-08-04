using System;
using System.Linq;
using MongoRepository;

namespace Neat.Data
{
    public interface IRepository<T> where T : class, IEntity<string>, new()
    {
        IQueryable<T> GetAll();
        T GetById(string id);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(string id);
    }

    public interface IGenericRepository
    {
        IQueryable<object> GetAll(Type type);
        object GetById(Type type, string id);
        object Add(Type type, object entity);
        void Update(Type type, object entity);
        void Delete(Type type, object entity);
        void Delete(Type type, string id);
    }
}