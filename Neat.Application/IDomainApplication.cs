using System.Linq;
using MongoRepository;
using Neat.Infrastructure.Security.Attribute;
using Neat.Infrastructure.Validation.Attribute;

namespace Neat.Application
{
    public interface IDomainApplication<T> where T : class, IEntity<string>, new()
    {
        [SecuredAction(Action = "Read")]
        IQueryable<T> GetAll();
        [SecuredAction(Action = "Read")]
        T GetById(string id);
        [SecuredAction(Action = "Create", Parameters = "entity")]
        [Validate]
        T Add(T entity);
        [SecuredAction(Action = "Update", Parameters = "entity")]
        [Validate]
        void Update(T entity);
        [SecuredAction(Action = "Delete", Parameters = "entity")]
        void Delete(T entity);
        [SecuredAction(Action = "Delete", Parameters = "id")]
        void Delete(string id);
    }
}