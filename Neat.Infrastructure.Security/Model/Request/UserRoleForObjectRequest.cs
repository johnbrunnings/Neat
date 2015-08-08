namespace Neat.Infrastructure.Security.Model.Request
{
    public class UserRoleForObjectRequest
    {
        public string Role { get; set; }
        public string ObjectType { get; set; }
        public string ObjectId { get; set; }

        public override string ToString()
        {
            return string.Format("Role: {0}, ObjectType: {1}, ObjectId: {2}", Role, ObjectType, ObjectId);
        }
    }
}