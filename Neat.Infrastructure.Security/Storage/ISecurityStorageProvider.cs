using Neat.Infrastructure.Security.Model;

namespace Neat.Infrastructure.Security.Storage
{
    public interface ISecurityStorageProvider
    {
        void CreateUser(User user);
        User GetUserById(string id);
        void UpdateUser(User user);
        void CreateSession(Session session);
        Session GetById(string id);
        void UpdateSession(Session session);
    }
}