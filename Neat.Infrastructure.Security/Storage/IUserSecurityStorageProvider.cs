using System.Linq;
using Neat.Infrastructure.Security.Model;

namespace Neat.Infrastructure.Security.Storage
{
    public interface IUserSecurityStorageProvider
    {
        IQueryable<User> GetAll();
        User GetById(string id);
        User Add(User entity);
        void Update(User entity);
        void Delete(User entity);
        void Delete(string id);
    }
}