using System.Linq;

namespace Neat.Infrastructure.Session.Storage
{
    public class SessionStorageProvider : ISessionStorageProvider
    {
        private readonly IStorageApplication<Model.Session> _sessionStorageApplication;

        public SessionStorageProvider(IStorageApplication<Model.Session> sessionStorageApplication)
        {
            _sessionStorageApplication = sessionStorageApplication;
        }

        public IQueryable<Model.Session> GetAll()
        {
            return _sessionStorageApplication.GetAll();
        }

        public Model.Session GetById(string id)
        {
            return _sessionStorageApplication.GetById(id);
        }

        public Model.Session Add(Model.Session entity)
        {
            return _sessionStorageApplication.Add(entity);
        }

        public void Update(Model.Session entity)
        {
            _sessionStorageApplication.Update(entity);
        }

        public void Delete(Model.Session entity)
        {
            _sessionStorageApplication.Delete(entity);
        }

        public void Delete(string id)
        {
            _sessionStorageApplication.Delete(id);
        }
    }
}