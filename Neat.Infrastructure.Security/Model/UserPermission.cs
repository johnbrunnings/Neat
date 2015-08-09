using System;
using MongoRepository;

namespace Neat.Infrastructure.Security.Model
{
    public class UserPermission : IEntity<string>
    {
        public UserPermission()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
        public string Action { get; set; }
        public string PropertyName { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, UserId: {1}, Role: {2}, Action: {3}, PropertyName: {4}", Id, UserId, Role, Action, PropertyName);
        }
    }
}