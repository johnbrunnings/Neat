using Neat.Infrastructure.Security.Model;

namespace Neat.Infrastructure.Security.Storage
{
    public class SecurityStorageProvider : ISecurityStorageProvider
    {
        private readonly IStorageApplication<User> _userStorageApplication;
        private readonly IStorageApplication<Session> _sessionStorageApplication;

        public SecurityStorageProvider(IStorageApplication<User> userStorageApplication, IStorageApplication<Session> sessionStorageApplication)
        {
            _userStorageApplication = userStorageApplication;
            _sessionStorageApplication = sessionStorageApplication;
        }

        public void CreateUser(User user)
        {
            _userStorageApplication.Add(user);
        }

        public User GetUserById(string id)
        {
            return _userStorageApplication.GetById(id);
        }

        public void UpdateUser(User user)
        {
            _userStorageApplication.Update(user);
        }

        public void CreateSession(Session session)
        {
            _sessionStorageApplication.Add(session);
        }

        public Session GetById(string id)
        {
            return _sessionStorageApplication.GetById(id);
        }

        public void UpdateSession(Session session)
        {
            _sessionStorageApplication.Update(session);
        }
    }
}