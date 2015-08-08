namespace Neat.Infrastructure.Security
{
    public interface ISecurityUserRoleProvider
    {
        void CreateUserRole(string role);
        void CreateUserRoleForObject(string role, string objectType, string objectId);
        void DeleteUserRole();
        void DeleteUserRoleForObject(string objectType, string objectId);
    }
}