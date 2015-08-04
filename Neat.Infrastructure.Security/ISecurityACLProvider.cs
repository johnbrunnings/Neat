namespace Neat.Infrastructure.Security
{
    public interface ISecurityACLProvider
    {
        string GetCurrentUserRoleForObject(object securedObject);
        string GetCurrentUserRole();
    }
}