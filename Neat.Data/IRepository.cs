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
}