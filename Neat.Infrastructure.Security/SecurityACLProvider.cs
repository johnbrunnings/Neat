namespace Neat.Infrastructure.Security
{
    public class SecurityACLProvider : ISecurityACLProvider
    {
        public string GetCurrentUserRoleForObject(object securedObject)
        {
            return "Admin";
        }

        public string GetCurrentUserRole()
        {
            return "Admin";
        }
    }
}