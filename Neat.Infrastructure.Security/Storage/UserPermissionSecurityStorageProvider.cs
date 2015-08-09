using System.Linq;
using Neat.Infrastructure.Security.Model;

namespace Neat.Infrastructure.Security.Storage
{
    public class UserPermissionSecurityStorageProvider : IUserPermissionSecurityStorageProvider
    {
        private readonly IStorageApplication<UserPermission> _userPermissionStorageApplication;

        public UserPermissionSecurityStorageProvider(IStorageApplication<UserPermission> userPermissionStorageApplication)
        {
            _userPermissionStorageApplication = userPermissionStorageApplication;
        }

        public IQueryable<UserPermission> GetAll()
        {
            return _userPermissionStorageApplication.GetAll();
        }

        public UserPermission GetById(string id)
        {
            return _userPermissionStorageApplication.GetById(id);
        }

        public UserPermission Add(UserPermission entity)
        {
            return _userPermissionStorageApplication.Add(entity);
        }

        public void Update(UserPermission entity)
        {
            _userPermissionStorageApplication.Update(entity);
        }

        public void Delete(UserPermission entity)
        {
            _userPermissionStorageApplication.Delete(entity);
        }

        public void Delete(string id)
        {
            _userPermissionStorageApplication.Delete(id);
        }
    }
}