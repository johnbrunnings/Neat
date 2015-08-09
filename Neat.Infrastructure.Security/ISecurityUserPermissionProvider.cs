namespace Neat.Infrastructure.Security
{
    public interface ISecurityUserPermissionProvider
    {
        void CreateUserPermission(string role, string action, string propertyName);
        void RemoveUserPermission(string role, string action, string propertyName);
    }
}