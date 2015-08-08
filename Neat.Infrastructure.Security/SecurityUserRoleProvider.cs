using System;
using System.Linq;
using Neat.Infrastructure.Security.Model;
using Neat.Infrastructure.Security.Storage;

namespace Neat.Infrastructure.Security
{
    public class SecurityUserRoleProvider : ISecurityUserRoleProvider
    {
        private readonly ISecurityUserProvider _securityUserProvider;
        private readonly IUserRoleSecurityStorageProvider _userRoleSecurityStorageProvider;


        public SecurityUserRoleProvider(ISecurityUserProvider securityUserProvider, IUserRoleSecurityStorageProvider userRoleSecurityStorageProvider)
        {
            _securityUserProvider = securityUserProvider;
            _userRoleSecurityStorageProvider = userRoleSecurityStorageProvider;
        }

        public void CreateUserRole(string role)
        {
            var user = _securityUserProvider.GetCurrentUser();
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var userRole = new UserRole()
            {
                UserId = user.Id,
                RoleName = role
            };

            _userRoleSecurityStorageProvider.Add(userRole);
        }

        public void CreateUserRoleForObject(string role, string objectType, string objectId)
        {
            var user = _securityUserProvider.GetCurrentUser();
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var userRole = new UserRole()
            {
                UserId = user.Id,
                RoleName = role,
                ObjectType = objectType,
                ObjectId = objectId
            };

            _userRoleSecurityStorageProvider.Add(userRole);
        }

        public void DeleteUserRole()
        {
            var user = _securityUserProvider.GetCurrentUser();
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var userRole =
                _userRoleSecurityStorageProvider.GetAll()
                    .FirstOrDefault(x => x.UserId == user.Id && x.ObjectType == null && x.ObjectId == null);
            if (userRole != null)
            {
                _userRoleSecurityStorageProvider.Delete(userRole);
            }
        }

        public void DeleteUserRoleForObject(string objectType, string objectId)
        {
            var user = _securityUserProvider.GetCurrentUser();
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var userRole = _userRoleSecurityStorageProvider.GetAll().FirstOrDefault(x => x.UserId == user.Id && x.ObjectType == objectType && x.ObjectId == objectId);
            if (userRole != null)
            {
                _userRoleSecurityStorageProvider.Delete(userRole);
            }
        }
    }
}