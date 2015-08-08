using System.Linq;
using Neat.Infrastructure.Security.Model;

namespace Neat.Infrastructure.Security.Storage
{
    public class UserRoleSecurityStorageProvider : IUserRoleSecurityStorageProvider
    {
        private readonly IStorageApplication<UserRole> _userRoleStorageApplication;

        public UserRoleSecurityStorageProvider(IStorageApplication<UserRole> userRoleStorageApplication)
        {
            _userRoleStorageApplication = userRoleStorageApplication;
        }

        public IQueryable<UserRole> GetAll()
        {
            return _userRoleStorageApplication.GetAll();
        }

        public UserRole GetById(string id)
        {
            return _userRoleStorageApplication.GetById(id);
        }

        public UserRole Add(UserRole entity)
        {
            return _userRoleStorageApplication.Add(entity);
        }

        public void Update(UserRole entity)
        {
            _userRoleStorageApplication.Update(entity);
        }

        public void Delete(UserRole entity)
        {
            _userRoleStorageApplication.Delete(entity);
        }

        public void Delete(string id)
        {
            _userRoleStorageApplication.Delete(id);
        }
    }
}