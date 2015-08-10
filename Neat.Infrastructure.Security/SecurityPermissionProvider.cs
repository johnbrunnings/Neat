using System.Collections.Generic;
using System.Linq;
using Neat.Infrastructure.Security.Storage;

namespace Neat.Infrastructure.Security
{
    public class SecurityPermissionProvider : ISecurityPermissionProvider
    {
        private readonly ISecurityUserProvider _securityUserProvider;
        private readonly IRoleActionSecurityStorageProvider _roleActionSecurityStorageProvider;
        private readonly IUserPermissionSecurityStorageProvider _userPermissionSecurityStorageProvider;

        public SecurityPermissionProvider(ISecurityUserProvider securityUserProvider, IRoleActionSecurityStorageProvider roleActionSecurityStorageProvider, IUserPermissionSecurityStorageProvider userPermissionSecurityStorageProvider)
        {
            _securityUserProvider = securityUserProvider;
            _roleActionSecurityStorageProvider = roleActionSecurityStorageProvider;
            _userPermissionSecurityStorageProvider = userPermissionSecurityStorageProvider;
        }

        public IEnumerable<string> GetActionsForRole(string role)
        {
            var actions = new List<string>();

            var roleActions = _roleActionSecurityStorageProvider.GetAll().Where(x => x.Role == role);
            foreach (var roleAction in roleActions)
            {
                actions.Add(roleAction.Action);
            }

            return actions;
        }

        public bool CanPerformActionOnProperty(string role, string action, string propertyName)
        {
            var actions = new List<string>();
            var user = _securityUserProvider.GetCurrentUser();
            if (user != null)
            {
                var userPermissions = _userPermissionSecurityStorageProvider.GetAll().Where(x => x.UserId == user.Id && x.Role == role && x.PropertyName == propertyName);
                if (userPermissions.Count() > 0)
                {
                    foreach (var userPermission in userPermissions)
                    {
                        actions.Add(userPermission.Action);
                    }
                }
                else
                {
                    actions = GetActionsForRole(role).ToList();
                }
            }
            else
            {
                actions = GetActionsForRole(role).ToList();
            }

            return actions.Contains(action);
        }
    }
}