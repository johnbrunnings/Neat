using System.Linq;
using Neat.Infrastructure.Security.Model;
using Neat.Infrastructure.Security.Storage;

namespace Neat.Infrastructure.Security
{
    public class SecurityRoleActionProvider : ISecurityRoleActionProvider
    {
        private readonly IRoleActionSecurityStorageProvider _roleActionSecurityStorageProvider;

        public SecurityRoleActionProvider(IRoleActionSecurityStorageProvider roleActionSecurityStorageProvider)
        {
            _roleActionSecurityStorageProvider = roleActionSecurityStorageProvider;
        }

        public void CreateRoleAction(string role, string action)
        {
            var roleAction = new RoleAction()
            {
                Role = role,
                Action = action
            };

            _roleActionSecurityStorageProvider.Add(roleAction);
        }

        public void RemoveRoleAction(string role, string action)
        {
            var roleAction = _roleActionSecurityStorageProvider.GetAll().FirstOrDefault(x => x.Role == role && x.Action == action);
            if (roleAction != null)
            {
                _roleActionSecurityStorageProvider.Delete(roleAction);
            }
        }
    }
}