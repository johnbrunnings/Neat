using System;
using MongoRepository;

namespace Neat.Infrastructure.Security.Model
{
    public class UserRole : IEntity<string>
    {
        public UserRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string UserId { get; set; }
        public string RoleName { get; set; }
        public string ObjectType { get; set; }
        public string ObjectId { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, UserId: {1}, RoleName: {2}, ObjectType: {3}, ObjectId: {4}", Id, UserId, RoleName, ObjectType, ObjectId);
        }
    }
}