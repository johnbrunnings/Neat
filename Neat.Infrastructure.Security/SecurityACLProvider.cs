namespace Neat.Infrastructure.Security
{
    public class SecurityACLProvider : ISecurityACLProvider
    {
        public string GetRoleForObject(object securedObject)
        {
            return "Admin";
        }
    }
}