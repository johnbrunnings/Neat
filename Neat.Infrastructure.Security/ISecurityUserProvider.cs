using Neat.Infrastructure.Security.Model;

namespace Neat.Infrastructure.Security
{
    public interface ISecurityUserProvider
    {
        string CreateUser(User user);
        void UpdateUser(User user);
        User GetUser(string userId);
    }
}