using System.Collections.Generic;

namespace Neat.Infrastructure.Security
{
    public class SecurityPermissionProvider : ISecurityPermissionProvider
    {
        public IEnumerable<string> GetActionsForRole(string role)
        {
            var actions = new List<string>();
            actions.Add("Read");
            actions.Add("Create");
            actions.Add("Update");
            actions.Add("Delete");

            return actions;
        }

        public bool CanWriteToProperty(string role, string propertyName)
        {
            return true;
        }

        public bool CanReadFromProperty(string role, string propertyName)
        {
            return true;
        }
    }
}