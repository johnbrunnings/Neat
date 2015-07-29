using System.Linq;
using MongoRepository;

namespace Neat.Infrastructure.Security.Storage
{
    public interface IStorageApplication<T> where T : class, IEntity<string>, new()
    {
        IQueryable<T> GetAll();
        T GetById(string id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(string id);
    }
}