using System.Linq;
using Neat.Infrastructure.Security.Model;

namespace Neat.Infrastructure.Security.Storage
{
    public interface IRoleActionSecurityStorageProvider
    {
        IQueryable<RoleAction> GetAll();
        RoleAction GetById(string id);
        RoleAction Add(RoleAction entity);
        void Update(RoleAction entity);
        void Delete(RoleAction entity);
        void Delete(string id);
    }
}