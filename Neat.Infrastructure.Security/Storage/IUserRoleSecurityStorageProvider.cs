using System.Linq;
using Neat.Infrastructure.Security.Model;

namespace Neat.Infrastructure.Security.Storage
{
    public interface IUserRoleSecurityStorageProvider
    {
        IQueryable<UserRole> GetAll();
        UserRole GetById(string id);
        UserRole Add(UserRole entity);
        void Update(UserRole entity);
        void Delete(UserRole entity);
        void Delete(string id);
    }
}