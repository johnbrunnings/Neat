using System.Collections.Generic;

namespace Neat.Infrastructure.Security
{
    public interface ISecurityPermissionProvider
    {
        IEnumerable<string> GetActionsForRole(string role);
        bool CanPerformActionOnProperty(string role, string action, string propertyName);
    }
}