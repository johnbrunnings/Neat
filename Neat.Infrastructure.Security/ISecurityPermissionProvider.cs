using System.Collections.Generic;

namespace Neat.Infrastructure.Security
{
    public interface ISecurityPermissionProvider
    {
        IEnumerable<string> GetActionsForRole(string role);
        bool CanWriteToProperty(string role, string propertyName);
        bool CanReadFromProperty(string role, string propertyName);
    }
}