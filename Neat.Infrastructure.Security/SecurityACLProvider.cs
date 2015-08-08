namespace Neat.Infrastructure.Security
{
    public class SecurityACLProvider : ISecurityACLProvider
    {
        private readonly ISecurityUserProvider _securityUserProvider;

        public SecurityACLProvider(ISecurityUserProvider securityUserProvider)
        {
            _securityUserProvider = securityUserProvider;
        }

        public string GetCurrentUserRoleForObject(object securedObject)
        {
            var user = _securityUserProvider.GetCurrentUser();

            if (user.Username.ToLower() == "admin")
            {
                return "Admin";
            }

            return "None";
        }

        public string GetCurrentUserRole()
        {
            var user = _securityUserProvider.GetCurrentUser();

            if (user.Username.ToLower() == "admin")
            {
                return "Admin";
            }

            return "None";
        }
    }
}