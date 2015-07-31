using System.Linq;

namespace Neat.Infrastructure.Session.Storage
{
    public interface ISessionStorageProvider
    {
        IQueryable<Model.Session> GetAll();
        Model.Session GetById(string id);
        Model.Session Add(Model.Session entity);
        void Update(Model.Session entity);
        void Delete(Model.Session entity);
        void Delete(string id);
    }
}