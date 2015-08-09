namespace Neat.Infrastructure.Security
{
    public interface ISecurityRoleActionProvider
    {
        void CreateRoleAction(string role, string action);
        void RemoveRoleAction(string role, string action);
    }
}