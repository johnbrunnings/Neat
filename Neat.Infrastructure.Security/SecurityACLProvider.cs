using System.Linq;
using MongoRepository;
using Neat.Infrastructure.Security.Storage;

namespace Neat.Infrastructure.Security
{
    public class SecurityACLProvider : ISecurityACLProvider
    {
        private readonly ISecurityUserProvider _securityUserProvider;
        private readonly IUserRoleSecurityStorageProvider _userRoleSecurityStorageProvider;

        public SecurityACLProvider(ISecurityUserProvider securityUserProvider, IUserRoleSecurityStorageProvider userRoleSecurityStorageProvider)
        {
            _securityUserProvider = securityUserProvider;
            _userRoleSecurityStorageProvider = userRoleSecurityStorageProvider;
        }

        public string GetCurrentUserRoleForObject(object securedObject)
        {
            var securedEntity = securedObject as IEntity<string>;
            var objectType = securedObject.GetType().ToString();
            var objectId = objectType.ToString();
            if (securedEntity != null)
            {
                objectId = securedEntity.Id;
            }

            var user = _securityUserProvider.GetCurrentUser();

            if (user != null)
            {
                var userRole = _userRoleSecurityStorageProvider.GetAll().FirstOrDefault(x => x.UserId == user.Id && x.ObjectType == objectType && x.ObjectId == objectId);
                if (userRole != null)
                {
                    return userRole.RoleName;
                }
            }

            return GetCurrentUserRole();
        }

        public string GetCurrentUserRole()
        {
            var user = _securityUserProvider.GetCurrentUser();

            if (user != null)
            {
                var userRole = _userRoleSecurityStorageProvider.GetAll().FirstOrDefault(x => x.UserId == user.Id && x.ObjectType == null && x.ObjectId == null);
                if (userRole != null)
                {
                    return userRole.RoleName;
                }
            }

            return "None";
        }
    }
}