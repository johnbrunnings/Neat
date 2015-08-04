using System.Linq;
using MongoRepository;
using Neat.Infrastructure.Security.Attribute;

namespace Neat.Application
{
    public interface IDomainApplication<T> where T : class, IEntity<string>, new()
    {
        [SecuredAction(Action = "Read")]
        IQueryable<T> GetAll();
        [SecuredAction(Action = "Read")]
        T GetById(string id);
        [SecuredAction(Action = "Create", Parameters = "entity")]
        T Add(T entity);
        [SecuredAction(Action = "Update", Parameters = "entity")]
        void Update(T entity);
        void Delete(T entity);
        void Delete(string id);
    }
}