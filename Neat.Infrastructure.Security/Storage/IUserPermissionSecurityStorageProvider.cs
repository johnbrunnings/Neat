using System.Linq;
using Neat.Infrastructure.Security.Model;

namespace Neat.Infrastructure.Security.Storage
{
    public interface IUserPermissionSecurityStorageProvider
    {
        IQueryable<UserPermission> GetAll();
        UserPermission GetById(string id);
        UserPermission Add(UserPermission entity);
        void Update(UserPermission entity);
        void Delete(UserPermission entity);
        void Delete(string id);
    }
}