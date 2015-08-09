using System.Linq;
using Neat.Infrastructure.Security.Model;

namespace Neat.Infrastructure.Security.Storage
{
    public class RoleActionSecurityStorageProvider : IRoleActionSecurityStorageProvider
    {
        private readonly IStorageApplication<RoleAction> _roleActionStorageApplication;

        public RoleActionSecurityStorageProvider(IStorageApplication<RoleAction> roleActionStorageApplication)
        {
            _roleActionStorageApplication = roleActionStorageApplication;
        }

        public IQueryable<RoleAction> GetAll()
        {
            return _roleActionStorageApplication.GetAll();
        }

        public RoleAction GetById(string id)
        {
            return _roleActionStorageApplication.GetById(id);
        }

        public RoleAction Add(RoleAction entity)
        {
            return _roleActionStorageApplication.Add(entity);
        }

        public void Update(RoleAction entity)
        {
            _roleActionStorageApplication.Update(entity);
        }

        public void Delete(RoleAction entity)
        {
            _roleActionStorageApplication.Delete(entity);
        }

        public void Delete(string id)
        {
            _roleActionStorageApplication.Delete(id);
        }
    }
}