using System.Linq;

namespace Neat.Infrastructure.Session.Storage
{
    public class SessionGraceStorageProvider : ISessionGraceStorageProvider
    {
        private readonly IStorageApplication<Model.SessionGrace> _sessionGraceStorageApplication;

        public SessionGraceStorageProvider(IStorageApplication<Model.SessionGrace> sessionGraceStorageApplication)
        {
            _sessionGraceStorageApplication = sessionGraceStorageApplication;
        }

        public IQueryable<Model.SessionGrace> GetAll()
        {
            return _sessionGraceStorageApplication.GetAll();
        }

        public Model.SessionGrace GetById(string id)
        {
            return _sessionGraceStorageApplication.GetById(id);
        }

        public Model.SessionGrace Add(Model.SessionGrace entity)
        {
            return _sessionGraceStorageApplication.Add(entity);
        }

        public void Update(Model.SessionGrace entity)
        {
            _sessionGraceStorageApplication.Update(entity);
        }

        public void Delete(Model.SessionGrace entity)
        {
            _sessionGraceStorageApplication.Delete(entity);
        }

        public void Delete(string id)
        {
            _sessionGraceStorageApplication.Delete(id);
        }
    }
}