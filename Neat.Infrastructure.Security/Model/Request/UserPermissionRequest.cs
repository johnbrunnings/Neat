namespace Neat.Infrastructure.Security.Model.Request
{
    public class UserPermissionRequest
    {
        public string Role { get; set; }
        public string Action { get; set; }
        public string PropertyName { get; set; }

        public override string ToString()
        {
            return string.Format("Role: {0}, Action: {1}, PropertyName: {2}", Role, Action, PropertyName);
        }
    }
}