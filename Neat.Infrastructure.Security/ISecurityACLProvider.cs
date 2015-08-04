namespace Neat.Infrastructure.Security
{
    public interface ISecurityACLProvider
    {
        string GetRoleForObject(object securedObject);
        string GetCurrentUserRole();
    }
}