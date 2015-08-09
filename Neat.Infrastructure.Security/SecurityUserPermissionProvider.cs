using System;
using System.Linq;
using Neat.Infrastructure.Security.Model;
using Neat.Infrastructure.Security.Storage;

namespace Neat.Infrastructure.Security
{
    public class SecurityUserPermissionProvider : ISecurityUserPermissionProvider
    {
        private readonly ISecurityUserProvider _securityUserProvider;
        private readonly IUserPermissionSecurityStorageProvider _userPermissionSecurityStorageProvider;

        public SecurityUserPermissionProvider(ISecurityUserProvider securityUserProvider, IUserPermissionSecurityStorageProvider userPermissionSecurityStorageProvider)
        {
            _securityUserProvider = securityUserProvider;
            _userPermissionSecurityStorageProvider = userPermissionSecurityStorageProvider;
        }

        public void CreateUserPermission(string role, string action, string propertyName)
        {
            var user = _securityUserProvider.GetCurrentUser();
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }
            var userPermission = new UserPermission()
            {
                UserId = user.Id,
                Role = role,
                Action = action,
                PropertyName = propertyName
            };

            _userPermissionSecurityStorageProvider.Add(userPermission);
        }

        public void RemoveUserPermission(string role, string action, string propertyName)
        {
            var user = _securityUserProvider.GetCurrentUser();
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var userPermission = _userPermissionSecurityStorageProvider.GetAll().FirstOrDefault(x => x.UserId == user.Id && x.Role == role && x.Action == action && x.PropertyName == propertyName);
            if (userPermission != null)
            {
                _userPermissionSecurityStorageProvider.Delete(userPermission);
            }
        }
    }
}