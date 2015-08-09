namespace Neat.Infrastructure.Security.Model.Request
{
    public class RoleActionRequest
    {
        public string Role { get; set; }
        public string Action { get; set; }

        public override string ToString()
        {
            return string.Format("Role: {0}, Action: {1}", Role, Action);
        }
    }
}