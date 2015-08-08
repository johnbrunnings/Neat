namespace Neat.Infrastructure.Security.Model.Request
{
    public class UserRoleRequest
    {
        public string Role { get; set; }

        public override string ToString()
        {
            return string.Format("Role: {0}", Role);
        }
    }
}