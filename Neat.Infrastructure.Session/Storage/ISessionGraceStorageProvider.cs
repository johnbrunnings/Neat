using System.Linq;

namespace Neat.Infrastructure.Session.Storage
{
    public interface ISessionGraceStorageProvider
    {
        IQueryable<Model.SessionGrace> GetAll();
        Model.SessionGrace GetById(string id);
        Model.SessionGrace Add(Model.SessionGrace entity);
        void Update(Model.SessionGrace entity);
        void Delete(Model.SessionGrace entity);
        void Delete(string id);
    }
}